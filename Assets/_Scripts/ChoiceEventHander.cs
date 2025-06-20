using MoreMountains.Feedbacks;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Bardent
{
    public class ChoiceEventHander : MonoBehaviour, ISelectHandler, IDeselectHandler
    {
        [SerializeField] private GameObject selectionObject;
        [SerializeField] private Image ratHeadImage;
        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] private FloatingCharactersTMP FloatingCharactersTMP;
        [SerializeField] private MMF_Player feedbackPlayer;
        public void OnDeselect(BaseEventData eventData)
        {
            selectionObject?.SetActive(false);
            text.color = Color.grey;
            transform.localScale = Vector3.one;
            FloatingCharactersTMP?.StopAnimation();
        }

        private void OnEnable()
        {
            selectionObject?.SetActive(false);
            text.color = Color.grey;
        }

        public void OnSelect(BaseEventData eventData)
        {
            selectionObject?.SetActive(true);
            text.color = Color.yellow;
            transform.localScale = new Vector3(1.05f, 1.05f, 1f);
            FloatingCharactersTMP?.StartAnimation();
            //feedbackPlayer?.PlayFeedbacks();
        }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Awake()
        {
            if (selectionObject == null) selectionObject = gameObject.transform.GetChild(1).gameObject;
            if(ratHeadImage == null) ratHeadImage = selectionObject.GetComponent<Image>();
            if(text == null) text = selectionObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            selectionObject?.SetActive(false);
        }
    }
}
