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
using UnityEngine;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;

namespace AbyssMoth
{
    public sealed class InsecureHttpOptionBuildHandler : IPreprocessBuildWithReport, IPostprocessBuildWithReport
    {
        public int callbackOrder => 0;

        private const string ProjectSettingsPath = "ProjectSettings/ProjectSettings.asset";
        private const string InsecureHttpOptionKey = "  insecureHttpOption: ";
        private string[] originalLines;

        public void OnPreprocessBuild(BuildReport report)
        {
            if (File.Exists(ProjectSettingsPath))
            {
                originalLines = File.ReadAllLines(ProjectSettingsPath);

                for (var i = 0; i < originalLines.Length; i++)
                {
                    if (originalLines[i].Contains(InsecureHttpOptionKey))
                    {
                        originalLines[i] = InsecureHttpOptionKey + "1";
                        break;
                    }
                }

                File.WriteAllLines(ProjectSettingsPath, originalLines);
                Debug.Log("Set insecureHttpOption to 1 for build.");
            }
            else
            {
                Debug.LogError("ProjectSettings.asset file not found.");
            }
        }

        public void OnPostprocessBuild(BuildReport report)
        {
            if (originalLines != null && File.Exists(ProjectSettingsPath))
            {
                for (var i = 0; i < originalLines.Length; i++)
                {
                    if (originalLines[i].Contains(InsecureHttpOptionKey))
                    {
                        originalLines[i] = InsecureHttpOptionKey + "0";
                        break;
                    }
                }

                File.WriteAllLines(ProjectSettingsPath, originalLines);
                Debug.Log("Restored insecureHttpOption to 0 after build.");
            }
            else
            {
                Debug.LogError(
                    "Original ProjectSettings.asset lines not found or ProjectSettings.asset file not found.");
            }
        }
    }
}