using UnityEngine;
using Wrecking_Clone.Core;
using Wrecking_Clone.GameMechanic;
namespace Wrecking_Clone.GamePlay
{
    public class EnemyParent : MonoBehaviour, IDieable
    {
        public void Die()
        {
            try
            {
                bool isDeath = GetComponentInChildren<AiEnemy>().isDeath;
                if (!isDeath)
                {
                    isDeath = true;
                    GameManager.instance.DestroyEnemyParent(this);
                    //Destroy(this.gameObject, 3f);
                }
            }
            catch (System.Exception e)
            {
                Debug.LogWarning(e);
            }
        }
    }
}