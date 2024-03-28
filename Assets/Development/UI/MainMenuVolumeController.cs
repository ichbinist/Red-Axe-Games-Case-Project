using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuVolumeController : MonoBehaviour
{
    public VolumeType VolumeType;
    public Button DecreaseButton, IncreaseButton;
    public MainMenuStylizedFloatArea MainMenuStylizedFloatArea;
    public TextMeshProUGUI ValueText;

    private void OnEnable()
    {
        MainMenuStylizedFloatArea.OnPointerEnterAction += SetValue;
        IncreaseButton.onClick.AddListener(IncreaseValue);
        DecreaseButton.onClick.AddListener(DecreaseValue);
    }

    private void OnDisable()
    {
        if(MainMenuStylizedFloatArea != null)
            MainMenuStylizedFloatArea.OnPointerEnterAction -= SetValue;

        IncreaseButton.onClick.RemoveListener(IncreaseValue);
        DecreaseButton.onClick.RemoveListener(DecreaseValue);
    }

    private void SetValue()
    {
        switch (VolumeType)
        {
            case VolumeType.Main:
                ValueText.SetText((AudioVolumeManager.Instance.MainVolume * 100f).ToString("F0"));
                break;
            case VolumeType.Music:
                ValueText.SetText((AudioVolumeManager.Instance.MusicVolume * 100f).ToString("F0"));
                break;
            default:
                break;
        }
    }

    private void IncreaseValue()
    {
        switch (VolumeType)
        {
            case VolumeType.Main:
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    AudioVolumeManager.Instance.MainVolume = Mathf.Clamp01(AudioVolumeManager.Instance.MainVolume + 0.1f);
                }
                else
                {
                    AudioVolumeManager.Instance.MainVolume = Mathf.Clamp01(AudioVolumeManager.Instance.MainVolume + 0.01f);
                }
                ValueText.SetText((AudioVolumeManager.Instance.MainVolume * 100f).ToString("F0"));
                break;
            case VolumeType.Music:
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    AudioVolumeManager.Instance.MusicVolume = Mathf.Clamp01(AudioVolumeManager.Instance.MusicVolume + 0.1f);
                }
                else
                {
                    AudioVolumeManager.Instance.MusicVolume = Mathf.Clamp01(AudioVolumeManager.Instance.MusicVolume + 0.01f);
                }
                ValueText.SetText((AudioVolumeManager.Instance.MusicVolume * 100f).ToString("F0"));
                break;
            default:
                break;
        }
    }

    private void DecreaseValue()
    {
        switch (VolumeType)
        {
            case VolumeType.Main:
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    AudioVolumeManager.Instance.MainVolume = Mathf.Clamp01(AudioVolumeManager.Instance.MainVolume - 0.1f);
                }
                else
                {
                    AudioVolumeManager.Instance.MainVolume = Mathf.Clamp01(AudioVolumeManager.Instance.MainVolume - 0.01f);
                }
                ValueText.SetText((AudioVolumeManager.Instance.MainVolume * 100f).ToString("F0"));
                break;
            case VolumeType.Music:
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    AudioVolumeManager.Instance.MusicVolume = Mathf.Clamp01(AudioVolumeManager.Instance.MusicVolume - 0.1f);
                }
                else
                {
                    AudioVolumeManager.Instance.MusicVolume = Mathf.Clamp01(AudioVolumeManager.Instance.MusicVolume - 0.01f);
                }
                ValueText.SetText((AudioVolumeManager.Instance.MusicVolume * 100f).ToString("F0"));
                break;
            default:
                break;
        }
    }
}

public enum VolumeType
{
    Main,
    Music
}