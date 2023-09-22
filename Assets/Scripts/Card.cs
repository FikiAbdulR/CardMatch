using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public bool facedUp, locked;

    private Card firstInPair, secondInPair;
    private string firstInPairName, secondInPairName;

    public static Queue<Card> sequence;

    public static int pairsFound;

    public static GameObject winText;

    [SerializeField]
    private Sprite faceSprite, backSprite;
    public Button button;

    public static bool coroutineAllowed;

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        button.image.sprite = backSprite;
        coroutineAllowed = true;
        facedUp = false;
        locked = false;
        sequence = new Queue<Card>();
        pairsFound = 0;

        if (winText == null)
        {
            winText = GameObject.Find("Winning");
            winText.SetActive(false);
        }
    }

    private void Update()
    {
        if(Time.timeScale == 0)
        {
            locked = true;
        }
        else
        {
            locked = false;
        }
    }

    public void Rotate()
    {
        if (!locked && coroutineAllowed)
        {
            StartCoroutine(RotateCard());
        }
    }

    private IEnumerator RotateCard()
    {
        coroutineAllowed = false;

        if (!facedUp)
        {
            sequence.Enqueue(this);
            for (float i = 0f; i <= 180f; i += 10f)
            {
                transform.rotation = Quaternion.Euler(0f, i, 0f);
                if (i == 90f)
                {
                    button.image.sprite = faceSprite;
                }
                yield return new WaitForSeconds(0.01f);
            }
        }

        else if (facedUp)
        {
            for (float i = 180f; i >= 0f; i -= 10f)
            {
                transform.rotation = Quaternion.Euler(0f, i, 0f);
                if (i == 90f)
                {
                    button.image.sprite = backSprite;
                }
                yield return new WaitForSeconds(0.01f);
                sequence.Clear();
            }
        }

        coroutineAllowed = true;

        facedUp = !facedUp;

        if (sequence.Count == 2)
        {
            CheckResults();
        }
    }

    private void CheckResults()
    {
        firstInPair = sequence.Dequeue();
        secondInPair = sequence.Dequeue();

        firstInPairName = firstInPair.name.Substring(0, firstInPair.name.Length - 1);
        secondInPairName = secondInPair.name.Substring(0, secondInPair.name.Length - 1);

        if (firstInPairName == secondInPairName)
        {
            firstInPair.locked = true;
            secondInPair.locked = true;
            pairsFound += 1;
        }
        else
        {
            firstInPair.StartCoroutine("RotateBack");
            secondInPair.StartCoroutine("RotateBack");
        }

        if(pairsFound == 7)
        {
            winText.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public IEnumerator RotateBack()
    {
        coroutineAllowed = false;
        yield return new WaitForSeconds(0.2f);
        for (float i = 180f; i >= 0f; i -= 10f)
        {
            transform.rotation = Quaternion.Euler(0f, i, 0f);
            if (i == 90f)
            {
                button.image.sprite = backSprite;
            }
            yield return new WaitForSeconds(0.01f);
            sequence.Clear();
        }
        facedUp = false;
        coroutineAllowed = true;
    }
}
