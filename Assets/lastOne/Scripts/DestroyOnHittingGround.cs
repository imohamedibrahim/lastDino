using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnHittingGround : MonoBehaviour
{
    private GameObject onHitParticleGameObject;
    private bool destroyCalled = false;
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag.Equals(TagHolder.GROUND) && !destroyCalled)
        {
            GetComponent<Renderer>().enabled = false;
            DestroyGameObject();
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag.Equals(TagHolder.GROUND) && !destroyCalled)
        {
            if(transform.parent != null )
                transform.parent.GetComponent<Renderer>().enabled = false;
        }
    }

    private void DestroyGameObject()
    {
        destroyCalled = true;
        onHitParticleGameObject = UtilFunctions.GetChildGameObjectWithTag(gameObject,TagHolder.ON_HIT_PARTICLE_EFFECT);
        onHitParticleGameObject.GetComponent<ParticleSystem>().Play();
        transform.tag = "Untagged";
        StartCoroutine(CallDestroy());
    }

    private IEnumerator CallDestroy()
    {
        yield return new WaitForSeconds(0.035f);
        Destroy(gameObject);
    }

}
