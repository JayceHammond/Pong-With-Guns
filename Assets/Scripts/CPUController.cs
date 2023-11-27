using System.Collections;
using UnityEngine;

public class CPUController : MonoBehaviour
{
    public GameObject Ball;
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Collider2D CPUCol;
    private int dir;
    private float ballSpeed;
    public GameObject player;
    private float distFromPlayer;
    public GameObject portal;
    public GameObject CPUPortal;
    private AudioSource audioData;
    public AudioSource playerHit;
    private float parryPercent = 46f;
    // Start is called before the first frame update
    void Start()
    {
        
        audioData = GetComponent<AudioSource>();
        rb = this.GetComponent<Rigidbody2D>();
        sprite = this.GetComponent<SpriteRenderer>();
        ballSpeed = Ball.GetComponent<BallController>().ballSpeed;
        CPUCol = this.GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        distFromPlayer = this.transform.position.y - player.transform.position.y;
        if(this.transform.position.y < Ball.transform.position.y){
            dir = 1;
            rb.AddForce(transform.up * dir * 0.1f, ForceMode2D.Impulse);
        }
        else if(this.transform.position.y > Ball.transform.position.y){
            dir = -1;
            rb.AddForce(transform.up * dir * 0.1f, ForceMode2D.Impulse);
        }
        else{
            dir = 0;
            rb.AddForce(transform.up * dir * 0.1f, ForceMode2D.Impulse);
        }
        
    }


    private void OnCollisionEnter2D(Collision2D other) {
        int parryChance = Random.Range(0,100);
        if(other.gameObject.tag == "Projectile" && parryChance < parryPercent && parryChance > 0f){
            Rigidbody2D bullet = other.gameObject.GetComponent<Rigidbody2D>();
            audioData.Play(0);
            StartCoroutine(PortalWait());
            other.gameObject.transform.position = new Vector2(other.gameObject.transform.position.x, player.transform.position.y);
            bullet.velocity = new Vector2(-bullet.velocity.x - 20, 0);
            GameObject portalSpawn = Instantiate(portal, new Vector3(this.transform.position.x, bullet.transform.position.y), this.transform.rotation);
            StartCoroutine(SelfDestruct(portalSpawn));
        }
        else if(other.gameObject.tag == "Projectile"){
            Destroy(other.gameObject);
            playerHit.Play(0);
            StartCoroutine(IFrames());
        }
    }

    IEnumerator IFrames(){
        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 0.5f);
        rb.simulated = false;
        CPUCol.enabled = false;
        yield return new WaitForSeconds(0.5f);
        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 1f);
        rb.simulated = true;
        CPUCol.enabled = true;

    }

    IEnumerator SelfDestruct(GameObject newPortal){
        yield return new WaitForSeconds(1f);
        Destroy(newPortal);
    }

    IEnumerator PortalWait(){
        CPUPortal.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        CPUPortal.SetActive(false);
    }
}
