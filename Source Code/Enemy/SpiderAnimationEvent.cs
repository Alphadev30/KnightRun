using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderAnimationEvent : MonoBehaviour
{

    // handle to the spider 
    private Spider _spider;

    public void Start()
    {
        _spider = transform.parent.GetComponent<Spider>();
    }

    public void Fire()
    {
        // Tell Spider to Fire 

       // Debug.Log("Spider Should Fire ");

        // use handle to call attack method on Spider 
        _spider.Attack();
    }
}
