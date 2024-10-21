using UnityEngine;
using Utilities;

namespace GameMechanic
{
    public class FlyingUp : MonoBehaviour
    {
        [SerializeField]
        private Vector2 speedMinMax;
        [SerializeField]
        private float speed;
        [SerializeField]
        private float visibleThresholdHeight;

        private void Start()
        {
            speed = Mathf.Lerp(speedMinMax.x, speedMinMax.y, GraduallyDifficult.GetDifficultPercent());
            // if (Camera.main != null)
            //     visibleThresholdHeight = -Camera.main.orthographicSize - transform.localScale.y;  
        }

        void Update()
        {            
          transform.Translate(Vector2.up * (speed * Time.deltaTime), Space.Self);
          visibleThresholdHeight -= Time.deltaTime;
          if (!(visibleThresholdHeight < 0)) return; 
          gameObject.SetActive(false);
          visibleThresholdHeight = 5;
        }
    }
}
