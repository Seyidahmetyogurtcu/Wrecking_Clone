using UnityEngine;
namespace Wrecking_Clone.GameMechanic
{
    public class Arena : MonoBehaviour, IDieable
    {
        public void Die()
        {
            Destroy(this.gameObject, 3f);
        }
    }
}