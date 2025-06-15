using UnityEngine;

namespace Bardent
{

    public enum AnimationtriggerType
    {
        EntryFinished,
        Attack,
        AttackFinished,
    }


    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(SpriteRenderer))]
    public class Graphic : MonoBehaviour
    {

        [SerializeField] private Graphics m_Graphics;
        private SpriteRenderer m_SpriteRenderer;
        public Animator animator;

        void Awake()
        {
            m_Graphics = m_Graphics ? m_Graphics : GetComponentInParent<Graphics>();
            animator = GetComponent<Animator>();
            m_SpriteRenderer = GetComponentInParent<SpriteRenderer>();
            m_Graphics.AddGraphic(this);
        }

        private void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        public void AnimationTrigger(AnimationtriggerType type)
        {
            //m_Graphics.
            switch (type)
            {
                case AnimationtriggerType.EntryFinished:
                    break;
                case AnimationtriggerType.Attack:
                    break;
                case AnimationtriggerType.AttackFinished:
                    break;
                default:
                    break;
            }
        }
    }
}
