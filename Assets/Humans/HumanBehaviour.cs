using UnityEngine;

public class HumanBehaviour : MonoBehaviour
{
    public float speed = 3f;
    private bool moveRight;
    private bool isMoving = true;
    
    private Animator animator;
    
    private void Awake()
    {
        animator = GetComponent<Animator>(); 
    }
    
    public void SetDirection(bool isMovingRight)
    {
        moveRight = isMovingRight;
    }

    private void Update()
    {
        if (isMoving)
        {
            if (moveRight)
            {
                transform.Translate(Vector2.right * speed * Time.deltaTime);
            }
            else
            {
                transform.Translate(Vector2.left * speed * Time.deltaTime);
            }
        }
        animator.SetBool("isMoving", isMoving);
    }

    
    public void StopMovement()
    {
        isMoving = false;
        animator.SetBool("isMoving", false);
        animator.SetBool("isHooked", true);
    }
    
    public void RestartMovement()
    {
        isMoving = true;
        animator.SetBool("isMoving", true);
        animator.SetBool("isHooked", false);
    }

    private void OnDestroy()
    {
        HumanSpawner.DecrementActiveHumans();
    }
}