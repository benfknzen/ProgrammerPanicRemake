using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySelfAfterSeconds : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Delay(2));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator Delay(int seconds)
    {
        yield return new WaitForSecondsRealtime(seconds);
        Destroy(gameObject);
    }
}
