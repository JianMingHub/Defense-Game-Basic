using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace COHENLI.DefenseBasic
{
    public class AudioController : MonoBehaviour
    {
        public static AudioController Ins;

        [Header("Main Setting:")]
        [Range(0f, 1f)]
        public float musicVol = 0.3f;
        [Range(0f, 1f)]
        public float soundVol = 1f;

        public AudioSource musicAus;
        public AudioSource soundAus;

        [Header("Music and Sound in Gameplay:")]
        public AudioClip playerAtk;
        public AudioClip enemyDead;
        public AudioClip gameOver;
        public AudioClip[] bgms;

        public void Awake()
        {
            Ins = this;
        }
        private void Start() 
        {
            if (musicAus == null || soundAus == null)  return;
            
            musicVol = Pref.musicVol;
            soundVol = Pref.soundVol;

            musicAus.volume = musicVol;
            soundAus.volume = soundVol;
        }

        public void PlaySound(AudioClip[] sounds, AudioSource aus = null)           // PlaySound(sounds)
        {
            if (!aus)
                aus = musicAus;
            if (aus == null) return;
            if (soundAus == null || sounds.Length <=0) return;      // Play sound from default
            
            int randIdx = Random.Range(0, sounds.Length);           // Random
            if (sounds[randIdx])
                aus.PlayOneShot(sounds[randIdx], soundVol);
        }
        public void PlaySound(AudioClip sound, AudioSource aus = null) // Play
        {
            if (!aus)
                aus = soundAus;
            if (aus == null) return;
            if (sound)
                aus.PlayOneShot(sound, soundVol);
        }
        public void PlayMusic(AudioClip[] musics, bool isLoop = true)
        {
            if (musicAus == null || musics == null || musics.Length <= 0) return;
            int randIdx = Random.Range(0, musics.Length);
            if (musics[randIdx])
            {
                musicAus.clip = musics[randIdx];
                musicAus.loop = isLoop;
                musicAus.volume = musicVol;
                musicAus.Play();
            }
        }
        public void PlayMusic(AudioClip music, bool isLoop = true)
        {
            if (musicAus == null || music == null) return;

            musicAus.clip = music;
            musicAus.loop = isLoop;
            musicAus.volume = musicVol;
            musicAus.Play();
        }

        public void SetMusicVolume(float vol)
        {
            if (musicAus == null) return;
            musicAus.volume = vol;
        }
        public void StopMusic()
        {
            if (musicAus == null) return;
            musicAus.Stop();
        }
        public void PlayBgm()
        {
            PlayMusic(bgms);
        }
    }
}
