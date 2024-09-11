using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScrollingBackground : MonoBehaviour
{
    public GameObject bg1;             // First background image
    public GameObject bg2;             // Second background image
    public GameObject bg3;             // Third background image
    public static float backgroundSpeed = 2f; // Initial background scroll speed
    public float speedIncreaseRate = 0.05f; // How much to increase speed per second
    public float despawnBuffer = 1f;
    public TextMeshProUGUI scoreT;
    public float score;

    private float backgroundWidth;     
    private Camera mainCamera;     

    private void UpdateScore()
    {
        scoreT.text = "SCORE: " + score;
    }

    void Start()
    {
        backgroundWidth = bg1.GetComponent<SpriteRenderer>().bounds.size.x;
        mainCamera = Camera.main;
        UpdateScore();
    }

    void Update()
    {
        bg1.transform.position += Vector3.left * backgroundSpeed * Time.deltaTime;
        bg2.transform.position += Vector3.left * backgroundSpeed * Time.deltaTime;
        bg3.transform.position += Vector3.left * backgroundSpeed * Time.deltaTime;

        backgroundSpeed += speedIncreaseRate * Time.deltaTime;

        score = (int)(backgroundSpeed*100)-200;
        UpdateScore();
        


        if (bg1.transform.position.x < mainCamera.transform.position.x - backgroundWidth - despawnBuffer)
        {
            RepositionBackground(bg1, bg3);
        }
        if (bg2.transform.position.x < mainCamera.transform.position.x - backgroundWidth - despawnBuffer)
        {
            RepositionBackground(bg2, bg1);
        }
        if (bg3.transform.position.x < mainCamera.transform.position.x - backgroundWidth - despawnBuffer)
        {
            RepositionBackground(bg3, bg2);
        }
    }

    private void RepositionBackground(GameObject bg, GameObject otherBg)
    {
        bg.transform.position = new Vector3(otherBg.transform.position.x + backgroundWidth, bg.transform.position.y, bg.transform.position.z);
    }
}
