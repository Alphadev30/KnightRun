using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinorNot : MonoBehaviour
{
    public GameObject doNotHaveKey;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (GameManager.Instance.HasKeyToTheCastle == true)
            {
                SceneManager.LoadScene("WinScreen");
                Debug.Log("You have Won The Game................. Congratulations");
            }
            else if (GameManager.Instance.HasKeyToTheCastle == false)
            {
                doNotHaveKey.SetActive(true);
                Debug.Log("................. Sorry");
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (GameManager.Instance.HasKeyToTheCastle == false)
            {
                doNotHaveKey.SetActive(false);
            }
        }
    }
}
