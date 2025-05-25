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
        public void OnDeselect(BaseEventData eventData)
        {
            selectionObject?.SetActive(false);
            text.color = Color.grey;
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
