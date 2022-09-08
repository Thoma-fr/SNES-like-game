using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class StartMenuManager : MonoBehaviour
{
    public GameObject objectsToAppear;
    public GameObject pressStart;
    public string sceneToLoad;
    public GameObject pepite;

    private bool shouldKeepBliking = true;
    private TextMeshProUGUI text;
    private SpriteRenderer spriteRenderer;
    

    private void Start()
    {
        spriteRenderer = pepite.GetComponent<SpriteRenderer>();
        text = pressStart.GetComponent<TextMeshProUGUI>();
        objectsToAppear.SetActive(false);
        StartCoroutine("MakeTextAppear");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }

    private IEnumerator MakeTextAppear()
    {
        yield return new WaitForSeconds(3.6f);
        objectsToAppear.SetActive(true);
        StartCoroutine("MakeTextBlink");
    }

    private IEnumerator MakeTextBlink()
    {
        yield return new WaitForSeconds(0.7f);
        text.color = new Vector4(255,255,255,0);
        spriteRenderer.flipX = true;
        yield return new WaitForSeconds(0.7f);
        text.color = new Vector4(255, 255, 255, 255);
        spriteRenderer.flipX = false;

        if (shouldKeepBliking)
        {
            StartCoroutine("MakeTextBlink");
        }
    }
}
