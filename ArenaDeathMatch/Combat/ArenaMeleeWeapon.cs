using UnityEngine;

namespace ArenaDeathMatch.Combat
{
    public abstract class MeleeWeapon : MonoBehaviour
    {
        public float Damage { get; protected set; }
        public float AttackSpeed { get; protected set; }
        public float BlockStrength { get; protected set; }

        protected virtual void Awake()
        {
            Damage = 0f;
            AttackSpeed = 0f;
            BlockStrength = 0f;
        }

        public abstract void Swing();

        public virtual void Block()
        {
            Debug.Log($"{GetType().Name} is blocking with strength: {BlockStrength}");
        }

        public abstract void SpecialAttack();
    }

    public class Sword : MeleeWeapon
    {
        protected override void Awake()
        {
            base.Awake();
            Damage = 25f;
            AttackSpeed = 1.2f;
            BlockStrength = 15f;
        }

        public override void Swing()
        {
            Debug.Log("Swinging the Sword with speed: " + AttackSpeed);
        }

        public override void SpecialAttack()
        {
            Debug.Log("Performing a Sword special attack!");
        }
    }

    public class Axe : MeleeWeapon
    {
        protected override void Awake()
        {
            base.Awake();
            Damage = 35f;
            AttackSpeed = 0.8f;
            BlockStrength = 10f;
        }

        public override void Swing()
        {
            Debug.Log("Swinging the Axe with speed: " + AttackSpeed);
        }

        public override void SpecialAttack()
        {
            Debug.Log("Performing an Axe special attack!");
        }
    }

    public class Hammer : MeleeWeapon
    {
        protected override void Awake()
        {
            base.Awake();
            Damage = 40f;
            AttackSpeed = 0.6f;
            BlockStrength = 20f;
        }

        public override void Swing()
        {
            Debug.Log("Swinging the Hammer with speed: " + AttackSpeed);
        }

        public override void SpecialAttack()
        {
            Debug.Log("Performing a Hammer special attack!");
        }
    }
}
