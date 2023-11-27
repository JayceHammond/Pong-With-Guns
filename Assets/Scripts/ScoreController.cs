using UnityEngine;

public class ScoreController : MonoBehaviour
{
    
    public int player1Score = 0;
    public int player2Score = 0;
    public GameObject ball;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(player1Score >= 10){
            Debug.Log("Player 1 wins");
        }

        if(player2Score >= 10){
            Debug.Log("Player 2 wins");
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if(this.gameObject.name == "CPUSide" && other.name == "Ball"){
            ball.transform.position = new Vector2(0,0);
            ball.GetComponent<Rigidbody2D>().AddForce(-transform.right * 5, ForceMode2D.Impulse);
            player1Score += 1;
        }
        if(this.gameObject.name == "Player1 Side" && other.name == "Ball"){
            ball.transform.position = new Vector2(0,0);
            ball.GetComponent<Rigidbody2D>().AddForce(-transform.right * 5, ForceMode2D.Impulse);
            player2Score += 1;
        }
        if(other.tag == "Projectile"){
            Destroy(other.gameObject);
        }
    }
}
