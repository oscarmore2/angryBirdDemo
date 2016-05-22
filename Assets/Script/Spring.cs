using UnityEngine;
using System.Collections;

public class Spring : MonoBehaviour {

	[SerializeField]
	GameObject Branch;

	[SerializeField]
	GameObject endpoint;

	[SerializeField]
	Transform endpointLook;

	// Use this for initialization
	void Start () {
	
	}

	void FixedUpdate()
	{
		Vector3 pos = (Branch.transform.localPosition + endpoint.transform.localPosition) / 2;
		this.gameObject.transform.localPosition = pos ;
		this.transform.rotation = endpointLook.rotation;
		float scale = (Branch.transform.localPosition - endpoint.transform.localPosition).magnitude;
		this.transform.localScale = new Vector3 (transform.localScale.x, transform.localScale.y, scale);
	}

	// Update is called once per frame
	void Update () {
	
	}
}
