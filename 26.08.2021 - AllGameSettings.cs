using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal;

public class AllGameSettings : MonoBehaviour
{
    [Header("Graph Elements")]

    [SerializeField] private Dropdown _changeGraph;
    [SerializeField] private UniversalRenderPipelineAsset _renderGraphicContainer;

    [Header("Music Elements")]

    [SerializeField]
    private Slider _changeMusicVolime, _changeAudioVolime, _changeVfxVolime;

    [Header("Audio Controller")]

    [SerializeField] private AudioSource _musicController;
    public  AudioSource MusicController
    {
        get
        {
            return _musicController;
        }
    }

    [SerializeField] private AudioSource[] _audioController, _vfxController;

    [Header("Music Collection")]

    [SerializeField] private AudioClip[] _musicPack;

    private void Awake()
    {
        _changeGraph.GetComponent<Dropdown>().value = PlayerPrefs.GetInt("GraphicValue");
    }

    private void Start()
    {
        if (_changeGraph.GetComponent<Dropdown>().value == 0)
        {
            _renderGraphicContainer.shadowCascadeCount = 1;
            _renderGraphicContainer.shadowDistance = 0;
            _renderGraphicContainer.msaaSampleCount = 0;
            _renderGraphicContainer.supportsHDR = false;
            _renderGraphicContainer.useSRPBatcher = true;
            _renderGraphicContainer.supportsDynamicBatching = true;
            _renderGraphicContainer.supportsCameraOpaqueTexture = false;
            _renderGraphicContainer.supportsCameraDepthTexture = false;
        }

        else if (_changeGraph.GetComponent<Dropdown>().value == 1)
        {
            _renderGraphicContainer.shadowCascadeCount = 2;
            _renderGraphicContainer.shadowDistance = 125;
            _renderGraphicContainer.msaaSampleCount = 2;
            _renderGraphicContainer.supportsHDR = false;
            _renderGraphicContainer.useSRPBatcher = true;
            _renderGraphicContainer.supportsDynamicBatching = true;
            _renderGraphicContainer.supportsCameraOpaqueTexture = false;
            _renderGraphicContainer.supportsCameraDepthTexture = false;
        }

        else if (_changeGraph.GetComponent<Dropdown>().value == 2)
        {
            _renderGraphicContainer.shadowCascadeCount = 4;
            _renderGraphicContainer.shadowDistance = 175;
            _renderGraphicContainer.msaaSampleCount = 4;
            _renderGraphicContainer.supportsHDR = true;
            _renderGraphicContainer.useSRPBatcher = true;
            _renderGraphicContainer.supportsDynamicBatching = true;
            _renderGraphicContainer.supportsCameraOpaqueTexture = false;
            _renderGraphicContainer.supportsCameraDepthTexture = false;
        }
    }

    private void LateUpdate()
    {
        MusicPlayer();
    }

    public void GraphicSettings()
    {
        if (_changeGraph.GetComponent<Dropdown>().value == 0)
        {
            _renderGraphicContainer.shadowCascadeCount = 1;
            _renderGraphicContainer.shadowDistance = 0;
            _renderGraphicContainer.msaaSampleCount = 0;
            _renderGraphicContainer.supportsHDR = false;
            _renderGraphicContainer.useSRPBatcher = true;
            _renderGraphicContainer.supportsDynamicBatching = true;
            _renderGraphicContainer.supportsCameraOpaqueTexture = false;
            _renderGraphicContainer.supportsCameraDepthTexture = false;

            PlayerPrefs.SetInt("GraphicValue", _changeGraph.GetComponent<Dropdown>().value);
            PlayerPrefs.Save();
        }

        else if (_changeGraph.GetComponent<Dropdown>().value == 1)
        {
            _renderGraphicContainer.shadowCascadeCount = 2;
            _renderGraphicContainer.shadowDistance = 125;
            _renderGraphicContainer.msaaSampleCount = 2;
            _renderGraphicContainer.supportsHDR = false;
            _renderGraphicContainer.useSRPBatcher = true;
            _renderGraphicContainer.supportsDynamicBatching = true;
            _renderGraphicContainer.supportsCameraOpaqueTexture = false;
            _renderGraphicContainer.supportsCameraDepthTexture = false;

            PlayerPrefs.SetInt("GraphicValue", _changeGraph.GetComponent<Dropdown>().value);
            PlayerPrefs.Save();
        }

        else if (_changeGraph.GetComponent<Dropdown>().value == 2)
        {
            _renderGraphicContainer.shadowCascadeCount = 4;
            _renderGraphicContainer.shadowDistance = 175;
            _renderGraphicContainer.msaaSampleCount = 4;
            _renderGraphicContainer.supportsHDR = true;
            _renderGraphicContainer.useSRPBatcher = true;
            _renderGraphicContainer.supportsDynamicBatching = true;
            _renderGraphicContainer.supportsCameraOpaqueTexture = false;
            _renderGraphicContainer.supportsCameraDepthTexture = false;

            PlayerPrefs.SetInt("GraphicValue", _changeGraph.GetComponent<Dropdown>().value);
            PlayerPrefs.Save();
        }
    }

    public void MusicPack()
    {
        _musicController.volume = _changeMusicVolime.value;
    }

    public void AudioController()
    {
        for (int i = 0; i < _audioController.Length ; i++)
        {
            _audioController[i].volume = _changeAudioVolime.value;
        }
    }

    public void VfxController()
    {
        for (int i = 0; i < _vfxController.Length; i++)
        {
            _vfxController[i].volume = _changeVfxVolime.value;
        }
    }

    public void MusicPlayer()
    {
        if (!_musicController.isPlaying)
        {
            GetComponent<AudioSource>().clip = _musicPack[Random.Range(0, _musicPack.Length)];
            GetComponent<AudioSource>().Play();
        }
    }
}
