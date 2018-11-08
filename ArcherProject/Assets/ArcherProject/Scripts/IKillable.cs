using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IKillable
{
    void Kill();
}

public interface IDamagable
{
    void TakeDamage(float amount);
}

public interface IInteractableplatform
{
    void MovePlatform();
}