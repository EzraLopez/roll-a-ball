using UnityEngine;
using UnityEngine.UI;

public class PlayerCotroller : MonoBehaviour {

	public float speed;
	public Text countText;
	public Text winText;
    public Button newGameButton;

	private Rigidbody rb;
	private int count;
    private bool gameOver;
    private GameObject[] pickUpObjects;

	void Start ()
	{
		if (SystemInfo.deviceType != DeviceType.Desktop) 
		{
			Screen.orientation = ScreenOrientation.Landscape;
			speed = 400;
		}
        pickUpObjects = GameObject.FindGameObjectsWithTag("Pick Up");
		rb = GetComponent<Rigidbody> ();
        gameOver = false;
        NewGame();
	}
			
	// Called befor any Physics calculations
	void FixedUpdate ()
	{
		if (SystemInfo.deviceType == DeviceType.Desktop) {
			float moveHorizontal = Input.GetAxis ("Horizontal");
			float moveVertical = Input.GetAxis ("Vertical");

			Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);

			rb.AddForce (movement * speed);
		} 
		else 
		{
			// Credits: http://answers.unity3d.com/answers/902238/view.html
			Vector3 movement = new Vector3 (Input.acceleration.x, 0.0f, Input.acceleration.y);
			rb.AddForce(movement * speed * Time.deltaTime);
		}
	}

	void OnTriggerEnter(Collider other) 
	{	
		if (other.gameObject.CompareTag ("Pick Up")) 
		{
			other.gameObject.SetActive (false);
			count = count + 1;
			SetCountText ();
		}
	}

	void SetCountText()
	{
		countText.text = "Count: " + count.ToString ();
		if (count >= pickUpObjects.Length) 
		{
            gameOver = true;
            winText.text = "You Win!";
            newGameButton.gameObject.SetActive(true);
        }
	}

    public void NewGame()
    {
        count = 0;
        SetCountText();
        winText.text = "";
        newGameButton.gameObject.SetActive(false);
        
		if(gameOver)
        {
            foreach(GameObject pu in pickUpObjects)
            {
                pu.gameObject.SetActive(true);
            }

			rb.transform.position = Vector3.zero;
			rb.velocity = Vector3.zero;
			rb.angularVelocity = Vector3.zero;
        }
    }
//	// Use this for initialization
//	void Start () {
//	
//	}
	
//	// Update is called once per frame
//	// Called before redering a frame
//	void Update () {
//	
//	}
}
