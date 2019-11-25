using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Camera MainCamera;
    public List<int> SequenceArray;
    public GameObject[] Spawner;
    public GameObject[] ObjectsToSpawn;
    private int iMax_Sequence = 3;
    private int iCycle_Through_Sequence = 0;
    private static float fDisplayDelay = 2.5f;
    private bool bTestSequenceTime;

    private readonly KeyCode[] kcKeys = new KeyCode[] { KeyCode.A, KeyCode.S, KeyCode.D };

    private bool[] bOption = { false, false, false };
    private int iPressTimes = 0;
    private int iNumberCorrect = 0;
    private int iRoundNumber = 0;

    // Start is called before the first frame update
    void Start()
    {
        Restart();
        RandomizeSequenceArray();
    }

    private void Restart()
    {
        bTestSequenceTime = false;
        iCycle_Through_Sequence = 0;
        bOption = new bool[] { false, false, false };
        iPressTimes = 0;
        iNumberCorrect = 0;
    }
    private void RandomizeSequenceArray()
    {
        SequenceArray = new List<int>();
        //SequenceArray.Clear();
        for (int i = 0; i < iMax_Sequence; i++)
        {
            SequenceArray.Add(UnityEngine.Random.Range(0, 3));
            Debug.Log(SequenceArray[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (iRoundNumber == 0)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StartCoroutine(coCycleThroughDisplaySequence());
            }
            if (bTestSequenceTime)
            {
                UserTestSequence();
            }
        }
        else if (iRoundNumber == 1)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StartCoroutine(coCycleThroughDisplaySequence());
            }
            if (bTestSequenceTime)
            {
                UserTestSequence();
            }
        }
        else if (iRoundNumber == 2)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StartCoroutine(coCycleThroughDisplaySequence());
            }
            if (bTestSequenceTime)
            {
                UserTestSequence();
            }
        }
        else if (iRoundNumber == 3)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StartCoroutine(coCycleThroughDisplaySequence());
            }
            if (bTestSequenceTime)
            {
                UserTestSequence();
            }
        }
        else
            Debug.Log("You WIN!");

    }
    private void FixedUpdate()
    {
        
    }

    IEnumerator RotateCamera()
    {
        yield return MainCamera.transform.eulerAngles += new Vector3(0, 90, 0);
    }

    private void LevelComplete()
    {
        StartCoroutine(RotateCamera());
    }

    IEnumerator coCycleThroughDisplaySequence()
    {
        bTestSequenceTime = false;
        for (int i = 0; i < iMax_Sequence; i++)
        {
            Instantiate(ObjectsToSpawn[SequenceArray[iCycle_Through_Sequence]], Spawner[iRoundNumber].transform);
            Debug.Log(SequenceArray[iCycle_Through_Sequence]);
            //StartCoroutine(Delay(3));
            if (iCycle_Through_Sequence < iMax_Sequence - 1)
                iCycle_Through_Sequence++;
            else
                iCycle_Through_Sequence = 0;
            yield return new WaitForSecondsRealtime(fDisplayDelay);
        }
        bTestSequenceTime = true;
    }

    private void UserTestSequence()
    {
        for (int i = 0; i < kcKeys.Length; i++)
        {
            if (Input.GetKeyDown(kcKeys[i]))
            {
                bOption[i] = true;
            }
            if (bOption[i] == true)
            {
                if (SequenceArray[iPressTimes] == i)
                {
                    iNumberCorrect++;
                    Debug.Log("RIGHT");
                }
                else
                    Debug.Log("WRONG");

                if (iPressTimes < iMax_Sequence - 1)
                {
                    iPressTimes++;
                }
                else
                {
                    if(iNumberCorrect == iMax_Sequence)
                    {
                        MoveToNextRound();
                    }
                    else
                    {
                        iPressTimes = 0;
                        MoveToRepeatRound();
                    }
                }
                    
            }
            bOption[i] = false;
        }
    }

    private void MoveToRepeatRound()
    {
        Restart();
        Debug.Log("REPEAT THE ROUND");
    }

    private void MoveToNextRound()
    {
        LevelComplete();
        RandomizeSequenceArray();
        iRoundNumber++;
        Restart();
        Debug.Log("ALL CORRECT NEXT ROUND! Round: " + iRoundNumber);
    }
}
