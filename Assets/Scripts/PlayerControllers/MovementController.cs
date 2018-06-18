using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Assertions.Comparers;

namespace PlayerControllers {
	[Serializable]
	public class MovementController {
		[SerializeField] public StepController m_StepController = new StepController();
		[SerializeField] public CrouchController m_CrouchController = new CrouchController();
		[SerializeField] public bool m_CrouchEnabled, m_JumpEnabled, m_SprintEnabled;
		[SerializeField] public float m_CrouchSpeed, m_WalkSpeed, m_SprintSpeed;
		[SerializeField] private float m_JumpSpeed;
		[SerializeField] private float m_StickToGroundForce;
		[SerializeField] private float m_GravityMultiplier;

		private ActorController actorCtrl;

		private CollisionFlags mCollisionFlags;
		private bool mPreviouslyGrounded = true;

		public void Init(ActorController pActorCtrl) {
			actorCtrl = pActorCtrl;
			m_StepController.Init(pActorCtrl);
			m_CrouchController.Init(pActorCtrl);
		}
		public void performMovement() {
			Vector3 moveDirection = getHorizontalMoveDir() + getVerticalMoveDir();

			mPreviouslyGrounded = actorCtrl.charCtrl.isGrounded;// Must be before CharacterController.Move
			mCollisionFlags = actorCtrl.charCtrl.Move(moveDirection * Time.fixedDeltaTime);

			m_CrouchController.performCrouch();
			m_StepController.progressStepCycle();
		}
		private Vector3 getHorizontalMoveDir() {
			Vector3 xMove = actorCtrl.transform.right * actorCtrl.iCtrl.getInput().x;
			Vector3 yMove = actorCtrl.transform.forward * actorCtrl.iCtrl.getInput().y;
			Vector3 desiredMove = xMove + yMove;

			// get a normal for the surface that is being touched to move along it
			RaycastHit hitInfo;
			Physics.SphereCast(actorCtrl.transform.position, actorCtrl.charCtrl.radius, Vector3.down, out hitInfo, actorCtrl.charCtrl.height / 2f);
			desiredMove = Vector3.ProjectOnPlane(desiredMove, hitInfo.normal);

			Vector3 moveDirection = new Vector3();
			moveDirection.x = desiredMove.x * actorCtrl.iCtrl.getInputMaxSpeed();
			moveDirection.z = desiredMove.z * actorCtrl.iCtrl.getInputMaxSpeed();
			return  moveDirection;
		}
		private Vector3 getVerticalMoveDir() {
			float y;
			if (actorCtrl.charCtrl.isGrounded) {
				if (!mPreviouslyGrounded)// Just Landed
					performLanding();
				if (actorCtrl.iCtrl.JumpIsPressed() && !actorCtrl.m_MovementController.m_CrouchController.m_IsCrouching)
					performJump(out y);
				else
					applyStickToGroundForce(out y);
			} else {
				applyGravity(out y);
			}
			return new Vector3(0,y,0);
		}
		private void applyGravity(out float y) {
			y = actorCtrl.charCtrl.velocity.y + Physics.gravity.y * m_GravityMultiplier * Time.fixedDeltaTime;
		}
		private void applyStickToGroundForce(out float y){
			y = - m_StickToGroundForce;
		}
		private void performJump(out float y) {
			actorCtrl.m_AudioController.playClip(actorCtrl.m_AudioController.m_AudioClips.JumpSound);
			y = m_JumpSpeed;
		}
		private void performLanding() {
			//StartCoroutine(m_JumpBob.DoBobCycle());
			m_StepController.updateNextStep();
			actorCtrl.m_AudioController.playClip(actorCtrl.m_AudioController.m_AudioClips.LandSound);
		}
		public void OnControllerColliderHit(ControllerColliderHit hit) {
			Rigidbody body = hit.collider.attachedRigidbody;
			//don't move the rigidbody if the character is on top of it
			if (mCollisionFlags == CollisionFlags.Below) {
				return;
			}
			if (body == null || body.isKinematic) {
				return;
			}
			body.AddForceAtPosition(actorCtrl.charCtrl.velocity * 0.1f, hit.point, ForceMode.Impulse);
		}
		/*public float getHorizontalSpeed(){
			return Vector3.ProjectOnPlane(actorCtrl.charCtrl.velocity, Vector3.up).magnitude;
		}*/
	}

}
