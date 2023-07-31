using UnityEngine;

namespace Interface
{
    public interface IBuffable
    {
        void Buff();
        void Buff(GameObject target);
    }
}