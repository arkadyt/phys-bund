using System;
using UnityEngine;

namespace PlayerControllers {
	[Serializable]
	public class CameraSwing {
		[SerializeField] private float RangeX = 1.0f;
		[SerializeField] private float RangeY = 1.0f;
		[SerializeField] private float RangeZ = 1.0f;
		[SerializeField] private AnimationCurve curveX = new AnimationCurve (new Keyframe (0f, 0f), new Keyframe (0.5f, 1f), new Keyframe (1f, 0f)); // sin curve for head bob
		[SerializeField] private AnimationCurve curveY = new AnimationCurve (new Keyframe (0f, 0f), new Keyframe (0.5f, -1f), new Keyframe (1f, 0f), new Keyframe (1.5f, 1f), new Keyframe (2f, 0f));
		[SerializeField] private AnimationCurve curveZ = new AnimationCurve (new Keyframe (0f, 0f), new Keyframe (0.5f, -1f), new Keyframe(1f, 0f), new Keyframe(1.5f, 1f), new Keyframe(2f, 0f));
		[SerializeField] private AnimationCurve curveAccelerationX = new AnimationCurve (new Keyframe (0f, 0f), new Keyframe (0.5f, -1f), new Keyframe(1f, 0f), new Keyframe(1.5f, 1f), new Keyframe(2f, 0f));
		[SerializeField] private AnimationCurve curveAccelerationY = new AnimationCurve (new Keyframe (0f, 0f), new Keyframe (0.5f, -1f), new Keyframe(1f, 0f), new Keyframe(1.5f, 1f), new Keyframe(2f, 0f));
		[SerializeField] private AnimationCurve curveAccelerationZ = new AnimationCurve (new Keyframe (0f, 0f), new Keyframe (0.5f, -1f), new Keyframe(1f, 0f), new Keyframe(1.5f, 1f), new Keyframe(2f, 0f));
		private float mLastSpeed = 0;
		private float mBaseInterval;
		private Vector3 mCyclePosition;
		private Vector3 mTime;
		private Vector3 mAccelerationTime;
		private ActorController refActor;

		public void Init(ActorController pActor/*, Transform controllerTransform*/, Camera camera, float BaseInterval) {
			refActor = pActor;
			mBaseInterval = BaseInterval;
			mCyclePosition = Vector3.zero;
			mTime = Vector3.zero;
			mAccelerationTime = Vector3.zero;
			mTime.x = curveX[curveX.length - 1].time;
			mTime.y = curveY[curveY.length - 1].time;
			mTime.z = curveZ[curveZ.length - 1].time;
			mAccelerationTime.x = curveAccelerationX[curveAccelerationX.length - 1].time;
			mAccelerationTime.y = curveAccelerationY[curveAccelerationY.length - 1].time;
			mAccelerationTime.z = curveAccelerationZ[curveAccelerationZ.length - 1].time;
		}

		public Vector3 getCameraSwing() {
			//float speed = refActor.m_MovementController.getHorizontalSpeed();
			float speed = refActor.iCtrl.getInputMaxSpeed();
			Vector3 rotation = Vector3.zero;
			if (speed != 0 && mLastSpeed != 0) {// Moving
				progressCycle(speed);
				progressRotation(out rotation, false);
				mLastSpeed = speed;
				clampCycle(mTime);
			} else if (speed == 0 && mLastSpeed != 0) {//stopping
				if (mCyclePosition.AlmostEquals(Vector3.zero, 0.01f)) {
					mLastSpeed = 0;
					mCyclePosition = Vector3.zero;
				} else {
					progressCycle(mLastSpeed);
					progressRotation(out rotation, false);
				}
				clampCycle(mTime);
			} else if (speed != 0 && mLastSpeed == 0) {//Acceleration
				progressCycle(speed);
				progressRotation(out rotation, true);
				//if(mCyclePosition.AlmostEquals())
				//TODO

				//Debug.Log("Acc; "+mCyclePosition.x+ "; "+mCyclePosition.y+"; "+mCyclePosition.z);
				clampCycle(mAccelerationTime);
			} else if (speed == 0 && mLastSpeed == 0) {
				mCyclePosition = Vector3.zero;

			}
			//Debug.Log(rotation.x+ "; "+rotation.y+"; "+rotation.z);
			return rotation;
		}
		private void progressRotation(out Vector3 rotation, bool accelerating){
			rotation.x = ((accelerating ? curveAccelerationX : curveX).Evaluate(mCyclePosition.x) * RangeX);
			rotation.y = ((accelerating ? curveAccelerationY : curveY).Evaluate(mCyclePosition.y) * RangeY);
			rotation.z = ((accelerating ? curveAccelerationZ : curveZ).Evaluate(mCyclePosition.z) * RangeZ);
		}
		private void progressCycle(float speed){
			mCyclePosition.x += (speed * Time.deltaTime) / mBaseInterval;
			mCyclePosition.y += (speed * Time.deltaTime) / mBaseInterval;
			mCyclePosition.z += (speed * Time.deltaTime) / mBaseInterval;
		}
		private void clampCycle(Vector3 time){
			if (mCyclePosition.x > time.x)
				mCyclePosition.x = mCyclePosition.x - time.x;
			if (mCyclePosition.y > time.y)
				mCyclePosition.y = mCyclePosition.y - time.y;
			if (mCyclePosition.z > time.z)
				mCyclePosition.z = mCyclePosition.z - time.z;
		}
		//private bool
		//TODO
	}
}