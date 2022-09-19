using UnityEngine;

public class BounceParticles : MonoBehaviour {

	[SerializeField] private ParticleSystem _dustParticleSystem;
	[SerializeField] private ParticleSystem _forceLinesParticleSystem;

	[SerializeField] private float _timeToDestroy;

	public void Play() {
		_dustParticleSystem.Play();
		_forceLinesParticleSystem.Play();
		Destroy(gameObject, _timeToDestroy);
	}

}