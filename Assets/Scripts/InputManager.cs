using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour {

	private const int GroundLayerNumber = 8;
	
	public event Action<Vector3> OnTouchEnded;
	public event Action<Vector3> OnTouchPositionChanged;

	private Camera _mainCamera;
	
	private Inputs _inputs;

	private Vector3 _currentTouchPosition;
	private Vector2 _currentScreenPosition;
	
	private void Awake() {
		_inputs = new Inputs();
		_mainCamera = Camera.main;
	}

	private void OnEnable() {
		_inputs.Enable();
	}

	private void Start() {
		_inputs.Ball.TouchPosition.performed += ChangeTouchPosition;
		_inputs.Ball.TouchRelease.performed += EndTouch;
	}
	
	private void EndTouch(InputAction.CallbackContext ctx) {
		OnTouchEnded?.Invoke(_currentTouchPosition);
	}

	private void ChangeTouchPosition(InputAction.CallbackContext ctx) {
		_currentScreenPosition = ctx.ReadValue<Vector2>();
		Vector3 worldPosition = ScreenToWorldPosition(_currentScreenPosition);
		_currentTouchPosition = worldPosition;
		OnTouchPositionChanged?.Invoke(worldPosition);
	}
	
	private Vector3 ScreenToWorldPosition(Vector2 screenPosition) {
		Ray ray = _mainCamera.ScreenPointToRay(screenPosition);
		LayerMask groundLayer = 1 << GroundLayerNumber;
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit, 30f, groundLayer)) 
			return hit.point - Vector3.up * hit.point.y;
		return Vector3.zero;
	}

	private void OnDisable() {
		_inputs.Disable();
	}

}