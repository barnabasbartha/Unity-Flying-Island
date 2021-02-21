using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Random = UnityEngine.Random;

public class CloudGenerator : MonoBehaviour {
   public int size;
   public float positionScatter;
   public GameObject cloudPrefab;
   public float scaleDuration;
   public float onDuration;
   public float offDuration;
   public float durationOffsetPerCloud;

   private List<GameObject> clouds;

   private void Start() {
      clouds = new List<GameObject>();

      for (var n = 0; n < size; n++) {
         var cloud = Instantiate(cloudPrefab, new Vector3(), Quaternion.identity);
         InitPosition(cloud, Rand(), Rand(), Rand());
         InitTween(cloud, n);
         clouds.Add(cloud);
      }
   }

   private void InitPosition(GameObject cloud, float x, float y, float z) {
      var thisTransform = transform;
      cloud.transform.parent = thisTransform;
      var position = thisTransform.position;
      cloud.transform.position = new Vector3(
         position.x + x * positionScatter / 2,
         position.y + y * positionScatter / 6,
         position.z + z * positionScatter / 2
      );
   }

   private void InitTween(GameObject cloud, float n, float delay = 0) {
      cloud.transform.localScale = Vector3.zero;
      cloud.transform.DOScale(Vector3.one, scaleDuration)
         .SetDelay(delay > 0 ? delay : durationOffsetPerCloud * n);
      cloud.transform.DOScale(Vector3.zero, scaleDuration)
         .SetDelay(delay > 0 ? delay + onDuration + scaleDuration : durationOffsetPerCloud * n)
         .OnComplete(() => { InitTween(cloud, n, offDuration); });
   }

   private static float Rand() {
      return Random.Range(-1f, 1f);
   }
}
