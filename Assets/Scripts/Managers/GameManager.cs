using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Scene
{
    MainMenu,
    Game
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    float sceneTransitionTime = 1;
    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public IEnumerator LoadScene_Fade(Scene s, Canvas c)
    {
        c.GetComponent<Animator>().SetTrigger("start");
        Debug.Log("IT'S HAPPENING");
        yield return new WaitForSeconds(sceneTransitionTime);
        SceneManager.LoadScene((int)s);
    }
}
