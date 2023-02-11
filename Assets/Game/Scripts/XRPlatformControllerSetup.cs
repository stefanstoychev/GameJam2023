using UnityEngine;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.XR.Management;
#else
using UnityEngine.XR.Management;
#endif

namespace Unity.Template.VR
{
    internal class XRPlatformControllerSetup : MonoBehaviour
    {
        [SerializeField]
        GameObject m_LeftController;

        [SerializeField]
        GameObject m_RightController;
        
        [SerializeField]
        GameObject m_LeftControllerOculusPackage;

        [SerializeField]
        GameObject m_RightControllerOculusPackage;

        List<UnityEngine.XR.InputDevice> gameControllers = new List<UnityEngine.XR.InputDevice>();

        void Start()
        {
#if UNITY_EDITOR
            var loaders = XRGeneralSettingsPerBuildTarget.XRGeneralSettingsForBuildTarget(BuildTargetGroup.Standalone).Manager.activeLoaders;
#else
            var loaders = XRGeneralSettings.Instance.Manager.activeLoaders;
#endif
            
            foreach (var loader in loaders)
            {
                if (loader.name.Equals("Oculus Loader"))
                {
                    m_RightController.SetActive(false);
                    m_LeftController.SetActive(false);
                    m_RightControllerOculusPackage.SetActive(true);
                    m_LeftControllerOculusPackage.SetActive(true);
                }
            }
            Debug.Log("Start called.");

            UnityEngine.XR.InputDevices.GetDevicesWithRole(UnityEngine.XR.InputDeviceRole.RightHanded, gameControllers);

        }

        void Update()
        {

            foreach (var device in gameControllers)
            {
                //Debug.Log(string.Format("Device name '{0}' has role '{1}'", device.name, device.role.ToString()));
                bool triggerValue;
                if (device.TryGetFeatureValue(UnityEngine.XR.CommonUsages.triggerButton, out triggerValue) && triggerValue)
                {
                    Debug.Log("Trigger button is pressed.");
                }
            }

        }
    }
}
