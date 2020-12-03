using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShutDownVersion : MonoBehaviour
{
    public const bool ShutDownActive = true;

    enum PlatformObject { Mobile, PC }

    [SerializeField] PlatformObject platformDisplay = PlatformObject.Mobile;

    private void Awake()
    {
        if (!ShutDownActive) return;

        if(Application.platform == RuntimePlatform.Android)
        {
            if (platformDisplay == PlatformObject.Mobile) gameObject.SetActive(true);
            else gameObject.SetActive(false);
        }
        else
        {
            if (platformDisplay == PlatformObject.Mobile) gameObject.SetActive(false);
            else gameObject.SetActive(true);
        }
    }
}
