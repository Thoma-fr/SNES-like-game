using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerLucasG : MonoBehaviour
{
    public static PlayerLucasG Instance { get; private set; }

    public bool canDestroy = false;
    private GameObject objectToDestroy;

    public int hp = 4;

    private float coin;
    [SerializeField]
    private GameObject coinText;
    private TextMeshProUGUI TMPtext;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        TMPtext = coinText.GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (canDestroy)
            {
                if (objectToDestroy.CompareTag("Coin"))
                {
                    coin++;
                    TMPtext.text = coin.ToString();
                }
                Destroy(objectToDestroy);
            }
        }

        if (hp == 0)
        {
            SceneManager.LoadScene("LucasGScene");
        }
    }

    public void AssignObstacleToDestroy(GameObject gameObj)
    {
        Debug.Log("assigned object to destroy");
        objectToDestroy = gameObj;

    }

    public void TakeDamage()
    {
        hp--;
        Debug.Log(hp);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle") && collision.gameObject.CompareTag("Coin"))
        {
            TakeDamage();
            Destroy(collision.gameObject);
        }
    }
}
