using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

// classe a invoquer lors d'un changement d'etat
public class Fall : MonoBehaviour
{
    public float fallSpeed = 0.8f;
    public LayerMask m_layerMask = 0x100;
    private float spawnTime;

    private void Start()
    {
        spawnTime = Time.time;
    }

    void Update()
    {
        // test de collision avec la premiere hitbox
        float decalY = Time.deltaTime * fallSpeed;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, decalY, m_layerMask))
        {
            // si l'objet tombe trop loin ou est present depuis trop longtemps on le détruit
            if ((transform.localPosition.y < -10) | ((Time.time - spawnTime) > 10))
                Destroy(gameObject);
        } else
        {
            Vector3 vect = new Vector3();
            vect.x = transform.localPosition.x;
            vect.z = transform.localPosition.z;
            vect.y = transform.localPosition.y - decalY;
            transform.localPosition = vect;
        }
    }
}
