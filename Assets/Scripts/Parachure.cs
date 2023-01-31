using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wrecking_Clone.GamePlay
{
    public class Parachure : MonoBehaviour
    {
        [Range(0.1f, 5.1f)] [SerializeField] private float fallSpeed = 1;
        [HideInInspector] public const string Player = "Player";
        [HideInInspector] public const string Arena = "Arena";
        Rigidbody m_rigidBody;
        private bool isFalling=true;

        void Start()
        {
            m_rigidBody = GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        void Update()
        {
            if (isFalling)
            {
                SoarDown();
            }
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(Player))
            {
                Destroy(transform.gameObject);
                //other.GetComponent<Player>().BoxCollect();
                Debug.Log("Collected");
            }
            else if (other.CompareTag(Arena) && transform.childCount == 1)
            {
                isFalling = false;
                Destroy(transform.GetChild(0).gameObject);

            }

        }
        private void SoarDown()
        {
            //model has -90 x rotation so I use Vector3.back instead of Vector3.down
            transform.Translate(Vector3.down * Time.deltaTime * fallSpeed, Space.World);
        }
    }
}