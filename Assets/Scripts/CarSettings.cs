using UnityEngine;
namespace Wrecking_Clone.GamePlay
{
    [CreateAssetMenu(menuName ="Car/Settings",fileName ="CarData")]
    class CarSettings : ScriptableObject
    {
        [SerializeField] private float moveSpeed = 10.0f;
        [SerializeField] private float turnSpeed = 300.0f;
        [SerializeField] private bool useAi=false;

        public float MoveSpeed { get { return moveSpeed; } }
        public float TurnSpeed { get { return turnSpeed; } }
        public bool UseAi { get { return useAi; } }
    }
}