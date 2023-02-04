using System.Collections;
using UnityEngine;
namespace Wrecking_Clone.GamePlay
{
    public class Enemy : MonoBehaviour
    {
        Transform navMeshAgentTansform;
        AiEnemy aiEnemy;
        Rigidbody m_rigidBody;
        private const string Ball = "Ball";
        ContactPoint cp;
        Vector3 hitForce = Vector3.zero;
        void Start()
        {
            m_rigidBody = GetComponent<Rigidbody>();
            transform.SetAsFirstSibling();//make this gameobject first siblings of parent
            navMeshAgentTansform = transform.parent.GetChild(1);//get second siblings which is not this gameobject
            aiEnemy = navMeshAgentTansform.gameObject.GetComponent<AiEnemy>();
            Debug.Log(navMeshAgentTansform.name);
        }

        void Update()
        {
            if (navMeshAgentTansform.gameObject.activeInHierarchy)
            {
                Vector3 position = new Vector3(navMeshAgentTansform.position.x, transform.position.y, navMeshAgentTansform.position.z);
                Quaternion rotation = Quaternion.Euler(navMeshAgentTansform.rotation.x, navMeshAgentTansform.rotation.y, navMeshAgentTansform.rotation.z);
                transform.SetPositionAndRotation(position, rotation);
            }
        }
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag(Ball))
            {
                cp = collision.GetContact(0);
                navMeshAgentTansform.gameObject.SetActive(false);
                hitForce = cp.normal * 100 + new Vector3(0, 1000, 0);
                m_rigidBody.AddRelativeForce(hitForce);
                StartCoroutine(Hit());
            }
        }
        private IEnumerator Hit()
        {
            yield return new WaitForSeconds(1);
            while (!aiEnemy.walkPointSet)
            {
                yield return new WaitForSeconds(1);
                aiEnemy.CheckIfGrounded();
            }
            Vector3 position = new Vector3(transform.position.x, navMeshAgentTansform.position.y, transform.position.z);
            Quaternion rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.rotation.z);
            navMeshAgentTansform.SetPositionAndRotation(position, rotation);

            yield return new WaitForSeconds(0.2f);
            navMeshAgentTansform.gameObject.SetActive(true);
        }
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawRay(transform.position, hitForce);
        }
    }
}