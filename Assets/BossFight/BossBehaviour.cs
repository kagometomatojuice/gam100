using UnityEngine;

public class BossBehaviour : MonoBehaviour
{
    public float minSpeed = 2f; 
    public float maxSpeed = 8f; 
    private float currentSpeed;
    private bool moveRight = true; 
    private bool isMoving = true;
    private bool HasShield = true;

    public float changeSpeedInterval = 3f;
    private float speedChangeTimer;

    private float screenWidth;
    
    public float offScreenBuffer = 2f;
    public BossHookMove bhmScript;
    
    private Animator bossAnimator;
    public BossHealthBar healthBarScript;

    void Start()
    {
        SetRandomSpeed();
        //speedChangeTimer = changeSpeedInterval;
        screenWidth = Camera.main.orthographicSize * 2 * Screen.width / Screen.height;
        bossAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        if (bhmScript && bhmScript.IsMinigameActive())
        {
            isMoving = false;
            return;
        }
        else
        {
            isMoving = true;
        }
        if (isMoving)
        {
            if (moveRight)
            {
                transform.Translate(Vector2.right * (currentSpeed * Time.deltaTime));
                if (transform.localScale.x < 0)
                {
                    transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
                }
            }
            else
            {
                transform.Translate(Vector2.left * (currentSpeed * Time.deltaTime));
                if (transform.localScale.x > 0)
                {
                    transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
                }
            }
            
            if (transform.position.x >= screenWidth / 2 + offScreenBuffer || transform.position.x <= -screenWidth / 2 - offScreenBuffer)
            {
                moveRight = !moveRight;
                SetRandomSpeed(); 
            }
        }
        bossAnimator.SetBool("isMoving", isMoving);
        bossAnimator.SetBool("HasShield", HasShield);
    }

    private void SetRandomSpeed()
    {
        currentSpeed = Random.Range(minSpeed, maxSpeed);
    }

    public void StopMovement()
    {
        isMoving = false;
        bossAnimator.SetBool("HasShield", false);
        bossAnimator.SetBool("isMoving", false);
        bossAnimator.SetBool("isHooked", true);
    }

    public void RestartMovement()
    {
        isMoving = true;
    }
    
    public void TakeDamage(float damage)
    {
        if (healthBarScript == null) return;

        healthBarScript.takeDamage(damage);
        
        if (healthBarScript.healthMax <= 52f)
        {
            bossAnimator.SetBool("HasShield", false);
            //Debug.Log("Shield disabled: HasShield set to false");
        }
    }
}
