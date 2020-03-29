using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractHit : MonoBehaviour
{
    public RuntimeAnimatorController animHit;
    public RuntimeAnimatorController animReaction;
    public RuntimeAnimatorController animMove;
    public float moveSpeed = 0.4f;
    public float rotaSpeed = 0.15f;
    private Animator anim = null;
    private int hitsRem = 3;
    private Transform camTransform;

    // Lorsque l'on appuie sur l'abeille elle arrete de se deplacer et fait l'animation "TouchedBee"
    private void OnMouseDown()
    {
        if (anim is null)
        {
            Destroy(gameObject.GetComponent<Patrol>());
            anim = gameObject.GetComponent<Animator>();
            anim.runtimeAnimatorController = animHit;
            camTransform = Camera.main.transform;
        }
        // reset l'anim
        else if (hitsRem > 1)
            anim.Play("GetDamage");
        else
        {
            // au bout de trois contacts elle rejoint l'utilisateur pour l'attaquer
            anim.runtimeAnimatorController = animMove;
            rotaSpeed *= 3;
        }
        hitsRem -= 1;
    }

    // retourne la nouvelle position ou rotation pres de celle ciblee
    float changeToPos(float initCoord, float targetCoord, float marginArea, float changespeed)
    {
        int sense = 1;
        if (initCoord > (targetCoord - marginArea))
        {
            // si l'objet est deja proche de la position cible, il ne bouge pas
            if ((targetCoord + marginArea) > initCoord)
                return initCoord;
            sense = -1;
        }
        return initCoord + Time.deltaTime * changespeed * sense;
    }

    // lorsque l'on a touche l'abeille trop de fois elle attaque
    void Update()
    {
        if (anim != null)
        {
            // l'abeille se tourne face a l'utilisateur
            float rotaY = changeToPos(transform.localRotation.eulerAngles.y%360, (camTransform.localRotation.eulerAngles.y + 180)%360, 0.01f, rotaSpeed);
            transform.rotation = Quaternion.Euler(0.0f, rotaY, 0.0f);
            //if ((hitsRem == 0) && (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !anim.IsInTransition(0)))
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Death"))
                Destroy(this);

            if (hitsRem < 1)
            {
                // temporaire
                /*if ((Time.time - spawnTime2) > 8)
                    anim.runtimeAnimatorController = animReaction;*/
                // Creation d'un nouveau vecteur qui reprend les coordoonees du prefab
                Vector3 vect = Vector3.zero;
                // l'abeille rejoint l'utilisateur pour le piquer
                vect.x = changeToPos(transform.localPosition.x, camTransform.localPosition.x, 0.18f, moveSpeed);
                vect.y = changeToPos(transform.localPosition.y, camTransform.localPosition.y, 0.05f, moveSpeed);
                vect.z = changeToPos(transform.localPosition.z, camTransform.localPosition.z, 0.18f, moveSpeed);
                if ((vect == transform.localPosition) && (anim.runtimeAnimatorController != animReaction))
                {
                    anim.runtimeAnimatorController = animReaction;
                    moveSpeed *= 3;
                }
                transform.localPosition = vect;
            }
        }
    }
}
