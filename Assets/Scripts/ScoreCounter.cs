using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class ScoreCounter : MonoBehaviour {

	[SerializeField] private TextMeshProUGUI _counterText;
	[SerializeField] private CoinCollector _coinCollector;

	[Tooltip("Time in seconds to add scores")] 
	[SerializeField] private float _scoreAddTime;
	
	[Tooltip("Score bust by collecting one coin")]
	[SerializeField] private int _scoreByCoin;

	private int _currentScore;
	private IEnumerator _addCoinRoutine;

	private void OnEnable() {
		_coinCollector.OnCoinCollected += AddCoin;
		_addCoinRoutine = AddCoinRoutine();
	}

	private void AddCoin() {
		_currentScore += _scoreByCoin;
		StartCoroutine(_addCoinRoutine);
	}

	private IEnumerator AddCoinRoutine() {
		float addScoreDelay = _scoreAddTime / _scoreByCoin;
		int textScore = 0;
		while (true) {
			while (textScore < _currentScore) {
				textScore++;
				ChangeScoreText(textScore);
				yield return new WaitForSeconds(addScoreDelay);
			}
			StopCoroutine(_addCoinRoutine);
			yield return new WaitForSeconds(addScoreDelay);
		}
		
		
	}

	private void ChangeScoreText(int score) {
		_counterText.text = score.ToString();
	}

	private void OnDisable() {
		_coinCollector.OnCoinCollected -= AddCoin;
	}

}