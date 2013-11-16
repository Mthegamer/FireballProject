using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
	public bool IsWon = false;
	public bool GodMode = true;
	public GUIText ScoreText;
	public GUIText ScoreTextShadow;
	public GUIText WinText;
	public GUIText WinTextShadow;
	public int Score = 0;
	public float JumpSpeed = 400f;
	public float speed;
	private bool IsGrounded = true;

	void Start()
	{
		WinText.text = "";
		WinTextShadow.text = WinText.text;
	}

	void FixedUpdate ()
	{
		float moveHorizontal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis("Vertical");
		Vector3 movement = new Vector3(moveHorizontal, 0.0f , moveVertical);

		rigidbody.AddForce(movement * speed * Time.deltaTime);

		//Jumping code
		if (Input.GetKeyDown(KeyCode.Space) && (IsGrounded == true || GodMode == true))
		{
			IsGrounded = false;
			rigidbody.AddForce(0, JumpSpeed, 0);
		}
	}

	void OnTriggerEnter(Collider other)
	{
		switch(other.gameObject.tag)
		{
		case "Pickup":
			Score ++;
			other.gameObject.SetActive (false);
			ScoreText.text = "Score:" + Score.ToString();
			ScoreTextShadow.text = ScoreText.text;
			if (Score > 19)
			{
				WinText.text = "You Win!";
				WinTextShadow.text = WinText.text;
			}
			break;
		}
	}

	void OnCollisionEnter(Collision other)
	{
		switch(other.gameObject.tag)
		{
		case "Ground":
			IsGrounded = true;
			break;
		}
	}

}
