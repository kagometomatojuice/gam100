using UnityEngine;

public class HumanBehaviour : MonoBehaviour
{
    public float speed = 3f;
    private bool moveRight;
    private bool isMoving = true;
    
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
    }

    
    public void StopMovement()
    {
        isMoving = false;
    }
    
    public void RestartMovement()
    {
        isMoving = true;
    }

    private void OnDestroy()
    {
        HumanSpawner.DecrementActiveHumans();
    }
}