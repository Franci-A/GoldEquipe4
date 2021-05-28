using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public static AudioManager instance;

    public string soundName;

    void Awake()
    {
        if (instance == null) {
            instance = this;
        }
        else {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    void Start()
    {
        Play("Music");
    }

    public void Play (string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.Log("Sound " + name + " not found!");
            return;
        }
        s.source.Play();
    }
    
    public void PlayWithoutStr ()
    {
        Sound s = Array.Find(sounds, sound => sound.name == soundName);
        if (s == null)
        {
            Debug.Log("Sound " + soundName + " not found!");
            return;
        }
        s.source.Play();
    }

    public void Music(string name, string status)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null) {
            Debug.Log("Sound " + name + " not found!");
            return;
        }

        if (status == "Pause") {
            s.source.Pause();
        }

        else if (status == "UnPause") {
            s.source.UnPause();
        }
    }

    public void Sfx(string name, bool On)
    {
        Sound[] s = Array.FindAll(sounds, sound => sound.name != name);

        if (On) {
            foreach (Sound sound in s) {
                sound.source.volume = 75;
            }
        }
        
        else {
            foreach (Sound sound in s) {
                sound.source.volume = 0;
            }
        }
    }
}
