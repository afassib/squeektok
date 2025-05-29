using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;

public class FeedbackLibrary : MonoBehaviour
{
    public static FeedbackLibrary Instance;
    [System.Serializable]
    public class NamedFeedback
    {
        public string feedbackName;
        public MMF_Player feedback;
    }

    [Header("List of Named Feedbacks")]
    public List<NamedFeedback> feedbacks = new List<NamedFeedback>();

    private Dictionary<string, MMF_Player> _feedbackDict;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else Destroy(gameObject);
        _feedbackDict = new Dictionary<string, MMF_Player>();
        foreach (var item in feedbacks)
        {
            if (!_feedbackDict.ContainsKey(item.feedbackName))
            {
                _feedbackDict.Add(item.feedbackName, item.feedback);
            }
        }
    }

    /// <summary>
    /// Plays the feedback by its name.
    /// </summary>

    public void Play(string name, FeedbackContext context = null)
    {
        if (_feedbackDict.TryGetValue(name, out MMF_Player fb))
        {
            if (context != null)
            {
                fb.FeedbacksIntensity = context.intensity ?? 1f;

                foreach (var feedback in fb.FeedbacksList)
                {
                    if (feedback is MMF_Sound sound && context.audioClip != null)
                    {
                        sound.Sfx = context.audioClip;
                    }

                    if (feedback is MMF_Flash color && context.color != null)
                    {
                        color.FlashColor = context.color ?? Color.white;
                    }

                    // Add more handlers as needed...
                }
            }

            fb.PlayFeedbacks();
        }
        else
        {
            Debug.LogWarning($"Feedback '{name}' not found in FeedbackLibrary.");
        }
    }

    /// <summary>
    /// Stops a feedback by its name.
    /// </summary>
    public void Stop(string name)
    {
        if (_feedbackDict.TryGetValue(name, out MMF_Player fb))
        {
            fb.StopFeedbacks();
        }
    }
}


public class FeedbackContext
{
    public float? intensity;
    public Color? color;
    public AudioClip audioClip;
    public Sprite sprite;

    // Add any more shared types you need
    public Vector3? position;
    public string message;

    // You could also add a Dictionary<string, object> for custom extension
}