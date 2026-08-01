using System;
using UnityEngine;
using UnityEngine.UI;

[DefaultExecutionOrder(-10)]
public class OptionsUI : MonoBehaviour
{
    public static OptionsUI Instance { get; private set; }

    [Header("Buttons")]
    [SerializeField] private Button backButton;

    [Header("Music")]
    [SerializeField] private Button musicLeftButton;
    [SerializeField] private Button musicRightButton;
    [SerializeField] private Image musicBar;

    [Header("SFX")]
    [SerializeField] private Button sfxLeftButton;
    [SerializeField] private Button sfxRightButton;
    [SerializeField] private Image sfxBar;

    private const float VolumeStep = 0.05f;

    private Action onClose;

    private void Awake()
    {
        if (Instance == null) Instance = this;

        Hide();
        backButton.onClick.AddListener(Hide);

        musicLeftButton.onClick.AddListener(MusicDown);
        musicRightButton.onClick.AddListener(MusicUp);

        sfxLeftButton.onClick.AddListener(SFXDown);
        sfxRightButton.onClick.AddListener(SFXUp);
    }

    private void Start()
    {
        UpdateBars();
    }

    public void Show(Action onClose)
    {
        this.onClose = onClose;

        UpdateBars();

        gameObject.SetActive(true);

        backButton.Select();
    }

    public void Hide()
    {
        gameObject.SetActive(false);

        onClose?.Invoke();
        onClose = null;
    }

    private void MusicUp()
    {
        float volume = Mathf.Min(1f, AudioManager.Instance.MusicVolume + VolumeStep);

        AudioManager.Instance.SetMusicVolume(volume);
        SaveManager.Instance.SetMusicVolume(volume);

        UpdateBars();
    }

    private void MusicDown()
    {
        float volume = Mathf.Max(0f, AudioManager.Instance.MusicVolume - VolumeStep);

        AudioManager.Instance.SetMusicVolume(volume);
        SaveManager.Instance.SetMusicVolume(volume);

        UpdateBars();
    }

    private void SFXUp()
    {
        float volume = Mathf.Min(1f, AudioManager.Instance.SFXVolume + VolumeStep);

        AudioManager.Instance.SetSFXVolume(volume);
        SaveManager.Instance.SetSFXVolume(volume);

        UpdateBars();
    }

    private void SFXDown()
    {
        float volume = Mathf.Max(0f, AudioManager.Instance.SFXVolume - VolumeStep);

        AudioManager.Instance.SetSFXVolume(volume);
        SaveManager.Instance.SetSFXVolume(volume);

        UpdateBars();
    }

    private void UpdateBars()
    {
        musicBar.fillAmount = AudioManager.Instance.MusicVolume;
        sfxBar.fillAmount = AudioManager.Instance.SFXVolume;
    }
}