using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UnityStandardAssets.CrossPlatformInput
{
    public class Joystick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
    {
        public int MovementRange = 100;
        public string horizontalAxisName = "Horizontal"; // The name given to the horizontal axis for the cross platform input
        public string verticalAxisName = "Vertical"; // The name given to the vertical axis for the cross platform input

        Vector2 m_StartPos, currentDelta;
        CrossPlatformInputManager.VirtualAxis m_HorizontalVirtualAxis; // Reference to the joystick in the cross platform input
        CrossPlatformInputManager.VirtualAxis m_VerticalVirtualAxis; // Reference to the joystick in the cross platform input

        void OnEnable()
        {
            CreateVirtualAxes();
        }

        void Start()
        {
            m_StartPos = transform.position;
            currentDelta = Vector2.zero;
        }

        void UpdateVirtualAxes(Vector2 value)
        {
            var delta = m_StartPos - value;
            delta.y = -delta.y;
            delta /= MovementRange;

            m_HorizontalVirtualAxis.Update(-delta.x);
            m_VerticalVirtualAxis.Update(delta.y);

        }

        void CreateVirtualAxes()
        {
            m_HorizontalVirtualAxis = new CrossPlatformInputManager.VirtualAxis(horizontalAxisName);
            CrossPlatformInputManager.RegisterVirtualAxis(m_HorizontalVirtualAxis);

            m_VerticalVirtualAxis = new CrossPlatformInputManager.VirtualAxis(verticalAxisName);
            CrossPlatformInputManager.RegisterVirtualAxis(m_VerticalVirtualAxis);
        }


        public void OnDrag(PointerEventData data)
        {
            currentDelta = data.position - m_StartPos;
            Vector2 maxDelta = currentDelta.normalized * MovementRange;

            if (currentDelta.magnitude > maxDelta.magnitude)
                currentDelta = maxDelta;

            transform.position = m_StartPos+currentDelta;
            UpdateVirtualAxes(transform.position);
        }


        public void OnPointerUp(PointerEventData data)
        {
            currentDelta = Vector2.zero;
            transform.position = m_StartPos;
            UpdateVirtualAxes(m_StartPos);
        }


        public void OnPointerDown(PointerEventData data) { }

        void OnDisable()
        {

            m_HorizontalVirtualAxis.Remove();
            m_VerticalVirtualAxis.Remove();

        }
    }
}