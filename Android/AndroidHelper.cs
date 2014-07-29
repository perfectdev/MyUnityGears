using System;
using UnityEngine;

public class AndroidHelper {
    public static string GetInstallerId(string packageName) {
#if UNITY_ANDROID
        var installerId = "";
        try {
            AndroidJNI.AttachCurrentThread();
            using (var jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer")) {
                using (var curActivity = jc.GetStatic<AndroidJavaObject>("currentActivity")) {
                    using (var pm = curActivity.Call<AndroidJavaObject>("getPackageManager")) {
                        installerId = pm.Call<string>("getInstallerPackageName", new object[] { packageName });
                    }
                }
            }

        } catch (Exception ex) {
            Debug.Log(ex.Message);
        }
        return installerId;
#else
        return "";
#endif
    }
}
