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

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        SetDirection();
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);

        // IA gagne
        if (transform.position.z < -zMaxDistance && direction.z < 0)
        {
            scoreComputer++;
            // Étape 2 : Appel de la vérification après le point de l'IA
            VerifierFinDePartie();
            SetDirection();
        }

        // Joueur gagne
        if (transform.position.z > zMaxDistance && direction.z > 0)
        {
            scorePlayer++;
            // Étape 2 : Appel de la vérification après le point du Joueur
            VerifierFinDePartie();
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

        audioSource.Play();
    }

    // Étape 1 : Ajout de la fonction de vérification de fin de partie tout en bas
    private void VerifierFinDePartie()
    {
        // On vérifie si l'un des deux scores a atteint ou dépassé 5 points
        if (scorePlayer >= 5 || scoreComputer >= 5)
        {
            // 1. On remet les scores numériques à zéro dans le code
            scorePlayer = 0;
            scoreComputer = 0;

            // 2. On met à jour l'affichage sur l'écran (corrigé en scoreText)
            scoreText.text = "0 - 0";

            // 3. Petit message dans la console Unity
            Debug.Log("Partie terminée ! Les scores reviennent à zéro.");
        }
    }
}
