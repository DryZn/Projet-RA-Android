using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ARTrackAndSpawn : ARTrackedImage
{
    public GameObject m_prefab = null;
    GameObject m_markerInstantiated = null;
    public float def_z = 1.5f;

    void UpdateMarker()
    {
        // Un objet spawn devant l'image traquee
        if (m_markerInstantiated == null)
        {
            m_markerInstantiated = Instantiate(m_prefab);
            m_markerInstantiated.transform.parent = this.transform;
            Vector3 vect = Vector3.zero;
            // transform.position
            vect.z += def_z;
            m_markerInstantiated.transform.localPosition = vect;
            m_markerInstantiated.transform.localRotation = Quaternion.identity;
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
        // lors de la detection de l'objet, on instancie une fois l'objet pour le moment
        UpdateMarker();
    }
}
