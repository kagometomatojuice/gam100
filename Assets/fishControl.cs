using UnityEngine;

public class fishControl : MonoBehaviour
{
    float dirX, moveSpeed;
    float dirY, moveSpeed2;
    private Vector2 screenBounds;
    private float playerHalfWidth;
    private float playerHalfHeight;

    Animator anim;
    
    public float oceanMinY;
    public float oceanMaxY;

    // Use this for initialization
    private void Start () {
        anim = GetComponent<Animator> ();
        moveSpeed = 5f;
        moveSpeed2 = 5f;
        screenBounds = Camera.main.ScreenToWorldPoint (new Vector2 (Screen.width, Screen.height));
        playerHalfWidth = GetComponent<SpriteRenderer> ().bounds.extents.x;
        playerHalfHeight = GetComponent<SpriteRenderer> ().bounds.extents.y;
    }
	
    // Update is called once per frame
    void Update () {
        dirX = Input.GetAxisRaw ("Horizontal") * moveSpeed * Time.deltaTime;
        dirY = Input.GetAxisRaw("Vertical") * moveSpeed2 * Time.deltaTime;

        transform.position = new Vector2(transform.position.x + dirX, transform.position.y + dirY);

        if (dirX != 0 || dirY != 0) {
            anim.SetBool ("isWalking", true);
        }
        else {
            anim.SetBool ("isWalking", false);
        }
        
        float clampedX = Mathf.Clamp (transform.position.x, -screenBounds.x + playerHalfWidth, screenBounds.x - playerHalfWidth);
        Vector2 pos = transform.position;
        pos.x = clampedX;
        transform.position = pos;
        
        float clampedY = Mathf.Clamp(transform.position.y, oceanMinY + playerHalfHeight, oceanMaxY - playerHalfHeight);
        pos.y = clampedY;
        transform.position = pos;
        }
    }
