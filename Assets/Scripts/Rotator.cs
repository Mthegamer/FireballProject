using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour {

	void Start()
	{
		gameObject.tag = "Pickup";
	}

	void Update ()
	{
		transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);
	}
}
