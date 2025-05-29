using System.Collections;
using Bardent.CoreSystem;
using Bardent.CoreSystem.StatsSystem;
using DG.Tweening;
using MoreMountains.Feedbacks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Bardent
{
    public class UI_StatController : MonoBehaviour
    {
        enum StatType
        {
            pHealth,
            mHealth,
            Poise
        }
        [SerializeField] private EventManager.GameEvent mEvent;
        [SerializeField] private string titleString;
        [SerializeField] private TextMeshProUGUI m_title;
        [SerializeField] private Image backGroundImage;
        [SerializeField] private Image healthValue;
        private Stat stat;
        private bool ready=false;
        [SerializeField] StatType type = StatType.pHealth;
        [SerializeField] MMF_Player player;

        private void Awake()
        {
            m_title.text = titleString;
            ready = false;
        }

        private void Start()
        {
            switch (type)
            {
                case StatType.pHealth:
                    stat = Player.instance.Core.GetCoreComponent<Stats>().physicalHealth; break;
                case StatType.mHealth:
                    stat = Player.instance.Core.GetCoreComponent<Stats>().mentalHealth; break;
                case StatType.Poise:
                    stat = Player.instance.Core.GetCoreComponent<Stats>().Poise; break;
                default:
                    stat = Player.instance.Core.GetCoreComponent<Stats>().physicalHealth; break;
            }
            EventManager.Instance.AddListener(mEvent, UpdateUI);
            ready = true;
        }

        public void UpdateUI()
        {
            if(ready)
            {
                player.PlayFeedbacks();
                healthValue.DOFillAmount(stat.GetPencentage(), 0.5f);
            }
        }

        private void OnEnable()
        {
            EventManager.Instance.AddListener(mEvent, UpdateUI);
        }
        private void OnDestroy()
        {
            EventManager.Instance.RemoveListener(mEvent, UpdateUI);
        }
        private void OnDisable()
        {
            EventManager.Instance.RemoveListener(mEvent, UpdateUI);
        }
    }
}
