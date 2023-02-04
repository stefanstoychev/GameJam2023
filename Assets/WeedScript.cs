using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeedScript : MonoBehaviour
{
    private HashSet<GameObject> infectedProduce = new HashSet<GameObject>();

    private Rigidbody rigidbody;
    private CapsuleCollider colider;

    [SerializeField]
    float growthRate = 1.05f;

    [SerializeField]
    int GrowthTickInSeconds = 1;

    [SerializeField] 
    int MaxTicks = 60;

    [SerializeField]
    int DamagePerTick = 3;

    IEnumerator Start()
    {
        rigidbody = this.GetComponent<Rigidbody>();
        colider = this.GetComponent<CapsuleCollider>();

        print("Starting " + Time.time);

        // Start function WaitAndPrint as a coroutine
        yield return StartCoroutine("DamageProduceCoroutine");
        print("Done " + Time.time);
    }

    // Start is called before the first frame update
    IEnumerator DamageProduceCoroutine()
    {
        for (int i = 0; i < MaxTicks; i++)
        {
            yield return new WaitForSeconds(GrowthTickInSeconds);

            if(i == MaxTicks/2)
            {
                DamagePerTick++;
            }

            GrowWeed();
            DamageProduce();
        }
    }

    private void DamageProduce()
    {
        foreach(var produce in infectedProduce)
        {
            produce.SendMessage("RecieveDamage", DamagePerTick);
        }
    }

    void GrowWeed()
    {
        var oldlocalScale = colider.transform.localScale;

        colider.radius = colider.radius * growthRate;

        oldlocalScale.Set(oldlocalScale.x, oldlocalScale.y * growthRate, oldlocalScale.z);

        colider.transform.localScale = oldlocalScale;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Produce")
        {
            infectedProduce.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Produce")
        {
            infectedProduce.Remove(other.gameObject);
        }
    }
}
