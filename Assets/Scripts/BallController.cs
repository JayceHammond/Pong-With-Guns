using UnityEngine;

public class BallController : MonoBehaviour
{
    private Rigidbody2D rb;
    public float ballSpeed;
    public float ballMax;
    public float ballRotation;
    private AudioSource audioData;
    // Start is called before the first frame update
    void Start()
    {
        audioData = GetComponent<AudioSource>();
        rb = this.GetComponent<Rigidbody2D>();
        rb.AddForce(-transform.right * ballSpeed, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update() {
        

    }

    private void OnCollisionEnter2D(Collision2D other) {
        audioData.Play(0);
        if(other.gameObject.name == "CPU"){
            rb.AddForce(-transform.right * (ballSpeed * 2), ForceMode2D.Impulse);
        }
    }

}
