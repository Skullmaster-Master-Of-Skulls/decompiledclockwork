using System;
using System.Globalization;
using System.Net;
using System.Net.Sockets;

namespace Microsoft.Web.Management.Utility
{
	// Token: 0x0200007E RID: 126
	internal static class BindingUtility
	{
		// Token: 0x06000388 RID: 904 RVA: 0x000093A4 File Offset: 0x000083A4
		public static IPEndPoint EndPointFromBindingInformation(string bindingInformation)
		{
			string empty = string.Empty;
			return BindingUtility.EndPointFromBindingInformation(bindingInformation, out empty);
		}

		// Token: 0x06000389 RID: 905 RVA: 0x000093C0 File Offset: 0x000083C0
		public static IPEndPoint EndPointFromBindingInformation(string bindingInformation, out string hostHeader)
		{
			IPEndPoint result = null;
			string text = BindingUtility.ParseIPInfoFromBindingInformation(bindingInformation, 0);
			string s = BindingUtility.ParseIPInfoFromBindingInformation(bindingInformation, 1);
			hostHeader = BindingUtility.ParseIPInfoFromBindingInformation(bindingInformation, 2);
			int port = 0;
			if (int.TryParse(s, out port))
			{
				try
				{
					if (text == "*" || string.IsNullOrEmpty(text))
					{
						result = new IPEndPoint(IPAddress.Any, port);
					}
					else
					{
						IPAddress address = IPAddress.Parse(text);
						result = new IPEndPoint(address, port);
					}
				}
				catch (Exception)
				{
				}
			}
			return result;
		}

		// Token: 0x0600038A RID: 906 RVA: 0x00009440 File Offset: 0x00008440
		public static bool IsIPAddressValid(string ipAddress, out string formattedIPAddressString)
		{
			formattedIPAddressString = string.Empty;
			ipAddress = ipAddress.Trim();
			if (ipAddress == "*")
			{
				formattedIPAddressString = "*";
				return true;
			}
			IPAddress ipaddress;
			if (!IPAddress.TryParse(ipAddress, out ipaddress))
			{
				return false;
			}
			formattedIPAddressString = ipaddress.ToString();
			if (ipaddress.AddressFamily == AddressFamily.InterNetworkV6)
			{
				formattedIPAddressString = "[" + formattedIPAddressString + "]";
			}
			return true;
		}

		// Token: 0x0600038B RID: 907 RVA: 0x000094A4 File Offset: 0x000084A4
		public static string ParseIPInfoFromBindingInformation(string bindingInformation, int returnItem)
		{
			string result = string.Empty;
			string result2 = string.Empty;
			string result3 = string.Empty;
			string[] array = bindingInformation.Split(new char[]
			{
				':'
			});
			if (array.Length == 3)
			{
				result = array[0];
				result2 = array[1];
				result3 = array[2];
			}
			else if (array.Length > 2)
			{
				int length = bindingInformation.LastIndexOf(':');
				string text = bindingInformation.Substring(0, length);
				int length2 = text.LastIndexOf(':');
				result = bindingInformation.Substring(0, length2);
				result2 = array[array.Length - 2];
				result3 = array[array.Length - 1];
			}
			if (returnItem == 0)
			{
				return result;
			}
			if (returnItem == 1)
			{
				return result2;
			}
			if (returnItem == 2)
			{
				return result3;
			}
			return string.Empty;
		}

		// Token: 0x0600038C RID: 908 RVA: 0x00009544 File Offset: 0x00008544
		public static string ParseIPInfoFromBindingInformation(string bindingProtocol, string bindingInformation, int returnItem)
		{
			string text = bindingProtocol.ToUpper(CultureInfo.InvariantCulture);
			if (text.Equals("HTTP") || text.Equals("HTTPS") || text.Equals("FTP"))
			{
				return BindingUtility.ParseIPInfoFromBindingInformation(bindingInformation, returnItem);
			}
			return string.Empty;
		}
	}
}
