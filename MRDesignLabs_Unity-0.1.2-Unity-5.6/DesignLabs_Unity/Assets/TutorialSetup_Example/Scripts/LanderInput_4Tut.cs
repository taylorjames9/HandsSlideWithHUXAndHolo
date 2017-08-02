
using UnityEngine;
using HUX.Utility;

namespace MRDL
{

    public class LanderInput_4Tut : Singleton<LanderInput_4Tut>
    {
        [SerializeField]
        [Tooltip("Specifies the InputType")]
        private InputTypeEnum inputType = InputTypeEnum.Hololens;

        [Tooltip("Attach script for right hand")]
        public LocalHandInput RightHandInput;

        [Tooltip("Attach script for left hand")]
        public LocalHandInput LeftHandInput;

        [Tooltip("Attach TutorialManager (if necessary)")]
        public TutorialManager_4Tut TutorialManager;


        public enum InputTypeEnum
        {
            Gamepad,
            Hololens,
            Oasis,
        }

        public InputTypeEnum InputType
        {
            get
            {
                return inputType;
            }
            set
            {
                inputType = value;
            }
        }

        public void Start()
        {
            inputType = InputTypeEnum.Hololens;
        }

        /// <summary>
        /// Registers lander input and moves lander according to hand or keyboard input
        /// </summary>
        private void Update()
        {
            switch (this.InputType)
            {
                case InputTypeEnum.Hololens:
                    if (InputSources.Instance.hands.IsHandVisible(InputSourceHands.HandednessEnum.Left))
                    {
                        if (LeftHandInput.Pressed)
                        {
                            Debug.Log("Left hand pressed.");
                            MoveLanderUp();
                        }
                    }
                    if (InputSources.Instance.hands.IsHandVisible(InputSourceHands.HandednessEnum.Right))
                    {
                        if (RightHandInput.Pressed)
                        {
                            Debug.Log("Right hand pressed.");
                            MoveLanderDown();
                        }
                    }
                    break;
                case InputTypeEnum.Oasis:

                    break;
                case InputTypeEnum.Gamepad:

                    break;
            }
            //use keyboard commands instead of hand input for testing the scene in Unity Editor
#if UNITY_EDITOR
            if (Input.GetKey(KeyCode.LeftBracket))
            {
                MoveLanderDown();
            }
            if (Input.GetKey(KeyCode.RightBracket))
            {
                MoveLanderUp();
            }
#endif
        }
        /// <summary>
        /// Moves the lander up when correct input is received
        /// </summary>
        void MoveLanderUp()
        {
            transform.Translate(Vector3.up * Time.deltaTime / 10.0f);
            TutorialManager.Progress += 0.1f;
        }
        /// <summary>
        /// Moves the lander down when correct input is received
        /// </summary>
        void MoveLanderDown()
        {
            transform.Translate(Vector3.down * Time.deltaTime / 10.0f);
            TutorialManager.Progress += 0.1f;
        }
    }
}


