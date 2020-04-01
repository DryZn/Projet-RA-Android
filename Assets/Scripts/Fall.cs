using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

// classe a invoquer lors d'un changement d'etat
public class Fall : MonoBehaviour
{
    public float fallSpeed = 0.8f;
    public LayerMask m_layerMask = 0x100;
    private float timeRef = 0.0f;

    void Update()
    {
        float decalY = Time.deltaTime * fallSpeed;
        RaycastHit hit;
        if (timeRef == 0.0f)
        {
            // si l'objet tombe trop loin on le détruit
            if (transform.localPosition.y < -3)
                Destroy(gameObject);
               // test de collision avec la premiere hitbox
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, decalY, m_layerMask))
                timeRef = Time.time;
            else
            {
                // chute de l'objet
                Vector3 vect = new Vector3();
                vect.x = transform.localPosition.x;
                vect.z = transform.localPosition.z;
                vect.y = transform.localPosition.y - decalY;
                transform.localPosition = vect;
            }
        }
        // destruction de l'objet lorsque tombe depuis trop longtemps
        else if ((Time.time - timeRef) > 6)
            Destroy(gameObject);
    }
}
