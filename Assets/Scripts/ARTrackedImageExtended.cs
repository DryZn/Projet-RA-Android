using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ARTrackedImageExtended : ARTrackedImage
{

    string m_prefabName = "MARKER_1";
    GameObject m_marker= null;
    GameObject m_markerInstantiated = null;
    // Start is called before the first frame update
    void Start()
    {
        m_prefabName = this.referenceImage.name;
        UpdateMarker();
    }


    void UpdateMarker()
    {
        if (m_markerInstantiated == null)
        {
            m_marker = Resources.Load<GameObject>(m_prefabName);
            m_markerInstantiated = Instantiate(m_marker);
            m_markerInstantiated.transform.parent = this.transform;
            m_markerInstantiated.transform.localPosition = Vector3.zero;
            m_markerInstantiated.transform.localRotation = Quaternion.identity;
            // do not change scale
        }

    }

    void DeleteMarker()
    {
        if (m_markerInstantiated)
            Destroy(m_markerInstantiated);
        m_markerInstantiated = null;
    }

    private void Update()
    {
        if (m_prefabName != this.referenceImage.name)
        {
            DeleteMarker();
            UpdateMarker();
        }
        m_markerInstantiated.SetActive(this.trackingState == UnityEngine.XR.ARSubsystems.TrackingState.Tracking);

    }

}
