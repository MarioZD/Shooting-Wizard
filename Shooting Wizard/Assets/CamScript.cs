using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamScript : MonoBehaviour
{

    //Camera Movement
    public static CamScript instance;
    public Transform player;
    Vector3 target, mousePos, shakeOffset;
    Vector3 refVel = Vector3.zero;
    [SerializeField] float cameraDistance = 2.5f;
    public float smoothTime = 0.3f, zStart;

    //Camera Shake
    float shakeMag, shakeTimeEnd;
    Vector3 shakeVector;
    bool shaking;


    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        target = player.position;
        zStart = transform.position.z;
    }

    void Update()
    {
        if (!DialogueManager.isActive)
        {
            mousePos = CaptureMousePos();
            shakeOffset = UpdateShake();
            target = UpdateTargetPos();
            UpdateCameraPosition();
        }
        else
        {
            Vector3 Offset = transform.position - player.position;
            transform.position = player.position + Offset;
        }
    }


    Vector3 CaptureMousePos()
    {
        Vector2 ret = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        ret *= 2;
        ret -= Vector2.one;
        float max = 0.9f;
        if (Mathf.Abs(ret.x) > max || Mathf.Abs(ret.y) > max)
        {
            ret = ret.normalized;
        }
        return ret;
    }

    Vector3 UpdateTargetPos()
    {
        Vector3 mouseOffset = mousePos * cameraDistance;
        Vector3 ret = mouseOffset + player.position;
        ret += shakeOffset;
        ret.z = zStart;
        return ret;
    }

    void UpdateCameraPosition()
    {
        Vector3 tempPos;
        tempPos = Vector3.SmoothDamp(transform.position, target, ref refVel, smoothTime);
        transform.position = tempPos;
    }

    public void Shake(Vector3 direction, float magnitude, float length)
    {
        shaking = true;
        shakeVector = direction;
        shakeMag = magnitude;
        shakeTimeEnd = Time.time + length;
    }

    Vector4 UpdateShake()
    {
        if (!shaking || Time.time > shakeTimeEnd)
        {
            shaking = false;
            return Vector3.zero;
        }
        Vector3 tempOffset = shakeVector;
        tempOffset *= shakeMag;
        return tempOffset;
    }


}
