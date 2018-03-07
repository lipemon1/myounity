using UnityEngine;

namespace Model
{
    [System.Serializable]
    public class Food
    {
        public int Life = 100;
        public float MoveSpeed = 10;
        public float RotateSlerpSpeed = 12;
        public int Damage = 30;
        public float AttackSpeed = 1;
        public float FireRate = 1.5f;
    }
}
