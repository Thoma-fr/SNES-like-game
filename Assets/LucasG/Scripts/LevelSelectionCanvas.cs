using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;

public class LevelSelectionCanvas : MonoBehaviour
{
    public GameObject globalVolume;
    public GameObject scanLine;
    private Volume volume;
    private ChromaticAberration aberration;
    private LensDistortion lensDistortion;
    private FilmGrain filmGrain;

    private bool FXSwitch = true;

    public string level1SceneName;
    public string level2SceneName;
    public string level3SceneName;

    private void Start()
    {
        volume = globalVolume.GetComponent<Volume>();
    }

    public void LoadLevel1()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(level1SceneName);
    }

    public void LoadLevel2()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(level2SceneName);
    }

    public void LoadLevel3()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(level3SceneName);
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
