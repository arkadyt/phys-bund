using PlayerControllers;
using System;
using UnityEngine;
namespace PlayerControllers {
	[Serializable]
	public class CrouchController {
		public float m_CrouchFactor = 0.5f;
		public float m_CrouchTime = 0.5f;

		[HideInInspector] public bool m_IsCrouching = false;
		private Vector3 mFpsCamPosition;
		private Vector3 mCharCtrlCenter;
		private float mCharCtrlHeight;

		private ActorController actorCtrl;

		public void Init(ActorController pActorCtrl) {
			actorCtrl = pActorCtrl;

			mFpsCamPosition = actorCtrl.m_CameraController.m_FpsCamera.transform.localPosition;
			mCharCtrlCenter = actorCtrl.charCtrl.center;
			mCharCtrlHeight = actorCtrl.charCtrl.height;
		}
		public void performCrouch() {
			if(actorCtrl.charCtrl.isGrounded && actorCtrl.m_MovementController.m_CrouchEnabled)
				doCrouching(actorCtrl.iCtrl.CrouchIsPressed());
		}
		private void doCrouching(bool pCrouching) {
			Vector3 targetFpsCamPosition = mFpsCamPosition * (pCrouching ?  m_CrouchFactor : 1);
			Vector3 targetCharCtrlCenter = mCharCtrlCenter * (pCrouching ?  m_CrouchFactor : 1);
			float targetCharCtrlHeight = mCharCtrlHeight * (pCrouching ? m_CrouchFactor : 1);

			actorCtrl.m_CameraController.m_FpsCamera.transform.localPosition = Vector3.Lerp(actorCtrl.m_CameraController.m_FpsCamera.transform.localPosition, targetFpsCamPosition, Time.fixedDeltaTime / m_CrouchTime);
			actorCtrl.charCtrl.center = Vector3.Lerp(actorCtrl.charCtrl.center, targetCharCtrlCenter, Time.fixedDeltaTime / m_CrouchTime);
			actorCtrl.charCtrl.height = Mathf.Lerp(actorCtrl.charCtrl.height, targetCharCtrlHeight, Time.fixedDeltaTime / m_CrouchTime);

			m_IsCrouching = actorCtrl.m_CameraController.m_FpsCamera.transform.localPosition.AlmostEquals(mFpsCamPosition * m_CrouchFactor, 0.1f);
		}
	}
}