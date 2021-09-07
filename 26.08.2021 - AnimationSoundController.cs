using UnityEngine;

public class AnimationSoundController : MonoBehaviour
{
    [Header("Audio Controller")]

    [SerializeField] private AudioSource _audioPack;

    [Header("Audio Collection")]

    [SerializeField] private AudioClip[] _putJump;
    [SerializeField] private AudioClip[] _navJump;
    [SerializeField] private AudioClip[] _putCrouch;
    [SerializeField] private AudioClip[] _navCrouch;

    public void PlayNavJump()
    {
        _audioPack.GetComponent<AudioSource>().clip = _navJump[Random.Range(0, _navJump.Length)];
        _audioPack.GetComponent<AudioSource>().Play();
    }

    public void PlayNavCrouch()
    {
        _audioPack.GetComponent<AudioSource>().clip = _navCrouch[Random.Range(0, _navCrouch.Length)];
        _audioPack.GetComponent<AudioSource>().Play();
    }

    public void PlayPutJump()
    {
        _audioPack.GetComponent<AudioSource>().clip = _putJump[Random.Range(0, _putJump.Length)];
        _audioPack.GetComponent<AudioSource>().Play();
    }
    public void PlayPutCrouch()
    {
        _audioPack.GetComponent<AudioSource>().clip = _putCrouch[Random.Range(0, _putCrouch.Length)];
        _audioPack.GetComponent<AudioSource>().Play();
    }
}
