using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using UnityEngine;

public class AlignNativeLibraries : MonoBehaviour
{
    public int callbackOrder => 999;

    // Windows 기준 objcopy 경로 설정
    string objcopyPath = @"D:\Unity\Editor\2022.3.39f1\Editor\Data\PlaybackEngines\AndroidPlayer\NDK\toolchains\llvm\prebuilt\windows-x86_64\bin\llvm-objcopy.exe";

    public void OnPostGenerateGradleAndroidProject(string path)
    {
        string[] abis = { "armeabi-v7a", "arm64-v8a", "x86", "x86_64" };

        foreach (string abi in abis)
        {
            string soPath = Path.Combine(path, "src", "main", "jniLibs", abi, "libil2cpp.so");
            if (File.Exists(soPath))
            {
                AlignLibrary(soPath);
            }
        }
    }

    void AlignLibrary(string soFile)
    {
        string alignedFile = soFile + ".aligned";

        ProcessStartInfo psi = new ProcessStartInfo
        {
            FileName = objcopyPath,
            Arguments = $"--pad-to=0x4000 --gap-fill=0x00 \"{soFile}\" \"{alignedFile}\"",
            UseShellExecute = false,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            CreateNoWindow = true
        };

        var process = Process.Start(psi);
        process.WaitForExit();

        if (File.Exists(alignedFile))
        {
            File.Delete(soFile);
            File.Move(alignedFile, soFile);
            UnityEngine.Debug.Log($"16KB aligned: {Path.GetFileName(soFile)}");
        }
        else
        {
            UnityEngine.Debug.LogWarning($"Failed to align {soFile}");
        }
    }
}
