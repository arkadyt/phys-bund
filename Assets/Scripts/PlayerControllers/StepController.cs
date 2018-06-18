using System;
using UnityEngine;

namespace PlayerControllers {
	[Serializable]
	public class StepController {
		[SerializeField] public float m_StepInterval = 6.5f;

		private ActorController actorCtrl;
		private float mStepCycle = 0;
		private float mNextStep = 0;

		public void Init(ActorController pActorCtrl) {
			actorCtrl = pActorCtrl;
		}

		public void progressStepCycle() {
			if (actorCtrl.charCtrl.velocity.sqrMagnitude > 0 && !actorCtrl.iCtrl.InputEqualsZero()) {
				mStepCycle += actorCtrl.charCtrl.velocity.magnitude * 2 * Time.fixedDeltaTime;
			}
			if (mStepCycle > mNextStep) {
				mNextStep = mStepCycle + m_StepInterval;
				if (actorCtrl.charCtrl.isGrounded) {
					actorCtrl.m_AudioController.playClip(actorCtrl.m_AudioController.m_AudioClips.FootstepSounds);
				}
			}
		}
		public void updateNextStep(){
			mNextStep = mStepCycle + .5f;
		}
	}
}