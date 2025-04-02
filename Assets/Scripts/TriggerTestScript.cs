using UnityEngine;

public class TriggerTestScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(gameObject.name + " triggered by " + other.gameObject.name);
    }
}
