using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Enemy, IDamagable
{
    public int Health { get; set; }


    public override void init()
    {
        base.init();
        Health = base.health;
    }

    public override void Movement()
    {
        base.Movement();

        float distance = Vector3.Distance(player.transform.localPosition, transform.localPosition);
        //Debug.Log("Distance from the players is : " + distance);

        Vector3 direction = player.transform.localPosition - transform.localPosition;
        //Debug.Log("X Cordinati is : " + direction.x);

        if (direction.x < 0 && anim.GetBool("InCombat") == true)
        {
            Sprite.flipX = true;
        }
        else if (direction.x > 0 && anim.GetBool("InCombat") == true)
        {
            Sprite.flipX = false;
        }


    }

    public void Damage()
    {
        //  Debug.Log("Damage()");
        if (isDead == true)
        {
            return;
        }
        Health--;
        anim.SetTrigger("hit");
        isHit = true;
        anim.SetBool("InCombat", true);
        onHitPlay.Play();

        if (Health < 1)
        {
            //Destroy(this.gameObject);
            isDead = true;
            anim.SetTrigger("death");
            GameObject diamonds = (GameObject)Instantiate(diamondPrefab, transform.position, Quaternion.identity);
            diamonds.GetComponent<Diamond>().gems = base.gems;
        }
    }
}
