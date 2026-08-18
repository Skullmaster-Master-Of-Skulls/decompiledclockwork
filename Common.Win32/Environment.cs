using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace TechnoPro.Common.Win32
{
	// Token: 0x02000012 RID: 18
	public static class Environment
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600006B RID: 107 RVA: 0x0000432C File Offset: 0x0000252C
		public static string FullComputerName
		{
			get
			{
				string domainName = IPGlobalProperties.GetIPGlobalProperties().DomainName;
				string hostName = Dns.GetHostName();
				if (hostName.Contains(domainName))
				{
					return hostName;
				}
				return string.Format("{0}.{1}", hostName, domainName);
			}
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00004364 File Offset: 0x00002564
		public static string GetIPAddress()
		{
			string hostName = Dns.GetHostName();
			if (!string.IsNullOrEmpty(hostName))
			{
				return Environment.GetIPAddress(hostName);
			}
			return string.Empty;
		}

		// Token: 0x0600006D RID: 109 RVA: 0x0000438C File Offset: 0x0000258C
		public static string GetIPAddress(string hostName)
		{
			string result;
			try
			{
				result = (from add in Dns.GetHostEntry(hostName).AddressList
				where add.AddressFamily == AddressFamily.InterNetwork
				select add).First<IPAddress>().ToString();
			}
			catch (Exception)
			{
				result = string.Empty;
			}
			return result;
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600006E RID: 110 RVA: 0x000043F0 File Offset: 0x000025F0
		public static string DomainName
		{
			get
			{
				return IPGlobalProperties.GetIPGlobalProperties().DomainName;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600006F RID: 111 RVA: 0x000043FC File Offset: 0x000025FC
		public static string ComputerName
		{
			get
			{
				return Dns.GetHostName();
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000070 RID: 112 RVA: 0x00004403 File Offset: 0x00002603
		public static bool Is64BitProcess
		{
			get
			{
				return IntPtr.Size == 8;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000071 RID: 113 RVA: 0x00004410 File Offset: 0x00002610
		public static bool Is64BitOperatingSystem
		{
			get
			{
				bool flag;
				return Environment.Is64BitProcess || (Environment.ModuleContainsFunction("kernel32.dll", "IsWow64Process") && Environment.IsWow64Process(Environment.GetCurrentProcess(), out flag) && flag);
			}
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00004448 File Offset: 0x00002648
		public static IList<DotNetVersion> GetDotNetVersionsInstalled()
		{
			List<DotNetVersion> list = new List<DotNetVersion>();
			string str = "SOFTWARE\\Microsoft\\NET Framework Setup\\NDP\\";
			try
			{
				string str2 = "v2.0.50727";
				int num = 0;
				if (Environment.GetRegistryValue<int>(RegistryHive.LocalMachine, str + str2, "Install", RegistryValueKind.DWord, out num) && num > 0)
				{
					list.Add(DotNetVersion.v2_0);
				}
			}
			catch
			{
			}
			try
			{
				string str2 = "v3.0";
				int num = 0;
				if (Environment.GetRegistryValue<int>(RegistryHive.LocalMachine, str + str2, "Install", RegistryValueKind.DWord, out num) && num > 0)
				{
					list.Add(DotNetVersion.v3_0);
				}
			}
			catch
			{
			}
			try
			{
				string str2 = "v3.5";
				int num = 0;
				if (Environment.GetRegistryValue<int>(RegistryHive.LocalMachine, str + str2, "Install", RegistryValueKind.DWord, out num) && num > 0)
				{
					list.Add(DotNetVersion.v3_5);
				}
			}
			catch
			{
			}
			try
			{
				string str2 = "v4.0";
				int num = 0;
				if (Environment.GetRegistryValue<int>(RegistryHive.LocalMachine, str + str2 + "\\Full", "Install", RegistryValueKind.DWord, out num) && num > 0)
				{
					list.Add(DotNetVersion.v4_0);
				}
			}
			catch
			{
			}
			try
			{
				string str2 = "v4";
				int num;
				int num2;
				if (Environment.GetRegistryValue<int>(RegistryHive.LocalMachine, str + str2 + "\\Full", "Install", RegistryValueKind.DWord, out num) && num > 0 && Environment.GetRegistryValue<int>(RegistryHive.LocalMachine, str + str2 + "\\Full", "Release", RegistryValueKind.DWord, out num2) && num2 > 0)
				{
					list.Add(Environment.CheckFor45PlusVersion(num2));
				}
			}
			catch
			{
			}
			list.Sort();
			return list;
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000073 RID: 115 RVA: 0x000045E0 File Offset: 0x000027E0
		public static InternetInformationServicesVersion ISSVersion
		{
			get
			{
				string key = "Software\\Microsoft\\InetStp";
				string value = "MajorVersion";
				string value2 = "MinorVersion";
				int num = 0;
				if (!Environment.GetRegistryValue<int>(RegistryHive.LocalMachine, key, value, RegistryValueKind.DWord, out num))
				{
					return InternetInformationServicesVersion.NoInstalled;
				}
				int num2 = -1;
				switch (num)
				{
				case 4:
					return InternetInformationServicesVersion.IIS4;
				case 5:
					if (!Environment.GetRegistryValue<int>(RegistryHive.LocalMachine, key, value2, RegistryValueKind.DWord, out num2))
					{
						return InternetInformationServicesVersion.IIS5;
					}
					if (num2 != 1)
					{
						return InternetInformationServicesVersion.IIS5;
					}
					return InternetInformationServicesVersion.IIS51;
				case 6:
					return InternetInformationServicesVersion.IIS6;
				case 7:
					if (!Environment.GetRegistryValue<int>(RegistryHive.LocalMachine, key, value2, RegistryValueKind.DWord, out num2))
					{
						return InternetInformationServicesVersion.IIS7;
					}
					if (num2 != 5)
					{
						return InternetInformationServicesVersion.IIS7;
					}
					return InternetInformationServicesVersion.IIS75;
				case 8:
					return InternetInformationServicesVersion.IIS8;
				default:
					if (num <= 7)
					{
						return InternetInformationServicesVersion.NoInstalled;
					}
					return InternetInformationServicesVersion.IIS75;
				}
			}
		}

		// Token: 0x06000074 RID: 116 RVA: 0x0000467C File Offset: 0x0000287C
		private static DotNetVersion CheckFor45PlusVersion(int releaseKey)
		{
			if (releaseKey >= 461808)
			{
				return DotNetVersion.v4_7_2;
			}
			if (releaseKey >= 461308)
			{
				return DotNetVersion.v4_7_1;
			}
			if (releaseKey >= 460798)
			{
				return DotNetVersion.v4_7;
			}
			if (releaseKey >= 394802)
			{
				return DotNetVersion.v4_6_2;
			}
			if (releaseKey >= 394254)
			{
				return DotNetVersion.v4_6_1;
			}
			if (releaseKey >= 393295)
			{
				return DotNetVersion.v4_6;
			}
			if (releaseKey >= 379893)
			{
				return DotNetVersion.v4_5_2;
			}
			if (releaseKey >= 378675)
			{
				return DotNetVersion.v4_5_1;
			}
			if (releaseKey >= 378389)
			{
				return DotNetVersion.v4_5;
			}
			return DotNetVersion.NotInstalled;
		}

		// Token: 0x06000075 RID: 117 RVA: 0x000046EC File Offset: 0x000028EC
		private static bool GetRegistryValue<T>(RegistryHive hive, string key, string value, RegistryValueKind kind, out T data)
		{
			bool result = false;
			data = default(T);
			using (RegistryKey registryKey = RegistryKey.OpenRemoteBaseKey(hive, string.Empty))
			{
				if (registryKey != null)
				{
					using (RegistryKey registryKey2 = registryKey.OpenSubKey(key, RegistryKeyPermissionCheck.ReadSubTree))
					{
						if (registryKey2 != null)
						{
							try
							{
								if (registryKey2.GetValueKind(value) == kind)
								{
									object value2 = registryKey2.GetValue(value, null);
									if (value2 != null)
									{
										data = (T)((object)Convert.ChangeType(value2, typeof(T), CultureInfo.InvariantCulture));
										result = true;
									}
								}
							}
							catch (IOException)
							{
							}
						}
					}
				}
			}
			return result;
		}

		// Token: 0x06000076 RID: 118 RVA: 0x000047A0 File Offset: 0x000029A0
		private static bool ModuleContainsFunction(string moduleName, string methodName)
		{
			IntPtr moduleHandle = Environment.GetModuleHandle(moduleName);
			return moduleHandle != IntPtr.Zero && Environment.GetProcAddress(moduleHandle, methodName) != IntPtr.Zero;
		}

		// Token: 0x06000077 RID: 119
		[DllImport("kernel32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool IsWow64Process(IntPtr hProcess, [MarshalAs(UnmanagedType.Bool)] out bool isWow64);

		// Token: 0x06000078 RID: 120
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern IntPtr GetCurrentProcess();

		// Token: 0x06000079 RID: 121
		[DllImport("kernel32.dll", CharSet = CharSet.Auto)]
		private static extern IntPtr GetModuleHandle(string moduleName);

		// Token: 0x0600007A RID: 122
		[DllImport("kernel32.dll", CharSet = CharSet.Ansi, SetLastError = true)]
		private static extern IntPtr GetProcAddress(IntPtr hModule, string methodName);
	}
}
