using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class VoicelineController : MonoBehaviour
{
    [Header("Main Components")]

    private static GameObject _gameSettings;

    [Header("Sound Controller")]

    [SerializeField] private AudioSource _audioController;

    [Header("Audio Collection")]

    [SerializeField] private AudioClip[] _voicePack;

    private void Awake()
    {
        _audioController.TryGetComponent(out AudioSource _audioSource);
        _gameSettings = GameObject.FindWithTag("GameSettings");
        _gameSettings.TryGetComponent(out AudioSource musicController);
        musicController.volume = PlayerPrefs.GetFloat("MusicValue");
    }

    public void UltimateEnableSound()
    {
        StartCoroutine(PlayVoiceline(15f));
    }

    public void EnableCharacterSound()
    {
        StartCoroutine(PlaySoundAudio(20f));
    }

    private IEnumerator PlayVoiceline(float seconds)
    {
        _audioController.TryGetComponent(out AudioSource audioSource);
        _gameSettings.TryGetComponent(out AllGameSettings allGameSettings);
        allGameSettings.MusicController.TryGetComponent(out AudioSource musicSource);
        audioSource.PlayOneShot(_voicePack[Random.Range(0, _voicePack.Length)]);
        audioSource.Play();

        _audioController.volume = 1.0f;
        allGameSettings.MusicController.volume = 0.5f;

        if (!_audioController.isPlaying)
        {
            allGameSettings.MusicController.volume = PlayerPrefs.GetFloat("MusicValue");
        }

        yield return new WaitForSeconds(seconds);

        allGameSettings.MusicController.volume = PlayerPrefs.GetFloat("MusicValue");
    }

    private IEnumerator PlaySoundAudio(float seconds)
    {
        _gameSettings.TryGetComponent(out AudioSource musicController);
        _audioController.TryGetComponent(out AudioSource audioSource);
        audioSource.PlayOneShot(_voicePack[Random.Range(0, _voicePack.Length)]);
        audioSource.Play();

        _audioController.volume = 1.0f;
        musicController.volume = 0.35f;

        if (!_audioController.isPlaying)
        {
            musicController.volume = 1.0f;
        }

        yield return new WaitForSeconds(seconds);

        musicController.volume = 1.0f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(PlayVoiceline(15f));
        }
    }
}
