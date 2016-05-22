using UnityEngine;
using System.Collections;

public class springPoint : MonoBehaviour {

	public enum DragState
	{
		Idle ,OnDrag, 
	}

	public Vector3 ForceOfSpring
	{
		get { return _forceOfSpring;}
	}


	[SerializeField]
	float Range;

	[SerializeField]
	float SpringModifier;
	float _SpringModifier;

	[SerializeField]
	float decaySpeed;
	float _decaySpeed = 1f;

	DragState _dragState;

	float Threshold = 0.3f;

	[SerializeField]
	Transform Anchor;

	[SerializeField]
	BoxCollider Dragable;

	[SerializeField]
	Rigidbody rigid;

	public delegate void ForceNotifier();
	public event ForceNotifier OnForceStart;

	void DummyForceProvide(){
	}

	// Use this for initialization
	void Start () {
		OnForceStart += DummyForceProvide;
		Dragable.tag = "Dragable";
		if (rigid == null) 
		{
			rigid = GetComponent<Rigidbody> ();
		}
	}


	Vector3 ForceDirection;
	bool actOnce;
	RaycastHit hit = new RaycastHit();
	Vector3 _forceOfSpring;
	void FixedUpdate () 
	{
		if (_dragState == DragState.Idle) 
		{
			rigid.isKinematic = false;
			if (!actOnce) {
				actOnce = true;

			}

			//TODO: calculate the direction and apply the force
			ForceDirection = Anchor.localPosition - transform.localPosition;

			if (_decaySpeed < decaySpeed) {
				_decaySpeed = decaySpeed * decaySpeed;
			} else {
				_decaySpeed -= decaySpeed / 10f;
			}


			// Calculate and apply Spring force
			if (ForceDirection.sqrMagnitude > Threshold) {
				_forceOfSpring = ForceDirection * _SpringModifier * ((_decaySpeed / 2f) + 0.5f);
			} else {
				_forceOfSpring = ForceDirection * _SpringModifier;
			}

			rigid.AddForceAtPosition (_forceOfSpring, transform.position);


			//calculate damping
			rigid.velocity -=  rigid.velocity * decaySpeed;
		}
	}

	void Update()
	{
		if (Input.GetMouseButtonDown (0)) 
		{
			//cast a ray
			Physics.Raycast (Camera.main.ScreenPointToRay (Input.mousePosition), out hit, 6000f);
			//Click on inital position
			if (hit.collider != null && hit.collider.tag == "Dragable") 
			{
				Debug.Log (hit.collider.tag);
				rigid.isKinematic = true;
				actOnce = false;
				_dragState = DragState.OnDrag;
				transform.position = new Vector3 (hit.point.x, hit.point.y, transform.position.z);
			}
			return;
		}

		if (_dragState == DragState.OnDrag) 
		{
			Physics.Raycast (Camera.main.ScreenPointToRay (Input.mousePosition), out hit, 6000f);

			if (hit.collider != null && hit.collider.tag == "HitWall") 
			{
				if (hit.point.x > (Anchor.position.x - 0.3f)) {
					transform.position = Anchor.position;
				} else {
					float range = (hit.point  - Anchor.position).magnitude;
					if (range > Range) {
						//Clamp the radius of spring
						float x1 = Range * (hit.point  - Anchor.position).x / range;
						float y1 = (Range / range) * (hit.point  - Anchor.position).y;
						Debug.Log (x1+"  "+y1);
						transform.position = new Vector3 (Anchor.position.x + x1, Anchor.position.y + y1, transform.position.z);
					} else {
						transform.position = new Vector3 (hit.point.x, hit.point.y, transform.position.z);
					}
				}
			}

			_SpringModifier = SpringModifier;
			_decaySpeed = 1f;
		}



		if (Input.GetMouseButtonUp (0)) 
		{
			_dragState = DragState.Idle;
			OnForceStart ();
		}

	}
}
