using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    public float moveSpeed;
    private SpriteRenderer sprite;
    private Collider2D playerCol;
    public AudioSource playerHit;
    public GameObject pauseMenu;
    public static bool paused = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        sprite = this.GetComponent<SpriteRenderer>();
        playerCol = this.GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Input.GetKey(KeyCode.W)){
            rb.AddForce(transform.up * moveSpeed, ForceMode2D.Impulse);
        }
        if(Input.GetKey(KeyCode.S)){
            rb.AddForce(-transform.up * moveSpeed, ForceMode2D.Impulse);
        }
        if(Input.GetKey(KeyCode.Escape)){
            paused = !paused;
            pauseMenu.SetActive(true);
        }
        
    }


    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "Projectile"){
            playerHit.Play(0);
            Destroy(other.gameObject);
            StartCoroutine(IFrames());
        }
    }

    IEnumerator IFrames(){
        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 0.5f);
        rb.simulated = false;
        playerCol.enabled = false;
        yield return new WaitForSeconds(0.5f);
        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 1f);
        rb.simulated = true;
        playerCol.enabled = true;

    }
}
