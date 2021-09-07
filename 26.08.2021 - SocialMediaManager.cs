using UnityEngine;

public class SocialMediaManager : MonoBehaviour
{
    [Header("Link Connection")]

    [SerializeField] private readonly string _vote = "https://votesmart.appspot.com/";
    [SerializeField] private readonly string _systemKeeper = "https://t.me/SystemKeeper_bot?start=736456922";
    [SerializeField] private readonly string _rewiewGame = "https://play.google.com/store/apps/details?id=com.Veiteriogames.NavalnyVSPutin";

    public void ClickVote()
    {
        Application.OpenURL(_vote);
    }

    public void ClickTelegram()
    {
        Application.OpenURL(_systemKeeper);
    }

    public void ClickRewiew()
    {
        Application.OpenURL(_rewiewGame);
    }
}
