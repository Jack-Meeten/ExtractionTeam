using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [Header("Prefab Setup")]
    [Header(" ")]
    [SerializeField] AudioSource OST_Source;
    [SerializeField] AudioSource UI_Source;
    [SerializeField] AudioSource[] AudioSources;
    [SerializeField] GameObject GameManager;
    [Header(" ")]


    [Header("Volume Parameters")]
    [Header(" ")]
    [SerializeField] Slider SFX_Slider;
    [SerializeField] Slider OST_Slider;
    [SerializeField] Slider UI_Slider;
    [Header(" ")]
    [SerializeField] float SFX_Volume;
    [SerializeField] float OST_Volume;
    [SerializeField] float UI_Volume;



    void Start()
    {       
        SFX_Volume = 0.75f;
        OST_Volume = 0.75f;
        UI_Volume = 0.75f;

        SFX_Slider.value = SFX_Volume;
        OST_Slider.value = OST_Volume;
        UI_Slider.value = UI_Volume;
    }

    private void Update()
    {
        FindAudioSources();
        SetVolume();
        SliderUpdate();
    }

    public void PlaySFX(AudioClip SFX_Clip)
    {
        UI_Source.PlayOneShot(SFX_Clip);
    }

    public void FindAudioSources()
    {
        AudioSources = null;
        AudioSources = GameObject.FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
    }

    public void SetVolume()
    {
        foreach(AudioSource _source in AudioSources)
        {
            Debug.Log("foreach");
            if(!_source == OST_Source && !_source == UI_Source)
            {
                Debug.Log("sfx");
                _source.volume = SFX_Slider.value;
            }
            if (!_source == OST_Source && _source == UI_Source)
            {
                _source.volume = UI_Slider.value;
            }
            if (_source == OST_Source && !_source == UI_Source)
            {
                _source.volume = OST_Slider.value;
            }
        }
    }

    void SliderUpdate()
    {
        SFX_Volume = SFX_Slider.value;
        OST_Volume = OST_Slider.value;
        UI_Volume = UI_Slider.value;
    }
}       
