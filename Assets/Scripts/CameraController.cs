using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public GameObject player;

	private Vector3 offset;

	// Use this for initialization
	void Start () 
	{
		offset = transform.position - player.transform.position;
	}
	
	// Update is called once per frame (This loads everything)
	// We are going to use LateUpdate because it guarantees that all items have
	// been process in Update (The player has moved for that fram)
	void LateUpdate () 
	{
		transform.position = player.transform.position + offset;
	}
}
