using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SoundSample {  GameOver,Delivery,StartShip,Button,ShipPicked,Bounce,Explosion,Prepare};

public class SoundManager : MonoBehaviour
{
    public static SoundManager sm;

    public AudioSource source;
   // public AudioClip main_theme;

    public AudioClip gameOver;
    public AudioClip delivery;
    public AudioClip startShip;
    public AudioClip button;
    public AudioClip shipPicked;
    public AudioClip bounce;
    public AudioClip explosion;
    public AudioClip prepare;

    // Use this for initialization
    void Awake ()
    {
        if (SoundManager.sm != null)
            Destroy(SoundManager.sm.gameObject);
        sm = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SingleSound(SoundSample sound)
    {
        switch (sound)
        {
            case SoundSample.GameOver:
                source.PlayOneShot(gameOver, 1.0f);
                break;
            case SoundSample.Delivery:
                source.PlayOneShot(delivery, 1.0f);
                break;
            case SoundSample.StartShip:
                source.PlayOneShot(startShip, 1.0f);
                break;
            case SoundSample.Button:
                source.PlayOneShot(button, 0.6f);
                break;
            case SoundSample.ShipPicked:
                source.PlayOneShot(shipPicked, 1.0f);
                break;
            case SoundSample.Bounce:
                source.PlayOneShot(bounce, 1.0f);
                break;
            case SoundSample.Explosion:
                source.PlayOneShot(explosion, 1.0f);
                break;
            case SoundSample.Prepare:
                source.PlayOneShot(prepare, 1.0f);
                
                break;
        }
    }
}
