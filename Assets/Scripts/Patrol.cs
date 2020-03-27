using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script permettant a l'objet de se deplacer et adaptant sa rotation selon sa direction
public class Patrol : MonoBehaviour
{
    public float def_z = 1.5f;
    public float speed = 0.3f;
    private float decal = 0;
    private float sense = 1;
    private float angle = 270.0f;

    void Update()
    {
        // Creation d'un nouveau vecteur qui reprend les coordoonees du prefab
        Vector3 vect = Vector3.zero;
        // coord de l'endroit d'appartiion de l'abeille
        vect.z = def_z;
        if (decal > 2)
            sense *= -1;
        else if (decal < -2)
            sense *= -1;
        float inc = 0.0f;
        if (sense == 1)
            inc += 180;
        this.transform.localRotation = Quaternion.Euler(0.0f, angle + inc, 0.0f);
        decal += Time.deltaTime * speed * sense;
        vect.x = decal;
        this.transform.localPosition = vect;
    }
}
