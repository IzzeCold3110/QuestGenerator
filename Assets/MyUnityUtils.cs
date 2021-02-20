using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyUnityUtils : MonoBehaviour
{
    public static Int32 unixTimestamp()
    {
        return (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
    }
}
