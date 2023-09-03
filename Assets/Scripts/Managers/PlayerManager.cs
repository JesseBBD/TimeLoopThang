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
    bool falling = false;
    [SerializeField] GameObject foreverBackground;
    [SerializeField] float backgroundScrollSpeed = 5.0f;
    [SerializeField] GameObject[] lastObjects;

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
        gameObject.GetComponent<SpriteRenderer>().color = new Color(234f / 255f, 150f / 255f, 150f / 255f);
        StartCoroutine(FlashRed());
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
            if (playerMovement.GetIsGrounded())
            {
                animator.SetTrigger("dieFloor");
            }
            else
            {
                animator.SetTrigger("dieFly");
            }
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

    public void TriggerEndOfGame()
    {
        gameObject.GetComponent<SpriteRenderer>().color = new Color(183f / 255f, 92f / 255f, 92f / 255f);
        animator.SetTrigger("dieFlyForever");
        playerMovement.setIsDead(true);
        foreverBackground.SetActive(true);
        falling = true;

        foreach(GameObject g in lastObjects){
            g.SetActive(false);
        }
    }

    IEnumerator FlashRed()
    {
        yield return new WaitForSeconds(0.2f);
        gameObject.GetComponent<SpriteRenderer>().color = Color.white;

    }

    void Update()
    {
        if (falling)
        {
            // Move the background to create the falling illusion
            Vector3 backgroundPosition = foreverBackground.transform.position;
            backgroundPosition.y += backgroundScrollSpeed * Time.deltaTime;
            foreverBackground.transform.position = backgroundPosition;
        }
    }
}
