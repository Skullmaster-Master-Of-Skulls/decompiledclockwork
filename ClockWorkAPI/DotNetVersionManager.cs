using System;
using System.Reflection;
using Microsoft.Win32;

namespace ClockWorkAPI
{
	// Token: 0x0200002D RID: 45
	public static class DotNetVersionManager
	{
		// Token: 0x06000255 RID: 597 RVA: 0x0000DB08 File Offset: 0x0000CB08
		public static DotNetVersions GetDotNetVersionsInstalled()
		{
			DotNetVersions installedVersions;
			if (DotNetVersionManager._installedVersions != DotNetVersions.NoVersions)
			{
				installedVersions = DotNetVersionManager._installedVersions;
			}
			else
			{
				try
				{
					RegistryKey localMachine = Registry.LocalMachine;
					foreach (object obj in Enum.GetValues(typeof(DotNetVersions)))
					{
						DotNetVersions dotNetVersions = (DotNetVersions)obj;
						RegKeyAttribute[] array = (RegKeyAttribute[])dotNetVersions.GetType().GetField(dotNetVersions.ToString()).GetCustomAttributes(typeof(RegKeyAttribute), false);
						if (array != null && array.Length > 0)
						{
							RegistryKey registryKey = localMachine.OpenSubKey(array[0].Name);
							if (registryKey != null && (int)registryKey.GetValue("Install") == 1)
							{
								DotNetVersionManager._installedVersions |= dotNetVersions;
							}
						}
					}
					installedVersions = DotNetVersionManager._installedVersions;
				}
				catch (Exception ex)
				{
					try
					{
						Assembly assembly = Assembly.LoadFrom("dotnet35TestLibrary.dll");
						Type[] types = assembly.GetTypes();
						MethodInfo method = types[0].GetMethod("Test", BindingFlags.Static | BindingFlags.Public);
						if ((bool)method.Invoke(null, null))
						{
							DotNetVersionManager._installedVersions |= DotNetVersions.DotnetVersion35;
						}
						installedVersions = DotNetVersionManager._installedVersions;
					}
					catch (Exception ex2)
					{
						DotNetVersionManager._installedVersions |= DotNetVersions.DotnetVersion20;
						installedVersions = DotNetVersionManager._installedVersions;
					}
				}
			}
			return installedVersions;
		}

		// Token: 0x06000256 RID: 598 RVA: 0x0000DCC0 File Offset: 0x0000CCC0
		public static bool IsInstalled(DotNetVersions dotnetNumber)
		{
			return (DotNetVersionManager.GetDotNetVersionsInstalled() & dotnetNumber) == dotnetNumber;
		}

		// Token: 0x06000257 RID: 599 RVA: 0x0000DCDC File Offset: 0x0000CCDC
		public static int GetVersionSP(DotNetVersions version)
		{
			int result;
			try
			{
				RegistryKey localMachine = Registry.LocalMachine;
				RegKeyAttribute[] array = (RegKeyAttribute[])version.GetType().GetField(version.ToString()).GetCustomAttributes(typeof(RegKeyAttribute), false);
				if (array != null && array.Length > 0)
				{
					RegistryKey registryKey = localMachine.OpenSubKey(array[0].Name);
					if (registryKey != null)
					{
						return (int)registryKey.GetValue("SP");
					}
				}
				result = 0;
			}
			catch (Exception)
			{
				result = 0;
			}
			return result;
		}

		// Token: 0x04000131 RID: 305
		private static DotNetVersions _installedVersions = DotNetVersions.NoVersions;
	}
}
