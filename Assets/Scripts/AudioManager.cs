using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource source;
    [SerializeField]
    private AudioClip current_clip;

    public static AudioManager _this;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        if (_this == null)
        {
            _this = this;
        }
        else
        {
            Destroy(gameObject);
        }

        source = GetComponent<AudioSource>();

        if (source != null) 
        {
            current_clip = source.clip;
        }
        
    }

    public void ChangeAudio(AudioClip clip) 
    {
        if (clip == current_clip)
        {
            return;
        }
        current_clip = clip;
        source.clip = clip;
        source.Play();
    }


}
