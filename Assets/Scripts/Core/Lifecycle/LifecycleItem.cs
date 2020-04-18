using System;

namespace KnowCrow.AT.KeepItAlive
{
    public abstract class LifecycleItem : ITick, IDisposable
    {
        public abstract void Tick(float deltaTime);
        public abstract void Dispose();
    }
}