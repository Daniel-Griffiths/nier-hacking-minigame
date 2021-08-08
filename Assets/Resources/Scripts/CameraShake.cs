using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    private Vector3 _originalPos;
    public static CameraShake _instance;

    void Awake()
    {
        _instance = this;
    }

    public static void Shake (float duration, float amount) 
    {
        _instance.StopAllCoroutines();
        _instance.StartCoroutine(_instance.cShake(duration, amount));
    }

    public IEnumerator cShake (float duration, float amount) 
    {
        var _originalPos = transform.localPosition;

        float endTime = Time.time + duration;

        while (Time.time < endTime) {
            transform.localPosition = _originalPos + Random.insideUnitSphere * amount;

            duration -= Time.deltaTime;

            yield return null;
        }

        transform.localPosition = _originalPos;
    }
}