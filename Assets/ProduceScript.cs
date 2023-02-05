using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class ProduceScript : MonoBehaviour
{
    CapsuleCollider capsuleCollider;

    [SerializeField]
    private GameObject roots;

    [SerializeField] 
    int Health = 100;

    [SerializeField] 
    int HealthRegen = 1;

    [SerializeField] 
    int MaxTicks = 60;

    [SerializeField] 
    int GrowthTickInSeconds = 1;

    [SerializeField]
    float growthRate = 1.05f;

    float currentGrowthRate = 1.0f;

    float maxGrowthRate = 1.0007f;

    float rootGrowRate { get { return 1 + 2.0f * (growthRate - 1.0f); } }

    float currentRootGrowthRate = 1.0f;

    [SerializeField]
    GameObject cornModel;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        capsuleCollider = GetComponent<CapsuleCollider>();

        // Start function WaitAndPrint as a coroutine
        yield return StartCoroutine("GrowProduce");
        print("Done " + Time.time);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void RecieveDamage(int damage)
    {
        Health -= damage;
     
        if (Health < 0)
            Health = 0;

        Renderer renderer = cornModel.GetComponent<Renderer>();

        Material tmp = renderer.material;

        var healthRatio = Health / 100.0f;
        System.Console.WriteLine(healthRatio);
        tmp.SetFloat("_Blend", healthRatio);

        System.Console.WriteLine(string.Format("Damaged by {0}",damage));
    }


    // Start is called before the first frame update
    IEnumerator GrowProduceCoroutine()
    {
  
        for (int i = 0; i < MaxTicks; i++)
        {
            yield return new WaitForSeconds(GrowthTickInSeconds);

            if (rootGrowRate < maxGrowthRate)
            {
                GrowProduce();
            }

            if (Health<100)
                Health += HealthRegen;
        }
    }

    void GrowProduce()
    {
        currentGrowthRate *= growthRate;

        capsuleCollider.radius = capsuleCollider.radius * currentGrowthRate;

        var oldlocalScale = transform.localScale;

        oldlocalScale.Set(oldlocalScale.x * currentGrowthRate, oldlocalScale.y * currentGrowthRate, oldlocalScale.z * currentGrowthRate);

        transform.localScale = oldlocalScale;

        currentRootGrowthRate *= rootGrowRate;

        roots.transform.localScale = new Vector3(roots.transform.localScale.x * currentRootGrowthRate, roots.transform.localScale.y, roots.transform.localScale.z * currentRootGrowthRate);

    }
}
