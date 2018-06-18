using Action;
using System;
using System.Collections;
using UnityEngine;

namespace PlayerControllers {
    [Serializable]
    public class ActionController {
        [HideInInspector] public ActionObject m_ActionObject;
        public ActorActionPanels m_ActorActionPanels;
        public float m_SmoothTime = 1;
        [HideInInspector] public bool isInAction = false;
        private ActorController actorCtrl;

        public void Init(ActorController pActorCtrl) {
            actorCtrl = pActorCtrl;
        }
        public void performActionCheck () {
            if (actorCtrl.iCtrl.isActionPressed()) {
                if (m_ActionObject != null) {
                    m_ActionObject.enterAction();
                }
            }
        }
    }
}
