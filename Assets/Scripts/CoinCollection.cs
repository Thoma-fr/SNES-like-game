using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class CoinCollection : MonoBehaviour
{
    private float coin;
    [SerializeField]
    private GameObject coinText;

    private TextMeshProUGUI TMPtext;
    private void Start()
    {
        TMPtext=coinText.GetComponent<TextMeshProUGUI>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Coin"))
        {
            coin++;
            TMPtext.text = coin.ToString();
            Destroy(collision.gameObject);
        }
    }
}
