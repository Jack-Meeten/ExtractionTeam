using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource SFX_Source;
    [SerializeField] GameObject GameManager;


    void Start()
    {
        SFX_Source = GameManager.GetComponent<AudioSource>();
    }

    public void PlaySFX(AudioClip SFX_Clip)
    {
        SFX_Source.PlayOneShot(SFX_Clip);
    }
}
