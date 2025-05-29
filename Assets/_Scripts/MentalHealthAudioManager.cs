using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace Bardent
{
    public class MentalHealthAudioManager : MonoBehaviour
    {
        [Header("Mental Health Settings")]
        [Range(0f, 1f)] public float mentalHealth = 1f;
        public float transitionTime = 1f;

        [Header("Audio Mixer Snapshots")]
        public AudioMixer audioMixer;
        public AudioMixerSnapshot depressedSnapshot;
        public AudioMixerSnapshot neutralSnapshot;
        public AudioMixerSnapshot positiveSnapshot;

        [Header("Audio Sources")]
        public AudioSource depressedSource;
        public AudioSource neutralSource;
        public AudioSource positiveSource;

        [Header("Playlists")]
        public List<AudioClip> depressedClips;
        public List<AudioClip> neutralClips;
        public List<AudioClip> positiveClips;

        private Coroutine depressedRoutine;
        private Coroutine neutralRoutine;
        private Coroutine positiveRoutine;

        private enum Mood { Depressed, Neutral, Positive }
        private Mood currentMood;

        void Start()
        {
            depressedRoutine = StartCoroutine(PlaylistRoutine(depressedClips, depressedSource));
            neutralRoutine = StartCoroutine(PlaylistRoutine(neutralClips, neutralSource));
            positiveRoutine = StartCoroutine(PlaylistRoutine(positiveClips, positiveSource));

            // Start with full volume on all, and let mixer handle the transition
            depressedSource.Play();
            neutralSource.Play();
            positiveSource.Play();
        }

        void Update()
        {
            Mood newMood = GetMoodFromHealth(mentalHealth);

            if (newMood != currentMood)
            {
                currentMood = newMood;
                TransitionMood(currentMood);
            }
        }

        Mood GetMoodFromHealth(float health)
        {
            if (health <= 0.3f) return Mood.Depressed;
            if (health <= 0.7f) return Mood.Neutral;
            return Mood.Positive;
        }

        void TransitionMood(Mood mood)
        {
            switch (mood)
            {
                case Mood.Depressed:
                    depressedSnapshot.TransitionTo(transitionTime);
                    break;
                case Mood.Neutral:
                    neutralSnapshot.TransitionTo(transitionTime);
                    break;
                case Mood.Positive:
                    positiveSnapshot.TransitionTo(transitionTime);
                    break;
            }
        }

        IEnumerator PlaylistRoutine(List<AudioClip> clips, AudioSource source)
        {
            if (clips == null || clips.Count == 0)
                yield break;

            int index = 0;

            while (true)
            {
                AudioClip clip = clips[index];
                int repeatCount = Random.Range(20, 40); // 3 to 5 inclusive

                for (int i = 0; i < repeatCount; i++)
                {
                    source.clip = clip;
                    source.Play();
                    yield return new WaitForSeconds(clip.length);
                }

                index = (index + 1) % clips.Count;
            }
        }

        public void StopCoroutines()
        {
            StopCoroutine(depressedRoutine);
            StopCoroutine(neutralRoutine);
            StopCoroutine(positiveRoutine);
        }

        private void OnDestroy()
        {
            StopCoroutines();
        }
    }
}
