using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShakeManager : MonoBehaviour
{
    public static CameraShakeManager Instance;
    [SerializeField] const float globalShakeForce = 1.0f;
    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    // Update is called once per frame
    public void CameraShake(CinemachineImpulseSource impulseSource, float force = globalShakeForce)
    {
        impulseSource.GenerateImpulseWithForce(force);
    }
}
