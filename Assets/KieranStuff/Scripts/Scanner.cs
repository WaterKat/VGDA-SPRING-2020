using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Scanner : MonoBehaviour
{
    private List<Scannable.ScannableIDS> scannedIdList = new List<Scannable.ScannableIDS>();
    private Dictionary<Scannable.ScannableIDS, float> scanningList = new Dictionary<Scannable.ScannableIDS, float>();

    [SerializeField]
    private Camera cam;
    [SerializeField]
    private float scanRange = 30f;

    public Image scanningBar;
    public GameObject scannedPanel;
    public TextMeshProUGUI scannedTitle;
    public TextMeshProUGUI scannedLore;
    private float scanPercentValue = 0f;
    private bool paused = false;
    private bool letGoOfScan = false;

    [Header("OBJECTS TO ENABLE/DISABLE")]
    public GameObject reticleCanvas;
    public GameObject healthCanvas;

    private void Update()
    {
        if(paused)
        {
            if (Input.GetKeyUp(KeyCode.Z) && letGoOfScan == false)
            {
                letGoOfScan = true;
                return;
            }
            if(Input.GetKeyUp(KeyCode.Z) && letGoOfScan)
            {
                DisableScannedPanel();
            }
        }

        RaycastHit hit;
        if(Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, scanRange))
        {
            if(Input.GetKey(KeyCode.Z))
            {
                Scannable scannableObject = hit.collider.GetComponent<Scannable>();
                if (scannableObject != null)
                {
                    
                    if (!scannedIdList.Contains(scannableObject.scannableID))
                    {
                        scanningBar.gameObject.SetActive(true);
                        if (!scanningList.ContainsKey(scannableObject.scannableID))
                        {
                            scanningList.Add(scannableObject.scannableID, 0f);
                        }
                        Scan(scannableObject);
                    }
                    else
                    {
                        EnableScannedPanel(scannableObject);
                    }
                }
            }
            else
            {
                if(scanningBar.gameObject.activeSelf == true)
                {
                    scanningBar.gameObject.SetActive(false);
                }
            }
        }
    }

    private void Scan(Scannable scannedObject)
    {
        scanningList[scannedObject.scannableID] += Time.deltaTime;
        scanPercentValue = scanningList[scannedObject.scannableID] / scannedObject.scanTimeLength;
        scanningBar.fillAmount = scanPercentValue;

        if (scanningList[scannedObject.scannableID] >= scannedObject.scanTimeLength)
        {
            scanningList.Remove(scannedObject.scannableID);
            scannedIdList.Add(scannedObject.scannableID);
            Debug.Log(scannedObject.scanLore);
            EnableScannedPanel(scannedObject);
        }
    }

    private void EnableScannedPanel(Scannable scannedObject)
    {
        scannedTitle.text = scannedObject.scanName;
        scannedLore.text = scannedObject.scanLore;
        reticleCanvas.SetActive(false);
        healthCanvas.SetActive(false);
        scannedPanel.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        paused = true;
        Time.timeScale = 0;
    }
    private void DisableScannedPanel()
    {
        reticleCanvas.SetActive(true);
        healthCanvas.SetActive(true);
        scannedPanel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        paused = false;
        letGoOfScan = false;
        Time.timeScale = 1;
    }

}
