using System;
using UnityEngine;

public class CoinCollector : MonoBehaviour {

	public event Action OnCoinCollected;
	
	private const int CoinsLayerNumber = 7;
	
	private void OnTriggerEnter(Collider other) {
		if (other.gameObject.layer == CoinsLayerNumber)
			CollectCoin(other.GetComponent<Coin>());
	}

	private void CollectCoin(Coin coin) {
		OnCoinCollected?.Invoke();
		coin.CollectCoin();
	}

}