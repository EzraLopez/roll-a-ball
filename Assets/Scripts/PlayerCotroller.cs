using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerCotroller : MonoBehaviour {

	public float speed;
	public Text countText;
	public Text winText;

	private Rigidbody rb;
	private int count;

	void Start ()
	{
		if (SystemInfo.deviceType != DeviceType.Desktop) 
		{
			Screen.orientation = ScreenOrientation.Landscape;
			speed = 400;
		}
			
		
		rb = GetComponent<Rigidbody> ();
		count = 0;
		SetCountText ();
		winText.text = "";
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
		if (count >= 13) 
		{
			winText.text = "You Win!";
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
