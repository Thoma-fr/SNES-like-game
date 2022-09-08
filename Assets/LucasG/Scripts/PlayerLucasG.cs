using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerLucasG : MonoBehaviour
{
    public static PlayerLucasG Instance { get; private set; }

    public int hp = 4;
    public bool canDestroy = false;
    public GameObject collisionWarning;

    private float coin;
    private float nbOfHealPresses;
    private bool canHeal = false;
    private bool isDamaged = false;
    [SerializeField]
    private GameObject coinText;
    private TextMeshProUGUI TMPtext;
   
    private GameObject objectToDestroy;
    private Animator animator;
    

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        TMPtext = coinText.GetComponent<TextMeshProUGUI>();
        animator = GetComponent<Animator>();
        collisionWarning.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (canDestroy)
            {
                if (objectToDestroy.CompareTag("Coin"))
                {
                    AddCoin();
                }
                Destroy(objectToDestroy);
            }
        }

        switch (hp)
        {
            case > 2:
                isDamaged = false;
                canHeal = false;
                break;
            case <=2:
                canHeal = true;
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

    public void AddCoin()
    {
        coin++;
        TMPtext.text = coin.ToString();
    }

    public void TakeDamage()
    {
        hp--;
        Debug.Log(hp);
    }

    public void Heal()
    {
        if (canHeal)
        {
            nbOfHealPresses++;
            if (nbOfHealPresses == 7)
            {
                hp++;
                nbOfHealPresses = 0;
            }
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
