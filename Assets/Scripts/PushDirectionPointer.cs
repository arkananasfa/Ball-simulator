using UnityEngine;

public class PushDirectionPointer : MonoBehaviour {
	
	[SerializeField] private InputManager _inputManager;

	private LineRenderer _lineRenderer;

	private void Awake() {
		_lineRenderer = GetComponent<LineRenderer>();
		HidePointer(Vector2.zero);
	}

	private void OnEnable() {
		_inputManager.OnTouchPositionChanged += ShowPointer;
		_inputManager.OnTouchEnded += HidePointer;
	}

	private void ShowPointer(Vector3 position) {
		_lineRenderer.SetPosition(1, position-transform.position);
	}

	private void HidePointer(Vector3 position) {
		_lineRenderer.SetPosition(1, Vector3.zero);
	}

	private void OnDisable() {
		_inputManager.OnTouchPositionChanged -= ShowPointer;
		_inputManager.OnTouchEnded -= HidePointer;
	}

}