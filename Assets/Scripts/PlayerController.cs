using UnityEngine;
using Wrecking_Clone.Core;
using Wrecking_Clone.GameMechanic;

namespace Wrecking_Clone.GamePlay
{
    public class PlayerController : MonoBehaviour, IDieable
    {
        public float speed = 3;
        public float turnSpeed = 20;
        public float horizontalInput;
        public float forwardInput;

        public static PlayerController instance;
        private void Awake()

        {
            instance = this;
        }
        private bool isDeath = false;
        public void Die()
        {
            if (!isDeath)
            {
                GameManager.instance.Lose();
                Destroy(this.gameObject, 3f);
                isDeath = true;
            }
        }
        void Start()
        {
            PlayerInput.ResetHorizontalInput();
            isDeath = false;
        }

        // Update is called once per frame
        void Update()
        {
            if (GameManager.isGamePlay)
            {
#if UNITY_EDITOR_64
                //test inputs
                horizontalInput = Input.GetAxis("Horizontal");
                forwardInput = Input.GetAxis("Vertical");
                //model's front is in x axis,it measn Vector3.right is front
                transform.position += transform.right * Time.deltaTime * speed * forwardInput;
                transform.Rotate(Vector3.up, turnSpeed * Time.deltaTime * horizontalInput);
#endif

                //#if UNITY_ANDROID && !UNITY_EDITOR_64
                //mobil inputs              
                horizontalInput = PlayerInput.GetHorizontalInput();
                forwardInput = 1;
                //model's front is in x axis,it measn Vector3.right is front
                transform.position += transform.right * Time.deltaTime * speed * forwardInput;
                transform.Rotate(Vector3.up, turnSpeed * Time.deltaTime * horizontalInput);
                //#endif
            }
        }

    }
}