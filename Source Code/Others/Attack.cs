using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{

    private bool _canDamage = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
       // Debug.Log("Hit : " + collision.name);

        IDamagable Hit = collision.GetComponent<IDamagable>();

        if (Hit != null)
        {
            if (_canDamage)
            {
                Hit.Damage();
                _canDamage = false;
                StartCoroutine(ResetDamage());
            }
            
        }

    }

    IEnumerator ResetDamage()
    {
        yield return new WaitForSeconds(0.6f);
        _canDamage = true;
    }
    
}
