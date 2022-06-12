using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;


public class audioManager : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] AudioClip themeMusic;
    [SerializeField] AudioClip walkingStep;
    [SerializeField] AudioClip[] ursinFiring;
    [SerializeField] AudioClip beachEnvironment;
    [SerializeField] AudioClip sharkDies;
    [SerializeField] AudioClip[] sharkAttack;
    [SerializeField] AudioClip[] scoreItem;
    [SerializeField] AudioClip[] pickupShell;
    [SerializeField] AudioClip laserBeam;
    [SerializeField] AudioClip onLoss;


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
