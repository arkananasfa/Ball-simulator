using UnityEngine;

public class BallController : MonoBehaviour {

	private const int WallsLayerNumber = 6;
	
	[SerializeField] private InputManager _inputManager;
	
	[Space(10)]
	
	[Header("Graphics")]
	[SerializeField] private GameObject _grafics;
	[SerializeField] private BallBounceEffector _effector;
	[SerializeField] private float _minVelocityToCompress;
	[SerializeField] private float _rotationSpeed = 45f;

	[Space(10)]
	
	[Header("Speed settings")]
	[SerializeField] private float _maxPushSpeed = 10f;
	[SerializeField] private float _maxSpeed = 25f;
	[SerializeField] private float _bounceVelocityLoss = 0.3f;
	[SerializeField] private float _velocityValueLoss = 0.1f;
	[SerializeField] private float _velocityPercentLoss = 0.15f;
	[SerializeField] private float _newPushVelocityPercentLoss = 0.25f;

	private Vector3 _velocity = Vector3.zero;

	private void OnEnable() {
		_inputManager.OnTouchEnded += Push;
	}

	private void FixedUpdate() {
		transform.Translate(_velocity*Time.fixedDeltaTime, Space.World);
		_velocity -= new Vector3( Mathf.Sign(_velocity.x), 0f, Mathf.Sign(_velocity.z))*_velocityValueLoss*Time.fixedDeltaTime;
		_velocity -= _velocity * _velocityPercentLoss * Time.fixedDeltaTime;
	}

	private void Update() {
		_grafics.transform.Rotate(new Vector3(_velocity.z, 0f, -_velocity.x)*Time.deltaTime*_rotationSpeed, Space.World);
	}

	private void OnCollisionEnter(Collision other) {
		if (other.gameObject.layer == WallsLayerNumber)
			CollisionWithWall(other.GetContact(0).normal);
	}
	
	private void OnCollisionStay(Collision other) {
		if (other.gameObject.layer == WallsLayerNumber)
			transform.Translate(other.GetContact(0).normal*_velocity.magnitude/_maxSpeed/2f); 
	}

	private void CollisionWithWall(Vector3 normal) {
		float dotProduction = _velocity.x * normal.x + _velocity.z * normal.z;
		_velocity = new Vector3(_velocity.x - 2 * normal.x * dotProduction, 0f, _velocity.z - 2 * normal.z * dotProduction);
		_velocity -= _velocity * _bounceVelocityLoss;
		if (_velocity.magnitude > _minVelocityToCompress)
			_effector.Bounce(normal);
		_effector.PlayEffects(normal);
	}

	private void Push(Vector3 touchWorldPosition) {
		_velocity -= _velocity * _newPushVelocityPercentLoss;
		Vector3 currentPosition = transform.position;
		_velocity += new Vector3(touchWorldPosition.x - currentPosition.x, 0f, touchWorldPosition.z-currentPosition.z) * _maxPushSpeed;
		if (_velocity.magnitude > _maxSpeed)
			_velocity = _velocity.normalized * _maxSpeed;
	}

	private void OnDisable() {
		_inputManager.OnTouchEnded -= Push;
	}

}