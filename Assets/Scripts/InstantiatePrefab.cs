using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiatePrefab : MonoBehaviour
{
    public GameObject m_prefab = null;          // prefab to instantiate
    public Transform m_parent = null;           // parent of the intantiated object
    public Vector3 m_position = Vector3.zero;   // position of the instantiated object in the parent referential

    GameObject m_instantiatedObject = null;     // object instanciated (PRIVATE)
    // Getter to read the instanciated object : we cannot modify it from outside of the InstantiatePrefab class
    public GameObject instantiatedObject {  get { return m_instantiatedObject;  } } 

    // Start is called before the first frame update
    void Start()
    {
        if (m_prefab == null) // only instantitate if prefab is valid
            return;

        // process instantiation and set local position in parent referential
        m_instantiatedObject = Instantiate(m_prefab, m_parent);
        m_instantiatedObject.transform.localPosition = m_position;
    }

}
