using UnityEngine;

namespace KenTank
{
    namespace LookAt2D
    {
        public class LookAt2D
        {
            public static void LookAt(Transform self, Vector3 target, float offset = 0)
            {
                Vector2 pos = new Vector3(target.x, target.y, self.position.z) - self.position;
                float rot_z = Mathf.Atan2(pos.y, pos.x) * Mathf.Rad2Deg;
                self.rotation = Quaternion.Euler(0f, 0f, rot_z + offset);
            }
            public static void LookAt(Transform self, Transform target, float offset = 0)
            {
                Vector2 pos = new Vector3(target.position.x, target.position.y, self.position.z) - self.position;
                float rot_z = Mathf.Atan2(pos.y, pos.x) * Mathf.Rad2Deg;
                self.rotation = Quaternion.Euler(0f, 0f, rot_z + offset);
            }
        }
    }
}
