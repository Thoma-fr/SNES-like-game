using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerLucasG : MonoBehaviour
{
    public static PlayerLucasG Instance { get; private set; }

    public int hp = 4;
    [Range(0.0f, 0.4f)]
    public float coroutineInterval;

    public bool canDestroy = false;
    public GameObject collisionWarning;

    private float coin;
    private float nbOfHealPresses;
    private bool canHeal = false;
    private bool canTakeDamage = true;
    private bool isDamaged = false;
    [SerializeField]
    private GameObject coinText;
    private TextMeshProUGUI TMPtext;
    private bool timeStopped;
    public GameObject pauseCanvas;

    public Sprite rock;
    public Sprite goldenRock;

    private GameObject objectToDestroy;
   //public Animator animator;
    public SpriteRenderer spriteRenderer;

    public Sprite[] attackSprite;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        TMPtext = coinText.GetComponent<TextMeshProUGUI>();
        //animator = GetComponent<Animator>();
       // spriteRenderer = GetComponent<SpriteRenderer>();
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
                    objectToDestroy.GetComponent<SpriteRenderer>().sprite = goldenRock;
                    objectToDestroy.GetComponent<Collider2D>().enabled = false;
                }
                else
                {
                    objectToDestroy.GetComponent<SpriteRenderer>().sprite = rock;
                    objectToDestroy.GetComponent<Collider2D>().enabled = false;
                }
                StartCoroutine(attack());
            }
        }
        else if (Input.GetKeyDown(KeyCode.H))
        {
            TakeDamage();
        }

        switch (hp)
        {
            case > 2:
                isDamaged = false;
                break;
            case <=2:
                canHeal = true;
                isDamaged = true;
                break;
        }
        if (hp==4)
            canHeal=false;

        if (hp == 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (isDamaged)
        {
            //animator.SetBool("isDamaged", true);
        }
        else
        {
            //animator.SetBool("isDamaged", false);
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
        if (canTakeDamage)
        {
            hp--;
            Debug.Log(hp);
            StartCoroutine("DamageCoroutine");
            canTakeDamage = false;
        }
    }

    public void Heal()
    {
        if (canHeal)
        {
            nbOfHealPresses++;
            if (nbOfHealPresses == 15)
            {
                hp++;
                nbOfHealPresses = 0;
            }
        }
        Debug.Log("heal");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            TakeDamage();
            collision.gameObject.GetComponent<SpriteRenderer>().sprite = rock;
            collision.gameObject.GetComponent<Collider2D>().enabled = false;
        }
        else if (collision.gameObject.CompareTag("Coin"))
        {
            TakeDamage();
            collision.gameObject.GetComponent<SpriteRenderer>().sprite = goldenRock;
            collision.gameObject.GetComponent<Collider2D>().enabled = false;
        }
    }

    private IEnumerator DamageCoroutine()
    {
        yield return new WaitForSeconds(coroutineInterval);
        spriteRenderer.color = new Vector4(255, 255, 255, 0);
        yield return new WaitForSeconds(coroutineInterval);
        spriteRenderer.color = new Vector4(255, 255, 255, 255);
        yield return new WaitForSeconds(coroutineInterval);
        spriteRenderer.color = new Vector4(255, 255, 255, 0);
        yield return new WaitForSeconds(coroutineInterval);
        spriteRenderer.color = new Vector4(255, 255, 255, 255);
        canTakeDamage = true;
    }
    public void DestroyObstacle()
    {
        if (canDestroy)
        {
            
            if (objectToDestroy.CompareTag("Coin"))
            {
                AddCoin();
                
            }
            Destroy(objectToDestroy);
            StartCoroutine(attack());
        }
        Debug.Log("destroy");
    }

    public void TriggerPause()
    {
        if (!timeStopped)
        {
            Time.timeScale = 0;
        }
        pauseCanvas.SetActive(!pauseCanvas.activeSelf);
        timeStopped = !timeStopped;
    }
    IEnumerator attack()
    {
        Debug.Log(attackSprite[0]);
        spriteRenderer.sprite = attackSprite[0];
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.sprite = attackSprite[1];
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.sprite = attackSprite[1];
    }
}
