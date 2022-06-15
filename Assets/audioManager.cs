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
    [SerializeField] public Toggle muteToggle;
    [SerializeField] public bool isMusic;


    float musicVolume;
    

    const string MIXER_MUSIC = "MusicVolume";

	private void Start()
	{
        if(isMusic)
		{
            muteToggle.isOn = StaticHelper.musicOn;
            Debug.Log(StaticHelper.musicOn);
		}
    }

    public void MuteMusicToggle(bool soundOn)
    {
        if (soundOn)
            StaticHelper.musicVolume = -15f;
        else
            StaticHelper.musicVolume = -80f;
        StaticHelper.musicOn = soundOn;
        audioMixer.SetFloat(MIXER_MUSIC, StaticHelper.musicVolume);
    }
}
