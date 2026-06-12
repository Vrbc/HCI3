using NUnit.Framework;
using UnityEngine;

public class PlatformTrigger : MonoBehaviour
{
    private PlatformSpawner spawner;
    private bool hasBeenTriggered;
    void Start()
    {
        hasBeenTriggered = false;
        spawner = FindAnyObjectByType<PlatformSpawner>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (hasBeenTriggered || !other.CompareTag("Player"))
        {
            return;
        }

        hasBeenTriggered = true;

        spawner.SpawnNextPlatform();
    }
}
