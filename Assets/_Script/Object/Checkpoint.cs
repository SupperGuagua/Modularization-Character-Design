using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] Collider2D checkpointcollider;
    [SerializeField] Transform particleposition;
    [SerializeField] GameObject particle;
    [SerializeField] int number;

    private bool AlreadyDone = false;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IUpdateRespawnPoint updateRespawnPoint))
        {
            updateRespawnPoint.RespawnUpdate(number);
            Instantiate(particle, particleposition.position, particleposition.rotation);
            checkpointcollider.enabled = false;

            if (AlreadyDone)
                return;

            AlreadyDone = true;
            Gamemanager.instance.SetTheSecondCamera();
        }
    }


}
