using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedIcon : MonoBehaviour
{
    public enum SelectedDirection { left, right };
    public SelectedDirection iconDirection;
    public Sprite settingsUnderline;
    public Sprite controlsUnderline;
    public Sprite selectedIcon;
    private SpriteRenderer selectedSpriteRenderer;
    public float yDistance = 2f;
    public float yStart = 4.7f;

    void Awake()
    {
        selectedSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    public void UpdateSelectedIconPosition(int layer, int x, int y, int mainMenuPositionX)
    {
        selectedSpriteRenderer.enabled = (layer == 2 && mainMenuPositionX == 3) ? false : true;
        if (iconDirection == SelectedDirection.left)
        {
            if (layer == 1)
            {
                selectedSpriteRenderer.sprite = selectedIcon;
                if (x == 1)
                {
                    gameObject.transform.position = new Vector3(-8f, 1.5f, 0f);
                }
                else if (x == 2)
                {
                    gameObject.transform.position = new Vector3(-8f, -1.5f, 0f);
                }
                else if (x == 3)
                {
                    gameObject.transform.position = new Vector3(-8f, -4.5f, 0f);
                }
                else if (x == 4)
                {
                    gameObject.transform.position = new Vector3(-8f, -7.5f, 0f);
                }
            }
            else if (layer == 2)
            {
                selectedSpriteRenderer.sprite = settingsUnderline;
                if (x == 1)
                {
                    gameObject.transform.position = new Vector3(-8f, 4.75f, 0f);
                }
                else if (x == 2)
                {
                    gameObject.transform.position = new Vector3(-8f, 2.5f, 0f);
                }
                else if (x == 3)
                {
                    gameObject.transform.position = new Vector3(-8f, 0.25f, 0f);
                }
                else if (x == 4)
                {
                    gameObject.transform.position = new Vector3(-8f, -2f, 0f);
                }
                else if (x == 5)
                {
                    gameObject.transform.position = new Vector3(-8f, -4.25f, 0f);
                }
                else if (x == 6)
                {
                    gameObject.transform.position = new Vector3(-8f, -6.5f, 0f);
                }
                else
                {
                    selectedSpriteRenderer.sprite = controlsUnderline;
                    gameObject.transform.position = new Vector3(0f, -10f, 0f);
                }
            }
            else if (layer == 3)
            {
                selectedSpriteRenderer.sprite = controlsUnderline;
                float xCoord;
                float yCoord;
                if (y == 1 && x < 12)
                {
                    xCoord = 5f;
                    yCoord = yStart - (yDistance * (x - 1));
                    gameObject.transform.position = new Vector3(xCoord, yCoord, 0f);
                }
                else if (y == 2 && x < 12)
                {
                    xCoord = 13f;
                    yCoord = yStart - (yDistance * (x - 1));
                    gameObject.transform.position = new Vector3(xCoord, yCoord, 0f);
                }
                else if (x == 12)
                {
                    if (y == 1)
                    {
                        gameObject.transform.position = new Vector3(0f, -10f, 0f);
                    }
                    else if (y == 2)
                    {
                        gameObject.transform.position = new Vector3(11.5f, -10f, 0f);
                    }
                }
            }
        }
        else
        {
            // RIGHT
            if (layer == 1)
            {
                selectedSpriteRenderer.sprite = selectedIcon;
                if (x == 1)
                {
                    gameObject.transform.position = new Vector3(8f, 1.5f, 0f);
                }
                else if (x == 2)
                {
                    gameObject.transform.position = new Vector3(8f, -1.5f, 0f);
                }
                else if (x == 3)
                {
                    gameObject.transform.position = new Vector3(8f, -4.5f, 0f);
                }
                else if (x == 4)
                {
                    gameObject.transform.position = new Vector3(8f, -7.5f, 0f);
                }
            }
            else if (layer == 2)
            {
                selectedSpriteRenderer.enabled = false;
            }
            else if (layer == 3)
            {
                selectedSpriteRenderer.enabled = false;
            }
        }
    }

    public void HideSelectedIcon()
    {
        selectedSpriteRenderer.enabled = false;
    }

    public void ShowSelectedIcon()
    {
        selectedSpriteRenderer.enabled = true;
    }
}
