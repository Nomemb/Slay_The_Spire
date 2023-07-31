using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDebuffable
{
    public void Debuff(GameObject target = null);
}
