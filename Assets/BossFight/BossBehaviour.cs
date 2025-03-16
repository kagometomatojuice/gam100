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
            
            float boundary = screenWidth / 2;
            float bufferZone = 1f; // Small zone to prevent rapid flipping

            if (moveRight && transform.position.x >= boundary + offScreenBuffer)
            {
                moveRight = false;
                transform.position = new Vector2(boundary + offScreenBuffer - bufferZone, transform.position.y); 
                SetRandomSpeed();
            }
            else if (!moveRight && transform.position.x <= -boundary - offScreenBuffer)
            {
                moveRight = true;
                transform.position = new Vector2(-boundary - offScreenBuffer + bufferZone, transform.position.y);
                SetRandomSpeed();
            }
        }
        // Set basic movement animation
        bossAnimator.SetBool("isMoving", isMoving);
        bossAnimator.SetBool("HasShield", HasShield);

        // Check boss health and trigger animations
        float currentHealth = healthBarScript.healthMax;

        if (currentHealth <= 82f)
        {
            bossAnimator.SetBool("isLightlyDamaged", true);
        }
        else
        {
            bossAnimator.SetBool("isLightlyDamaged", false);
        }

        if (currentHealth <= 62f)
        {
            bossAnimator.SetBool("isHeavilyDamaged", true);
        }
        else
        {
            bossAnimator.SetBool("isHeavilyDamaged", false);
        }

        if (currentHealth <= 52f)
        {
            HasShield = false;
            bossAnimator.SetBool("HasShield", false);
        }
    }

    private void SetRandomSpeed()
    {
        currentSpeed = Random.Range(minSpeed, maxSpeed);
    }

    public void StopMovement()
    {
        isMoving = false;
        bossAnimator.SetBool("isMoving", false);
        bossAnimator.SetBool("isHooked", true);
    }

    public void RestartMovement()
    {
        isMoving = true;
        bossAnimator.SetBool("isMoving", true);
        bossAnimator.SetBool("isHooked", false);
    }
}