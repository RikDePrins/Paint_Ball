using UnityEngine;
using UnityEngine.Rendering.Universal;

public class SplashesSpawn : MonoBehaviour
{
    [SerializeField]
    private GameObject _splashTemplate;

    private GameObject _lastSpawnedSplash = null;

    private void OnCollisionEnter(Collision collision)
    {
        SpawnSplash();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Splash"))
        {
            if (other.gameObject != _lastSpawnedSplash)
            {
                Destroy(other.gameObject);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Splash"))
        {
            SpawnSplash();
        }
    }

    private void SpawnSplash()
    {
        if (!_splashTemplate) return;

        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit outRay, 1f))
        {
            _lastSpawnedSplash = Instantiate(_splashTemplate, outRay.point, Quaternion.identity);
            var xRotation = _lastSpawnedSplash.transform.rotation.x;
            _lastSpawnedSplash.transform.parent = null;
            _lastSpawnedSplash.transform.forward = -outRay.normal;
        }
        else _lastSpawnedSplash = null;
    }
}
