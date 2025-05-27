using UnityEditor.Animations;
using UnityEngine;

namespace Bardent
{
    [CreateAssetMenu(fileName ="DefaultMineData", menuName = "Mine/New Mine Data")]
    public class MineData : ScriptableObject
    {
        public AnimatorController mineAnimatorController;
        public AnimatorController bombAnimatorController;
        public float activationTime;
        public string parameterKillName; // parameter name to increase if killed by this mine; if empty ignore it
        public float damage;
        public float poiseDamage;
        public float impactVelocity;
        public string feedBackName;
        public AudioClip feedBackSound;
        public float feedBackEntensity;
        public float subFeedBackDelay;
        public float postFeedBackDelay;
    }
}
