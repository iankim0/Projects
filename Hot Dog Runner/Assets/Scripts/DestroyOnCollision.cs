using UnityEngine;

public class DestroyOnCollision : MonoBehaviour
{
    // This method is called when another collider enters the box collider attached to this GameObject
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Person"))
        {
            Debug.Log("Hit");
            Destroy(other.gameObject);
        }
    }
    

}
