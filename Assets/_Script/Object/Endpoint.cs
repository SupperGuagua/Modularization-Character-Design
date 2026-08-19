using UnityEngine;

public class Endpoint : MonoBehaviour
{
    [SerializeField] private GameObject endscreen;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            endscreen.SetActive(true);
        }
    }
}
