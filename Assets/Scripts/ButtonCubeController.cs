using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonCubeController : MonoBehaviour
{
    public float buttonExtendAmount = 35f;
    public GameObject targetCube;
    private bool isActivated = false;

    private Vector3 originalScale;
    private Vector3 originalPosition;

    void Start()
    {
        originalScale = targetCube.transform.localScale;
        originalPosition = targetCube.transform.position;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ToggleCube();
        }
    }

    public void ToggleCube()
    {
        if (isActivated)
        {
            StartCoroutine(AnimateCube(originalScale, originalPosition));
        }
        else
        {
            Vector3 targetScale = new Vector3(originalScale.x, originalScale.y, originalScale.z + buttonExtendAmount);
            Vector3 targetPosition = originalPosition - new Vector3(0f, 0f, buttonExtendAmount / 2);

            StartCoroutine(AnimateCube(targetScale, targetPosition));
        }

        isActivated = !isActivated;
    }

    public IEnumerator AnimateCube(Vector3 targetScale, Vector3 targetPosition)
    {
        float duration = 1f;
        float elapsedTime = 0f;

        Vector3 startScale = targetCube.transform.localScale;
        Vector3 startPosition = targetCube.transform.position;

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            targetCube.transform.localScale = Vector3.Lerp(startScale, targetScale, t);
            targetCube.transform.position = Vector3.Lerp(startPosition, targetPosition, t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Asegurarse de que el objeto esté en el estado final
        targetCube.transform.localScale = targetScale;
        targetCube.transform.position = targetPosition;
    }
}