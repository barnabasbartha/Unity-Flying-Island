using UnityEngine;

public class RotateWheel : MonoBehaviour {
   public float speed;

   private void Update() {
      gameObject.transform.Rotate(Vector3.forward * (speed * Time.deltaTime));
   }
}
