using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
	public int Lives = 3;
	public bool IsWon = false;
	public bool GodMode = true;
	public GUIText ScoreText;
	public GUIText ScoreTextShadow;
	public GUIText WinText;
	public GUIText WinTextShadow;
	public GUIText LivesText;
	public GUIText LivesTextShadow;
	public int Score = 0;
	public float JumpSpeed = 400f;
	public float speed;
	private bool IsGrounded = true;

	void Start()
	{
		WinText.text = "";
		WinTextShadow.text = WinText.text;
		LivesText.text = "Lives:" + Lives.ToString();
		LivesTextShadow.text = LivesText.text;
	}

	void Update ()
	{
		if (rigidbody.position.y < -100)
		{
			Death();
		}
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

	void Death ()
	{
		Lives --;
		rigidbody.MovePosition(new Vector3(0, 0.5f, 0));
		rigidbody.velocity = Vector3.zero;
		rigidbody.angularVelocity = Vector3.zero;
		WinText.text = "Ouchies!";
		WinTextShadow.text = WinText.text;
		LivesText.text = "Lives:" + Lives.ToString();
		LivesTextShadow.text = LivesText.text;
		StartCoroutine(HideText());
	}

	IEnumerator HideText()
	{
		yield return new WaitForSeconds(3.0f);
		WinText.text = "";
		WinTextShadow.text = WinText.text;
	}
}
