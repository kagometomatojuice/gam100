using UnityEngine;
using UnityEngine.UIElements;

public class BossHookMove : MonoBehaviour
{
    public float minZ = -50f, maxZ = 50f;
    private bool isRotating, moveRight;
    public float rotateSpeed;

    private float rotateAngle;

    public float moveSpeed;
    private float initialMoveSpeed;
    public float minY = -2.5f;
    private float initialY;

    private bool moveDown;

    private RopeRenderer ropeRenderer;

    public GameObject hookedObject;

    private bool isMinigameActive = false;
    private Vector3 hookedOffset;
    
    public BossMinigame fishingMinigameScript;
    public BossHealthBar bhbScript;
    public LevelManager lmScript;

    void Awake()
    {
        ropeRenderer = GetComponent<RopeRenderer>();
    }

    void Start()
    {
        initialY = transform.position.y;
        initialMoveSpeed = moveSpeed;
        isRotating = true;
    }

    void Update()
    {
        if (isMinigameActive) 
            return;

        Rotate();
        GetInput();
        MoveRope();
    }

    void Rotate()
    {
        
        if (!isRotating) 
            return;

        if (moveRight)
        {
            rotateAngle += rotateSpeed * Time.deltaTime;
        }
        else
        {
            rotateAngle -= rotateSpeed * Time.deltaTime;
        }

        transform.rotation = Quaternion.AngleAxis(rotateAngle, Vector3.forward);

        if (rotateAngle > maxZ)
        {
            moveRight = false;
        }
        else if (rotateAngle < minZ)
        {
            moveRight = true;
        }
    }

    void GetInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isRotating)
            {
                isRotating = false;
                moveDown = true;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
    if (moveDown)
    {
        if (other.CompareTag("boss"))
        {
            HookBoss(other);
        }
        else if (other.CompareTag("trash")) // Check if the hooked object is trash
        {
            HookTrash(other);
        }
    }
    }

    void HookBoss(Collider2D other)
    {
        hookedObject = other.gameObject;
        CircleCollider2D circleCollider = other.GetComponent<CircleCollider2D>();

        if (circleCollider != null)
        {
            Vector2 bossCenter = circleCollider.bounds.center;

            if (Vector2.Distance(transform.position, bossCenter) <= 3f) // Threshold for hooking
            {
                HumanBehaviour humanBehaviour = hookedObject.GetComponent<HumanBehaviour>();
                if (humanBehaviour != null)
                {
                    humanBehaviour.StopMovement();
                }

                if (bhbScript.healthMax > 50f && !isMinigameActive)
                {
                    bhbScript.takeDamage(10f);
                    moveSpeed = initialMoveSpeed;
                    moveDown = false;

                    if (hookedObject != null)
                    {
                        HumanBehaviour hookedHumanBehaviour = hookedObject.GetComponent<HumanBehaviour>();
                        if (hookedHumanBehaviour != null)
                        {
                            hookedHumanBehaviour.RestartMovement();
                        }
                        hookedObject = null;
                    }
                }
                else if (bhbScript.healthMax <= 50f && !isMinigameActive)
                {
                    isMinigameActive = true;
                    moveSpeed = 0;

                    if (fishingMinigameScript != null)
                    {
                        fishingMinigameScript.hmScript = this;
                        fishingMinigameScript.RestartMinigame();
                    }
                }

                if (hookedObject != null)
                    hookedOffset = hookedObject.transform.position - transform.position;
                
                moveDown = false;
            }
        }
    }

    void HookTrash(Collider2D other)
    {
        hookedObject = other.gameObject;
        moveSpeed = 1; 
        hookedOffset = hookedObject.transform.position - transform.position;
        moveDown = false;
    }

    void MoveRope()
    {
        if (isRotating || isMinigameActive)
            return;

        Vector3 temp = transform.position;

        if (moveDown)
        {
            temp -= transform.up * (Time.deltaTime * moveSpeed);
        }
        else
        {
            temp += transform.up * (Time.deltaTime * moveSpeed);

            if (hookedObject != null)
            {
                hookedObject.transform.position = temp + hookedOffset;
            }
        }

        transform.position = temp;

        if (temp.y <= minY)
        {
            moveDown = false;
        }

        if (temp.y >= initialY)
        {
            isRotating = true;
            ropeRenderer.RenderLine(temp, false);
            
            // Restore normal speed when reaching the top
            moveSpeed = initialMoveSpeed;

            if (hookedObject != null)
            {
                if (hookedObject.CompareTag("trash")) // Destroy trash when returned
                {
                    Destroy(hookedObject);
                }
                else
                {
                    hookedObject.SetActive(false);
                    lmScript.ChangeSceneOnWin();
                }

                hookedObject = null;
            }
        }

        ropeRenderer.RenderLine(temp, true);
    }
    
// ReSharper disable Unity.PerformanceAnalysis
    public void OnMinigameComplete(bool success)
    {
        isMinigameActive = false;

        if (success)
        {
            moveSpeed = initialMoveSpeed;
        }
        else
        {
            moveSpeed = initialMoveSpeed;
            if (hookedObject != null)
            {
                HumanBehaviour humanBehaviour = hookedObject.GetComponent<HumanBehaviour>();
                if (humanBehaviour != null)
                {
                    humanBehaviour.RestartMovement();
                }
                hookedObject = null;
            }
        }
        moveDown = false;
    }
    
    public bool IsMinigameActive()
    {
        return isMinigameActive;
    }
}
