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
		Vector3 pos = (Branch.transform.localPosition - endpoint.transform.localPosition) / -2f;
		this.gameObject.transform.localPosition = new Vector3 (pos.x, Branch.transform.localPosition.y, pos.z) ;
		this.transform.rotation = endpointLook.rotation;
		float scale = (Branch.transform.localPosition - endpoint.transform.localPosition).magnitude;
		Debug.Log (scale);
		this.transform.localScale = new Vector3 (transform.localScale.x, transform.localScale.y, scale);
	}

	// Update is called once per frame
	void Update () {
	
	}
}
