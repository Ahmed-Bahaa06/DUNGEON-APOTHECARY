using Unity.VisualScripting;
using UnityEngine;


[DefaultExecutionOrder(-10)]
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance {  get; private set; }

    [SerializeField] private SoundLibrarySO librarySO;

    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioSource musicSource;

    [SerializeField] private float sfxVolume = 0.8f;
    [SerializeField] private Vector2 pitchRange = new Vector2(0.95f, 1.05f);

    public float SFXVolume => sfxVolume;
    public float MusicVolume => musicSource.volume;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    private void Start()
    {
        SetSFXVolume(SaveManager.Instance.GetSFXVolume());
        SetMusicVolume(SaveManager.Instance.GetMusicVolume());

        CraftingTable.Instance.OnCraftStarted += CraftingTable_OnCraftStarted;
        ScoreManager.Instance.OnScoreChanged += ScoreManager_OnScoreChanged;
        ScoreManager.Instance.OnMilestoneAchived += ScoreManager_OnMilestoneAchived;
        DeliveryManager.Instance.OnCorrectDelivery += DeliveryManager_OnCorrectDelivery;
        Player.Instance.health.OnHealthChanged += Player_OnHealthChanged;
    }

    private void ScoreManager_OnMilestoneAchived()
    {
        PlaySound(librarySO.bigScore);
    }

    private void DeliveryManager_OnCorrectDelivery(Monster obj)
    {
        PlaySound(librarySO.heal);
    }
    private void Player_OnHealthChanged(int t)
    {
        PlaySound(librarySO.hurt);
    }

    private void ScoreManager_OnScoreChanged()
    {
        PlaySound(librarySO.smallScore);
    }

    private void CraftingTable_OnCraftStarted()
    {
        PlaySound(librarySO.craft);
    }

    public void PlaySound(SoundSO[] sounds)
    {
        SoundSO sound = sounds[Random.Range(0, sounds.Length)];

        float pitch = Random.Range(pitchRange.x, pitchRange.y);
        sfxSource.pitch = pitch;
        sfxSource.PlayOneShot(sound.clip, sfxVolume);
    }

    public void SetSFXVolume(float volume)
    {
        sfxVolume = Mathf.Clamp01(volume);
    }

    public void SetMusicVolume(float volume)
    {
        musicSource.volume = Mathf.Clamp01(volume);
    }
}
