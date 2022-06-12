using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;


public class audioManager : MonoBehaviour
{
    [SerializeField] public AudioMixer audioMixer;
    [SerializeField] public AudioClip themeMusic;
    [SerializeField] public AudioClip walkingStep;
    [SerializeField] public AudioClip[] ursinFiring;
    [SerializeField] public AudioClip beachEnvironment;
    [SerializeField] public AudioClip sharkDies;
    [SerializeField] public AudioClip[] sharkAttack;
    [SerializeField] public AudioClip[] scoreItem;
    [SerializeField] public AudioClip[] pickupShell;
    [SerializeField] public AudioClip laserBeam;
    [SerializeField] public AudioClip onLoss;


    float musicVolume;
    

    const string MIXER_MUSIC = "MusicVolume";

    public void MuteMusicToggle(bool muted)
    {
        Debug.Log(muted);
        if(muted)
        {
            audioMixer.SetFloat(MIXER_MUSIC, 0);
        }
        if(!muted)
        {
            audioMixer.SetFloat(MIXER_MUSIC, -80);
        }
    }
}
