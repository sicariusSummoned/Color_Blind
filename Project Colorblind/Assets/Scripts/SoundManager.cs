using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    public AudioSource sfxSource;
    public AudioSource bgmSource;

    public static SoundManager instance = null;

    public AudioClip buttonClip;
    public AudioClip doorClip;
    public AudioClip playerdeathClip1;
    public AudioClip playerdeathClip2;
    public AudioClip playerdeathClip3;


    //This keeps this a singleton
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }else if(instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
    

    public void PlayButtonSound()
    {
        sfxSource.PlayOneShot(buttonClip,1.0f);
    }

    public void PlayDoorSound()
    {
        sfxSource.PlayOneShot(doorClip, 1.0f);
    }

    public void PlayDeathSound(int playerNumber)
    {
        switch(playerNumber)
        {
            case 1:
                sfxSource.PlayOneShot(playerdeathClip1, 1.0f);
                break;
            case 2:
                sfxSource.PlayOneShot(playerdeathClip2, 1.0f);
                break;
            case 3:
                sfxSource.PlayOneShot(playerdeathClip3, 1.0f);
                break;
            default:
                Debug.Log("ERR: incorrect player index");
                break;
        }

        
    }
}
