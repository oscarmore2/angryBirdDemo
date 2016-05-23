using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class Bird : MonoBehaviour {

	[SerializeField]
	springPoint springForce;

	[SerializeField]
	float Mass = 4f;

	[SerializeField]
	Transform EndPoint;

	[SerializeField]
	Transform Anchor;

	Rigidbody rigid;

	void Start () {
		rigid = GetComponent<Rigidbody> ();
		springForce.OnForceStart += OnReceiveForece;
		rigid.isKinematic = true;
		rigid.useGravity = false;
		rigid.mass = Mass;
	}

	bool ForceStart = false;
	void OnReceiveForece()
	{
		rigid.isKinematic = false;
		ForceStart = true;

	}

	void FixedUpdate()
	{
		if (Anchor.localPosition.x > -0.1f && ForceStart) {
			springForce.OnForceStart -= OnReceiveForece;
			rigid.isKinematic = false;
			rigid.useGravity = true;
			rigid.AddForceAtPosition (springForce.ForceOfSpring, transform.position);
		}


		if (transform.localPosition.x < 1.1f && !ForceStart) {
			transform.localPosition = EndPoint.localPosition + Vector3.right * 0.5f;
		}

		if (transform.position.y < 1.2f) {
			ForceStart = false;
		}
		
	}

	void OnDestory()
	{
		springForce.OnForceStart -= OnReceiveForece;
	}
}
