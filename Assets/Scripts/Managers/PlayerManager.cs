using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    [SerializeField] Image fade;
    [SerializeField] MusicTriggerController musicTriggerController;
    float fallingCounter = 0f;
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
            musicTriggerController.RestartTriggers();
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

        StartCoroutine(DelayEOG());
    }

    IEnumerator FlashRed()
    {
        yield return new WaitForSeconds(0.2f);
        gameObject.GetComponent<SpriteRenderer>().color = Color.white;

    }

    IEnumerator DelayEOG(){
        yield return new WaitForSeconds(backgroundScrollSpeed);
        SceneManager.LoadScene((int)Scene.MainMenu);
        
    }

    void Update()
    {
        if (falling)
        {
            fallingCounter += Time.deltaTime;
            float t = Mathf.Clamp01(fallingCounter / backgroundScrollSpeed);
            fade.color = new Color(0, 0, 0, t);

            // Move the background to create the falling illusion
            Vector3 backgroundPosition = foreverBackground.transform.position;
            backgroundPosition.y += backgroundScrollSpeed * Time.deltaTime;
            foreverBackground.transform.position = backgroundPosition;
        }
    }
}
