using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections.Specialized;

public class PlayerController : MonoBehaviour
{
    // Rigidbody of the player.
    private Rigidbody rb;
    private int count;
    private int time;

    // Movement along X and Y axes.
    private float movementX;
    private float movementY;

    // Speed at which the player moves.
    public float speed = 0;
    public TextMeshProUGUI countText;
    public TextMeshProUGUI countTime;
    private float countdown = 1f;
    public GameObject audioBox;
    //public GameObject winTextObject;



    // Start is called before the first frame update.
    void Start()
    {
        // Get and store the Rigidbody component attached to the player.
        rb = GetComponent<Rigidbody>();
        count = 0;
        time = 15;
        SetCountText();
        //winTextObject.SetActive(false);
    }

    // This function is called when a move input is detected.
    void OnMove(InputValue movementValue)
    {
        // Convert the input value into a Vector2 for movement.
        Vector2 movementVector = movementValue.Get<Vector2>();

        // Store the X and Y components of the movement.
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void SetCountText()
    {
        countText.text = "Time Plus: " + count.ToString() + " s";
        countTime.text = "Time: " + time.ToString() + " s";
    
        //winTextObject.SetActive(true);
        //SceneManager.LoadSceneAsync(3);
        
    }

    // FixedUpdate is called once per fixed frame-rate frame.
    private void FixedUpdate()
    {
        // Create a 3D movement vector using the X and Y inputs.
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);

        // Apply force to the Rigidbody to move the player.
        rb.AddForce(movement * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object the player collided with has the "Pickup" tag.
        if (other.gameObject.CompareTag("Pickup"))
        {
            GameObject preFabSound = Instantiate(audioBox, new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, this.gameObject.transform.position.z), Quaternion.identity);
            Destroy(preFabSound.gameObject, 1f);
            // Deactivate the collided object (making it disappear).
            other.gameObject.SetActive(false);
            count = count +  1;
            time = time + 1;
            SetCountText();
        }
        if (other.gameObject.CompareTag("Enemy"))
        {
            SceneManager.LoadSceneAsync(2);
        }

        if (other.gameObject.CompareTag("SafeWin"))
        {
            SceneManager.LoadSceneAsync(3);
        }
    }


    void Update()
    {
        countdown = countdown - Time.deltaTime; 

        if (countdown <= 0f)
        {
            time = time - 1;
            SetCountText();  
            countdown = 1f;  
        }

        if (time == 0)
        {
            SceneManager.LoadSceneAsync(2);  
        }

        // Verifica se o jogador caiu do mapa
        if (this.transform.position.y < -260f)  // Ajuste o valor conforme o tamanho da sua cena
        {
            SceneManager.LoadSceneAsync(2);  // Carrega a tela de Game Over
        }
    }



}