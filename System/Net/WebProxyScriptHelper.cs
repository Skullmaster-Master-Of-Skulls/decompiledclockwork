using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Sockets;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace System.Net
{
	// Token: 0x020004B2 RID: 1202
	internal class WebProxyScriptHelper : IReflect
	{
		// Token: 0x06002513 RID: 9491 RVA: 0x000931EB File Offset: 0x000921EB
		MethodInfo IReflect.GetMethod(string name, BindingFlags bindingAttr, Binder binder, Type[] types, ParameterModifier[] modifiers)
		{
			return null;
		}

		// Token: 0x06002514 RID: 9492 RVA: 0x000931EE File Offset: 0x000921EE
		MethodInfo IReflect.GetMethod(string name, BindingFlags bindingAttr)
		{
			return null;
		}

		// Token: 0x06002515 RID: 9493 RVA: 0x000931F1 File Offset: 0x000921F1
		MethodInfo[] IReflect.GetMethods(BindingFlags bindingAttr)
		{
			return new MethodInfo[0];
		}

		// Token: 0x06002516 RID: 9494 RVA: 0x000931F9 File Offset: 0x000921F9
		FieldInfo IReflect.GetField(string name, BindingFlags bindingAttr)
		{
			return null;
		}

		// Token: 0x06002517 RID: 9495 RVA: 0x000931FC File Offset: 0x000921FC
		FieldInfo[] IReflect.GetFields(BindingFlags bindingAttr)
		{
			return new FieldInfo[0];
		}

		// Token: 0x06002518 RID: 9496 RVA: 0x00093204 File Offset: 0x00092204
		PropertyInfo IReflect.GetProperty(string name, BindingFlags bindingAttr)
		{
			return null;
		}

		// Token: 0x06002519 RID: 9497 RVA: 0x00093207 File Offset: 0x00092207
		PropertyInfo IReflect.GetProperty(string name, BindingFlags bindingAttr, Binder binder, Type returnType, Type[] types, ParameterModifier[] modifiers)
		{
			return null;
		}

		// Token: 0x0600251A RID: 9498 RVA: 0x0009320A File Offset: 0x0009220A
		PropertyInfo[] IReflect.GetProperties(BindingFlags bindingAttr)
		{
			return new PropertyInfo[0];
		}

		// Token: 0x0600251B RID: 9499 RVA: 0x00093214 File Offset: 0x00092214
		MemberInfo[] IReflect.GetMember(string name, BindingFlags bindingAttr)
		{
			return new MemberInfo[]
			{
				new WebProxyScriptHelper.MyMethodInfo(name)
			};
		}

		// Token: 0x0600251C RID: 9500 RVA: 0x00093232 File Offset: 0x00092232
		MemberInfo[] IReflect.GetMembers(BindingFlags bindingAttr)
		{
			return new MemberInfo[0];
		}

		// Token: 0x0600251D RID: 9501 RVA: 0x0009323A File Offset: 0x0009223A
		object IReflect.InvokeMember(string name, BindingFlags bindingAttr, Binder binder, object target, object[] args, ParameterModifier[] modifiers, CultureInfo culture, string[] namedParameters)
		{
			return null;
		}

		// Token: 0x170007AD RID: 1965
		// (get) Token: 0x0600251E RID: 9502 RVA: 0x0009323D File Offset: 0x0009223D
		Type IReflect.UnderlyingSystemType
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0600251F RID: 9503 RVA: 0x00093240 File Offset: 0x00092240
		public bool isPlainHostName(string hostName)
		{
			if (hostName == null)
			{
				if (Logging.On)
				{
					Logging.PrintWarning(Logging.Web, SR.GetString("net_log_proxy_called_with_null_parameter", new object[]
					{
						"WebProxyScriptHelper.isPlainHostName()",
						"hostName"
					}));
				}
				throw new ArgumentNullException("hostName");
			}
			return hostName.IndexOf('.') == -1;
		}

		// Token: 0x06002520 RID: 9504 RVA: 0x0009329C File Offset: 0x0009229C
		public bool dnsDomainIs(string host, string domain)
		{
			if (host == null)
			{
				if (Logging.On)
				{
					Logging.PrintWarning(Logging.Web, SR.GetString("net_log_proxy_called_with_null_parameter", new object[]
					{
						"WebProxyScriptHelper.dnsDomainIs()",
						"host"
					}));
				}
				throw new ArgumentNullException("host");
			}
			if (domain == null)
			{
				if (Logging.On)
				{
					Logging.PrintWarning(Logging.Web, SR.GetString("net_log_proxy_called_with_null_parameter", new object[]
					{
						"WebProxyScriptHelper.dnsDomainIs()",
						"domain"
					}));
				}
				throw new ArgumentNullException("domain");
			}
			int num = host.LastIndexOf(domain);
			return num != -1 && num + domain.Length == host.Length;
		}

		// Token: 0x06002521 RID: 9505 RVA: 0x0009334C File Offset: 0x0009234C
		public bool localHostOrDomainIs(string host, string hostDom)
		{
			if (host == null)
			{
				if (Logging.On)
				{
					Logging.PrintWarning(Logging.Web, SR.GetString("net_log_proxy_called_with_null_parameter", new object[]
					{
						"WebProxyScriptHelper.localHostOrDomainIs()",
						"host"
					}));
				}
				throw new ArgumentNullException("host");
			}
			if (hostDom == null)
			{
				if (Logging.On)
				{
					Logging.PrintWarning(Logging.Web, SR.GetString("net_log_proxy_called_with_null_parameter", new object[]
					{
						"WebProxyScriptHelper.localHostOrDomainIs()",
						"hostDom"
					}));
				}
				throw new ArgumentNullException("hostDom");
			}
			if (this.isPlainHostName(host))
			{
				int num = hostDom.IndexOf('.');
				if (num > 0)
				{
					hostDom = hostDom.Substring(0, num);
				}
			}
			return string.Compare(host, hostDom, StringComparison.OrdinalIgnoreCase) == 0;
		}

		// Token: 0x06002522 RID: 9506 RVA: 0x00093408 File Offset: 0x00092408
		public bool isResolvable(string host)
		{
			if (host == null)
			{
				if (Logging.On)
				{
					Logging.PrintWarning(Logging.Web, SR.GetString("net_log_proxy_called_with_null_parameter", new object[]
					{
						"WebProxyScriptHelper.isResolvable()",
						"host"
					}));
				}
				throw new ArgumentNullException("host");
			}
			IPHostEntry iphostEntry = null;
			try
			{
				iphostEntry = Dns.InternalGetHostByName(host);
			}
			catch
			{
			}
			if (iphostEntry == null)
			{
				return false;
			}
			for (int i = 0; i < iphostEntry.AddressList.Length; i++)
			{
				if (iphostEntry.AddressList[i].AddressFamily == AddressFamily.InterNetwork)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002523 RID: 9507 RVA: 0x000934A0 File Offset: 0x000924A0
		public string dnsResolve(string host)
		{
			if (host == null)
			{
				if (Logging.On)
				{
					Logging.PrintWarning(Logging.Web, SR.GetString("net_log_proxy_called_with_null_parameter", new object[]
					{
						"WebProxyScriptHelper.dnsResolve()",
						"host"
					}));
				}
				throw new ArgumentNullException("host");
			}
			IPHostEntry iphostEntry = null;
			try
			{
				iphostEntry = Dns.InternalGetHostByName(host);
			}
			catch
			{
			}
			if (iphostEntry == null)
			{
				return string.Empty;
			}
			for (int i = 0; i < iphostEntry.AddressList.Length; i++)
			{
				if (iphostEntry.AddressList[i].AddressFamily == AddressFamily.InterNetwork)
				{
					return iphostEntry.AddressList[i].ToString();
				}
			}
			return string.Empty;
		}

		// Token: 0x06002524 RID: 9508 RVA: 0x0009354C File Offset: 0x0009254C
		public string myIpAddress()
		{
			IPAddress[] localAddresses = NclUtilities.LocalAddresses;
			for (int i = 0; i < localAddresses.Length; i++)
			{
				if (!IPAddress.IsLoopback(localAddresses[i]) && localAddresses[i].AddressFamily == AddressFamily.InterNetwork)
				{
					return localAddresses[i].ToString();
				}
			}
			return string.Empty;
		}

		// Token: 0x06002525 RID: 9509 RVA: 0x00093590 File Offset: 0x00092590
		public int dnsDomainLevels(string host)
		{
			if (host == null)
			{
				if (Logging.On)
				{
					Logging.PrintWarning(Logging.Web, SR.GetString("net_log_proxy_called_with_null_parameter", new object[]
					{
						"WebProxyScriptHelper.dnsDomainLevels()",
						"host"
					}));
				}
				throw new ArgumentNullException("host");
			}
			int num = 0;
			int num2 = 0;
			while ((num = host.IndexOf('.', num)) != -1)
			{
				num2++;
				num++;
			}
			return num2;
		}

		// Token: 0x06002526 RID: 9510 RVA: 0x000935FC File Offset: 0x000925FC
		public bool isInNet(string host, string pattern, string mask)
		{
			if (host == null)
			{
				if (Logging.On)
				{
					Logging.PrintWarning(Logging.Web, SR.GetString("net_log_proxy_called_with_null_parameter", new object[]
					{
						"WebProxyScriptHelper.isInNet()",
						"host"
					}));
				}
				throw new ArgumentNullException("host");
			}
			if (pattern == null)
			{
				if (Logging.On)
				{
					Logging.PrintWarning(Logging.Web, SR.GetString("net_log_proxy_called_with_null_parameter", new object[]
					{
						"WebProxyScriptHelper.isInNet()",
						"pattern"
					}));
				}
				throw new ArgumentNullException("pattern");
			}
			if (mask == null)
			{
				if (Logging.On)
				{
					Logging.PrintWarning(Logging.Web, SR.GetString("net_log_proxy_called_with_null_parameter", new object[]
					{
						"WebProxyScriptHelper.isInNet()",
						"mask"
					}));
				}
				throw new ArgumentNullException("mask");
			}
			try
			{
				IPAddress ipaddress = IPAddress.Parse(host);
				IPAddress ipaddress2 = IPAddress.Parse(pattern);
				IPAddress ipaddress3 = IPAddress.Parse(mask);
				byte[] addressBytes = ipaddress3.GetAddressBytes();
				byte[] addressBytes2 = ipaddress.GetAddressBytes();
				byte[] addressBytes3 = ipaddress2.GetAddressBytes();
				if (addressBytes.Length != addressBytes2.Length || addressBytes.Length != addressBytes3.Length)
				{
					return false;
				}
				for (int i = 0; i < addressBytes.Length; i++)
				{
					if ((addressBytes3[i] & addressBytes[i]) != (addressBytes2[i] & addressBytes[i]))
					{
						return false;
					}
				}
			}
			catch
			{
				return false;
			}
			return true;
		}

		// Token: 0x06002527 RID: 9511 RVA: 0x00093764 File Offset: 0x00092764
		public bool shExpMatch(string host, string pattern)
		{
			if (host == null)
			{
				if (Logging.On)
				{
					Logging.PrintWarning(Logging.Web, SR.GetString("net_log_proxy_called_with_null_parameter", new object[]
					{
						"WebProxyScriptHelper.shExpMatch()",
						"host"
					}));
				}
				throw new ArgumentNullException("host");
			}
			if (pattern == null)
			{
				if (Logging.On)
				{
					Logging.PrintWarning(Logging.Web, SR.GetString("net_log_proxy_called_with_null_parameter", new object[]
					{
						"WebProxyScriptHelper.shExpMatch()",
						"pattern"
					}));
				}
				throw new ArgumentNullException("pattern");
			}
			bool result;
			try
			{
				ShellExpression shellExpression = new ShellExpression(pattern);
				result = shellExpression.IsMatch(host);
			}
			catch (FormatException)
			{
				result = false;
			}
			return result;
		}

		// Token: 0x06002528 RID: 9512 RVA: 0x0009381C File Offset: 0x0009281C
		public bool weekdayRange(string wd1, [Optional] object wd2, [Optional] object gmt)
		{
			if (wd1 == null)
			{
				if (Logging.On)
				{
					Logging.PrintWarning(Logging.Web, SR.GetString("net_log_proxy_called_with_null_parameter", new object[]
					{
						"WebProxyScriptHelper.weekdayRange()",
						"wd1"
					}));
				}
				throw new ArgumentNullException("wd1");
			}
			string text = null;
			string text2 = null;
			if (gmt != null && gmt != DBNull.Value && gmt != Missing.Value)
			{
				text = (gmt as string);
				if (text == null)
				{
					throw new ArgumentException(SR.GetString("net_param_not_string", new object[]
					{
						gmt.GetType().FullName
					}), "gmt");
				}
			}
			if (wd2 != null && wd2 != DBNull.Value && gmt != Missing.Value)
			{
				text2 = (wd2 as string);
				if (text2 == null)
				{
					throw new ArgumentException(SR.GetString("net_param_not_string", new object[]
					{
						wd2.GetType().FullName
					}), "wd2");
				}
			}
			if (text != null)
			{
				if (!WebProxyScriptHelper.isGMT(text))
				{
					if (Logging.On)
					{
						Logging.PrintWarning(Logging.Web, SR.GetString("net_log_proxy_called_with_null_parameter", new object[]
						{
							"WebProxyScriptHelper.weekdayRange()",
							"gmt"
						}));
					}
					throw new ArgumentException(SR.GetString("net_proxy_not_gmt"), "gmt");
				}
				return WebProxyScriptHelper.weekdayRangeInternal(DateTime.UtcNow, WebProxyScriptHelper.dayOfWeek(wd1), WebProxyScriptHelper.dayOfWeek(text2));
			}
			else
			{
				if (text2 == null)
				{
					return WebProxyScriptHelper.weekdayRangeInternal(DateTime.Now, WebProxyScriptHelper.dayOfWeek(wd1), WebProxyScriptHelper.dayOfWeek(wd1));
				}
				if (WebProxyScriptHelper.isGMT(text2))
				{
					return WebProxyScriptHelper.weekdayRangeInternal(DateTime.UtcNow, WebProxyScriptHelper.dayOfWeek(wd1), WebProxyScriptHelper.dayOfWeek(wd1));
				}
				return WebProxyScriptHelper.weekdayRangeInternal(DateTime.Now, WebProxyScriptHelper.dayOfWeek(wd1), WebProxyScriptHelper.dayOfWeek(text2));
			}
		}

		// Token: 0x06002529 RID: 9513 RVA: 0x000939BE File Offset: 0x000929BE
		private static bool isGMT(string gmt)
		{
			return string.Compare(gmt, "GMT", StringComparison.OrdinalIgnoreCase) == 0;
		}

		// Token: 0x0600252A RID: 9514 RVA: 0x000939D0 File Offset: 0x000929D0
		private static DayOfWeek dayOfWeek(string weekDay)
		{
			if (weekDay != null && weekDay.Length == 3)
			{
				if (weekDay[0] == 'T' || weekDay[0] == 't')
				{
					if ((weekDay[1] == 'U' || weekDay[1] == 'u') && (weekDay[2] == 'E' || weekDay[2] == 'e'))
					{
						return DayOfWeek.Tuesday;
					}
					if ((weekDay[1] == 'H' || weekDay[1] == 'h') && (weekDay[2] == 'U' || weekDay[2] == 'u'))
					{
						return DayOfWeek.Thursday;
					}
				}
				if (weekDay[0] == 'S' || weekDay[0] == 's')
				{
					if ((weekDay[1] == 'U' || weekDay[1] == 'u') && (weekDay[2] == 'N' || weekDay[2] == 'n'))
					{
						return DayOfWeek.Sunday;
					}
					if ((weekDay[1] == 'A' || weekDay[1] == 'a') && (weekDay[2] == 'T' || weekDay[2] == 't'))
					{
						return DayOfWeek.Saturday;
					}
				}
				if ((weekDay[0] == 'M' || weekDay[0] == 'm') && (weekDay[1] == 'O' || weekDay[1] == 'o') && (weekDay[2] == 'N' || weekDay[2] == 'n'))
				{
					return DayOfWeek.Monday;
				}
				if ((weekDay[0] == 'W' || weekDay[0] == 'w') && (weekDay[1] == 'E' || weekDay[1] == 'e') && (weekDay[2] == 'D' || weekDay[2] == 'd'))
				{
					return DayOfWeek.Wednesday;
				}
				if ((weekDay[0] == 'F' || weekDay[0] == 'f') && (weekDay[1] == 'R' || weekDay[1] == 'r') && (weekDay[2] == 'I' || weekDay[2] == 'i'))
				{
					return DayOfWeek.Friday;
				}
			}
			return (DayOfWeek)(-1);
		}

		// Token: 0x0600252B RID: 9515 RVA: 0x00093BA0 File Offset: 0x00092BA0
		private static bool weekdayRangeInternal(DateTime now, DayOfWeek wd1, DayOfWeek wd2)
		{
			if (wd1 < DayOfWeek.Sunday || wd2 < DayOfWeek.Sunday)
			{
				if (Logging.On)
				{
					Logging.PrintWarning(Logging.Web, SR.GetString("net_log_proxy_called_with_invalid_parameter", new object[]
					{
						"WebProxyScriptHelper.weekdayRange()"
					}));
				}
				throw new ArgumentException(SR.GetString("net_proxy_invalid_dayofweek"), (wd1 < DayOfWeek.Sunday) ? "wd1" : "wd2");
			}
			if (wd1 <= wd2)
			{
				return wd1 <= now.DayOfWeek && now.DayOfWeek <= wd2;
			}
			return wd2 >= now.DayOfWeek || now.DayOfWeek >= wd1;
		}

		// Token: 0x0600252C RID: 9516 RVA: 0x00093C37 File Offset: 0x00092C37
		public string getClientVersion()
		{
			return "1.0";
		}

		// Token: 0x0600252D RID: 9517 RVA: 0x00093C40 File Offset: 0x00092C40
		public unsafe string sortIpAddressList(string IPAddressList)
		{
			if (IPAddressList == null || IPAddressList.Length == 0)
			{
				return string.Empty;
			}
			string[] array = IPAddressList.Split(new char[]
			{
				';'
			});
			if (array.Length > WebProxyScriptHelper.MAX_IPADDRESS_LIST_LENGTH)
			{
				throw new ArgumentException(string.Format(SR.GetString("net_max_ip_address_list_length_exceeded"), WebProxyScriptHelper.MAX_IPADDRESS_LIST_LENGTH), IPAddressList);
			}
			if (array.Length == 1)
			{
				return IPAddressList;
			}
			SocketAddress[] array2 = new SocketAddress[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = array[i].Trim();
				if (array[i].Length == 0)
				{
					throw new ArgumentException(SR.GetString("dns_bad_ip_address"), "IPAddressList");
				}
				SocketAddress socketAddress = new SocketAddress(AddressFamily.InterNetworkV6, 28);
				SocketError socketError = UnsafeNclNativeMethods.OSSOCK.WSAStringToAddress(array[i], AddressFamily.InterNetworkV6, IntPtr.Zero, socketAddress.m_Buffer, ref socketAddress.m_Size);
				if (socketError != SocketError.Success)
				{
					SocketAddress socketAddress2 = new SocketAddress(AddressFamily.InterNetwork, 16);
					socketError = UnsafeNclNativeMethods.OSSOCK.WSAStringToAddress(array[i], AddressFamily.InterNetwork, IntPtr.Zero, socketAddress2.m_Buffer, ref socketAddress2.m_Size);
					if (socketError != SocketError.Success)
					{
						throw new ArgumentException(SR.GetString("dns_bad_ip_address"), "IPAddressList");
					}
					IPEndPoint ipendPoint = new IPEndPoint(IPAddress.Any, 0);
					IPEndPoint ipendPoint2 = (IPEndPoint)ipendPoint.Create(socketAddress2);
					byte[] addressBytes = ipendPoint2.Address.GetAddressBytes();
					byte[] array3 = new byte[16];
					for (int j = 0; j < 10; j++)
					{
						array3[j] = 0;
					}
					array3[10] = byte.MaxValue;
					array3[11] = byte.MaxValue;
					array3[12] = addressBytes[0];
					array3[13] = addressBytes[1];
					array3[14] = addressBytes[2];
					array3[15] = addressBytes[3];
					IPAddress address = new IPAddress(array3);
					IPEndPoint ipendPoint3 = new IPEndPoint(address, ipendPoint2.Port);
					socketAddress = ipendPoint3.Serialize();
				}
				array2[i] = socketAddress;
			}
			int num = Marshal.SizeOf(typeof(UnsafeNclNativeMethods.OSSOCK.SOCKET_ADDRESS_LIST)) + (array2.Length - 1) * Marshal.SizeOf(typeof(UnsafeNclNativeMethods.OSSOCK.SOCKET_ADDRESS));
			Dictionary<IntPtr, KeyValuePair<SocketAddress, string>> dictionary = new Dictionary<IntPtr, KeyValuePair<SocketAddress, string>>();
			GCHandle[] array4 = new GCHandle[array2.Length];
			for (int k = 0; k < array2.Length; k++)
			{
				array4[k] = GCHandle.Alloc(array2[k].m_Buffer, GCHandleType.Pinned);
			}
			IntPtr intPtr = Marshal.AllocHGlobal(num);
			string result;
			try
			{
				UnsafeNclNativeMethods.OSSOCK.SOCKET_ADDRESS_LIST* ptr = (UnsafeNclNativeMethods.OSSOCK.SOCKET_ADDRESS_LIST*)((void*)intPtr);
				ptr->iAddressCount = array2.Length;
				UnsafeNclNativeMethods.OSSOCK.SOCKET_ADDRESS* ptr2 = &ptr->Addresses;
				for (int l = 0; l < ptr->iAddressCount; l++)
				{
					ptr2[l].iSockaddrLength = 28;
					ptr2[l].lpSockAddr = array4[l].AddrOfPinnedObject();
					dictionary[ptr2[l].lpSockAddr] = new KeyValuePair<SocketAddress, string>(array2[l], array[l]);
				}
				Socket socket = new Socket(AddressFamily.InterNetworkV6, SocketType.Stream, ProtocolType.Tcp);
				socket.IOControl((IOControlCode)((ulong)-939524071), intPtr, num, intPtr, num);
				StringBuilder stringBuilder = new StringBuilder();
				for (int m = 0; m < ptr->iAddressCount; m++)
				{
					IntPtr lpSockAddr = ptr2[m].lpSockAddr;
					stringBuilder.Append(dictionary[lpSockAddr].Value);
					if (m != ptr->iAddressCount - 1)
					{
						stringBuilder.Append(";");
					}
				}
				result = stringBuilder.ToString();
			}
			finally
			{
				if (intPtr != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(intPtr);
				}
				for (int n = 0; n < array4.Length; n++)
				{
					if (array4[n].IsAllocated)
					{
						array4[n].Free();
					}
				}
			}
			return result;
		}

		// Token: 0x0600252E RID: 9518 RVA: 0x00093FF8 File Offset: 0x00092FF8
		public bool isInNetEx(string ipAddress, string ipPrefix)
		{
			if (ipAddress == null)
			{
				if (Logging.On)
				{
					Logging.PrintWarning(Logging.Web, SR.GetString("net_log_proxy_called_with_null_parameter", new object[]
					{
						"WebProxyScriptHelper.isResolvable()",
						"ipAddress"
					}));
				}
				throw new ArgumentNullException("ipAddress");
			}
			if (ipPrefix == null)
			{
				if (Logging.On)
				{
					Logging.PrintWarning(Logging.Web, SR.GetString("net_log_proxy_called_with_null_parameter", new object[]
					{
						"WebProxyScriptHelper.isResolvable()",
						"ipPrefix"
					}));
				}
				throw new ArgumentNullException("ipPrefix");
			}
			IPAddress ipaddress;
			if (!IPAddress.TryParse(ipAddress, out ipaddress))
			{
				throw new FormatException(SR.GetString("dns_bad_ip_address"));
			}
			int num = ipPrefix.IndexOf("/");
			if (num < 0)
			{
				throw new FormatException(SR.GetString("net_bad_ip_address_prefix"));
			}
			string[] array = ipPrefix.Split(new char[]
			{
				'/'
			});
			if (array.Length != 2 || array[0] == null || array[0].Length == 0 || array[1] == null || array[1].Length == 0 || array[1].Length > 2)
			{
				throw new FormatException(SR.GetString("net_bad_ip_address_prefix"));
			}
			IPAddress ipaddress2;
			if (!IPAddress.TryParse(array[0], out ipaddress2))
			{
				throw new FormatException(SR.GetString("dns_bad_ip_address"));
			}
			int num2 = 0;
			if (!int.TryParse(array[1], out num2))
			{
				throw new FormatException(SR.GetString("net_bad_ip_address_prefix"));
			}
			if (ipaddress.AddressFamily != ipaddress2.AddressFamily)
			{
				throw new FormatException(SR.GetString("net_bad_ip_address_prefix"));
			}
			if ((ipaddress.AddressFamily == AddressFamily.InterNetworkV6 && (num2 < 1 || num2 > 64)) || (ipaddress.AddressFamily == AddressFamily.InterNetwork && (num2 < 1 || num2 > 32)))
			{
				throw new FormatException(SR.GetString("net_bad_ip_address_prefix"));
			}
			byte[] addressBytes = ipaddress2.GetAddressBytes();
			byte b = (byte)(num2 / 8);
			byte b2 = (byte)(num2 % 8);
			byte b3 = b;
			if (b2 != 0)
			{
				if ((255 & (int)addressBytes[(int)b] << (int)b2) != 0)
				{
					throw new FormatException(SR.GetString("net_bad_ip_address_prefix"));
				}
				b3 += 1;
			}
			int num3 = (ipaddress2.AddressFamily == AddressFamily.InterNetworkV6) ? 16 : 4;
			while ((int)b3 < num3)
			{
				byte[] array2 = addressBytes;
				byte b4 = b3;
				b3 = b4 + 1;
				if (array2[(int)b4])
				{
					throw new FormatException(SR.GetString("net_bad_ip_address_prefix"));
				}
			}
			byte[] addressBytes2 = ipaddress.GetAddressBytes();
			for (b3 = 0; b3 < b; b3 += 1)
			{
				if (addressBytes2[(int)b3] != addressBytes[(int)b3])
				{
					return false;
				}
			}
			if (b2 > 0)
			{
				byte b5 = addressBytes2[(int)b];
				byte b6 = addressBytes[(int)b];
				b5 = (byte)(b5 >> (int)(8 - b2));
				b5 = (byte)(b5 << (int)(8 - b2));
				if (b5 != b6)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600252F RID: 9519 RVA: 0x00094290 File Offset: 0x00093290
		public string myIpAddressEx()
		{
			StringBuilder stringBuilder = new StringBuilder();
			try
			{
				IPAddress[] localAddresses = NclUtilities.LocalAddresses;
				for (int i = 0; i < localAddresses.Length; i++)
				{
					if (!IPAddress.IsLoopback(localAddresses[i]))
					{
						stringBuilder.Append(localAddresses[i].ToString());
						if (i != localAddresses.Length - 1)
						{
							stringBuilder.Append(";");
						}
					}
				}
			}
			catch
			{
			}
			if (stringBuilder.Length <= 0)
			{
				return string.Empty;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06002530 RID: 9520 RVA: 0x00094310 File Offset: 0x00093310
		public string dnsResolveEx(string host)
		{
			if (host == null)
			{
				if (Logging.On)
				{
					Logging.PrintWarning(Logging.Web, SR.GetString("net_log_proxy_called_with_null_parameter", new object[]
					{
						"WebProxyScriptHelper.dnsResolve()",
						"host"
					}));
				}
				throw new ArgumentNullException("host");
			}
			IPHostEntry iphostEntry = null;
			try
			{
				iphostEntry = Dns.InternalGetHostByName(host);
			}
			catch
			{
			}
			if (iphostEntry == null)
			{
				return string.Empty;
			}
			IPAddress[] addressList = iphostEntry.AddressList;
			if (addressList.Length == 0)
			{
				return string.Empty;
			}
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < addressList.Length; i++)
			{
				stringBuilder.Append(addressList[i].ToString());
				if (i != addressList.Length - 1)
				{
					stringBuilder.Append(";");
				}
			}
			if (stringBuilder.Length <= 0)
			{
				return string.Empty;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06002531 RID: 9521 RVA: 0x000943E8 File Offset: 0x000933E8
		public bool isResolvableEx(string host)
		{
			if (host == null)
			{
				if (Logging.On)
				{
					Logging.PrintWarning(Logging.Web, SR.GetString("net_log_proxy_called_with_null_parameter", new object[]
					{
						"WebProxyScriptHelper.dnsResolve()",
						"host"
					}));
				}
				throw new ArgumentNullException("host");
			}
			IPHostEntry iphostEntry = null;
			try
			{
				iphostEntry = Dns.InternalGetHostByName(host);
			}
			catch
			{
			}
			if (iphostEntry == null)
			{
				return false;
			}
			IPAddress[] addressList = iphostEntry.AddressList;
			return addressList.Length != 0;
		}

		// Token: 0x04002510 RID: 9488
		private static int MAX_IPADDRESS_LIST_LENGTH = 1024;

		// Token: 0x020004B3 RID: 1203
		private class MyMethodInfo : MethodInfo
		{
			// Token: 0x06002534 RID: 9524 RVA: 0x0009447C File Offset: 0x0009347C
			public MyMethodInfo(string name)
			{
				this.name = name;
			}

			// Token: 0x170007AE RID: 1966
			// (get) Token: 0x06002535 RID: 9525 RVA: 0x0009448C File Offset: 0x0009348C
			public override Type ReturnType
			{
				get
				{
					Type result = null;
					if (string.Compare(this.name, "isPlainHostName", StringComparison.Ordinal) == 0)
					{
						result = typeof(bool);
					}
					else if (string.Compare(this.name, "dnsDomainIs", StringComparison.Ordinal) == 0)
					{
						result = typeof(bool);
					}
					else if (string.Compare(this.name, "localHostOrDomainIs", StringComparison.Ordinal) == 0)
					{
						result = typeof(bool);
					}
					else if (string.Compare(this.name, "isResolvable", StringComparison.Ordinal) == 0)
					{
						result = typeof(bool);
					}
					else if (string.Compare(this.name, "dnsResolve", StringComparison.Ordinal) == 0)
					{
						result = typeof(string);
					}
					else if (string.Compare(this.name, "myIpAddress", StringComparison.Ordinal) == 0)
					{
						result = typeof(string);
					}
					else if (string.Compare(this.name, "dnsDomainLevels", StringComparison.Ordinal) == 0)
					{
						result = typeof(int);
					}
					else if (string.Compare(this.name, "isInNet", StringComparison.Ordinal) == 0)
					{
						result = typeof(bool);
					}
					else if (string.Compare(this.name, "shExpMatch", StringComparison.Ordinal) == 0)
					{
						result = typeof(bool);
					}
					else if (string.Compare(this.name, "weekdayRange", StringComparison.Ordinal) == 0)
					{
						result = typeof(bool);
					}
					else if (Socket.OSSupportsIPv6)
					{
						if (string.Compare(this.name, "dnsResolveEx", StringComparison.Ordinal) == 0)
						{
							result = typeof(string);
						}
						else if (string.Compare(this.name, "isResolvableEx", StringComparison.Ordinal) == 0)
						{
							result = typeof(bool);
						}
						else if (string.Compare(this.name, "myIpAddressEx", StringComparison.Ordinal) == 0)
						{
							result = typeof(string);
						}
						else if (string.Compare(this.name, "isInNetEx", StringComparison.Ordinal) == 0)
						{
							result = typeof(bool);
						}
						else if (string.Compare(this.name, "sortIpAddressList", StringComparison.Ordinal) == 0)
						{
							result = typeof(string);
						}
						else if (string.Compare(this.name, "getClientVersion", StringComparison.Ordinal) == 0)
						{
							result = typeof(string);
						}
					}
					return result;
				}
			}

			// Token: 0x170007AF RID: 1967
			// (get) Token: 0x06002536 RID: 9526 RVA: 0x000946C5 File Offset: 0x000936C5
			public override ICustomAttributeProvider ReturnTypeCustomAttributes
			{
				get
				{
					return null;
				}
			}

			// Token: 0x170007B0 RID: 1968
			// (get) Token: 0x06002537 RID: 9527 RVA: 0x000946C8 File Offset: 0x000936C8
			public override RuntimeMethodHandle MethodHandle
			{
				get
				{
					return default(RuntimeMethodHandle);
				}
			}

			// Token: 0x170007B1 RID: 1969
			// (get) Token: 0x06002538 RID: 9528 RVA: 0x000946DE File Offset: 0x000936DE
			public override MethodAttributes Attributes
			{
				get
				{
					return MethodAttributes.Public;
				}
			}

			// Token: 0x170007B2 RID: 1970
			// (get) Token: 0x06002539 RID: 9529 RVA: 0x000946E1 File Offset: 0x000936E1
			public override string Name
			{
				get
				{
					return this.name;
				}
			}

			// Token: 0x170007B3 RID: 1971
			// (get) Token: 0x0600253A RID: 9530 RVA: 0x000946E9 File Offset: 0x000936E9
			public override Type DeclaringType
			{
				get
				{
					return typeof(WebProxyScriptHelper.MyMethodInfo);
				}
			}

			// Token: 0x170007B4 RID: 1972
			// (get) Token: 0x0600253B RID: 9531 RVA: 0x000946F5 File Offset: 0x000936F5
			public override Type ReflectedType
			{
				get
				{
					return null;
				}
			}

			// Token: 0x0600253C RID: 9532 RVA: 0x000946F8 File Offset: 0x000936F8
			public override object[] GetCustomAttributes(bool inherit)
			{
				return null;
			}

			// Token: 0x0600253D RID: 9533 RVA: 0x000946FB File Offset: 0x000936FB
			public override object[] GetCustomAttributes(Type type, bool inherit)
			{
				return null;
			}

			// Token: 0x0600253E RID: 9534 RVA: 0x000946FE File Offset: 0x000936FE
			public override bool IsDefined(Type type, bool inherit)
			{
				return type.Equals(typeof(WebProxyScriptHelper));
			}

			// Token: 0x0600253F RID: 9535 RVA: 0x00094710 File Offset: 0x00093710
			public override object Invoke(object target, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture)
			{
				return typeof(WebProxyScriptHelper).GetMethod(this.name, (BindingFlags)(-1)).Invoke(target, (BindingFlags)(-1), binder, args, culture);
			}

			// Token: 0x06002540 RID: 9536 RVA: 0x00094734 File Offset: 0x00093734
			public override ParameterInfo[] GetParameters()
			{
				return typeof(WebProxyScriptHelper).GetMethod(this.name, (BindingFlags)(-1)).GetParameters();
			}

			// Token: 0x06002541 RID: 9537 RVA: 0x0009475E File Offset: 0x0009375E
			public override MethodImplAttributes GetMethodImplementationFlags()
			{
				return MethodImplAttributes.IL;
			}

			// Token: 0x06002542 RID: 9538 RVA: 0x00094761 File Offset: 0x00093761
			public override MethodInfo GetBaseDefinition()
			{
				return null;
			}

			// Token: 0x170007B5 RID: 1973
			// (get) Token: 0x06002543 RID: 9539 RVA: 0x00094764 File Offset: 0x00093764
			public override Module Module
			{
				get
				{
					return base.GetType().Module;
				}
			}

			// Token: 0x04002511 RID: 9489
			private string name;
		}
	}
}
