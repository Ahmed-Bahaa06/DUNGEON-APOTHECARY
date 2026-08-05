using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class GameIntro : MonoBehaviour
{
    [SerializeField] private GameObject canvas;
    [SerializeField] private CinemachineCamera mainCamera;

    [SerializeField] private PlayerMovement playerMovement;

    [SerializeField] private GameObject introUI;

    private void Start()
    {
        introUI.SetActive(true);

        playerMovement.Stop();
        SpawnManager.Instance.Stop();

        PlayerInput.Instance.OnInteractAction += HandleInteractAction;
    }

    private void HandleInteractAction()
    {
        PlayerInput.Instance.OnInteractAction -= HandleInteractAction;

        mainCamera.Priority = 10;
        playerMovement.Resume();

        introUI.SetActive(false);
        canvas.SetActive(true);
        StartCoroutine(IntroSequence());
    }

    private IEnumerator IntroSequence()
    {
        yield return StartCoroutine(CameraSequence());

        yield return StartCoroutine(CountdownSequence());

        playerMovement.Resume();
        SpawnManager.Instance.Resume();

        Destroy(gameObject);
    }

    private IEnumerator CameraSequence()
    {
        // We'll implement this next using Cinemachine.

        yield return null;
    }

    private IEnumerator CountdownSequence()
    {
        // We'll implement this after the camera.

        yield return null;
    }

    private void OnDestroy()
    {
        if (PlayerInput.Instance != null)
        {
            PlayerInput.Instance.OnInteractAction -= HandleInteractAction;
        }
    }
}
