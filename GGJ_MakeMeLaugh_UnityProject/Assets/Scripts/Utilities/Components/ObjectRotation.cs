using UnityEngine;
using GameJam.Utilities.Attributes;

namespace GameJam.Utilities.Components
{
    public class ObjectRotation : MonoBehaviour
    {
        [Header("Rotation Config:")]
        [SerializeField] private float speed;
        [SerializeField, ShowField(nameof(increment))] private float incrementCap;
        [SerializeField] private bool increment;
        [SerializeField] private bool useUnscaledTime;
        [SerializeField] private bool canRotate;
        [SerializeField] private Space spaceType;

        [SerializeField, HorizontalGroup(50f, 10f, nameof(rotateX), nameof(rotateY), nameof(rotateZ))] private Void rotationGroup;
        [SerializeField, HideInInspector] private bool rotateX, rotateY, rotateZ;

        private float incrementSpeed;
        private float xRotation, yRotation, zRotation;

        void Update()
        {
            if (!canRotate) return;

            var time = useUnscaledTime ? Time.unscaledDeltaTime : Time.deltaTime;

            if (increment)
            {
                if (incrementSpeed < incrementCap)
                {
                    incrementSpeed += speed;
                }
                else if (incrementSpeed > incrementCap)
                {
                    incrementSpeed = incrementCap;
                }

				xRotation = rotateX ? incrementSpeed * time : 0f;
				yRotation = rotateY ? incrementSpeed * time : 0f;
				zRotation = rotateZ ? incrementSpeed * time : 0f;

				transform.Rotate(new Vector3(xRotation, yRotation, zRotation), spaceType);
			}
            else
            {
				xRotation = rotateX ? speed * time : 0f;
				yRotation = rotateY ? speed * time : 0f;
				zRotation = rotateZ ? speed * time : 0f;

				transform.Rotate(new Vector3(xRotation, yRotation, zRotation), spaceType);
			}
        }

        public void SetCanRotate(bool value) => canRotate = value; // Called by event
    }
}
