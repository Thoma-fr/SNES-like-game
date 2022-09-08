using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class GameOverCanvas : MonoBehaviour
{
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
