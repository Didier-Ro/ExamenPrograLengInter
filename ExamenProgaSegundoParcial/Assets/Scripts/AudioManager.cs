using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    private AudioSource _audioSource = default;
    [SerializeField] private AudioClip[] _audioClips = default;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();

        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void AudioSelect(int index, float volume)
    {
        _audioSource.PlayOneShot(_audioClips[index], volume);
    }
}
