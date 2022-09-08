using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class PauseCanvasManager : MonoBehaviour
{
    [SerializeField]
    private GameObject pauseCanvas;
    public string sceneToLoadOnRestart;
    public string sceneToLoadOnQuit;

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
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseCanvas.SetActive(!pauseCanvas.activeSelf);
        }
    }

    public void Resume()
    {
        pauseCanvas.SetActive(false);
    }

    public void Restart()
    {
        SceneManager.LoadScene(sceneToLoadOnRestart);
    }

    public void Quit()
    {
        SceneManager.LoadScene(sceneToLoadOnQuit);
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
}
