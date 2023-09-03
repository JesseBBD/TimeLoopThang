using System.Collections;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] float playerAge;
    [SerializeField] const float playerMaxAge = 100;
    [SerializeField] Hourglass hourglass;
    [SerializeField] Canvas canvas;
    private Animator animator;
    [SerializeField] PlayerMovement playerMovement;

    void Awake()
    {
        playerAge = 0;
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(float damage)
    {
        playerAge += damage;
        if (playerAge >= playerMaxAge)
        {
            playerAge = 100;
            gameObject.GetComponent<SpriteRenderer>().color = new Color(183f / 255f, 92f / 255f, 92f / 255f);
            animator.SetTrigger("dieFloor");
            playerMovement.setIsDead(true);
            StartCoroutine(TriggerEndOfGame());
        }
        hourglass.addAge(damage, playerAge);
    }

    IEnumerator TriggerEndOfGame()
    {
        Debug.Log("TRIGGER END OF GAME");
        yield return new WaitForSeconds(3);
        GameManager.instance.LoadScene_Fade(Scene.MainMenu, canvas);
    }}
