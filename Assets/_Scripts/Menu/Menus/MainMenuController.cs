using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class MainMenuController : MonoBehaviour, ISceneController
{
    private int menuLayer = 1;
    private Vector2Int mainMenuPosition = new Vector2Int(1, 1);
    private Vector2Int settingsPosition = new Vector2Int(1, 1);
    private Vector2Int controlsPosition = new Vector2Int(1, 1);

    // xbounds (left to right), ybounds (bottom to top), layer, row, col
    // layer 1
    private List<float> playButton = new List<float>() { -4f, 4f, 0f, 3f, 1f, 1f, 1f };
    private List<float> settingsButton = new List<float>() { -4f, 4f, -3f, -0.01f, 1f, 2f, 1f};
    private List<float> creditsButton = new List<float>() { -4f, 4f, -6f, -3.01f, 1f, 3f, 1f };
    private List<float> quitButton = new List<float>() { -4f, 4f, -9f, -6.01f, 1f, 4f, 1f };
    // layer 2
    private List<float> fullscreen = new List<float>() { 4.6f, 14.6f, 4.65f, 6.65f, 2f, 1f, 1f };
    private List<float> fullscreenRight = new List<float>() { 14.61f, 16.6f, 4.65f, 6.65f, 2f, 1f, 2f };
    private List<float> fullscreenLeft = new List<float>() { 2.59f, 4.59f, 4.65f, 6.65f, 2f, 1f, -1f };
    private List<float> music = new List<float>() { 4.6f, 14.6f, 2.4f, 4.4f, 2f, 2f, 1f };
    private List<float> musicRight = new List<float>() { 14.61f, 16.6f, 2.4f, 4.4f, 2f, 2f, 2f };
    private List<float> musicLeft = new List<float>() { 2.59f, 4.59f, 2.4f, 4.4f, 2f, 2f, -1f };
    private List<float> effects = new List<float>() { 4.6f, 14.6f, 0.15f, 2.15f, 2f, 3f, 1f };
    private List<float> effectsRight = new List<float>() { 14.61f, 16.6f, 0.15f, 2.15f, 2f, 3f, 2f };
    private List<float> effectsLeft = new List<float>() { 2.59f, 4.59f, 0.15f, 2.15f, 2f, 3f, -1f };
    private List<float> keyboardSettings = new List<float>() { 4.6f, 14.6f, -2.1f, -0.1f, 2f, 4f, 1f };
    private List<float> gamepadSettings = new List<float>() { 4.6f, 14.6f, -4.35f, -2.35f, 2f, 5f, 1f };
    private List<float> language = new List<float>() { 4.6f, 14.6f, -6.6f, -4.6f, 2f, 6f, 1f };
    private List<float> languageRight = new List<float>() { 14.61f, 16.6f, -6.6f, -4.6f, 2f, 6f, 2f };
    private List<float> languageLeft = new List<float>() { 2.59f, 4.59f, -6.6f, -4.6f, 2f, 6f, -1f };
    private List<float> settingsExit = new List<float>() { -4f, 4f, -10.5f, -6.5f, 2f, 7f, 1f };
    // layer 3
    private List<float> select1 = new List<float>() { 1.25f, 8.75f, 4.65f, 5.95f, 3f, 1f, 1f };
    private List<float> select2 = new List<float>() { 9.25f, 16.75f, 4.65f, 5.95f, 3f, 1f, 2f };

    private List<float> jump1 = new List<float>() { 1.25f, 8.75f, 3.525f, 4.825f, 3f, 2f, 1f };
    private List<float> jump2 = new List<float>() { 9.25f, 16.75f, 3.525f, 4.825f, 3f, 2f, 2f };
    private List<float> grab1 = new List<float>() { 1.25f, 8.75f, 2.4f, 3.7f, 3f, 3f, 1f };
    private List<float> grab2 = new List<float>() { 9.25f, 16.75f, 2.4f, 3.7f, 3f, 3f, 2f };
    private List<float> dash1 = new List<float>() { 1.25f, 8.75f, 1.275f, 2.575f, 3f, 4f, 1f };
    private List<float> dash2 = new List<float>() { 9.25f, 16.75f, 1.275f, 2.575f, 3f, 4f, 2f };
    private List<float> primaryAttack1 = new List<float>() { 1.25f, 8.75f, 0.15f, 1.45f, 3f, 5f, 1f };
    private List<float> primaryAttack2 = new List<float>() { 9.25f, 16.75f, 0.15f, 1.45f, 3f, 5f, 2f };
    private List<float> secondaryAttack1 = new List<float>() { 1.25f, 8.75f, -0.975f, 0.325f, 3f, 6f, 1f };
    private List<float> secondaryAttack2 = new List<float>() { 9.25f, 16.75f, -0.975f, 0.325f, 3f, 6f, 2f };

    private List<float> back1 = new List<float>() { 1.25f, 8.75f, -2.1f, -0.8f, 3f, 7f, 1f };
    private List<float> back2 = new List<float>() { 9.25f, 16.75f, -2.1f, -0.8f, 3f, 7f, 2f };
    private List<float> up1 = new List<float>() { 1.25f, 8.75f, -3.225f, -1.925f, 3f, 8f, 1f };
    private List<float> up2 = new List<float>() { 9.25f, 16.75f, -3.225f, -1.925f, 3f, 8f, 2f };
    private List<float> down1 = new List<float>() { 1.25f, 8.75f, -4.35f, -3.05f, 3f, 9f, 1f };
    private List<float> down2 = new List<float>() { 9.25f, 16.75f, -4.35f, -3.05f, 3f, 9f, 2f };
    private List<float> left1 = new List<float>() { 1.25f, 8.75f, -5.475f, -4.175f, 3f, 10f, 1f };
    private List<float> left2 = new List<float>() { 9.25f, 16.75f, -5.475f, -4.175f, 3f, 10f, 2f };
    private List<float> right1 = new List<float>() { 1.25f, 8.75f, -6.6f, -5.3f, 3f, 11f, 1f };
    private List<float> right2 = new List<float>() { 9.25f, 16.75f, -6.6f, -5.3f, 3f, 11f, 2f };
    

    private List<float> controlsExit = new List<float>() { -4f, 4f, -10.5f, -6.5f, 3f, 12f, 1f };
    private List<float> controlsReset = new List<float>() { 6f, 14f, -10.25f, -6.75f, 3f, 12f, 2f };


    private List<List<float>> sceneClickables = new List<List<float>>();
    private ControlsController controlsController;
    private SettingsController settingsController;

    private int mainMenuLayerMax = 4;
    private int settingsPosXMax = 7;
    private int controlsPosXMax = 12;

    private SelectedIcon selectedIconLeft;
    private SelectedIcon selectedIconRight;

    private List<TextMeshPro> mainMenuOptions = new List<TextMeshPro>();
    private List<TextMeshPro> creditsOptions = new List<TextMeshPro>();


    void Awake()
    {
        menuLayer = 1;
        sceneClickables.Add(playButton);
        sceneClickables.Add(settingsButton);
        sceneClickables.Add(creditsButton);
        sceneClickables.Add(quitButton);
        sceneClickables.Add(fullscreen);
        sceneClickables.Add(music);
        sceneClickables.Add(effects);
        sceneClickables.Add(keyboardSettings);
        sceneClickables.Add(gamepadSettings);
        sceneClickables.Add(language);
        sceneClickables.Add(settingsExit);
        sceneClickables.Add(fullscreenLeft);
        sceneClickables.Add(fullscreenRight);
        sceneClickables.Add(musicLeft);
        sceneClickables.Add(musicRight);
        sceneClickables.Add(effectsLeft);
        sceneClickables.Add(effectsRight);
        sceneClickables.Add(languageLeft);
        sceneClickables.Add(languageRight);
        sceneClickables.Add(select1);
        sceneClickables.Add(select2);
        sceneClickables.Add(jump1);
        sceneClickables.Add(jump2);
        sceneClickables.Add(grab1);
        sceneClickables.Add(grab2);
        sceneClickables.Add(dash1);
        sceneClickables.Add(dash2);
        sceneClickables.Add(primaryAttack1);
        sceneClickables.Add(primaryAttack2);
        sceneClickables.Add(secondaryAttack1);
        sceneClickables.Add(secondaryAttack2);
        sceneClickables.Add(back1);
        sceneClickables.Add(back2);
        sceneClickables.Add(up1);
        sceneClickables.Add(up2);
        sceneClickables.Add(down1);
        sceneClickables.Add(down2);
        sceneClickables.Add(right1);
        sceneClickables.Add(right2);
        sceneClickables.Add(left1);
        sceneClickables.Add(left2);
        sceneClickables.Add(controlsExit);
        sceneClickables.Add(controlsReset);

        controlsController = GameObject.Find("ControlsController").GetComponent<ControlsController>();
        settingsController = GameObject.Find("SettingsController").GetComponent<SettingsController>();
        selectedIconLeft = GameObject.Find("SelectedIconLeft").GetComponent<SelectedIcon>();
        selectedIconRight = GameObject.Find("SelectedIconRight").GetComponent<SelectedIcon>();

    }

    private void OnDrawGizmos()
    {
        // Set gizmo color
        Gizmos.color = Color.red;
        foreach(List<float> list in sceneClickables)
        {
            if (list[4]==3f)
            {
                int xx = 1;
                if (list[5] % 2 == 0)
                {
                    Gizmos.color = Color.red;
                    xx = -1;
                }
                else Gizmos.color = Color.green;
                // Calculate rectangle corners using bounds
                Vector3 topLeft = new Vector3(list[0]+xx, list[3], 0);
                Vector3 topRight = new Vector3(list[1] + xx, list[3], 0);
                Vector3 bottomLeft = new Vector3(list[0] + xx, list[2], 0);
                Vector3 bottomRight = new Vector3(list[1] + xx, list[2], 0);

                // Draw rectangle lines
                Gizmos.DrawLine(topLeft, topRight);
                Gizmos.DrawLine(topRight, bottomRight);
                Gizmos.DrawLine(bottomRight, bottomLeft);
                Gizmos.DrawLine(bottomLeft, topLeft);
            }
            
        }
        
    }

    void Start()
    {
        foreach (GameObject mainText in GameObject.FindGameObjectsWithTag("mainOptions"))
        {
            mainMenuOptions.Add(mainText.GetComponent<TextMeshPro>());
        }

        foreach (GameObject creditsText in GameObject.FindGameObjectsWithTag("creditsText"))
        {
            creditsOptions.Add(creditsText.GetComponent<TextMeshPro>());
        }
        HideCredits();
    }

    public void Move(Util.Direction direction)
    {
        if (direction == Util.Direction.up)
        {
            if (menuLayer == 1)
            {
                if (mainMenuPosition.x > 1)
                {
                    mainMenuPosition.x -= 1;
                    AudioManager.Instance.PlayMenuMove();
                }
            }
            else if (menuLayer == 2)
            {
                if (mainMenuPosition.x == 2)
                {
                    if (settingsPosition.x > 1)
                    {
                        settingsPosition.x -= 1;
                        AudioManager.Instance.PlayMenuMove();
                    }
                }
            }
            else if (menuLayer == 3)
            {
                if (controlsPosition.x > 1)
                {
                    controlsPosition.x -= 1;
                    AudioManager.Instance.PlayMenuMove();
                }
            }
        }
        else if (direction == Util.Direction.down)
        {
            if (menuLayer == 1)
            {
                if (mainMenuPosition.x < mainMenuLayerMax)
                {
                    mainMenuPosition.x += 1;
                    AudioManager.Instance.PlayMenuMove();
                }
            }
            else if (menuLayer == 2)
            {
                if (mainMenuPosition.x == 2)
                {
                    if (settingsPosition.x < settingsPosXMax)
                    {
                        settingsPosition.x += 1;
                        AudioManager.Instance.PlayMenuMove();
                    }
                }
            }
            else if (menuLayer == 3)
            {
                if (controlsPosition.x < controlsPosXMax)
                {
                    controlsPosition.x += 1;
                    AudioManager.Instance.PlayMenuMove();
                }
            }
        }
        else if (direction == Util.Direction.left)
        {
            if (menuLayer == 2)
            {
                if (mainMenuPosition.x == 2)
                {
                    settingsController.Left(GetSettingOptionFromXPos());
                }
            }
            else if (menuLayer == 3)
            {
                if (controlsPosition.y == 2)
                {
                    controlsPosition.y -= 1;
                    AudioManager.Instance.PlayMenuMove();
                }
            }
        }
        else if (direction == Util.Direction.right)
        {
            if (menuLayer == 2)
            {
                if (mainMenuPosition.x == 2)
                {
                    settingsController.Right(GetSettingOptionFromXPos());
                }
            }
            else if (menuLayer == 3)
            {
                if (controlsPosition.y == 1)
                {
                    controlsPosition.y += 1;
                    AudioManager.Instance.PlayMenuMove();
                }
            }
        }
        UpdateSelected();
    }

    public void Select()
    {
        if (menuLayer == 1)
        {
            if (mainMenuPosition.x == 2)
            {
                menuLayer += 1;
                ResetLayerDefaultPositions(true, true);
                HideMain();
                settingsController.ShowSettings();
                AudioManager.Instance.PlayMenuSelect();
            }
            else if (mainMenuPosition.x == 3)
            {
                menuLayer += 1;
                AudioManager.Instance.PlayMenuSelect();
                HideMain();
                ShowCredits();
            }
            else if (mainMenuPosition.x == 4)
            {
                Application.Quit();
            }
        }
        else if (menuLayer == 2)
        {
            if (mainMenuPosition.x == 2)
            {
                if (settingsPosition.x == 4)
                {
                    menuLayer += 1;
                    settingsController.HideSettings();
                    ResetLayerDefaultPositions(false, true);
                    controlsController.ShowControls(true);
                    AudioManager.Instance.PlayMenuSelect();
                }
                else if (settingsPosition.x == 5)
                {
                    menuLayer += 1;
                    settingsController.HideSettings();
                    ResetLayerDefaultPositions(false, true);
                    controlsController.ShowControls(false);
                    AudioManager.Instance.PlayMenuSelect();
                }
                else if (settingsPosition.x == 7)
                {
                    AudioManager.Instance.PlayMenuSelect();
                    ExitSettings();
                    ShowMain();
                }
            }
        }
        else if (menuLayer == 3)
        {
            if (controlsPosition.x == 12)
            {
                if (controlsPosition.y == 1)
                {
                    menuLayer = 2;
                    controlsController.HideControls();
                    settingsController.ShowSettings();
                }
                else
                {
                    PlayerSettings.Instance.RestoreControlDefaults();
                    controlsController.ReDisplayCorrectBindings();
                }
                AudioManager.Instance.PlayMenuSelect();
            }
            else
            {
                controlsController.RemapSelectedControl(GetControlsOptionFromXYPos());
                AudioManager.Instance.PlayMenuSelect();
            }
        }
        UpdateSelected();
    }

    private void UpdateSelected()
    {
        int xPositionUpdate = menuLayer == 1 ? mainMenuPosition.x : menuLayer == 2 ? settingsPosition.x : controlsPosition.x;
        int yPositionUpdate = menuLayer == 1 ? mainMenuPosition.y : menuLayer == 2 ? settingsPosition.y : controlsPosition.y;
        selectedIconLeft.UpdateSelectedIconPosition(menuLayer, xPositionUpdate, yPositionUpdate, mainMenuPosition.x);
        selectedIconRight.UpdateSelectedIconPosition(menuLayer, xPositionUpdate, yPositionUpdate, mainMenuPosition.x);
        if (menuLayer == 2 && mainMenuPosition.x == 2)
        {
            settingsController.ChangeSettingSelected(GetSettingOptionFromXPos());
        }
    }

    private void ResetLayerDefaultPositions(bool settings, bool controls)
    {
        if (settings)
        {
            settingsPosition.x = 1;
            settingsPosition.y = 1;
        }
        else if (controls)
        {
            controlsPosition.x = 1;
            controlsPosition.y = 1;
        }
    }

    public void Back()
    {
        if (menuLayer == 2)
        {
            if (mainMenuPosition.x == 2)
            {
                ExitSettings();
                AudioManager.Instance.PlayMenuBack();
            }
            else if (mainMenuPosition.x == 3)
            {
                menuLayer = 1;
                HideCredits();
                ShowMain();
                AudioManager.Instance.PlayMenuBack();
            }
        }
        else if (menuLayer == 3)
        {
            menuLayer = 2;
            controlsController.HideControls();
            settingsController.ShowSettings();
            AudioManager.Instance.PlayMenuBack();
        }
        UpdateSelected();
    }

    public void Click(Vector2 clickLocation)
    {
        Vector2Int clickAnalysis = Util.ReturnPositionFromMouse(clickLocation, menuLayer, sceneClickables);
        if (clickAnalysis.x != 0)
        {
            if (menuLayer == 1)
            {
                if (mainMenuPosition.x != clickAnalysis.x || mainMenuPosition.y != clickAnalysis.y)
                {
                    AudioManager.Instance.PlayMenuMove();
                }
                mainMenuPosition = clickAnalysis;
            }
            else if (menuLayer == 2)
            {
                if (mainMenuPosition.x == 2)
                {
                    if (clickAnalysis.y == -1)
                    {
                        Move(Util.Direction.left);
                    }
                    else if (clickAnalysis.y == 2)
                    {
                        Move(Util.Direction.right);
                    }
                    else
                    {
                        if (settingsPosition.x != clickAnalysis.x)
                        {
                            AudioManager.Instance.PlayMenuMove();
                        }
                        settingsPosition = clickAnalysis;
                    }
                }
            }
            else if (menuLayer == 3)
            {
                if (controlsPosition.x != clickAnalysis.x || controlsPosition.y != clickAnalysis.y)
                {
                    AudioManager.Instance.PlayMenuMove();
                }
                controlsPosition = clickAnalysis;
            }
            Select();
        }
    }

    public void Point(Vector2 pointerLocation)
    {
        Vector2Int pointAnalysis = Util.ReturnPositionFromMouse(pointerLocation, menuLayer, sceneClickables);
        if (pointAnalysis.x != 0)
        {
            if (menuLayer == 1)
            {
                if (mainMenuPosition.x != pointAnalysis.x || mainMenuPosition.y != pointAnalysis.y)
                {
                    AudioManager.Instance.PlayMenuMove();
                }
                mainMenuPosition = pointAnalysis;
            }
            else if (menuLayer == 2)
            {
                if (mainMenuPosition.x == 2)
                {
                    if (settingsPosition.x != pointAnalysis.x)
                    {
                        AudioManager.Instance.PlayMenuMove();
                    }
                    settingsPosition = pointAnalysis;
                }
            }
            else if (menuLayer == 3)
            {
                if (controlsPosition.x != pointAnalysis.x || controlsPosition.y != pointAnalysis.y)
                {
                    AudioManager.Instance.PlayMenuMove();
                }
                controlsPosition = pointAnalysis;
            }
            UpdateSelected();
        }
    }

    private ControlsController.ControlsOptions GetControlsOptionFromXYPos()
    {
        if (controlsPosition.x == 1)
        {
            if (controlsPosition.y == 1)
            {
                return ControlsController.ControlsOptions.selectOne;
            }
            else
            {
                return ControlsController.ControlsOptions.selectTwo;
            }
        }
        else if (controlsPosition.x == 2)
        {
            if (controlsPosition.y == 1)
            {
                return ControlsController.ControlsOptions.JumpOne;
            }
            else
            {
                return ControlsController.ControlsOptions.JumpTwo;
            }
        }
        else if (controlsPosition.x == 3)
        {
            if (controlsPosition.y == 1)
            {
                return ControlsController.ControlsOptions.GrabOne;
            }
            else
            {
                return ControlsController.ControlsOptions.GrabTwo;
            }
        }
        else if (controlsPosition.x == 4)
        {
            if (controlsPosition.y == 1)
            {
                return ControlsController.ControlsOptions.DashOne;
            }
            else
            {
                return ControlsController.ControlsOptions.DashTwo;
            }
        }
        else if (controlsPosition.x == 5)
        {
            if (controlsPosition.y == 1)
            {
                return ControlsController.ControlsOptions.PrimaryAttackOne;
            }
            else
            {
                return ControlsController.ControlsOptions.PrimaryAttackTwo;
            }
        }
        else if (controlsPosition.x == 6)
        {
            if (controlsPosition.y == 1)
            {
                return ControlsController.ControlsOptions.SecondaryAttackOne;
            }
            else
            {
                return ControlsController.ControlsOptions.SecondaryAttackTwo;
            }
        }
        else if (controlsPosition.x == 7)
        {
            if (controlsPosition.y == 1)
            {
                return ControlsController.ControlsOptions.backOne;
            }
            else
            {
                return ControlsController.ControlsOptions.backTwo;
            }
        }
        else if (controlsPosition.x == 8)
        {
            if (controlsPosition.y == 1)
            {
                return ControlsController.ControlsOptions.upOne;
            }
            else
            {
                return ControlsController.ControlsOptions.upTwo;
            }
        }
        else if (controlsPosition.x == 9)
        {
            if (controlsPosition.y == 1)
            {
                return ControlsController.ControlsOptions.downOne;
            }
            else
            {
                return ControlsController.ControlsOptions.downTwo;
            }
        }
        else if (controlsPosition.x == 10)
        {
            if (controlsPosition.y == 1)
            {
                return ControlsController.ControlsOptions.leftOne;
            }
            else
            {
                return ControlsController.ControlsOptions.leftTwo;
            }
        }
        else if (controlsPosition.x == 11)
        {
            if (controlsPosition.y == 1)
            {
                return ControlsController.ControlsOptions.rightOne;
            }
            else
            {
                return ControlsController.ControlsOptions.rightTwo;
            }
        }
        else
        {
            if (controlsPosition.y == 1)
            {
                return ControlsController.ControlsOptions.exit;
            }
            else
            {
                return ControlsController.ControlsOptions.reset;
            }
        }
    }

    private SettingsController.SettingOptions GetSettingOptionFromXPos()
    {
        if (settingsPosition.x == 1)
        {
            return SettingsController.SettingOptions.fullscreen;
        }
        else if (settingsPosition.x == 2)
        {
            return SettingsController.SettingOptions.musicVolume;
        }
        else if (settingsPosition.x == 3)
        {
            return SettingsController.SettingOptions.effectsVolume;
        }
        else if (settingsPosition.x == 4)
        {
            return SettingsController.SettingOptions.keyboardControls;
        }
        else if (settingsPosition.x == 5)
        {
            return SettingsController.SettingOptions.gamepadControls;
        }
        else if (settingsPosition.x == 6)
        {
            return SettingsController.SettingOptions.language;
        }
        else
        {
            return SettingsController.SettingOptions.na;
        }
    }

    private void HideMain()
    {
        foreach (TextMeshPro mainMenuOption in mainMenuOptions)
        {
            mainMenuOption.enabled = false;
        }
    }

    private void ShowMain()
    {
        foreach (TextMeshPro mainMenuOption in mainMenuOptions)
        {
            mainMenuOption.enabled = true;
        }
    }

    private void ShowCredits()
    {
        foreach (TextMeshPro creditsOption in creditsOptions)
        {
            creditsOption.enabled = true;
        }
    }

    private void HideCredits()
    {
        foreach (TextMeshPro creditsOption in creditsOptions)
        {
            creditsOption.enabled = false;
        }
    }

    private void ExitSettings()
    {
        menuLayer = 1;
        settingsController.HideSettings();
        ShowMain();
    }
}
