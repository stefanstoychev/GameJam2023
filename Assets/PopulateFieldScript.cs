using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PopulateFieldScript : MonoBehaviour
{

    [field: SerializeField]
    GameObject Produce { get; set; }

    [field: SerializeField]
    GameObject Weed { get; set; }

    [field: SerializeField]
    int RowLenght;

    [field: SerializeField]
    float ProduceSpacing { get; set; } = 2.0f;

    [field: SerializeField]
    float WeedWiggle { get; set; } = 0.25f;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = -RowLenght; i < RowLenght; i++)
        {
            for (int j = -RowLenght; j < RowLenght; j++)
            {
                var newInstance = Instantiate(Produce, new Vector3(i * ProduceSpacing + Random.Range(0,2), 0, j * ProduceSpacing + +Random.Range(0, 2)), Quaternion.identity);

                newInstance.transform.rotation = Quaternion.Euler(1.0f, Random.Range(0.0f, 360f), 1.0f);

                newInstance.transform.SetParent(transform);

                if (ShouldPlaceWeed())
                {
                    var wiggle = Random.Range(-WeedWiggle*100, WeedWiggle*100)/100f;
                    var weedInstance = Instantiate(Weed, new Vector3(i * ProduceSpacing, 0, j * ProduceSpacing + wiggle), Quaternion.identity);
                    weedInstance.transform.SetParent(transform);
                }
            }
        }
    }

    bool ShouldPlaceWeed()
    {
        return Random.Range(0, 1) == 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
