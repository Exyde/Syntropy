using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLerpToTarget : MonoBehaviour
{
    [SerializeField] Transform _target;

    [SerializeField] float _lerpSpeed = .5f;

    void LateUpdate(){
        if (_target != null){
            Vector3 targetPos = new Vector3(_target.position.x, _target.position.y, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, targetPos, _lerpSpeed * Time.deltaTime);
        }
    }
}
