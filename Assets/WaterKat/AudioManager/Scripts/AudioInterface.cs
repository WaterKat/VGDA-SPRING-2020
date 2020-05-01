using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace WaterKat.Audio
{
    [CreateAssetMenu(fileName = "NewAudioInterface", menuName = "WaterKat/Audio/AudioInterface", order = 50)]
    public class AudioInterface : ScriptableObject
    {
        AudioSource audioSource;

        [SerializeField]
        AudioMixerGroup audioMixerGroup = null;

        [SerializeField]
        AudioClip audioClip = null;
        public AudioClip AudioClip { get { return AudioClip; } set { } }

        [Space(30)]
        [SerializeField]
        bool Mute = false;
        [SerializeField]
        bool Loop = false;
        [Range(0, 1)]
        [SerializeField]
        float Volume = 1;
        [Range(0, 3)]
        [SerializeField]
        float Pitch = 1;
        [SerializeField]
        bool Reverse = false;

        [Range(0f, 0.5f)]
        public float RandomVolumeModifier;
        [Range(0f, 0.5f)]
        public float RandomPitchModifier;


        public void SetSource(AudioSource _audioSource)
        {
            audioSource = _audioSource;
            UpdateAudioSource(audioSource);
        }
        public void UpdateAudioSource(AudioSource audioSource)
        {
            audioSource.outputAudioMixerGroup = audioMixerGroup;
            audioSource.clip = audioClip;
            audioSource.mute = Mute;
            audioSource.loop = Loop;
            audioSource.volume = Volume;
            if (!Reverse)
            {
                audioSource.pitch = Pitch;
            }
            else
            {
                audioSource.pitch = -Pitch;
            }
            audioSource.playOnAwake = false;
        }

        public void Play()
        {
            if (audioSource == null) { return; }
            audioSource.volume = Volume * (1 + Random.Range(-RandomVolumeModifier / 2f, RandomVolumeModifier / 2f));
            audioSource.pitch = Pitch * (1 + Random.Range(-RandomPitchModifier / 2f, RandomPitchModifier / 2f));
            audioSource.Play();
        }
        public void Play(float _delay)
        {
            if (audioSource == null) { return; }
            audioSource.volume = Volume * (1 + Random.Range(-RandomVolumeModifier / 2f, RandomVolumeModifier / 2f));
            audioSource.pitch = Pitch * (1 + Random.Range(-RandomPitchModifier / 2f, RandomPitchModifier / 2f));
            audioSource.PlayDelayed(_delay);
        }

        public bool isPlaying()
        {
            if (audioSource == null) { return; }
            return audioSource.isPlaying;
        }

        public void Pause()
        {
            if (audioSource == null) { return; }
            audioSource.Pause();
        }
        public void unPause()
        {
            if (audioSource == null) { return; }
            audioSource.UnPause();
        }
        public void Stop()
        {
            if (audioSource == null) { return; }
            audioSource.Stop();
        }

        public void PlayAtPoint(Vector3 vector3Point)
        {
            if (audioSource == null) { return; }
            float tempVolume = Volume * (1 + Random.Range(-RandomVolumeModifier / 2f, RandomVolumeModifier / 2f));
            AudioSource.PlayClipAtPoint(audioClip, vector3Point, tempVolume);
        }
    }
}