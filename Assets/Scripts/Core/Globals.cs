using UnityEngine;

namespace Core
{
    public static class Globals
    {
        public static void NullAction()
        {
            //Intentionally left blank
        }
        public static bool CheckIfVectorsAreEqual(Vector3 vectorOne, Vector3 vectorTwo)
        {
            return (Mathf.Approximately(vectorTwo.x, vectorOne.x) &&
                    Mathf.Approximately(vectorTwo.y, vectorOne.y) &&
                    Mathf.Approximately(vectorTwo.z, vectorOne.z));
        }
    }
}