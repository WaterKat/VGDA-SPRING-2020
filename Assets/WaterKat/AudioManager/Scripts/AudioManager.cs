using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEditor;
using System;

namespace WaterKat.Audio
{
    public class AudioManager : MonoBehaviour
    {

        private static AudioManager _singleton = null;
        private static readonly object _lockobject = new object();

        public static AudioManager instance
        {
            get
            {
                if (_singleton != null)
                {
                    return _singleton;
                }
                else
                {
                    GameObject newHome = new GameObject();
                    newHome.name = typeof(AudioManager).ToString();
                    newHome.AddComponent<AudioManager>();
                    DontDestroyOnLoad(newHome);
                    return newHome.GetComponent<AudioManager>();
                }
            }
        }

        AudioManager() { }

        string className;
        void Awake()
        {
            className = this.GetType().ToString();
            lock (_lockobject)
            {
                _singleton = this;
                AudioManager[] _audioManagerList = UnityEngine.Object.FindObjectsOfType<AudioManager>();
                if (_audioManagerList.Length > 1)
                {
                    Debug.LogWarning(className + ".Awake() Warning: Duplicate " + className + "(s) found!");
                }
                foreach (AudioManager _audioManager in _audioManagerList)
                {
                    if (_audioManager != this)
                    {
                        _audioManager.gameObject.SetActive(false);
                        Destroy(_audioManager.gameObject);
                    }
                }
            }
            //DontDestroyOnLoad(transform.gameObject);
            transform.gameObject.name = className;
        }

        [SerializeField]
        string StartingAudioName = "";
        [SerializeField]
        private string mainAudioPath = "Assets/WaterKat/AudioManager/AudioInterfaces";
        [Space(30)]
        [SerializeField]
        List<AudioInterface> AudioClips = new List<AudioInterface>();

        private void Start()
        {
            foreach (AudioInterface audioClip in AudioClips)
            {
                GameObject gameObject = new GameObject("Audio_" + audioClip.name);
                gameObject.transform.SetParent(transform);
                audioClip.SetSource(gameObject.AddComponent<AudioSource>());
            }
            if (StartingAudioName == "WK_Music") { Debug.LogAssertion("Hello World! Thank you for choosing the WaterKat AudioManager! Please use the ReadMe to customize your setup!"); }
            if (StartingAudioName != "")
            {
                PlaySound(StartingAudioName);
            }
        }

        private AudioInterface GetAudioClip(string audioName)
        {
            foreach (AudioInterface audioClip in AudioClips)
            {
                if ((audioClip.name) == audioName)
                {
                    return audioClip;
                }
            }
            Debug.Log("GetAudioClip() audioClip " + audioName + " not found!");
            return null;
        }

        public static void PlaySound(string _audioName)
        {
            AudioInterface audioClip = instance.GetAudioClip(_audioName);
            if (audioClip != null)
            {
                audioClip.Play();
            }
        }
        public static void PlaySoundWithDelay(string _audioName, float _delay)
        {
            AudioInterface audioClip = instance.GetAudioClip(_audioName);
            if (audioClip != null)
            {
                audioClip.Play(_delay);
            }
        }
        public static void PauseSound(string _audioName)
        {
            foreach (AudioInterface audioClip in instance.AudioClips)
            {
                if (audioClip.name == _audioName)
                {
                    audioClip.Pause();
                    return;
                }
            }
        }
        public static void unPauseSound(string _audioName)
        {
            foreach (AudioInterface audioClip in instance.AudioClips)
            {
                if (audioClip.name == _audioName)
                {
                    audioClip.unPause();
                    return;
                }
            }
        }
        public static void StopSound(string _audioName)
        {
            foreach (AudioInterface audioClip in instance.AudioClips)
            {
                if (audioClip.name == _audioName)
                {
                    audioClip.Stop();
                    return;
                }
            }
        }

        public static void PlayAudioClipAtPoint(string audioName, Vector3 vector3Point)
        {
            AudioInterface audioClip = instance.GetAudioClip(audioName);
            if (audioClip != null)
            {
                audioClip.PlayAtPoint(vector3Point);
                return;
            }
        }
#if UNITY_EDITOR
        [ContextMenu("UpdateAssets() Warning! This WILL override CURRENT DATA!")]
                void UpdateAssets()
                {
                    AudioClips.Clear();

                    string[] guids = AssetDatabase.FindAssets("t:" + typeof(AudioInterface).FullName, new[] { mainAudioPath });

                    List<string> assetPaths = new List<string>();

                    foreach (string guid in guids)
                    {
                        assetPaths.Add(AssetDatabase.GUIDToAssetPath(guid));
                    }

                    foreach (string assetPath in assetPaths)
                    {
                        Debug.Log(assetPath);
                        AudioInterface audioInterface = AssetDatabase.LoadAssetAtPath(assetPath, typeof(AudioInterface)) as AudioInterface;
                        AudioClips.Add(audioInterface);
                    }
                }

#endif
    }
}