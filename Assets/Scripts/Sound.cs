using UnityEngine.Audio;
using UnityEngine;


[System.Serializable]
public class Sound {

    [SerializeField]
    private AudioClip clip;
    [SerializeField]
    private string name;
    [SerializeField]
    [Range(0f, 1f)]
    private float volume;
    [SerializeField]
    [Range(.1f, 3f)]
    private float pitch;
    [SerializeField]
    private bool loop;

    [SerializeField]
    private AudioMixerGroup mixerGroup;

    [SerializeField]
    [HideInInspector]
    private AudioSource source;

	public Sound()
	{
		volume = 1;
		pitch = 1;
		loop = false;
	}

    
    public AudioClip Clip
    {
        get
        {
            return clip;
        }

        set
        {
            clip = value;
        }
    }

    public string Name
    {
        get
        {
            return name;
        }

        set
        {
            name = value;
        }
    }

    public float Volume
    {
        get
        {
            return volume;
        }

        set
        {
            volume = value;
        }
    }

    public float Pitch
    {
        get
        {
            return pitch;
        }

        set
        {
            pitch = value;
        }
    }

    public AudioSource Source
    {
        get
        {
            return source;
        }

        set
        {
            source = value;
        }
    }

    public bool Loop
    {
        get
        {
            return loop;
        }

        set
        {
            loop = value;
        }
    }

    public AudioMixerGroup MixerGroup
    {
        get
        {
            return mixerGroup;
        }

        set
        {
            mixerGroup = value;
        }
    }
}
