using UnityEngine;
using UnityEngine.UIElements;

public class HookMove : MonoBehaviour
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
    private bool minigameWasSuccessful = false;
    private Vector3 hookedOffset;
    
    public FishingMinigame fishingMinigameScript;
    public PointHUD phScript;

    [SerializeField] private AudioClip[] humanSound;
    [SerializeField] private AudioClip victorySFX;
    [SerializeField] private AudioClip failSFX;
    [SerializeField] private AudioClip hookSFX;
    
    private AudioSource source;
    private int lastPlayedIndex = -1;

    void Awake()
    {
        ropeRenderer = GetComponent<RopeRenderer>();
    }

    void Start()
    {
        initialY = transform.position.y;
        initialMoveSpeed = moveSpeed;
        isRotating = true;
        source = GetComponent<AudioSource>();
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
                
                if (hookSFX)
                {
                    source.PlayOneShot(hookSFX);
                }
            }
        }
    }

    // ReSharper disable Unity.PerformanceAnalysis
    void MoveRope()
    {
        if (isRotating || isMinigameActive)
            return;

        Vector3 temp = transform.position;
        
        if (moveDown)
        {
            if (!GetComponent<CircleCollider2D>().enabled)
                GetComponent<CircleCollider2D>().enabled = true;
            
            temp -= transform.up * (Time.deltaTime * moveSpeed);
        }
        else
        {
            if (GetComponent<CircleCollider2D>().enabled)
                GetComponent<CircleCollider2D>().enabled = false;
            
            temp += transform.up * (Time.deltaTime * moveSpeed);

            if (hookedObject != null)
            {
                hookedObject.transform.position = temp + hookedOffset; //pick up from where hook hits human
            }
        }
        transform.position = temp;

        if (temp.y <= minY)
        {
            Debug.Log("Hook reached minY, stopping downward movement.");
            moveDown = false;
        }

        if (temp.y >= initialY)
        {
            isRotating = true;
            ropeRenderer.RenderLine(temp, false);
            moveSpeed = initialMoveSpeed;
            
            if (hookedObject != null)
            {
                //Debug.Log("SetActive false");
                hookedObject.SetActive(false);
                hookedObject = null;
                
                if (minigameWasSuccessful)
                {
                    phScript.AddPoint();
                    minigameWasSuccessful = false;
                }
            }
        }

        ropeRenderer.RenderLine(temp, true);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (moveDown && other.CompareTag("midHuman"))
        {
            if (humanSound.Length > 0)
            {
                int randomIndex;
                do
                {
                    randomIndex = Random.Range(0, humanSound.Length);
                } while (randomIndex == lastPlayedIndex && humanSound.Length > 1);

                lastPlayedIndex = randomIndex;
                source.clip = humanSound[randomIndex];
                source.Play();
            }
            
            Debug.Log(other.tag);
            hookedObject = other.gameObject;
            CircleCollider2D circleCollider = other.GetComponent<CircleCollider2D>();
            if (circleCollider != null)
            {
                Vector2 humanCenter = circleCollider.bounds.center;
                //Debug.Log("human centre");

                if (Vector2.Distance(transform.position, humanCenter) <= 5f) //threshold (larger more forgiving)
                {
                    //Debug.Log("hooked");
                    
                    HumanBehaviour humanBehaviour = hookedObject.GetComponent<HumanBehaviour>();
                    if (humanBehaviour != null)
                    {
                        humanBehaviour.StopMovement();
                    }
                    isMinigameActive = true;
                    moveSpeed = 0; 
                    fishingMinigameScript.RestartMinigame(); 
                    hookedOffset = hookedObject.transform.position - transform.position;
                    moveDown = false;
                    //hookedObject = null;
                }
            }
        }
    }
    
    // ReSharper disable Unity.PerformanceAnalysis
    public void OnMinigameComplete(bool success)
    {
        isMinigameActive = false;

        if (success)
        {
            moveSpeed = initialMoveSpeed;
            minigameWasSuccessful = true;
            
            if (victorySFX != null)
            {
                source.PlayOneShot(victorySFX);
            }
        }
        else
        {
            moveSpeed = initialMoveSpeed;
            
            if (failSFX != null)
            {
                source.PlayOneShot(failSFX);
            }
            
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
}
