using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Wrecking_Clone.GameMechanic
{
    public class ArenaManager : MonoBehaviour
    {
        private const int arenaPieceCount = 5; //just there is 1 round piece left
        [SerializeField] public PieceArray[] arenaPiecesArray = new PieceArray[arenaPieceCount];
        private bool isPieceLeft = true;
        public NavMeshSurface navMeshSurface;

        private int listIndex = 0;
        public int ListIndex
        {
            get { return listIndex; }
            set
            {
                if (listIndex > arenaPieceCount)
                {
                    listIndex = arenaPieceCount;
                }
                listIndex = value;
            }
        }

        private void Start()
        {
            DestructHindmostPiece();
        }
        private void DestructHindmostPiece()
        {
            try
            {
                if (isPieceLeft)
                {
                    StartCoroutine(FloatPieces());
                    StartCoroutine(DestructorTimer());
                }
            }
            catch (System.Exception e)
            {
                Debug.LogWarning("ListofList Error: " + e.Message);
            }

        }
        private IEnumerator FloatPieces()
        {
            foreach (GameObject piece in arenaPiecesArray[ListIndex].pieces)
            {
                for (int i = 0; i < 20; i++)
                {
                    yield return new WaitForSeconds(0.005f);
                    piece.transform.Translate(Vector3.back * 0.5f);
                }
            }
        }
        private IEnumerator DestructorTimer()
        {
            navMeshSurface.BuildNavMesh();
            yield return new WaitForSeconds(20.0f);

            ListIndex++;
            if (ListIndex >= 5)
            {
                isPieceLeft = false;
            }
            DestructHindmostPiece();
        }

        [System.Serializable]
        public class PieceArray
        {
            public List<GameObject> pieces = new();
        }
    }
}