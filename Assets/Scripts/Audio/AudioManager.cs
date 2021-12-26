using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    #region Properties
    public AudioSource navigateAudio;

    public AudioSource clickAudio;

    #endregion
    
    #region Methods
    void Awake()
    {
        instance = this;
    }

    public void NavigationAudio()
    {
        navigateAudio.Play();
    }

    public void ClickItemAudio()
    {
        clickAudio.Play();
    }
    #endregion
}
