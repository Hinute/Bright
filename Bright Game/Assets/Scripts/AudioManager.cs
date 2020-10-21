using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour {
    public Sound[] sounds; // store all our sounds
    public Sound[] playlist; // store all our music

    private int currentPlayingIndex = 999; // set high to signify no song playing

    // a play music flag so we can stop playing music during cutscenes etc
    private bool shouldPlayMusic = false;

    public static AudioManager instance; // will hold a reference to the first AudioManager created

    private float mvol; // Global music volume
    private float evol; // Global effects volume

    private void Start() {
        // nothing here
    }

    private void Awake() {
        if (instance == null) { // if the instance var is null this is first AudioManager
            instance = this; //save this AudioManager in instance 
        } else {
            Destroy(gameObject); // this isnt the first so destroy it
            return; // since this isn't the first return so no other code is run
        }

        DontDestroyOnLoad(gameObject); // do not destroy me when a new scene loads

        // get preferences
        mvol = PlayerPrefs.GetFloat("MusicVolume", 0.75f);
        evol = PlayerPrefs.GetFloat("EffectsVolume", 0.75f);

        createAudioSources(sounds, evol); // create sources for effects
        createAudioSources(playlist, mvol); // create sources for music
    }

    // create sources
    private void createAudioSources(Sound[] sounds, float volume) {
        foreach (Sound s in sounds) { // loop through each music/effect
            s.source = gameObject.AddComponent<AudioSource>(); // create anew audio source(where the sound splays from in the world)
            s.source.clip = s.clip; // the actual music/effect clip
            s.source.volume = s.volume * volume; // set volume based on parameter
            s.source.pitch = s.pitch; // set the pitch
            s.source.loop = s.loop; // should it loop

            s.valid = true; // only set to true if created
        }
    }

    private Sound FindAudioByName(Sound[] audioList, string name) {
        return Array.Find(audioList, audio => audio.name == name);
    }
    public void PlaySound(string name) {
        // here we get the Sound from our array with the name passed in the methods parameters
        Debug.Log("AudioManager: Trying to play sound: " + name);

        Sound s = FindAudioByName(sounds, name);
        if (!s.valid) {
            Debug.LogError("Unable to play sound " + name);
            return;
        }
        s.source.Play(); // play the sound
    }

    public void PlayMusic(string musicName = "null") {
        Debug.Log("AudioManager: Trying to play music: " + musicName);

        if (shouldPlayMusic == false) {
            shouldPlayMusic = true;

            if (musicName == "null") {
                // start at the beginning of the playlist
                currentPlayingIndex = 0;
            } else {
                Sound music = FindAudioByName(playlist, musicName);
                if (!music.valid) {
                    Debug.LogError("Unable to play music " + musicName);
                    NextIndex();
                }

                currentPlayingIndex = System.Array.IndexOf(playlist, music);
            }

            playlist[currentPlayingIndex].source.volume = playlist[0].volume * mvol; // set the volume
            playlist[currentPlayingIndex].source.Play(); // play it

            Debug.Log("AudioManager: Playing Music: " + getSongName());
        }

    }

    // stop music
    public void StopMusic() {
        Debug.Log("AudioManager: Stopping Music: " + getSongName());

        if (shouldPlayMusic == true) {
            shouldPlayMusic = false;
            playlist[currentPlayingIndex].source.Stop();
            currentPlayingIndex = 999; // reset playlist counter
        } else if (currentPlayingIndex != 999) {
            playlist[currentPlayingIndex].source.Stop();
            currentPlayingIndex = 999;
        }
    }

    // pause music
    public void PauseMusic() {
        Debug.Log("AudioManager: Pausing Music: " + getSongName());

        if (shouldPlayMusic == true) {
            playlist[currentPlayingIndex].source.Pause();
            shouldPlayMusic = false;
        }
    }

    // resume music from where you left off
    public void ResumeMusic() {
        Debug.Log("AudioManager: Resuming Music: " + getSongName());

        if (shouldPlayMusic == false) {
            playlist[currentPlayingIndex].source.UnPause();
            shouldPlayMusic = true;
        }
    }

    private void NextIndex() {
        currentPlayingIndex++; // set next index
        if (currentPlayingIndex >= playlist.Length) { //have we went too high
            currentPlayingIndex = 0; // reset list when max reached
        }
    }

    void Update() {
        if (shouldPlayMusic == true) {
            // if we are playing a track from the playlist && it has stopped playing
            if (currentPlayingIndex != 999 && !playlist[currentPlayingIndex].source.isPlaying) {
                if (playlist[currentPlayingIndex].loopToAudio == "") {
                    NextIndex();
                } else {
                    Sound music = FindAudioByName(playlist, playlist[currentPlayingIndex].loopToAudio);
                    if (!music.valid) {
                        Debug.LogError("Unable to play music " + playlist[currentPlayingIndex].loopToAudio);
                        NextIndex();
                        return;
                    }

                    currentPlayingIndex = System.Array.IndexOf(playlist, music);
                }

                playlist[currentPlayingIndex].source.Play(); // play that funky music
            }
        }
    }

    // get the song name
    public String getSongName() {
        return playlist[currentPlayingIndex].name;
    }

    // if the music volume change update all the audio sources
    public void musicVolumeChanged() {
        foreach (Sound m in playlist) {
            mvol = PlayerPrefs.GetFloat("MusicVolume", 0.5);
            m.source.volume = playlist[0].volume * mvol;
        }
    }

    //if the effects volume changed update the audio sources
    public void effectVolumeChanged() {
        evol = PlayerPrefs.GetFloat("EffectsVolume", 0.75f);
        foreach (Sound s in sounds) {
            s.source.volume = s.volume * evol;
        }
        PlaySound("Select"); // play an effect so user can her effect volume
    }
}