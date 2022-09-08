using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerLucasG : MonoBehaviour
{
    public static PlayerLucasG Instance { get; private set; }

    public int hp = 4;

    private float coin;
    [SerializeField]
    private GameObject coinText;
    private TextMeshProUGUI TMPtext;
    public bool canDestroy = false;
    private GameObject objectToDestroy;
    private bool isDamaged = false;
    private Animator animator;

    private float nbOfHealPresses;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        TMPtext = coinText.GetComponent<TextMeshProUGUI>();
        animator = GetComponent<Animator>();
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

        switch (hp)
        {
            case > 2:
                isDamaged = false;
                break;
            case <=2:
                isDamaged = true;
                break;
        }

        if (hp == 0)
        {
            SceneManager.LoadScene("LucasScene");
        }

        if (isDamaged)
        {
            animator.SetBool("isDamaged", true);
        }
        else
        {
            animator.SetBool("isDamaged", false);
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

    public void Heal()
    {
        nbOfHealPresses++;
        if (nbOfHealPresses == 5)
        {
            hp++;
            nbOfHealPresses = 0;
        }
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
