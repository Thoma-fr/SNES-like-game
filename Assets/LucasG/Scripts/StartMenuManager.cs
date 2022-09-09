using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;

public class StartMenuManager : MonoBehaviour
{
    public GameObject objectsToAppear;
    public GameObject pressStart;
    public string sceneToLoad;
    public GameObject pepite;

    private bool shouldKeepBliking = true;
    private TextMeshProUGUI text;
    private SpriteRenderer spriteRenderer;

    public GameObject globalVolume;
    public GameObject scanLine;
    private Volume volume;
    private ChromaticAberration aberration;
    private LensDistortion lensDistortion;
    private FilmGrain filmGrain;

    private bool FXSwitch = true;

    private void Start()
    {
        volume = globalVolume.GetComponent<Volume>();
        spriteRenderer = pepite.GetComponent<SpriteRenderer>();
        text = pressStart.GetComponent<TextMeshProUGUI>();
        objectsToAppear.SetActive(false);
        StartCoroutine("MakeTextAppear");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            nextScene();
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

    public void ToggleFX()
    {
        if (FXSwitch)
        {
            if (volume.profile.TryGet<ChromaticAberration>(out aberration))
            {
                aberration.active = false;
            }

            if (volume.profile.TryGet<LensDistortion>(out lensDistortion))
            {
                lensDistortion.active = false;
            }

            if (volume.profile.TryGet<FilmGrain>(out filmGrain))
            {
                filmGrain.active = false;
            }

            scanLine.SetActive(false);

            FXSwitch = !FXSwitch;
        }
        else
        {
            if (volume.profile.TryGet<ChromaticAberration>(out aberration))
            {
                aberration.active = true;
            }

            if (volume.profile.TryGet<LensDistortion>(out lensDistortion))
            {
                lensDistortion.active = true;
            }

            if (volume.profile.TryGet<FilmGrain>(out filmGrain))
            {
                filmGrain.active = true;
            }

            scanLine.SetActive(true);

            FXSwitch = !FXSwitch;
        }
    }
    public void nextScene()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
