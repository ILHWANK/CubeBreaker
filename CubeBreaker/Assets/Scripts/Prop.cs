 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prop : MonoBehaviour
{
    public int score = 20 ;

    public ParticleSystem explosionPartical;

    public float hp = 10;

    public void TakeDamage(float damage)
    {
        hp -= damage;

        if(hp <= 0)
        {
            ParticleSystem instance =  Instantiate(explosionPartical, transform.position,
                                                    transform.rotation);
            //(생성할 Object,  생성위치, 회전

            AudioSource explotionAudio = instance.GetComponent<AudioSource>();

            //explosionPartical.transform.parent = null; //부모 gameObject 를 지운다.
                                                       //tranform 은 부모와 자식 Object 간에 관계를 활용 할대 사용한다.

            //explosionPartical.Play();


            explotionAudio.Play();

            GameManager.instance.AddScore(score);

            Destroy(instance.gameObject, instance.duration);
            gameObject.SetActive(false);
            //Prop를 파괴 하는 것이 아닌 보이지 않도록 했다가 다시 보여 주기 위해서 사


        }
    }
}
