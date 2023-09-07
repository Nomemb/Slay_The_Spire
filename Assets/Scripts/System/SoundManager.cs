using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    
    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }
    
    [SerializeField] private AudioSource audioSource;

    [SerializeField] private List<AudioClip> audioClips;
    [SerializeField] private List<AudioClip> backgroundClips;

    
    public void PlaySound(string action)
    {
        switch (action)
        {
            case "CardSelect":
                audioSource.PlayOneShot(audioClips[0]);
                break;
            
            case "DefenseBreak":
                audioSource.PlayOneShot(audioClips[1]);
                break;
            
            case "EndTurn":
                audioSource.PlayOneShot(audioClips[2]);
                break;
            
            case "Attack":
                audioSource.PlayOneShot(audioClips[3]);
                break;
            
            case "GainDefense":
                audioSource.PlayOneShot(audioClips[4]);
                break;
            
            case "UIClick":
                audioSource.PlayOneShot(audioClips[5]);
                break;
            
            default:
                break;

        }
    }

    public void ChangeBGM(int number)
    {
        audioSource.clip = backgroundClips[number];
        audioSource.loop = true;
        audioSource.Play();
    }
}
