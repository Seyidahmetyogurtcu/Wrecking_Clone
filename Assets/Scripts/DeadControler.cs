using UnityEngine;
using Wrecking_Clone.GameMechanic;

public class DeadControler : MonoBehaviour
{
    private const string Enemy = "Enemy";
    private const string Water = "Water";
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Water))
        {
            if (!gameObject.CompareTag(Enemy))
            {
                gameObject.GetComponent<IDieable>().Die();
            }
            else
            {
                transform.parent.GetComponent<IDieable>().Die();
            }
        }
    }
}
