using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Bardent
{
    public class MainMenuScript : MonoBehaviour
    {
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        public List<Button> buttonsList;
        public List<GameObject> subMenuList;
        public MainMenuScript parent = null;
        public int parentIndex=0;

        [SerializeField] GameObject loadingPanel;
        [SerializeField] Image loadingImage;
        [SerializeField] TextMeshProUGUI loadingText;
        
        void Start()
        {
            if (buttonsList != null)
            {
                if (buttonsList.Count > 0)
                {
                    SelectButton(0);
                }
            }
        }

        public void SelectButton(int index)
        {
            if (buttonsList.Count >= index && subMenuList.Count > index)
            {
                buttonsList[0].Select();
            }
        }

        public void OnClickButton(int index)
        {
            if (buttonsList.Count >= index && subMenuList.Count > index)
            {
                HideAllSubMenus();
                if (subMenuList[index] != null)
                {
                    subMenuList[index].SetActive(true);
                    MainMenuScript subMenu = subMenuList[index].GetComponent<MainMenuScript>();
                    if (subMenu != null)
                    {
                        subMenu.SelectButton(0);
                    }
                }
                
            }
        }

        public void Return()
        {
            if (parent!=null)
            {
                parent.gameObject.SetActive(true);
                parent.SelectButton(parentIndex);
            }
            gameObject.SetActive(false);
        }

        public void HideAllSubMenus()
        {
            foreach (GameObject gameObject in subMenuList)
            {
                if (gameObject != null)
                {
                    gameObject.SetActive(false);
                    MainMenuScript childMenu = gameObject.GetComponent<MainMenuScript>();
                    if (childMenu!=null) childMenu.HideAllSubMenus();
                } 
                
            }
        }

        public void LoadSceneWithLoadingUI()
        {
            StartCoroutine(LoadingCoroutine());
        }

        private IEnumerator LoadingCoroutine()
        {
            AsyncOperation operaion = SceneManager.LoadSceneAsync(1);

            while (!operaion.isDone)
            {
                loadingPanel.SetActive(true);
                float loadingFloat = Mathf.Clamp01(operaion.progress);
                loadingImage.fillAmount = loadingFloat;
                loadingText.text = "..." + loadingFloat*100 + "%...";
                yield return null;
            }
        }
    }
}
