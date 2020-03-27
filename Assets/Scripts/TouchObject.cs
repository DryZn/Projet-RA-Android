using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

[RequireComponent(typeof(Camera))]
public class TouchObject : MonoBehaviour
{

    Camera m_meCamera = null;
    ARSessionOrigin m_ARorigin = null;
    public string LayerName = null;
    public GameObject m_prefabToInstantiate = null;
    private LayerMask m_layerMask;

    // Start is called before the first frame update
    void Start()
    {
        m_meCamera = GetComponent<Camera>();
        m_ARorigin = FindObjectOfType<ARSessionOrigin>();
        m_layerMask = LayerMask.GetMask("Bees");
    }

    // Update is called once per frame
    void Update()
    {
        if (m_prefabToInstantiate == null)
            return;

        bool instantiate = false;
        Vector3 pos = Vector3.zero;
        if (Input.GetMouseButtonDown(0))
        {
            pos = Input.mousePosition;
            instantiate = true;
        }
        if ((Input.touches.Length > 0 && Input.touches[0].phase == TouchPhase.Began))
        {
            pos = Input.touches[0].position;
            instantiate = true;
        }
        if (!instantiate)
            return;

        Ray r = m_meCamera.ScreenPointToRay(pos);
        RaycastHit rch;
        if (Physics.Raycast(r, out rch, 100.0f, m_layerMask))
        {
            GameObject prefabInstantiated = Instantiate(m_prefabToInstantiate);
            if (prefabInstantiated)
            {
                prefabInstantiated.transform.position = rch.point;
                prefabInstantiated.transform.parent = m_ARorigin.transform; // position relative par rapport a la camera
                Debug.Log("Prefab instantiated on plane");
            }
        }
        else
        {
            Debug.LogError("Prefab cannot be instantiated, no plane detected");
        }
    }
}
