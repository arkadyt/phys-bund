using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
namespace PlayerControllers {
    public class InputController {
        private ActorController actorCtrl;

        public InputController(ActorController pActorCtrl) {
            actorCtrl = pActorCtrl;
        }

        public void showCursor(bool show){
            Cursor.visible = show;
        }
        public Vector2 getInput() {
			float horizontal = CrossPlatformInputManager.GetAxis("Horizontal");
            float vertical = CrossPlatformInputManager.GetAxis("Vertical");

            Vector2 input = new Vector2(horizontal, vertical);

            if (input.sqrMagnitude > 1)
                input.Normalize();
            return input;
        }
        public Vector2 getMouseInput(float xSensitivity, float ySensitivity) {
            float yRot = CrossPlatformInputManager.GetAxis("Mouse X") * xSensitivity;
            float xRot = CrossPlatformInputManager.GetAxis("Mouse Y") * ySensitivity;
            if (Input.GetKey(KeyCode.Z)) {
                yRot = xRot = 0;
                showCursor(true);
            } else {
                showCursor(false);
            }
            // XXX Only for Debug
            return new Vector2(xRot, yRot);
        }
        public float getInputMaxSpeed() {
            if (!InputEqualsZero()) {
                if (actorCtrl.m_MovementController.m_CrouchController.m_IsCrouching)
                    return actorCtrl.m_MovementController.m_CrouchSpeed;
                else if (SprintIsPressed() && isMovingForward())
                    return actorCtrl.m_MovementController.m_SprintSpeed;
                else
                    return actorCtrl.m_MovementController.m_WalkSpeed;
            }
            else
                return 0;
        }

        public bool isMovingForward() {
            return getInput().y > 0 && getInput().x == 0;
        }

        public bool InputEqualsZero() {
            return getInput().Equals(Vector2.zero);
        }

        public bool SprintIsPressed() {
            return actorCtrl.m_MovementController.m_SprintEnabled ? CrossPlatformInputManager.GetButton("Sprint") : false;
        }
        public bool JumpIsPressed() {
            return actorCtrl.m_MovementController.m_JumpEnabled ? CrossPlatformInputManager.GetButton("Jump") : false;
        }
        public bool CrouchIsPressed() {
            return actorCtrl.m_MovementController.m_CrouchEnabled ? CrossPlatformInputManager.GetButton("Crouch") : false;
        }
        public bool isActionPressed() {
            return CrossPlatformInputManager.GetButtonDown("Action");
        }
        public bool isMainMenuPressed() {
            return CrossPlatformInputManager.GetButtonDown("MainMenu");
        }
    }
}
