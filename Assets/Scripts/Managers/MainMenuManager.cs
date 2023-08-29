using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    public void StartGameButton(Canvas c)
    {
        StartCoroutine(GameManager.instance.LoadScene_Fade(Scene.Game, c));
    }
}
