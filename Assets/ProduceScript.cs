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

    float rootGrowRate { get { return 1 + 2.0f * (growthRate - 1.0f); } }

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
        tmp.SetFloat("_Blend", Health/100.0f);

        System.Console.WriteLine(string.Format("Damaged by {0}",damage));
    }


    // Start is called before the first frame update
    IEnumerator GrowProduce()
    {
        for (int i = 0; i < MaxTicks; i++)
        {
            yield return new WaitForSeconds(GrowthTickInSeconds);

            capsuleCollider.radius = capsuleCollider.radius * growthRate;

            var oldlocalScale = transform.localScale;

            oldlocalScale.Set(oldlocalScale.x * growthRate, oldlocalScale.y * growthRate, oldlocalScale.z * growthRate);

            transform.localScale = oldlocalScale;

            roots.transform.localScale = new Vector3(roots.transform.localScale.x * rootGrowRate, roots.transform.localScale.y, roots.transform.localScale.z * rootGrowRate);

            if (Health<100)
                Health += HealthRegen;
        }
    }
}
