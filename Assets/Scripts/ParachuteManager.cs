using UnityEngine;
namespace Wrecking_Clone.GameMechanic
{
    public class ParachuteManager : MonoBehaviour
    {
        public GameObject parachurePrefab;
        float resetTime = 0;
        float delay = 5;
        void Start()
        {

        }

        private void CallNewParachute()
        {
            Instantiate(parachurePrefab, transform.position + RandomPosition(), Quaternion.Euler(-90, 0, 0), transform);
        }

        private Vector3 RandomPosition()
        {
            float x = UnityEngine.Random.Range(-25f, 25f);
            float y = 40.0f;
            float z = UnityEngine.Random.Range(-35f, 35f);
            return new Vector3(x, y, z);
        }

        void Update()
        {
            if (resetTime < Time.timeSinceLevelLoad)
            {
                resetTime = Time.timeSinceLevelLoad + delay;

                ; CallNewParachute();

            }
        }
    }
}