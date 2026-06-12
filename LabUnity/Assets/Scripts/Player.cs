using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Player : MonoBehaviour
{
    
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private float jumpForce = 7f;
    [SerializeField] private float fallLimit = -5f;
    [SerializeField] private TextMeshProUGUI coinsText;

    private float coinsCollected = 0;
    private float horizontalInput;
    private float verticalInput;

    private bool isGrounded;
    private bool jumpRequested;

    private Rigidbody rigidbodyComponent;

    private void Start()
    {
        rigidbodyComponent = GetComponent<Rigidbody>();
        updateCoinText();
    }

    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            jumpRequested = true;
        }

        if (transform.position.y < fallLimit)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    private void FixedUpdate()
    {
        Vector3 currentVelocity = rigidbodyComponent.linearVelocity;

        rigidbodyComponent.linearVelocity = new Vector3(
            horizontalInput * movementSpeed,
            currentVelocity.y,
            verticalInput * movementSpeed
        );


        if (jumpRequested)
        {
            rigidbodyComponent.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);

            jumpRequested = false;
            isGrounded = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        isGrounded = true;
    }

    private void OnCollisionStay(Collision collision)
    {
        isGrounded = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            Destroy(other.gameObject);
            coinsCollected++;
            updateCoinText();
        }
    }


    private void updateCoinText()
    {
        coinsText.text = "Coins: " + coinsCollected;
    }
}
