using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenGalleryScene : MonoBehaviour
{
    [Header("Main Values")]
    
    private const int _galleryCount = 2;

    public void LoadSelectedScene()
    {
        SceneManager.LoadScene(_galleryCount);
    }
}
