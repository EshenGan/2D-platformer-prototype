using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class FallingSpikes : MonoBehaviour
{
    Rigidbody2D spike_rb;
    private Vector2 initialposition;
    [SerializeField] float gravity;

    // Start is called before the first frame update
    void Start()
    {
        spike_rb = GetComponent<Rigidbody2D>();
        spike_rb.isKinematic = true;
        initialposition = transform.position;
        
    }
    private void Update()
    {

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) {

            spike_rb.isKinematic = false;
            spike_rb.gravityScale = gravity;

            
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ground")) 
        {
            gameObject.SetActive(false);
            Debug.Log("deactivate falling spike");
        }
    }

    private void OnEnable()
    {
        spike_rb.bodyType = RigidbodyType2D.Kinematic;
        transform.position = initialposition;
        spike_rb.velocity = Vector2.zero;
        gameObject.SetActive(true);


    }

}
