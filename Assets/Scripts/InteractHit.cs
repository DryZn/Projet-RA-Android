using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractHit : MonoBehaviour
{
    public float speed = 0.0f;
    private float decal = 0;
    private float sense = 1;
    private float angle = 270.0f;
    private Animator anim;
    private System.DateTime spawnTime;
    public RuntimeAnimatorController anim2;

    // Lorsque l'on appuie sur l'abeille change son animation
    private void OnMouseDown()
    {
        Destroy(this.gameObject.GetComponent<Patrol>());
        // reset l'anim
        anim = this.gameObject.GetComponent<Animator>();
        anim.runtimeAnimatorController = anim2;
        //anim.runtimeAnimatorController = Instantiate(Resources.Load<RuntimeAnimatorController>("TouchedBee"));
        spawnTime = System.DateTime.Now;
    }

    // 
    void Update()
    {
        // temporaire
        /*if ((spawnTime.Second) < 3)
            anim.runtimeAnimatorController = Instantiate(Resources.Load<RuntimeAnimatorController>("StingBee"));*/
        Debug.Log("AttentionICI");
        Debug.Log(spawnTime.Second);
        // Creation d'un nouveau vecteur qui reprend les coordoonees du prefab
        /* Vector3 vect = Vector3.zero;
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
         this.transform.localPosition = vect;*/
    }
}
