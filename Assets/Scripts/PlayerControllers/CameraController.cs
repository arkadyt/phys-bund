using PlayerControllers;
using System;
using UnityEngine;
namespace PlayerControllers {
	[Serializable]
	public class CameraController {
		public Camera m_FpsCamera;
		public Camera m_WeaponCamera;
		public float m_xSensitivity = 2f;
		public float m_ySensitivity = 2f;
		public float m_MinimumX = -86f;
		public float m_MaximumX = 86f;
		public float m_SmoothTime = 10f;
		public bool m_Smooth;
		[SerializeField] private CameraSwing m_CameraSwinging = new CameraSwing();

		private Quaternion m_CharacterTargetRot;
		private Quaternion m_CameraTargetRot;
		private ActorController refActor;

		public void Init(ActorController pActor) {
			refActor = pActor;
			m_CharacterTargetRot = refActor.transform.localRotation;
			m_CameraTargetRot = m_FpsCamera.transform.localRotation;
			m_CameraSwinging.Init(refActor, m_FpsCamera, refActor.m_MovementController.m_StepController.m_StepInterval);
		}

		public void performMouseInput() {
			Vector2 input = refActor.iCtrl.getMouseInput(m_xSensitivity,m_ySensitivity);
			addTargetRotation(input);
			setRotation();
			clampRotationAroundXAxis();
		}
		public void performCameraSwing(){
			addTargetRotation(m_CameraSwinging.getCameraSwing());//m_CharacterController.velocity.magnitude +
			addRotation(m_CameraSwinging.getCameraSwing());
		}
		private void addTargetRotation(Vector3 rotation){
			m_CharacterTargetRot *= Quaternion.Euler(0f, rotation.y, 0f);
			m_CameraTargetRot *= Quaternion.Euler(-rotation.x, 0f, rotation.z);
		}
		private void addRotation(Vector3 rotation){
			refActor.transform.localRotation *= Quaternion.Euler(0f, rotation.y, 0f);
			m_FpsCamera.transform.localRotation *= Quaternion.Euler(rotation.x, 0f, rotation.z);
		}
		private void setRotation(){
			refActor.transform.localRotation = m_Smooth ? Quaternion.Slerp(refActor.transform.localRotation, m_CharacterTargetRot, m_SmoothTime * Time.deltaTime) : m_CharacterTargetRot;
			m_FpsCamera.transform.localRotation = m_Smooth ? Quaternion.Slerp(m_FpsCamera.transform.localRotation, m_CameraTargetRot, m_SmoothTime * Time.deltaTime) : m_CameraTargetRot;
		}
		private void clampRotationAroundXAxis() {
			Quaternion q = m_CameraTargetRot;
			q.x /= q.w;
			q.y /= q.w;
			q.z /= q.w;
			q.w = 1.0f;

			float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan(q.x);
			angleX = Mathf.Clamp(angleX, m_MinimumX, m_MaximumX);
			q.x = Mathf.Tan(0.5f * Mathf.Deg2Rad * angleX);
			m_FpsCamera.transform.localRotation = q;
		}
	}
}
