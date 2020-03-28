using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractHit : MonoBehaviour
{
    public RuntimeAnimatorController animHit;
    public RuntimeAnimatorController animReaction;
    public float speed = 0.1f;
    public float rotaSpeed = 0.15f;
    private float decalX = 0;
    private float decalY = 0;
    private float decalZ = 0;
    private float sense = 1;
    private float rotaY;
    private Animator anim = null;
    private int hitsRem = 3;
    private GameObject camera;

    // Lorsque l'on appuie sur l'abeille elle arrete de se deplacer et fait l'animation "TouchedBee"
    private void OnMouseDown()
    {
        if (anim is null)
        {
            Destroy(gameObject.GetComponent<Patrol>());
            rotaY = transform.localRotation.y;
            anim = gameObject.GetComponent<Animator>();
            anim.runtimeAnimatorController = animHit;
            camera = GameObject.FindGameObjectWithTag("MainCamera");
        }
        // reset l'anim
        else if (hitsRem > 0)
            anim.Play("GetDamage");
        else
            anim.runtimeAnimatorController = animReaction;
        hitsRem -= 1;
    }

    // lorsque l'on a touche l'abeille trop de fois elle attaque
    void Update()
    {
        if (anim != null)
        {
            //if ((hitsRem == 0) && (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !anim.IsInTransition(0)))
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Wait"))
                Destroy(this);

            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
            {
                // l'abeille se tourne face a l'utilisateur
                rotaY = (GameObject.FindGameObjectWithTag("MainCamera").transform.localRotation.y + 180) - Time.deltaTime * rotaSpeed;
                transform.rotation = Quaternion.Euler(0.0f, rotaY, 0.0f);
                Debug.Log("test");
                Debug.Log(camera.transform.localRotation.y);
                // temporaire
                /*if ((Time.time - spawnTime2) > 8)
                    anim.runtimeAnimatorController = animReaction;*/
                // Creation d'un nouveau vecteur qui reprend les coordoonees du prefab
                Vector3 vect = Vector3.zero;
                //Vector3 vect = transform.localPosition;
                // l'abeille rejoint l'utilisateur pour le piquer
                if (transform.localPosition.x > 0.1)
                    //transform.right = Time.deltaTime * speed;
                    vect.x = transform.localPosition.x - Time.deltaTime * speed;
                if (transform.localPosition.y > 0.1)
                    vect.y = transform.localPosition.y - Time.deltaTime * speed;
                if (transform.localPosition.z > 0.1)
                    vect.z = transform.localPosition.z - Time.deltaTime * speed;
                decalX += Time.deltaTime * speed * sense;
                vect.x = decalX;
                //transform.localPosition = vect;
            }
        }
    }
}
