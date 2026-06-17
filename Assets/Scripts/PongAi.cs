using UnityEngine;

public class PongAI : MonoBehaviour
{
    public Transform pongBall;
    public float latency = 4f;
    public float viewDistance = 20;
    public int nbShots = 0;

    void Update()
    {
        if (Vector3.Distance(transform.position, pongBall.position) < viewDistance)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                new Vector3(pongBall.transform.position.x, .5f, 14),
                latency * Time.deltaTime
            );
        }
    }

    public void AddBounce()
    {
        nbShots++;
        if (nbShots >= 10)
        {
            nbShots = 0;
            latency -= 0.25f;
        }
    }
}
