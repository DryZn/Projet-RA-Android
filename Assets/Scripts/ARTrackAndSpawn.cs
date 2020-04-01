using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ARTrackAndSpawn : ARTrackedImage
{
    public GameObject m_prefab = null;
    GameObject m_markerInstantiated = null;
    public float def_z = 1.5f;
    Vector3 vectSpawn;

    private void Start()
    {
        vectSpawn = Vector3.zero;
    }

    void UpdateMarker()
    {
        // Un objet spawn devant l'image traquee
        if (m_markerInstantiated == null)
        {
            m_markerInstantiated = Instantiate(m_prefab);
            m_markerInstantiated.transform.parent = transform;
            if (vectSpawn == Vector3.zero)
            {
                vectSpawn = GameObject.FindGameObjectWithTag("MainCamera").transform.localPosition;
                vectSpawn.z += def_z;
            }
            m_markerInstantiated.transform.localPosition = vectSpawn;
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
