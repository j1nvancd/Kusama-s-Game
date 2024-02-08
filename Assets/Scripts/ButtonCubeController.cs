using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonCubeController : MonoBehaviour
{
    public float buttonExtendAmount = 2f;
    public float targetScaleZ = 35f;
    public GameObject targetCube;
    private int touchCount = 0;

    private Vector3 originalScale;
    private Vector3 originalPosition;

    void Start()
    {
        originalScale = targetCube.transform.localScale;
        originalPosition = targetCube.transform.position;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ToggleCube();
        }
    }

    public void ToggleCube()
    {
        touchCount++;

        if (touchCount % 2 == 1)
        {
            StartCoroutine(ExtendCube());
        }
        else
        {
            StartCoroutine(ShrinkCube());
        }
    }

    IEnumerator ExtendCube()
    {
        Vector3 targetScale = new Vector3(originalScale.x, originalScale.y, targetScaleZ);

        float duration = 1f;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            targetCube.transform.localScale = Vector3.Lerp(originalScale, targetScale, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        targetCube.transform.localScale = targetScale;
    }

    IEnumerator ShrinkCube()
    {
        Vector3 targetScale = originalScale;

        float duration = 1f;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            targetCube.transform.localScale = Vector3.Lerp(targetCube.transform.localScale, targetScale, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        targetCube.transform.localScale = targetScale;

        // Restaurar la posición original
        targetCube.transform.position = originalPosition;
    }
}