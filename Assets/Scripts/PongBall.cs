using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PongBall : MonoBehaviour
{
    public float speed;
    public Vector3 direction;
    public Text scoreText;

    private float zMaxDistance = 15f;
    private int scorePlayer = 0;
    private int scoreComputer = 0;

    private void Start()
    {
        SetDirection();
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);

        // IA gagne
        if (transform.position.z < -zMaxDistance && direction.z < 0)
        {
            scoreComputer++;
            SetDirection();
        }

        // Joueur gagne
        if (transform.position.z > zMaxDistance && direction.z > 0)
        {
            scorePlayer++;
            SetDirection();
        }
    }

    public void SetDirection()
    {
        scoreText.text = scorePlayer.ToString() + " - " + scoreComputer.ToString();
        transform.position = new Vector3(0, .5f, 0);
        direction = new Vector3(Random.Range(0.75f, 1.75f), 0, -1).normalized;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bar")
        {
            bool isPlayer = collision.gameObject.GetComponent<PongBar>().isHumanPlayer;

            if ((isPlayer && direction.z < 0) || (!isPlayer && direction.z > 0))
            {
                direction.z *= -1;
            }

            if (!isPlayer)
            {
                collision.gameObject.GetComponent<PongAI>().AddBounce();
            }
        }

        if (collision.gameObject.tag == "Side")
        {
            direction.x *= -1;
        }
    }
}
