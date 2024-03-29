using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour,Damaging
{
    private float damage;



    public float GetDamage()
    {
        if (damage == 0) return 20f;
        return damage;
    }

    public void Activate(float damage, float radius) {
        if (GameManager.isComplete) DestroyImmediate(gameObject);
        this.damage = damage;
        gameObject.transform.localScale = new Vector3(radius, radius, 1);
        StartCoroutine(Explode());
    }

    IEnumerator Explode() {
        if (GameManager.isComplete) DestroyImmediate(gameObject);
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        gameObject.GetComponent<SpriteRenderer>().sortingLayerName = "Projectile";
        gameObject.GetComponent<Animator>().SetBool("exploding", true);
        yield return new WaitForSeconds(0.1f);
        gameObject.GetComponent<CircleCollider2D>().enabled = true;
        yield return new WaitForSeconds(0.1f);
        Destroy(this.gameObject);
    }

   

}
