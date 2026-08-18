using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Win32;

namespace System.CodeDom.Compiler
{
	// Token: 0x02000684 RID: 1668
	internal static class RedistVersionInfo
	{
		// Token: 0x06003D7A RID: 15738 RVA: 0x000FC45C File Offset: 0x000FA65C
		public static string GetCompilerPath(IDictionary<string, string> provOptions, string compilerExecutable)
		{
			string text = Executor.GetRuntimeInstallDirectory();
			if (provOptions != null)
			{
				string result;
				bool flag = provOptions.TryGetValue("CompilerDirectoryPath", out result);
				string text2;
				bool flag2 = provOptions.TryGetValue("CompilerVersion", out text2);
				if (flag && flag2)
				{
					throw new InvalidOperationException(SR.GetString("Cannot_Specify_Both_Compiler_Path_And_Version", new object[]
					{
						"CompilerDirectoryPath",
						"CompilerVersion"
					}));
				}
				if (flag)
				{
					return result;
				}
				if (flag2 && !(text2 == "v4.0"))
				{
					if (!(text2 == "v3.5"))
					{
						if (!(text2 == "v2.0"))
						{
							text = null;
						}
						else
						{
							text = RedistVersionInfo.GetCompilerPathFromRegistry(text2);
						}
					}
					else
					{
						text = RedistVersionInfo.GetCompilerPathFromRegistry(text2);
					}
				}
			}
			if (text == null)
			{
				throw new InvalidOperationException(SR.GetString("CompilerNotFound", new object[]
				{
					compilerExecutable
				}));
			}
			return text;
		}

		// Token: 0x06003D7B RID: 15739 RVA: 0x000FC524 File Offset: 0x000FA724
		private static string GetCompilerPathFromRegistry(string versionVal)
		{
			string environmentVariable = Environment.GetEnvironmentVariable("COMPLUS_InstallRoot");
			string environmentVariable2 = Environment.GetEnvironmentVariable("COMPLUS_Version");
			string text;
			if (!string.IsNullOrEmpty(environmentVariable) && !string.IsNullOrEmpty(environmentVariable2))
			{
				text = Path.Combine(environmentVariable, environmentVariable2);
				if (Directory.Exists(text))
				{
					return text;
				}
			}
			string str = versionVal.Substring(1);
			string keyName = "HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\MSBuild\\ToolsVersions\\" + str;
			text = (Registry.GetValue(keyName, "MSBuildToolsPath", null) as string);
			if (text != null && Directory.Exists(text))
			{
				return text;
			}
			return null;
		}

		// Token: 0x04002CC8 RID: 11464
		internal const string DirectoryPath = "CompilerDirectoryPath";

		// Token: 0x04002CC9 RID: 11465
		internal const string NameTag = "CompilerVersion";

		// Token: 0x04002CCA RID: 11466
		internal const string DefaultVersion = "v4.0";

		// Token: 0x04002CCB RID: 11467
		internal const string InPlaceVersion = "v4.0";

		// Token: 0x04002CCC RID: 11468
		internal const string RedistVersion = "v3.5";

		// Token: 0x04002CCD RID: 11469
		internal const string RedistVersion20 = "v2.0";

		// Token: 0x04002CCE RID: 11470
		private const string MSBuildToolsPath = "MSBuildToolsPath";

		// Token: 0x04002CCF RID: 11471
		private const string dotNetFrameworkRegistryPath = "HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\MSBuild\\ToolsVersions\\";
	}
}
