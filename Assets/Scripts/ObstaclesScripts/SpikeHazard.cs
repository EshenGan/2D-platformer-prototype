using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpikeHazard : MonoBehaviour
{
    Vector2 originalPosition;
    public AnimationCurve curve;
    [SerializeField] private bool vertical;
    [SerializeField] private bool horizontal;
    [SerializeField] private bool up;
    [SerializeField] private bool right;
    // Start is called before the first frame update
    void Start()
    {
        originalPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (vertical && !horizontal)
        {
            if (up) {
                transform.position = new Vector2(originalPosition.x, curve.Evaluate(Time.time) + originalPosition.y);
            }
            else if (!up) {
                transform.position = new Vector2(originalPosition.x,  originalPosition.y- curve.Evaluate(Time.time));
            }
            
        }
        else if (!vertical && horizontal)
        {
            if (right) {
                transform.position = new Vector2(curve.Evaluate(Time.time) + originalPosition.x, originalPosition.y);
            }
            else if (!right) {
                transform.position = new Vector2(originalPosition.x- curve.Evaluate(Time.time), originalPosition.y);
            }
            
        }
    }

 /*   private void OnCollisionEnter2D(Collision2D collision)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }*/
}
