using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterAudioManager : MonoBehaviour
{
    [System.Serializable]
    public class AudioEntry
    {
        public string key;
        public AudioClip clip;
    }

    public List<AudioEntry> audioEntries; // List to set in the Inspector
    private Dictionary<string, AudioClip> audioDictionary;
    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        audioDictionary = new Dictionary<string, AudioClip>();
        foreach (var entry in audioEntries)
        {
            if (!audioDictionary.ContainsKey(entry.key))
            {
                audioDictionary.Add(entry.key, entry.clip);
            }
        }
    }

    [ContextMenu("Play Sound Test")]
    public void PlaySound(string soundKey)
    {
        if (audioDictionary.TryGetValue(soundKey, out AudioClip clip))
        {
            audioSource.PlayOneShot(clip);
        }
        else
        {
            Debug.LogWarning("Sound key not found: " + soundKey);
        }
    }
}
