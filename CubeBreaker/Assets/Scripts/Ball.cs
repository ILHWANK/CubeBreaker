using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public LayerMask whatIsProp;
    //Layer의 경우 한번에 많은 값을 필터 할 수 있다.
    //범위 안에 모는 Object정보를 가지고 올 수도 있지만 성능 낭비가 심하다.

    public ParticleSystem explotionParticle;

    public AudioSource explotionAudio;

    public float maxDamage = 100f;

    public float explosionForce = 1000f;

    public float lifeTime = 10f;

    public float explosionRadius = 20f;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void OnTriggerEnter(Collider other)//물체의 충돌을 감지하고 other으로 충돌 한 물체가 무엇인지 알 수 있다.
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius, whatIsProp);
        //(가상원을 만들 위치, 반지름, 필터)

        for(int i = 0; i < colliders.Length; i++)
        {
            Rigidbody targetRegidbody = colliders[i].GetComponent<Rigidbody>();

            targetRegidbody.AddExplosionForce(explosionForce, transform.position,
                                              explosionRadius);

            Prop targetProp = colliders[i].GetComponent<Prop>();

            float damage = CalulateDamage(colliders[i].transform.position);

            targetProp.TakeDamage(damage);

        }

        explotionParticle.transform.parent = null; //부모 gameObject 를 지운다.
                                                   //tranform 은 부모와 자식 Object 간에 관계를 활용 할대 사용한다.

        explotionParticle.Play();

        explotionAudio.Play();

        GameManager.instance.OnBallDestory(); 

        Destroy(explotionParticle.gameObject, explotionParticle.duration);
        // duration 을 사용하여 해당 Object 의 running time을 알 수 있다.

        Destroy(gameObject);
    }
    private float CalulateDamage(Vector3 targetposition)
    {
        Vector3 explrotionToTarget = targetposition - transform.position;

        float distance = explrotionToTarget.magnitude;

        float edgetoCenterDistance = explosionRadius - distance;

        float percentage = edgetoCenterDistance / explosionRadius;

        float damage = maxDamage * percentage;

        damage = Mathf.Max(0, damage);
        // damage 가 0 미만으로 감소 하는 경우 hp 가 회복 되는 것을 방지 하기 위헤서
        // 0 미만 인 경우 해당 값을 0으로 변경채

        return damage;
    }

}