using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class DestroyWithAudio : MonoBehaviour
{
    [SerializeField] private AudioClip[] _destructionAudio;

    private void Start()
    {
        gameObject.TryGetComponent(out AudioSource audioManager);
        audioManager.PlayOneShot(_destructionAudio[Random.Range(0, _destructionAudio.Length)]);
        audioManager.Play();
    }
}
