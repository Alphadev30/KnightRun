using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamond : MonoBehaviour
{
    public int gems = 1;

    // onTrigeer to collect 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check for the Player
        if (collision.tag == "Player")
        {
            Player player = collision.GetComponent<Player>();
            if (player != null)
            {
                // Add the Value of Gems To the Player 
                player.AddGems(gems);
                Destroy(this.gameObject); 
            }
        }

    }
    
    
    


}
