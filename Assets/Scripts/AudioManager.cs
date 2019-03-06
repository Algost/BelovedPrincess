using UnityEngine.Audio;
using UnityEngine;
using System.Collections;
using System;

public class AudioManager : MonoBehaviour {

	public Sound[] _sounds;
	public static AudioManager instance;

	AudioMixer mixer;

	// Use this for initialization
	void Awake () {

		if (instance == null)
			instance = this;
		else
		{
			Destroy(gameObject);
			return;
		}

		DontDestroyOnLoad(gameObject);

		foreach (Sound son in _sounds)
		{
			son.Source = gameObject.AddComponent<AudioSource>();
			son.Source.clip = son.Clip;

			son.Source.volume = son.Volume;
			son.Source.pitch = son.Pitch;
			son.Source.loop = son.Loop;
			son.Source.outputAudioMixerGroup = son.MixerGroup;
		}

	}

	public void Start()
	{
		Play("blind-love");
		mixer = Resources.Load ("MainMixer") as AudioMixer;
	}

	public void Play(string name)
	{
		Sound s = Array.Find(_sounds, sound => sound.Name == name);
		if (s == null)
		{
			Debug.Log("erreur de son : " + name + " not found");
			return;
		}
		s.Source.Play();
	}

    public void playSound(string path)
    {
		Debug.Log("path : "+path);
        AudioClip sound;
        sound = Resources.Load<AudioClip>(path);

        Sound son = new Sound();

        son.Source = gameObject.AddComponent<AudioSource>();
		son.Source.clip = sound;

        son.Source.volume = son.Volume;
        son.Source.pitch = son.Pitch;
		son.Source.loop = son.Loop;
		son.Source.outputAudioMixerGroup = mixer.FindMatchingGroups("Effect")[0];

		son.Source.Play ();
    }
}