using UnityEngine;

public class CoinSpin : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 100f;

    private void Update()
    {
        transform.Rotate(rotationSpeed * Time.deltaTime, 0f, 0f);
    }
}
