using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private AudioClip clip;

    private void Start()
    {
        AudioManager._this.ChangeAudio(clip);
    }

}
