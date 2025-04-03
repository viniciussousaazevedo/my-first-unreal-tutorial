using UnityEngine;

public class KeeperAttack : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) {
            collision.GetComponent<PlayerController>().life--;
        }
    }
}