
using UnityEngine;
using System.Collections;

public class person : MonoBehaviour
{
    private float speed = 0f;
    public GameObject hotDogPlayer; // Reference to the hot dog player
    public float detectionRange = 5f; // The range at which the sprite will change
    public Sprite regularSprite; // The default sprite
    public Sprite openMouthSprite; // The sprite with the open mouth

    private SpriteRenderer spriteRenderer;
    private bool mouthOpen = false;

    IEnumerator Kill() 
    {
        yield return new WaitForSeconds(8f);
        Destroy(gameObject);
    }
    
    void Start()
    {
        // Get the SpriteRenderer component
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Set the initial sprite to the regular one
        spriteRenderer.sprite = regularSprite;
        StartCoroutine(Kill());
    }

    void Update()
    {
        float moveSpeed = ScrollingBackground.backgroundSpeed + speed;
        transform.position += Vector3.left * moveSpeed * Time.deltaTime;
        // Calculate the distance between the hot dog player and this object
        float distanceToHotDog = Vector2.Distance(transform.position, hotDogPlayer.transform.position);

        // If the hot dog is within the detection range and the mouth isn't open, switch to the open mouth sprite
        if (distanceToHotDog <= detectionRange && !mouthOpen)
        {
            spriteRenderer.sprite = openMouthSprite;
            mouthOpen = true;
        }
        // If the hot dog moves out of range, switch back to the regular sprite
        else if (distanceToHotDog > detectionRange && mouthOpen)
        {
            spriteRenderer.sprite = regularSprite;
            mouthOpen = false;
        }
    }
}
