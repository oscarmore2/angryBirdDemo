using UnityEngine;
using System.Collections;

public class LookDir : MonoBehaviour {


	[SerializeField]
	Transform LookDirObj;
	// Use this for initialization
	void Start () {
	
	}

	void FixedUpdate () 
	{
		transform.LookAt (LookDirObj.position);
	}
}
