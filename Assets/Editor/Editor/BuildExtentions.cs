using System;
using System.Globalization;
using UnityEditor;
using UnityEngine;

public static class BuildExtentions
{
    private const string ANDROID_BUILD_PATH_D = "C:/Users/HP/Desktop/TestBuild v.{0}.apk";
    private const string ANDROID_BUILD_PATH_R = "C:/Users/HP/Desktop/Build v.{0}.aab";

#if UNITY_ANDROID
    [MenuItem("Builds/Android Development")]
#endif
    public static void BuildDevelopmentAndroid()
    {
        CommonSetupAndroid();

        EditorUserBuildSettings.buildAppBundle = false;
        PlayerSettings.SetScriptingBackend(BuildTargetGroup.Android, ScriptingImplementation.Mono2x);
        PlayerSettings.Android.targetArchitectures = AndroidArchitecture.ARMv7;

        BuildPipeline.BuildPlayer(
            EditorBuildSettings.scenes,
            string.Format(ANDROID_BUILD_PATH_D, PlayerSettings.bundleVersion + "d"),
            BuildTarget.Android,
            BuildOptions.None
            );
        Debug.Log("DEV BUILD IS READY : V" + GetCurrentVerion());
    }

#if UNITY_ANDROID
    [MenuItem("Builds/Android Release")]
#endif
    public static void BuildReleaseAndroid()
    {
        CommonSetupAndroid();

        EditorUserBuildSettings.buildAppBundle = true;
        PlayerSettings.SetScriptingBackend(BuildTargetGroup.Android, ScriptingImplementation.IL2CPP);
        PlayerSettings.Android.targetArchitectures = AndroidArchitecture.ARM64;


        BuildPipeline.BuildPlayer(
            EditorBuildSettings.scenes,
            string.Format(ANDROID_BUILD_PATH_R, PlayerSettings.bundleVersion),
            BuildTarget.Android,
            BuildOptions.None
            );

        Debug.Log("RELEASE BUILD IS READY : V" + GetCurrentVerion());
    }

    public static void CommonSetupAndroid()
    {

        double versionCurrent = GetCurrentVerion();
        double versionNew = Math.Round(versionCurrent + 0.01f, 2);
        SetNewVersion(versionNew);
    }

    private static double GetCurrentVerion()
    {
        CultureInfo ci = (CultureInfo)CultureInfo.CurrentCulture.Clone();
        ci.NumberFormat.CurrencyDecimalSeparator = ".";
        return double.Parse(PlayerSettings.bundleVersion, NumberStyles.Any, ci);
    }

    private static void SetNewVersion(double newVersion)
    {
        PlayerSettings.bundleVersion = newVersion.ToString("0.00", CultureInfo.InvariantCulture);
    }
}
