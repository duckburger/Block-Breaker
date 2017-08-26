using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {

    
    private paddle paddle;
    private bool hasStarted = false;
    private Vector3 paddleToBallVector;

	// Use this for initialization
	void Start () {
        paddle = GameObject.FindObjectOfType<paddle>();
        paddleToBallVector = this.transform.position - paddle.transform.position;
        
        
	}

    

    // Update is called once per frame
    void Update()
    {
        if (!hasStarted)
        {
            transform.position = paddle.transform.position + paddleToBallVector;

            if (Input.GetMouseButtonDown(0))
            {
                print("Mouse clicked!");
                hasStarted = true;
                GetComponent<Rigidbody2D>().velocity = new Vector2(2f, 10f);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        Vector2 tweak = new Vector2 (Random.Range(0f, 0.2f), Random.Range(0f, 0.2f));
        print(tweak);

        if (hasStarted)
        { 
        GetComponent<AudioSource>().Play();
        GetComponent<Rigidbody2D>().velocity += tweak;
            
        }
    }

}
