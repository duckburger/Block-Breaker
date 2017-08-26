using UnityEngine;
using System.Collections;

public class Brick : MonoBehaviour {

    
    private int timesHit;
    private LevelManager levelManager;
    private bool isBreakable;

    
    public GameObject smoke;
    public static int breakableCount = 0;
    public AudioClip crack;
    public Sprite[] hitSprites;

	// Use this for initialization
	void Start () {
        
        isBreakable = (this.tag == "Breakable");
        // Keep track of breakable bricks
        if (isBreakable)
        {
            breakableCount++;
            
        }

        levelManager = GameObject.FindObjectOfType<LevelManager>();
        timesHit = 0;
	}

    

    // Update is called once per frame
    void Update () {
	
	}

    

    void OnCollisionEnter2D(Collision2D col)
    {

        AudioSource.PlayClipAtPoint(crack, transform.position);
        if (isBreakable)
        {
            
            HandleHits();
            
        }
        
    }

    void HandleHits ()
    {
        int maxHits = hitSprites.Length + 1;
        timesHit++;
        if (timesHit >= maxHits)
        {
            breakableCount--;
            levelManager.BrickDestroyed();
            Destroy(gameObject);
            PuffSmoke();
        }
        else
        {
            LoadSprites();
        }
    }

    void PuffSmoke ()
    {
        GameObject smokePuff = Instantiate(smoke, transform.position, Quaternion.identity) as GameObject;
        smokePuff.GetComponent<ParticleSystem>().startColor = gameObject.GetComponent<SpriteRenderer>().color;
    }
    void LoadSprites ()
    {
        int spriteIndex = timesHit - 1;
        if (hitSprites[spriteIndex])
        {
            this.GetComponent<SpriteRenderer>().sprite = hitSprites[spriteIndex];
        } else
        {
            Debug.LogError("Briick sprite is missing");
        }
    }

    //TODO Remove this methind once we cab actually win!
    void SimulateWin ()
    {
        levelManager.LoadNextLevel();
    }
}

