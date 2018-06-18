using Action;
using PlayerControllers;
using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace PlayerControllers {
	[RequireComponent(typeof(CharacterController))]
	public class ActorController : MonoBehaviour {
		[SerializeField] public CameraController m_CameraController = new CameraController();
		[SerializeField] public AudioController m_AudioController = new AudioController();
		[SerializeField] public MovementController m_MovementController = new MovementController();
        [SerializeField] public ActionController m_ActionController = new ActionController();

		[HideInInspector] public CharacterController charCtrl;
		public InputController iCtrl;
		private bool m_IsMoving;
		private bool m_WasMoving;
		private bool smoothZ;

		private void Start() {
			charCtrl = GetComponent<CharacterController>();
			m_CameraController.Init(this);
			m_AudioController.Init(this);
			m_MovementController.Init(this);
            m_ActionController.Init(this);
			iCtrl = new InputController(this);

            iCtrl.showCursor(false);
		}
		private void Update() {
			m_CameraController.performCameraSwing();
			m_CameraController.performMouseInput();
            m_ActionController.performActionCheck();
		}
		private void FixedUpdate() {
			m_MovementController.performMovement();
		}
		private void OnControllerColliderHit(ControllerColliderHit hit) {
			m_MovementController.OnControllerColliderHit(hit);
		}
	}
}