using UnityEngine;
using Unity;
namespace hivebombnetcode
{
    internal class ExplosionHandler
    {
        public static void ExplodeAt(float x, float y, float z, float rand, float knockback, float visible, float dmg, float rad)
        {
            bool v = true;
            bool k = true;
            if (visible == 0)
            {
                v = false;
            }
            if (knockback == 0)
            {
                k = false;
            }
            Landmine.SpawnExplosion(new Vector3(x,y,z), v, 0,rad, (int)dmg, k ? rand : 0);
        }
    }
}