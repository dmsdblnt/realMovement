using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinPickUp : MonoBehaviour
{
    #region Show in editor

    [SerializeField] private string coinTag;
    [SerializeField] private TextMeshProUGUI text;

    #endregion

    private int counter;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == coinTag)
        {
            counter++;
            text.text = "Coins: " + counter.ToString();
            Destroy(collider.gameObject);
        }
    } 
    
}
