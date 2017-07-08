using UnityEngine;
using UnityEngine.UI;
using VRStandardAssets.Utils;

namespace VRStandardAssets.Examples
{
    public class ButtonFunctionality : MonoBehaviour
    {
        [SerializeField]
        private Color m_NormalColor;
        [SerializeField]
        private Color m_OverColor;
        [SerializeField]
        private Color m_ClickedColor;
        [SerializeField]
        private VRInteractiveItem m_InteractiveItem;
        [SerializeField]
        private Image m_Button;

        public string handlerMethod;
        public ButtonHandler handler;

        private void Awake()
        {
            m_Button.color = m_NormalColor;
        }


        private void OnEnable()
        {
            m_InteractiveItem.OnOver += HandleOver;
            m_InteractiveItem.OnOut += HandleOut;
            m_InteractiveItem.OnClick += HandleClick;
        }


        private void OnDisable()
        {
            m_InteractiveItem.OnOver -= HandleOver;
            m_InteractiveItem.OnOut -= HandleOut;
            m_InteractiveItem.OnClick -= HandleClick;
        }


        //Handle the Over event
        private void HandleOver()
        {
            m_Button.color = m_OverColor;
        }


        //Handle the Out event
        private void HandleOut()
        {
            m_Button.color = m_NormalColor;
        }


        //Handle the Click event
        private void HandleClick()
        {
            m_Button.color = m_ClickedColor;
            handler.DetermineHandler(handlerMethod);
        }        
    }

}