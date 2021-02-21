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
   public float moveX;

   private List<GameObject> clouds;

   private void Start() {
      clouds = new List<GameObject>();

      for (var n = 0; n < size; n++) {
         var cloud = Instantiate(cloudPrefab, new Vector3(), Quaternion.identity);
         cloud.transform.parent = transform;
         InitTween(cloud, n);
         clouds.Add(cloud);
      }
   }

   private void InitPosition(GameObject cloud) {
      var position = transform.position;
      cloud.transform.position = new Vector3(
         position.x + Rand() * positionScatter / 2,
         position.y + Rand() * positionScatter / 6,
         position.z + Rand() * positionScatter / 2
      );
   }

   private void InitTween(GameObject cloud, float n, float delay = 0) {
      cloud.transform.localScale = Vector3.zero;
      InitPosition(cloud);
      cloud.transform.DOScale(Vector3.one, scaleDuration)
         .SetLoops(0)
         .SetDelay(delay > 0 ? delay : durationOffsetPerCloud * n);
      cloud.transform.DOMoveX(cloud.transform.position.x + moveX, scaleDuration * 2 + onDuration)
         .SetLoops(0)
         .SetEase(Ease.Linear)
         .SetDelay(delay > 0 ? delay : durationOffsetPerCloud * n);
      cloud.transform.DOScale(Vector3.zero, scaleDuration)
         .SetLoops(0)
         .SetDelay(delay > 0 ? delay + onDuration + scaleDuration : durationOffsetPerCloud * n)
         .OnComplete(() => { InitTween(cloud, n, offDuration); });
   }

   private static float Rand() {
      return Random.Range(-1f, 1f);
   }
}
