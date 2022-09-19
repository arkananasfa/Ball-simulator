using UnityEngine;

public class BallBounceEffector : MonoBehaviour {

	[SerializeField] private BounceParticles _bounceParticles;
	
	private Animator _animator;

	private void Awake() {
		_animator = GetComponent<Animator>();
		_animator.enabled = false;
	}

	public void Bounce(Vector3 normal) {
		transform.forward = -normal;
		_animator.enabled = true;
		BounceParticles particlesObject = Instantiate(_bounceParticles,
			transform.position - normal / 3f,
			Quaternion.identity);
		particlesObject.transform.forward = normal;
		particlesObject.Play();
	}
	
	private void EndCompress() {
		_animator.enabled = false;
	}

}