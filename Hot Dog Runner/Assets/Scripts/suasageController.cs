using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class suasageController : MonoBehaviour
{
    [Header("Jumping")]
    public float jumpForce;
    [Header("Animation")]
    public Sprite[] spriteUp = new Sprite[2];
    public Sprite[] spriteRun = new Sprite[2];
    public Sprite stillSprite;

    private Rigidbody2D _sausageRB;
    private AudioSource _jump;
    private SpriteRenderer _sausageSR;
    private bool isGrounded;
    private int cycle = 0;
    private float animationSpeed = 0.2f;  // Time between frames (in seconds)
    private float timer = 0;
    private float jumpSpeed = 0.4f;
    private float jumpTimer = 0;
 

    public KeyCode[] keyCodes;
    private KeyCode currentKeyCode;


    public TextMeshProUGUI wordText; 


    private string word = "H O T D O G";
    private char[] letters;
    private int idx = 0;

    private void animateSausage()
    {
        Vector2 jumping = _sausageRB.linearVelocity;

        if (jumping.y == 0)
        {
            timer += Time.deltaTime;
            if (timer >= animationSpeed)
            {
                _sausageSR.sprite = spriteRun[cycle];
                cycle += 1;
                if (cycle >= spriteRun.Length)
                {
                    cycle = 0;
                }
                timer = 0;
            }
        }

        else 
        {
            if (jumping.y > 0) 
            {
                jumpTimer += Time.deltaTime;
                if (jumpTimer >= jumpSpeed)
                {
                    _sausageSR.sprite = spriteUp[1];
                }
                else
                {
                    _sausageSR.sprite = spriteUp[0];
                }
            }

        }
    }
    
    private void UpdateWordDisplay()
    {
       string displayText = "";
       int letterIndex = 0;


       for (int i = 0; i < word.Length; i++)
       {
           if (word[i] == ' '){
               displayText += " ";
           }
           else{
               if (letterIndex == idx)
               {
                   displayText += "<color=#FFFF00>" + word[i] + "</color>";
               }
               else
               {
                   displayText += word[i];
               }
               letterIndex++;
           }
       }


       wordText.text = displayText;
    }


    private void Jump()
    {
        Vector2 vel = _sausageRB.linearVelocity;
        if (Input.GetKeyDown(currentKeyCode)){
            vel.y = jumpForce;
            _sausageRB.linearVelocity = vel;
            _jump.Play();
        

            idx = Random.Range(0, letters.Length);
            currentKeyCode = GetKeyCodeForLetter(letters[idx]);


            UpdateWordDisplay();
        }
    }

    private void checkGrounded()
    {
        Vector2 onGround = _sausageRB.linearVelocity;
        if (onGround.y == 0) 
        {
            isGrounded = true;
        }
    }

    private void reloadScene() 
    {
        CameraController.followSpeed = 2f;
        ScrollingBackground.backgroundSpeed = 2f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnCollisionEnter2D(Collision2D collision) 
    {
        if (collision.gameObject.CompareTag("Person"))
        {

            reloadScene();
        }
    }

       private char[] GetLettersArray()
   {
       return word.Replace(" ", "").ToCharArray();
   }


   private KeyCode GetKeyCodeForLetter(char letter)
   {
       for (int i = 0; i < keyCodes.Length; i++){
           if (keyCodes[i].ToString().Equals(letter.ToString(), System.StringComparison.OrdinalIgnoreCase)){
               return keyCodes[i];
           }
       }
       return KeyCode.None;
   }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _sausageRB = GetComponent<Rigidbody2D>();
        _sausageSR = GetComponent<SpriteRenderer>();
        _jump = GetComponent<AudioSource>();
        _sausageSR.sprite = stillSprite;
        letters = GetLettersArray();
      
        idx = Random.Range(0, letters.Length);
        currentKeyCode = GetKeyCodeForLetter(letters[idx]);


        UpdateWordDisplay();
    }

    // Update is called once per frame
    void Update()
    {

        animateSausage();
        checkGrounded();
        if (isGrounded)
        {
            Jump();
            isGrounded = false;
        }
    }

}
