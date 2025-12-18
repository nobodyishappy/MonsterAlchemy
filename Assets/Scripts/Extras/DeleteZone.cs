using UnityEngine;

public class DeleteZone : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        Destroy(other.transform.root.gameObject);
    }
}
