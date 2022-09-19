using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {

	private static readonly int Collected = Animator.StringToHash("Collected");

	[SerializeField] private List<GameObject> _objectsToDeleteAfterCollect;
	[SerializeField] private ParticleSystem _explosionCircle;
	[SerializeField] private ParticleSystem _explosionCoinParts;
	[SerializeField] private ParticleSystem _explosionLines;

	private Animator _animator;
	private Collider _collider;
	private bool _isCollected;

	private void Awake() {
		_animator = GetComponent<Animator>();
		_collider = GetComponent<Collider>();
	}

	public void CollectCoin() {
		Destroy(_collider);
		_isCollected = true;
		_animator.SetTrigger(Collected);
	}

	private void StartExplosion() {
		_explosionCircle.Play();
		_explosionLines.Play();
	}

	private void DestroyCoin() {
		foreach (var go in _objectsToDeleteAfterCollect) {
			Destroy(go);
		}
		_explosionCoinParts.Play();
		Destroy(gameObject, _explosionCoinParts.main.startLifetime.constantMax);
	}

}