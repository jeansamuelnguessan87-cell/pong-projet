using UnityEngine;

public class PongBar : MonoBehaviour
{
    public bool isHumanPlayer = false;
    public float speed = 15;

    private float xMaxDistance = 9.5f;

    void Update()
    {
        float move;

        if (isHumanPlayer)
        {
            move = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        }
        else
        {
            move = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        }

        transform.Translate(move * Vector3.right);

        if (transform.position.x < -xMaxDistance)
        {
            transform.position = new Vector3(-xMaxDistance, transform.position.y, transform.position.z);
        }

        if (transform.position.x > xMaxDistance)
        {
            transform.position = new Vector3(xMaxDistance, transform.position.y, transform.position.z);
        }
    }
}
