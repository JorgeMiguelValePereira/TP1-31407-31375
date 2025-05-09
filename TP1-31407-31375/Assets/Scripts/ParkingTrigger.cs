using UnityEngine;
using System.Collections;

public class ParkingTrigger : MonoBehaviour
{
    public UIManager uiManager;

    private ParkingManager parkingManager;

    private bool isCounting = false;
    private Coroutine waitCoroutine;
    private Coroutine countdownCoroutine;
    private Rigidbody carRb;
    private Collider triggerZone;
    private bool isInZone = false;
    private Vector3 lastPosition;

    private void Start()
    {
        if (uiManager == null)
            uiManager = Object.FindFirstObjectByType<UIManager>();

        triggerZone = GetComponent<Collider>();
    }

    public void SetManager(ParkingManager manager)
    {
        parkingManager = manager;
    }

    private void Update()
    {
        if (isInZone && !isCounting && waitCoroutine == null && carRb != null)
        {
            if (IsCarFullyInside())
            {
                waitCoroutine = StartCoroutine(WaitUntilCarFullyInsideAndStill());
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Rigidbody rb = other.GetComponentInParent<Rigidbody>();

        if (rb != null && rb.CompareTag("Player"))
        {
            isInZone = true;

            if (carRb == null)
            {
                carRb = rb;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Rigidbody rb = other.GetComponentInParent<Rigidbody>();
        if (rb != null && rb.CompareTag("Player") && rb == carRb)
        {
            isInZone = false;

            if (waitCoroutine != null)
            {
                StopCoroutine(waitCoroutine);
                waitCoroutine = null;
            }

            if (countdownCoroutine != null)
            {
                StopCoroutine(countdownCoroutine);
                countdownCoroutine = null;
            }

            uiManager?.HideCountdown();
            isCounting = false;
            carRb = null;
        }
    }

    private IEnumerator WaitUntilCarFullyInsideAndStill()
    {
        lastPosition = carRb.position;

        while (!IsCarFullyInside() || Vector3.Distance(carRb.position, lastPosition) > 0.001f)
        {
            lastPosition = carRb.position;
            yield return null;
        }

        yield return new WaitForSeconds(0.5f);

        if (IsCarFullyInside() && Vector3.Distance(carRb.position, lastPosition) < 0.001f)
        {
            countdownCoroutine = StartCoroutine(StartCountdown());
        }

        waitCoroutine = null;
    }

    private IEnumerator StartCountdown()
    {
        if (isCounting) yield break;

        isCounting = true;
        float countdown = 5f;
        lastPosition = carRb.position;

        while (countdown > 0f)
        {
            if (!IsCarFullyInside() || Vector3.Distance(carRb.position, lastPosition) > 0.001f)
            {
                uiManager?.HideCountdown();
                isCounting = false;
                yield break;
            }

            lastPosition = carRb.position;
            uiManager?.UpdateCountdown(Mathf.CeilToInt(countdown));
            yield return new WaitForSeconds(1f);
            countdown -= 1f;
        }

        uiManager?.HideCountdown();
        uiManager?.ShowSuccessMessage();
        isCounting = false;

        parkingManager?.OnParkingSuccess();
    }

    private bool IsCarFullyInside()
    {
        if (carRb == null || triggerZone == null)
            return false;

        BoxCollider mainBox = carRb.GetComponent<BoxCollider>();
        if (mainBox == null)
            return false;

        Bounds carBounds = mainBox.bounds;
        Bounds triggerBounds = triggerZone.bounds;
        triggerBounds.Expand(-0.1f);

        return triggerBounds.Contains(carBounds.min) && triggerBounds.Contains(carBounds.max);
    }
}
