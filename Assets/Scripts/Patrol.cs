using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    public float m_rotationSpeed = 10.0f;   // rotation speed in degrees/second
    public float def_z = 0.0f;
    public float speed = 0.0f;
    private float decal = 0;
    private float sense = 1;
    private float angle = 270.0f;

    // Update is called once per frame
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


        // calculate the new angle according to the time elapsed since last frame
        //m_angle += m_rotationSpeed * Time.deltaTime;
        //Random.Range(0.0f, 360.0f);

    }
}
