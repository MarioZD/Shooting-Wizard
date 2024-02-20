using System.Xml.Serialization;
using UnityEngine;

public interface IDamagable
{    
    float Health { get; set; }
    void Damage(float DamageAmount);
}

public interface IInteractable
{
    void Interact();
}
