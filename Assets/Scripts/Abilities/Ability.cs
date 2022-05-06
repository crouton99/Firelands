using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : ScriptableObject
{
    
    public new string name;
    public float cdTime;
    public float duration;

    public virtual void Activate(GameObject parent, CameraController cam, HealthDisplay livesBar) {}

    public virtual void Deactivate(GameObject parent, CameraController cam, HealthDisplay livesBar) {}
}
