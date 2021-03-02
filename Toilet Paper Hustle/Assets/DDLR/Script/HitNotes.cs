using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HitNotes : MonoBehaviour
{
    SpriteCycle spriteCycler;

    public bool failSafeMode = false;

    public AudioManager audioManager;

    [SerializeField]
    AudioSource backgroundBeat;

    [SerializeField]
    GameObject nonPaintedSprites;

    [SerializeField]
    GameObject tryAgain;

    [SerializeField]
    GameObject[] multiplierBars;

    [SerializeField]
    GameObject[] multiplierNumbers;

    [SerializeField]
    GameObject discoFever;

    [SerializeField]
    GameObject boo;

    [SerializeField]
    GameObject[] hearts;

    [SerializeField]
    GameObject[] lastNotes;

    [SerializeField]
    GameObject[] hitSprites;

    [SerializeField]
    GameObject[] missSprites;

    [SerializeField]
    GameObject[] hitSpaces;

    [SerializeField]
    int[] hitsPerMultiplier = new int[3];

    [SerializeField]
    DancingLimb[] dancingLimbs;

    [SerializeField]
     Text scoreText;

    public float tickTime;

    int score = 0;

    [SerializeField]
    int scorePerHit = 5;
    
    [SerializeField]
    int yellowIndex = 0;
    [SerializeField]
    int greenIndex = 1;
    [SerializeField]
    int redIndex = 2;
    [SerializeField]
    int blueIndex = 3;

    int consecutiveHits = 0;
    [SerializeField]
    int health = 3;

    int multiplier = 1;

    bool notAdded = true;

    bool playAgainSound = false;

    List<GameObject> notesToHit = new List<GameObject>();

    bool[] hitKeys = new bool[4];

    bool played1000DDLR = false;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < hitKeys.Length; i++)
        {
            hitKeys[i] = false;
        }
        spriteCycler = FindObjectOfType<SpriteCycle>();
        tickTime = spriteCycler.tickTime;
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = score.ToString();

        CheckNotesToHit();

        if (!hitKeys[yellowIndex])
        {
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.Q))
            {
                hitKeys[yellowIndex] = true;
                hitSpaces[yellowIndex].SetActive(false);
                dancingLimbs[yellowIndex].MakeManDance();
                if (lastNotes[yellowIndex].activeInHierarchy)
                {
                    //Do good hit stuff

                    //Set good hit sprites on for a little
                    //Add score
                    //Make guy dance
                    //Add to multiplier progress
                    audioManager.PlaySound("Note 2");
                    RemoveHitNote();
                    ActivateHitSprites(true, yellowIndex);
                    AddScore();
                    consecutiveHits++;
                }
                else
                {
                    //Do bad hit stuff

                    //Set bad hit sprites on for a little
                    //Reset multiplier

                    EnableBoo();

                    ActivateHitSprites(false, yellowIndex);
                    RemoveHealth();
                    consecutiveHits = 0;
                }
            }
        }

        if (!hitKeys[greenIndex])
        {
            if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.W))
            {
                hitKeys[greenIndex] = true;
                hitSpaces[greenIndex].SetActive(false);
                dancingLimbs[greenIndex].MakeManDance();
                if (lastNotes[greenIndex].activeInHierarchy)
                {
                    //Do good hit stuff

                    //Set good hit sprites on for a little
                    //Add score
                    //Make guy dance
                    //Add to multiplier progress
                    audioManager.PlaySound("Note 3");

                    RemoveHitNote();
                    ActivateHitSprites(true, greenIndex);
                    AddScore();
                    consecutiveHits++;
                }
                else
                {
                    //Do bad hit stuff

                    //Set bad hit sprites on for a little
                    //Reset multiplier

                    EnableBoo();

                    ActivateHitSprites(false, greenIndex);
                    RemoveHealth();
                    consecutiveHits = 0;
                }
            }
        }

        if (!hitKeys[redIndex])
        {
            if (Input.GetKeyDown(KeyCode.O))
            {
                hitKeys[redIndex] = true;
                hitSpaces[redIndex].SetActive(false);
                dancingLimbs[redIndex].MakeManDance();
                if (lastNotes[redIndex].activeInHierarchy)
                {
                    //Do good hit stuff

                    //Set good hit sprites on for a little
                    //Add score
                    //Make guy dance
                    //Add to multiplier progress
                    audioManager.PlaySound("Note 4");

                    RemoveHitNote();
                    ActivateHitSprites(true, redIndex);
                    AddScore();
                    consecutiveHits++;
                }
                else
                {
                    //Do bad hit stuff

                    //Set bad hit sprites on for a little
                    //Reset multiplier

                    EnableBoo();

                    ActivateHitSprites(false, redIndex);
                    RemoveHealth();
                    consecutiveHits = 0;
                }
            }
        }

        if (!hitKeys[blueIndex])
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                hitKeys[blueIndex] = true;
                hitSpaces[blueIndex].SetActive(false);
                dancingLimbs[blueIndex].MakeManDance();
                if (lastNotes[blueIndex].activeInHierarchy)
                {
                    //Do good hit stuff

                    //Set good hit sprites on for a little
                    //Add score
                    //Make guy dance
                    //Add to multiplier progress
                    audioManager.PlaySound("Note 1");

                    RemoveHitNote();
                    ActivateHitSprites(true, blueIndex);
                    AddScore();
                    consecutiveHits++;
                }
                else
                {
                    //Do bad hit stuff

                    //Set bad hit sprites on for a little
                    //Reset multiplier

                    EnableBoo();

                    ActivateHitSprites(false, blueIndex);
                    RemoveHealth();
                    consecutiveHits = 0;
                }
            }
        }


        UpdateMultiplier();
        ManageMultiplierSprites();

        if (health <= 0)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Time.timeScale = 1f;
                SceneManager.UnloadSceneAsync(1);
                SceneManager.LoadScene("Axel's Scene", LoadSceneMode.Additive);
            }
        }
    }


    void UpdateMultiplier()
    {
        if (consecutiveHits >= hitsPerMultiplier[2])
        {
            multiplier = 8;
        }
        else if (consecutiveHits >= hitsPerMultiplier[1])
        {
            multiplier = 4;
        }
        else if (consecutiveHits >= hitsPerMultiplier[0])
        {
            multiplier = 2;
        }
        else
        {
            multiplier = 1;
        }
    }

    void ManageMultiplierSprites()
    {
        if (consecutiveHits < hitsPerMultiplier[0] / 3)
        {
            for (int i = 0; i < multiplierBars.Length; i++)
            {
                multiplierBars[i].SetActive(false);
            }
            for (int i = 0; i < multiplierNumbers.Length; i++)
            {
                multiplierNumbers[i].SetActive(false);
            }
            discoFever.SetActive(false);
        }
        if (consecutiveHits >= hitsPerMultiplier[0] / 3)
        {
            multiplierBars[0].SetActive(true);
        }
        if (consecutiveHits >= hitsPerMultiplier[0] / 3 * 2)
        {
            multiplierBars[1].SetActive(true);
        }
        if (consecutiveHits >= hitsPerMultiplier[0])
        {
            multiplierBars[2].SetActive(true);
            multiplierNumbers[0].SetActive(true);
        }
        if (consecutiveHits >= ((hitsPerMultiplier[1] - hitsPerMultiplier[0])/ 3) + hitsPerMultiplier[0])
        {
            multiplierBars[3].SetActive(true);
        }
        if (consecutiveHits >= ((hitsPerMultiplier[1] - hitsPerMultiplier[0]) / 3 * 2) + hitsPerMultiplier[0])
        {
            multiplierBars[4].SetActive(true);
        }
        if (consecutiveHits >= (hitsPerMultiplier[1] - hitsPerMultiplier[0]) + hitsPerMultiplier[0])
        {
            multiplierBars[5].SetActive(true);
            multiplierNumbers[1].SetActive(true);
        }
        if (consecutiveHits >= ((hitsPerMultiplier[2] - hitsPerMultiplier[1])/ 3) + hitsPerMultiplier[1])
        {
            multiplierBars[6].SetActive(true);
        }
        if (consecutiveHits >= ((hitsPerMultiplier[2] - hitsPerMultiplier[1]) / 3 * 2) + hitsPerMultiplier[1])
        {
            multiplierBars[7].SetActive(true);
        }
        if (consecutiveHits >= (hitsPerMultiplier[2] - hitsPerMultiplier[1]) + hitsPerMultiplier[1])
        {
            multiplierBars[8].SetActive(true);
            multiplierNumbers[2].SetActive(true);
            discoFever.SetActive(true);
        }
    }

    void RemoveHealth()
    {
        if (!failSafeMode)
        {
            health--;
            audioManager.PlaySound("Mistake");
            //Set heart sprite inactive
            if (health >= 0)
            {
                hearts[health].SetActive(false);
            }
            if (health <= 0)
            {
                tryAgain.SetActive(true);
                nonPaintedSprites.SetActive(false);
                if (!playAgainSound)
                {
                    backgroundBeat.Stop();
                    audioManager.PlaySound("Try Again");
                    audioManager.PlaySound("DDLRLose");
                    playAgainSound = true;
                }
                //Time.timeScale = 0f;
            }
        }
    }

    void ActivateHitSprites(bool hit, int index)
    {
        if (hit)
        {
            hitSprites[index].SetActive(true);
            missSprites[index].SetActive(true);
        }
        else
        {
            missSprites[index].SetActive(true);
        }
    }

    public void ResetHitKeys()
    {
        for (int i = 0; i < hitKeys.Length; i++)
        {
            hitKeys[i] = false;
        }
    }

    public void ResetHitSpaces()
    {
        for (int i = 0; i < hitSpaces.Length; i++)
        {
            hitSpaces[i].SetActive(true);
        }
    }

    public void DisableThumperEffectSprites()
    {
        for (int i = 0; i < hitSprites.Length; i++)
        {
            hitSprites[i].SetActive(false);
            missSprites[i].SetActive(false);
        }
    }

    void EnableBoo()
    {
        boo.SetActive(true);
        Invoke("DisableBoo", tickTime);
    }

    public void DisableBoo()
    {
        boo.SetActive(false);
    }

    void AddScore()
    {
        score += scorePerHit * multiplier;
        if (score >= 1000 && !played1000DDLR)
        {
            audioManager.PlaySound("DDLR1000");
            played1000DDLR = true;
        }
    }

    void CheckNotesToHit()
    {
        if (notAdded)
        {
            for (int i = 0; i < lastNotes.Length; i++)
            {
                if (lastNotes[i].activeInHierarchy)
                {
                    notesToHit.Add(lastNotes[i]);
                }
            }
            notAdded = false;
        }
    }

    public void CheckIfAllNotesHit()
    {
        if (notesToHit.Count > 0)
        {
            EnableBoo();
            for (int i = 0; i < notesToHit.Count; i++)
            {
                RemoveHealth();
            }
            notesToHit.Clear();
        }
        notAdded = true;
    }

    void RemoveHitNote()
    {
        if (notesToHit.Count > 0)
        {
            notesToHit.RemoveAt(0);
        }
    }
}
