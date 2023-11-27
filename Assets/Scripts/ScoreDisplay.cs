using TMPro;
using UnityEngine;

public class ScoreDisplay : MonoBehaviour
{
    public TextMeshProUGUI score;
    private int player1Score;
    private int player2Score;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        player1Score = GameObject.Find("CPUSide").GetComponent<ScoreController>().player1Score;
        player2Score = GameObject.Find("Player1 Side").GetComponent<ScoreController>().player2Score;
        score.text =  player1Score + " : " + player2Score;
    }
}
