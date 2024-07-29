// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: 
//          - Gmail:    rimuru.dev@gmail.com
//          - GitHub:   https://github.com/RimuruDev
//          - LinkedIn: https://www.linkedin.com/in/rimuru/
//
// **************************************************************** //

using System.IO;
using UnityEditor;
using UnityEngine;

namespace AbyssMoth
{
    public sealed class InsecureHttpOptionEditor : Editor
    {
        private const string ProjectSettingsPath = "ProjectSettings/ProjectSettings.asset";
        private const string InsecureHttpOptionKey = "  insecureHttpOption: ";

        [MenuItem("RimuruDev Tools/GA/Set insecureHttpOption to 1")]
        public static void SetInsecureHttpOption()
        {
            var lines = File.ReadAllLines(ProjectSettingsPath);
            if (File.Exists(ProjectSettingsPath))
            {
                for (var i = 0; i < lines.Length; i++)
                {
                    if (lines[i].Contains(InsecureHttpOptionKey))
                    {
                        lines[i] = InsecureHttpOptionKey + "1";
                        break;
                    }
                }

                File.WriteAllLines(ProjectSettingsPath, lines);
                Debug.Log("Set insecureHttpOption to 1.");
            }
            else
            {
                Debug.LogError("ProjectSettings.asset file not found.");
            }
        }

        [MenuItem("RimuruDev Tools/GA//Set insecureHttpOption to 0")]
        public static void ResetInsecureHttpOption()
        {
            if (File.Exists(ProjectSettingsPath))
            {
                var lines = File.ReadAllLines(ProjectSettingsPath);
                for (var i = 0; i < lines.Length; i++)
                {
                    if (lines[i].Contains(InsecureHttpOptionKey))
                    {
                        lines[i] = InsecureHttpOptionKey + "0";
                        break;
                    }
                }

                File.WriteAllLines(ProjectSettingsPath, lines);
                Debug.Log("Set insecureHttpOption to 0.");
            }
            else
            {
                Debug.LogError("ProjectSettings.asset file not found.");
            }
        }
    }
}