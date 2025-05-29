using UnityEngine;

namespace Bardent
{
    public class ImageZigzagger : MonoBehaviour
    {
        public enum Direction
        {
            Horizontal,
            Vertical
        }

        public Direction moveDirection = Direction.Horizontal;
        public float distance = 1f;
        public float duration = 2f;

        private Vector3 startPos;

        void Start()
        {
            startPos = transform.GetChild(0).localPosition;
        }

        void Update()
        {
            float pingPong = Mathf.PingPong(Time.time / duration, 1f) * 2f - 1f; // Ranges from -1 to 1

            Vector3 offset = Vector3.zero;
            if (moveDirection == Direction.Horizontal)
            {
                offset = new Vector3(pingPong * distance, 0f, 0f);
            }
            else if (moveDirection == Direction.Vertical)
            {
                offset = new Vector3(0f, pingPong * distance, 0f);
            }

            transform.GetChild(0).localPosition = startPos + offset;
        }
    }
}