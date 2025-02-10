using UnityEngine;

namespace Utilities
{
    public static class GraduallyDifficult
    {
        private const float SECONDS_TO_DIFFICULT = 60f;

        public static float GetDifficultPercent()
        {
            Debug.Log("Level loader: "+ Time.timeSinceLevelLoad);
            return Mathf.Clamp01(Time.timeSinceLevelLoad / SECONDS_TO_DIFFICULT);
        }
    }
}