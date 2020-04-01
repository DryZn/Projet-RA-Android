using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script permettant a l'objet de se deplacer et adaptant sa rotation selon sa direction
public class Patrol : MonoBehaviour
{
    public float speed = 0.3f;
    private float decalX = 0;
    private float sense = 1;
    private float rotaY;

    private void Start()
    {
        rotaY = 90;
    }

    void Update()
    {
        // Creation d'un nouveau vecteur qui reprend les coordoonees du prefab
        Vector3 vect = Vector3.zero;
        // coord de l'endroit d'appartiion de l'abeille
        vect.z = transform.localPosition.z;
        if (decalX > 0.6)
            sense *= -1;
        else if (decalX < -0.6)
            sense *= -1;
        float inc = 0.0f;
        if (sense == -1)
            inc += 180;
        transform.localRotation = Quaternion.Euler(0.0f, rotaY + inc, 0.0f);
        decalX += Time.deltaTime * speed * sense;
        vect.x = decalX;
        transform.localPosition = vect;
    }
}
