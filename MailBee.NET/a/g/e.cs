using System;
using System.Configuration;
using System.Management;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using MailBee;
using MailBee.DnsMX;
using Microsoft.Win32;

namespace a.g
{
	// Token: 0x020003EF RID: 1007
	internal class e
	{
		// Token: 0x060023BB RID: 9147 RVA: 0x0009619C File Offset: 0x0009519C
		private static bool a(string A_0)
		{
			try
			{
				if (IPAddress.Parse(A_0).AddressFamily == AddressFamily.InterNetworkV6)
				{
					return true;
				}
			}
			catch (Exception)
			{
				return true;
			}
			return false;
		}

		// Token: 0x060023BC RID: 9148 RVA: 0x000961D8 File Offset: 0x000951D8
		public static int d(DnsServerCollection A_0, bool A_1)
		{
			string text = ConfigurationSettings.AppSettings[A_0.GetType().FullName];
			if (text == null)
			{
				return 0;
			}
			string[] array = text.Split(new char[]
			{
				';'
			});
			for (int i = 0; i < array.Length; i++)
			{
				string[] array2 = array[i].Split(new char[]
				{
					','
				});
				if (array2.Length != 0)
				{
					string text2 = array2[0].Trim();
					if (!A_1 || !e.a(text2))
					{
						int priority = 0;
						if (array2.Length > 1)
						{
							try
							{
								priority = int.Parse(array2[1].Trim());
							}
							catch (Exception)
							{
							}
						}
						A_0.Add(text2, priority);
					}
				}
			}
			return A_0.Count;
		}

		// Token: 0x060023BD RID: 9149 RVA: 0x0009628C File Offset: 0x0009528C
		public static int c(DnsServerCollection A_0, bool A_1)
		{
			RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("System\\CurrentControlSet\\Services\\Tcpip\\Parameters");
			if (registryKey == null)
			{
				return 0;
			}
			string text = (string)registryKey.GetValue("NameServer");
			registryKey.Close();
			if (text == null || text == string.Empty)
			{
				registryKey = Registry.LocalMachine.OpenSubKey("System\\CurrentControlSet\\Services\\Tcpip\\Parameters\\Interfaces");
				if (registryKey == null)
				{
					return 0;
				}
				string[] subKeyNames = registryKey.GetSubKeyNames();
				for (int i = 0; i < subKeyNames.Length; i++)
				{
					RegistryKey registryKey2 = registryKey.OpenSubKey(subKeyNames[i]);
					if (registryKey2 != null)
					{
						text = (string)registryKey2.GetValue("NameServer");
						registryKey2.Close();
						if (text != null && text != string.Empty)
						{
							break;
						}
					}
				}
				registryKey.Close();
			}
			if (text == null || text == string.Empty)
			{
				return 0;
			}
			string[] array = text.Split(new char[]
			{
				',',
				';',
				' '
			});
			for (int j = 0; j < array.Length; j++)
			{
				if (!A_1 || !e.a(array[j]))
				{
					try
					{
						A_0.Add(array[j], j);
					}
					catch (MailBeeInvalidArgumentException)
					{
					}
				}
			}
			return A_0.Count;
		}

		// Token: 0x060023BE RID: 9150 RVA: 0x000963B8 File Offset: 0x000953B8
		public static int b(DnsServerCollection A_0, bool A_1)
		{
			foreach (ManagementBaseObject managementBaseObject in new ManagementObjectSearcher("SELECT * FROM Win32_NetworkAdapterConfiguration WHERE IPEnabled=True").Get())
			{
				string[] array = (string[])((ManagementObject)managementBaseObject)["DNSServerSearchOrder"];
				if (array != null)
				{
					for (int i = 0; i < array.Length; i++)
					{
						if (!A_1 || !e.a(array[i]))
						{
							A_0.Add(array[i], i);
						}
					}
					if (A_0.Count > 0)
					{
						return A_0.Count;
					}
				}
			}
			return 0;
		}

		// Token: 0x060023BF RID: 9151 RVA: 0x0009645C File Offset: 0x0009545C
		public static int a(DnsServerCollection A_0, bool A_1)
		{
			NetworkInterface[] array = null;
			try
			{
				array = NetworkInterface.GetAllNetworkInterfaces();
			}
			catch (NotImplementedException)
			{
				return 0;
			}
			foreach (NetworkInterface networkInterface in array)
			{
				if (networkInterface.OperationalStatus == OperationalStatus.Up)
				{
					IPInterfaceProperties ipproperties = networkInterface.GetIPProperties();
					try
					{
						foreach (IPAddress ipaddress in ipproperties.DnsAddresses)
						{
							if (!A_1 || !e.a(ipaddress.ToString()))
							{
								A_0.Add(ipaddress.ToString());
							}
						}
					}
					catch (TypeLoadException)
					{
						return 0;
					}
				}
			}
			return A_0.Count;
		}

		// Token: 0x060023C0 RID: 9152 RVA: 0x0009652C File Offset: 0x0009552C
		public static int a(DnsServerCollection A_0)
		{
			A_0.Add("8.8.8.8");
			A_0.Add("8.8.4.4");
			return A_0.Count;
		}
	}
}
