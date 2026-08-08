using UnityEngine;
using UnityEngine.SceneManagement;

[DefaultExecutionOrder(-10)]
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [SerializeField] private SoundLibrarySO librarySO;

    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioSource musicSource;

    [SerializeField] private AudioSource heartbeatSource;
    [SerializeField] private AudioClip heartbeatClip;

    [SerializeField] private float sfxVolume = 0.8f;
    [SerializeField] private Vector2 pitchRange = new Vector2(0.95f, 1.05f);

    public float SFXVolume => sfxVolume;
    public float MusicVolume => musicSource.volume;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded += SceneManager_sceneLoaded;
    }

    private void Start()
    {
        SetSFXVolume(SaveManager.Instance.GetSFXVolume());
        SetMusicVolume(SaveManager.Instance.GetMusicVolume());
    }

    private void SceneManager_sceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "GameScene")
        {
            SubscribeToGameEvents();
        }
    }

    private void SubscribeToGameEvents()
    {
        CraftingTable.Instance.OnCraftStarted += CraftingTable_OnCraftStarted;

        ScoreManager.Instance.OnScoreChanged += ScoreManager_OnScoreChanged;
        ScoreManager.Instance.OnMilestoneAchived += ScoreManager_OnMilestoneAchived;

        DeliveryManager.Instance.OnCorrectDelivery += DeliveryManager_OnCorrectDelivery;

        Player.Instance.health.OnHealthChanged += Player_OnHealthChanged;
        PlayerInventory.Instance.OnItemAdded += PlayerInventory_OnItemAdded;
    }

    private void PlayerInventory_OnItemAdded()
    {
        PlaySound(librarySO.takeItem);
    }

    private void ScoreManager_OnMilestoneAchived()
    {
        PlaySound(librarySO.bigScore);
    }

    private void DeliveryManager_OnCorrectDelivery(Monster obj)
    {
        PlaySound(librarySO.heal);
    }

    private void Player_OnHealthChanged(int health)
    {
        if (health == 1)
        {
            StartHeartbeat();
        }
        else if (health > 1)
        {
            StopHeartbeat();
        }
        else if (health <= 0)
        {
            StopHeartbeat();
        }

        if (health > 0)
        {
            PlaySound(librarySO.hurt);
        }
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

    public void StartHeartbeat()
    {
        if (heartbeatSource.isPlaying)
            return;

        heartbeatSource.clip = heartbeatClip;
        heartbeatSource.loop = true;
        heartbeatSource.Play();
    }

    public void StopHeartbeat()
    {
        if (!heartbeatSource.isPlaying)
            return;

        heartbeatSource.Stop();
    }

    public void SetSFXVolume(float volume)
    {
        sfxVolume = Mathf.Clamp01(volume);
        heartbeatSource.volume = sfxVolume;

    }

    public void SetMusicVolume(float volume)
    {
        musicSource.volume = Mathf.Clamp01(volume);
    }

    private void OnDestroy()
    {
        if (Instance != this)
            return;

        SceneManager.sceneLoaded -= SceneManager_sceneLoaded;
    }
}