using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Wrecking_Clone.GamePlay
{
    public class Car : MonoBehaviour
    {
        [SerializeField] private CarSettings carSettings;

        private ICarInput carInput;
        private CarMotor carMotor;
        private void Awake()
        {
            carInput = carSettings.UseAi ? new AiInput() as ICarInput : new ControllerInput();
            carMotor = new CarMotor(carInput, transform, carSettings);
        }

        
        private void Update()
        {
            carInput.ReadInput();
            carMotor.Tick();
        }

        private class CarMotor
        {
            private readonly ICarInput carInput;
            private readonly Transform transform;
            private readonly CarSettings carSettings;

            private float moveSpeed = 10f;
            private float turnSpeed = 300f;
            public CarMotor(ICarInput carInput, Transform transform, CarSettings carSettings)
            {
                this.carInput = carInput;
                this.transform = transform;
                this.carSettings = carSettings;

            }
            public void Tick()
            {
                transform.Rotate(Vector3.up * carInput.Rotation * Time.deltaTime * carSettings.TurnSpeed);
                transform.Translate(Vector3.right * Time.deltaTime * carSettings.MoveSpeed * carInput.Thrust);

               // transform.position += transform.right * carInput.Thrust * Time.deltaTime * carSettings.MoveSpeed;
            }
        }
    }

    internal class ControllerInput : ICarInput
    {
        public float Rotation { get; private set; }

        public float Thrust { get; private set; }

        public void ReadInput()
        {
            Rotation = Input.GetAxis("Horizontal");
            Thrust = Input.GetAxis("Vertical");
        }
    }

    internal class AiInput : ICarInput
    {
        public float Rotation { get; private set; }

        public float Thrust { get; private set; }

        public void ReadInput()
        {
            Rotation = UnityEngine.Random.Range(-1f, 1f);
            Thrust = UnityEngine.Random.Range(0f, 1f);
        }
    }
    internal interface ICarInput
    {
        void ReadInput();
        float Rotation { get; }
        float Thrust { get; }
    }
}