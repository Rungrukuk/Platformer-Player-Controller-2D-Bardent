using UnityEngine;

namespace _Scripts.Core.CoreComponents
{
    public class CoreComponent : MonoBehaviour, ILogicUpdate
    {
        protected Core core;
        protected Movement Movement => movement ? movement : core.GetCoreComponent(ref movement);

        private Movement movement;

        private CollisionSenses collisionSenses;
        protected CollisionSenses CollisionSenses => collisionSenses ? collisionSenses : core.GetCoreComponent(ref collisionSenses);
        private Stats stats;
        protected Stats Stats => stats ? stats : core.GetCoreComponent(ref stats);

        protected virtual void Awake()
        {
            if (!transform.parent.TryGetComponent(out core))
                Debug.LogError("There is no core on the parent");
            core.AddComponent(this);
        }
        public virtual void LogicUpdate()
        {

        }
    }
}
