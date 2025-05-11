using UnityEngine;

public class ParkingManager : MonoBehaviour
{
    public GameObject parkingZonePrefab;      // Prefab do estacionamento
    public Vector3 areaCenter = Vector3.zero; // Centro da área de spawn
    public Vector3 areaSize = new Vector3(40, 0, 40); // Tamanho da área

    public UIManager uiManager;

    private GameObject currentZone;

    private void Start()
    {
        uiManager.SetManager(this);
        SpawnParkingZone();
    }

    public void SpawnParkingZone()
    {
        if (currentZone != null)
            Destroy(currentZone);

        Vector3 pos = GetRandomPositionInArea();
        currentZone = Instantiate(parkingZonePrefab, pos, Quaternion.identity);

        ParkingTrigger trigger = currentZone.GetComponent<ParkingTrigger>();
        trigger.uiManager = uiManager;
        trigger.SetManager(this);
    }

    public void OnParkingSuccess()
    {
        uiManager.ShowNextParkingPrompt();
    }

    public void OnUserChoseNext()
    {
        Time.timeScale = 1f;
        SpawnParkingZone();
    }

    public void OnUserChoseEnd()
    {
        Time.timeScale = 0f;
        Debug.Log("🏁 Estacionamento finalizado.");
    }

    private Vector3 GetRandomPositionInArea()
    {
        float x = Random.Range(-areaSize.x / 2f, areaSize.x / 2f);
        float z = Random.Range(-areaSize.z / 2f, areaSize.z / 2f);
        return areaCenter + new Vector3(x, 0, z);
    }
}
