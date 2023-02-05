using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WeePicker : MonoBehaviour
{
    [SerializeField]
    float moveScale = 0.4f;

    [SerializeField]
    private GameObject vrRootNode;

    private HashSet<GameObject> weeds = new HashSet<GameObject>();

    List<UnityEngine.XR.InputDevice> gameControllers = new List<UnityEngine.XR.InputDevice>();

    // Start is called before the first frame update
    void Start()
    {
        UnityEngine.XR.InputDevices.GetDevicesWithRole(UnityEngine.XR.InputDeviceRole.RightHanded, gameControllers);
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var device in gameControllers)
        {
            //Debug.Log(string.Format("Device name '{0}' has role '{1}'", device.name, device.role.ToString()));
            bool triggerValue;
            if (device.TryGetFeatureValue(UnityEngine.XR.CommonUsages.triggerButton, out triggerValue) && triggerValue)
            {
                PickWeeds();
            }

            Vector2 moveAxis;
            if (device.TryGetFeatureValue(UnityEngine.XR.CommonUsages.primary2DAxis, out moveAxis))
            {
                var oldTransform = vrRootNode.transform.localPosition;

                oldTransform = new Vector3(oldTransform.x + moveScale * moveAxis.x, oldTransform.y, oldTransform.z + moveScale* moveAxis.y);

                vrRootNode.transform.localPosition = oldTransform;

                Debug.Log(string.Format("Device name {0} 'x: {1}' 'y: {2}'", device.name, moveAxis.x, moveAxis.y));   
            }


            bool primaryButton;
            if (device.TryGetFeatureValue(UnityEngine.XR.CommonUsages.primaryButton, out primaryButton))
            {
                if(primaryButton)
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }

    }

    void PickWeeds()
    {
        foreach(var weed in weeds)
        {
            Destroy(weed);
        }

        weeds.Clear();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Weed")
        {
            System.Console.WriteLine("weed found");
            weeds.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Weed")
        {
            weeds.Remove(other.gameObject);
        }
    }
}
