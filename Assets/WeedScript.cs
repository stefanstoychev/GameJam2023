using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeedScript : MonoBehaviour
{
    private HashSet<GameObject> infectedProduce = new HashSet<GameObject>();

    [SerializeField]
    private GameObject roots;

    private SphereCollider colider;

    [SerializeField]
    float growthRate = 1.05f;

    float rootGrowRate { get { return 1 + 2.0f * (growthRate - 1.0f); } }

    [SerializeField]
    int GrowthTickInSeconds = 1;

    [SerializeField] 
    int MaxTicks = 60;

    [SerializeField]
    int DamagePerTick = 3;

    IEnumerator Start()
    {
        colider = this.GetComponent<SphereCollider>();

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
        var oldlocalScale = transform.localScale;

        colider.radius = colider.radius * growthRate;

        transform.localScale = new Vector3(oldlocalScale.x, oldlocalScale.y * growthRate, oldlocalScale.z);

        roots.transform.localScale = new Vector3(roots.transform.localScale.x * rootGrowRate, roots.transform.localScale.y, roots.transform.localScale.z * rootGrowRate);
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
