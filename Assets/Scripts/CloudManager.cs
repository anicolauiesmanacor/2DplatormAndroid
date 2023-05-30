using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudManager : MonoBehaviour {
    [SerializeField] private float swingSpeed;
    [SerializeField] private float minSwingSpeed = 0.5f;
    [SerializeField] private float maxSwingSpeed = 2f;
    [SerializeField] private float minSwingAmplitude = 15f;
    [SerializeField] private float maxSwingAmplitude = 45f;
    [SerializeField] private float minShakyMagnitude = 0.05f;
    [SerializeField] private float maxShakyMagnitude = 0.15f;
    [SerializeField] private float minShakySpeed = 1f;
    [SerializeField] private float maxShakySpeed = 3f;

    private Quaternion[] initialRotations;
    private Transform[] childTransforms;
    private float[] swingSpeeds;
    private float[] swingAmplitudes;
    private float[] shakyMagnitudes;
    private float[] shakySpeeds;

    private void Start()
    {
        int childCount = transform.childCount;
        initialRotations = new Quaternion[childCount];
        childTransforms = new Transform[childCount];
        swingSpeeds = new float[childCount];
        swingAmplitudes = new float[childCount];
        shakyMagnitudes = new float[childCount];
        shakySpeeds = new float[childCount];

        for (int i = 0; i < childCount; i++)
        {
            childTransforms[i] = transform.GetChild(i);
            initialRotations[i] = childTransforms[i].localRotation;

            swingSpeeds[i] = Random.Range(minSwingSpeed, maxSwingSpeed);
            swingAmplitudes[i] = Random.Range(minSwingAmplitude, maxSwingAmplitude);
            shakyMagnitudes[i] = Random.Range(minShakyMagnitude, maxShakyMagnitude);
            shakySpeeds[i] = Random.Range(minShakySpeed, maxShakySpeed);
        }
    }

    private void Update()
    {
        for (int i = 0; i < childTransforms.Length; i++)
        {
            Transform childTransform = childTransforms[i];
            Quaternion initialRotation = initialRotations[i];

            float t = Mathf.Sin(Time.time * swingSpeeds[i]) * 0.5f + 0.5f;
            float angle = Mathf.Lerp(-swingAmplitudes[i], swingAmplitudes[i], t);
            Quaternion targetRotation = Quaternion.Euler(0f, 0f, angle);

            float shakyOffset = Mathf.PerlinNoise(Time.time * shakySpeeds[i], i) * 2f - 1f;
            Quaternion shakyRotation = Quaternion.Euler(0f, 0f, shakyOffset * shakyMagnitudes[i]);

            childTransform.localRotation = Quaternion.Slerp(initialRotation * shakyRotation, targetRotation * shakyRotation, t);
        }
    }
}
