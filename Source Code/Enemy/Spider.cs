using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : Enemy, IDamagable
{
    public GameObject _acidEffectPrefab;

    public int Health { get ; set ; }

    public override void init()
    {
        base.init();

        Health = base.health;
    }

    public void Damage()
    {
        if (isDead == true)
        {
            return;
        }
        Health--;
        if (Health < 1)
        {
            //Destroy(this.gameObject);
            isDead = true;
            anim.SetTrigger("death");
            GameObject diamonds = (GameObject)Instantiate(diamondPrefab, transform.position, Quaternion.identity);
            diamonds.GetComponent<Diamond>().gems = base.gems;
        }

    //  throw new System.NotImplementedException();
    }
    
    public override void Movement()
    {
        // Stay Still
    }
    
    public void Attack()
    {
        Instantiate(_acidEffectPrefab, transform.position, Quaternion.identity);
    }

}
