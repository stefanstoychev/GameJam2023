using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeePicker : MonoBehaviour
{
    private HashSet<GameObject> weeds = new HashSet<GameObject>();

    List<UnityEngine.XR.InputDevice> gameControllers = new List<UnityEngine.XR.InputDevice>();

    // Start is called before the first frame update
    void Start()
    {
        UnityEngine.XR.InputDevices.GetDevicesWithRole(UnityEngine.XR.InputDeviceRole.LeftHanded, gameControllers);
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
