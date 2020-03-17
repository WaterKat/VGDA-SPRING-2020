using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    private List<int> scannedIdList = new List<int>();
    private Dictionary<int, float> scanningList = new Dictionary<int, float>();

    [SerializeField]
    private Camera cam;
    [SerializeField]
    private float scanRange = 30f;

    private void Update()
    {
        RaycastHit hit;
        if(Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, scanRange))
        {
            Scannable scannableObject = hit.collider.GetComponent<Scannable>();
            if(scannableObject != null)
            {
                if(!scannedIdList.Contains(scannableObject.scanId))
                {
                    if(!scanningList.ContainsKey(scannableObject.scanId))
                    {
                        scanningList.Add(scannableObject.scanId, 0f);
                    }
                    Scan(scannableObject);
                }
            }
        }
    }

    private void Scan(Scannable scannedObject)
    {
        scanningList[scannedObject.scanId] += Time.deltaTime;
        Debug.Log("SCANNING " + scannedObject.gameObject.name + " : " + scanningList[scannedObject.scanId]);

        if (scanningList[scannedObject.scanId] >= scannedObject.scanTimeLength)
        {
            scanningList.Remove(scannedObject.scanId);
            scannedIdList.Add(scannedObject.scanId);
            Debug.Log(scannedObject.gameObject.name + " SCANNED");
        }
    }

}
