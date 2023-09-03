using System.Collections;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] float playerAge;
    public void SetPlayerAge(float p)
    {
        playerAge = p;
        CheckAge();
    }
    [SerializeField] const float playerMaxAge = 100;
    [SerializeField] Hourglass hourglass;
    [SerializeField] Canvas canvas;
    private Animator animator;
    [SerializeField] PlayerMovement playerMovement;
    Vector3 startPos = new Vector3(-8, 4, 0);

    void Awake()
    {
        Restart();
        animator = GetComponent<Animator>();
    }

    void Restart()
    {
        playerAge = 0;
    }

    public void TakeDamage(float damage)
    {
        playerAge += damage;
        hourglass.addAge(damage, playerAge);
        CheckAge();
    }

    void CheckAge()
    {
        if (playerAge >= playerMaxAge)
        {
            playerAge = 100;
            gameObject.GetComponent<SpriteRenderer>().color = new Color(183f / 255f, 92f / 255f, 92f / 255f);
            animator.SetTrigger("dieFloor");
            playerMovement.setIsDead(true);
            StartCoroutine(TriggerRestartGame());
        }
    }
    IEnumerator TriggerRestartGame()
    {
        yield return new WaitForSeconds(1);
        playerMovement.RestartAndLerpToTarget(startPos);
        hourglass.Restart();
        Restart();
    }
    IEnumerator TriggerEndOfGame()
    {
        yield return new WaitForSeconds(3);
        GameManager.instance.LoadScene_Fade(Scene.MainMenu, canvas);
    }
}
