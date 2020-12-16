using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidEffect : MonoBehaviour
{
    
    // Destroy this effect after 5 seconds 
    private void Start()
    {
        Destroy(this.gameObject, 4.0f);
    }


    // Move Roght at 4 ms Per second 
    private void Update()
    {
        transform.Translate(Vector3.right * 4 * Time.deltaTime);
    }


    // Detect Player And Deal Damage 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            IDamagable Hit = collision.GetComponent<IDamagable>();

            if (Hit != null)
            {
                Hit.Damage();
                Destroy(this.gameObject);
            }
        }
    }    
}
