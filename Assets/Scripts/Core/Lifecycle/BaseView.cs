using UnityEngine;

namespace KnowCrow.AT.KeepItAlive
{
    public abstract class BaseView : MonoBehaviour, ITick
    {
        public virtual void Tick(float deltaTime) {}
        public virtual void Dispose() {}
    }
}