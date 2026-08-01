using UnityEngine;
using UnityEngine.UI;

public class LoaderCallBack : MonoBehaviour
{
    [SerializeField] private Image fillImage;
    [SerializeField] private float lerpSpeed = 10f;

    private float targetFill = 1f;
    private bool hasLoaded;

    private void Update()
    {
        fillImage.fillAmount = Mathf.Lerp(
            fillImage.fillAmount,
            targetFill,
            Time.deltaTime * lerpSpeed
        );

        if (fillImage.fillAmount >= 0.99f && !hasLoaded)
        {
            hasLoaded = true;
            fillImage.fillAmount = 1f;

            Loader.LoaderCallback();
        }
    }
}