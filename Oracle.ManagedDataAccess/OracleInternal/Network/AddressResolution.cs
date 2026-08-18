using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using OracleInternal.Common;

namespace OracleInternal.Network
{
	// Token: 0x0200014E RID: 334
	internal class AddressResolution : IEnumerable
	{
		// Token: 0x06000D31 RID: 3377 RVA: 0x0008FB70 File Offset: 0x0008DD70
		private static void SetupValidChars()
		{
			for (int i = 0; i < AddressResolution.NCS.Length; i++)
			{
				AddressResolution.ValidChars[(int)AddressResolution.NCS[i]] = 1;
			}
			for (int i = 0; i < AddressResolution.reservedNCS.Length; i++)
			{
				AddressResolution.ValidChars[(int)AddressResolution.reservedNCS[i]] = 0;
			}
		}

		// Token: 0x06000D32 RID: 3378 RVA: 0x0008FBC0 File Offset: 0x0008DDC0
		private static string VetCIDValue(string CIDValue)
		{
			StringBuilder stringBuilder;
			try
			{
				stringBuilder = new StringBuilder(CIDValue);
				for (int i = 0; i < stringBuilder.Length; i++)
				{
					if (AddressResolution.ValidChars[(int)stringBuilder[i]] == 0)
					{
						stringBuilder[i] = '?';
					}
				}
			}
			catch (Exception)
			{
				return "";
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000D33 RID: 3379 RVA: 0x0008FC20 File Offset: 0x0008DE20
		[EnvironmentPermission(SecurityAction.Assert, Unrestricted = true)]
		static AddressResolution()
		{
			string[] namesDirectoryPath = SqlNetOraConfig.NamesDirectoryPath;
			AddressResolution._NamingAdapters.Add(AddressResolution._DataSourcesAdapter = new DataSources());
			if (namesDirectoryPath != null)
			{
				foreach (string text in namesDirectoryPath)
				{
					string a;
					if ((a = text.ToUpperInvariant()) != null)
					{
						if (!(a == "TNSNAMES"))
						{
							if (!(a == "LDAP"))
							{
								if (a == "EZCONNECT" || a == "HOSTNAME")
								{
									AddressResolution._NamingAdapters.Add(new EZConnect());
								}
							}
							else
							{
								AddressResolution._NamingAdapters.Add(new LDAP());
							}
						}
						else
						{
							AddressResolution._NamingAdapters.Add(new TNSNames());
						}
					}
				}
			}
			AddressResolution.SetupValidChars();
			try
			{
				AddressResolution.PROGRAMNAME = Environment.GetCommandLineArgs()[0];
			}
			catch (Exception)
			{
			}
			if (AddressResolution.PROGRAMNAME == null)
			{
				AddressResolution.PROGRAMNAME = "";
			}
			try
			{
				AddressResolution.HOSTNAME = Dns.GetHostName();
			}
			catch (Exception)
			{
			}
			if (AddressResolution.HOSTNAME == null && (AddressResolution.HOSTNAME = Environment.MachineName) == null)
			{
				AddressResolution.HOSTNAME = "";
			}
			if ((AddressResolution.USERNAME = Environment.UserName) == null)
			{
				AddressResolution.USERNAME = "";
			}
			AddressResolution.PROGRAMNAME = AddressResolution.VetCIDValue(AddressResolution.PROGRAMNAME);
			AddressResolution.USERNAME = AddressResolution.VetCIDValue(AddressResolution.USERNAME);
			AddressResolution.HOSTNAME = AddressResolution.VetCIDValue(AddressResolution.HOSTNAME);
			AddressResolution.val_CID = string.Concat(new string[]
			{
				AddressResolution.BEG_PROGRAM,
				AddressResolution.PROGRAMNAME,
				AddressResolution.BEG_HOST,
				AddressResolution.HOSTNAME,
				AddressResolution.BEG_USER,
				AddressResolution.USERNAME,
				AddressResolution.END_ONE_BRACE
			});
			try
			{
				AddressResolution.nvp_CID = new NVPair(AddressResolution.CID, AddressResolution.val_CID);
			}
			catch (Exception)
			{
			}
			AddressResolution.ful_CID = string.Concat(new string[]
			{
				AddressResolution.BEG_CID,
				AddressResolution.BEG_PROGRAM,
				AddressResolution.PROGRAMNAME,
				AddressResolution.BEG_HOST,
				AddressResolution.HOSTNAME,
				AddressResolution.BEG_USER,
				AddressResolution.USERNAME,
				AddressResolution.END_TWO_BRACES
			});
		}

		// Token: 0x06000D34 RID: 3380 RVA: 0x00090144 File Offset: 0x0008E344
		[EnvironmentPermission(SecurityAction.Assert, Unrestricted = true)]
		internal AddressResolution(string TNSAlias, string instanceName = null)
		{
			this.m_tnsAddress = TNSAlias;
			this.m_instanceName = instanceName;
			this.m_tnsAddress = AddressResolution.Resolve(TNSAlias, out this.m_ConnectionOption, instanceName);
			if (this.m_tnsAddress == null || this.m_tnsAddress.Length == 0)
			{
				throw new NetworkException(12154);
			}
		}

		// Token: 0x06000D35 RID: 3381 RVA: 0x00090198 File Offset: 0x0008E398
		public static void RefreshNamingAdapters()
		{
			foreach (INamingAdapter namingAdapter in AddressResolution._NamingAdapters)
			{
				namingAdapter.Refresh();
			}
		}

		// Token: 0x06000D36 RID: 3382 RVA: 0x000901EC File Offset: 0x0008E3EC
		public static IEnumerable NamingAdapters()
		{
			foreach (INamingAdapter NA in AddressResolution._NamingAdapters)
			{
				yield return NA;
			}
			yield break;
		}

		// Token: 0x06000D37 RID: 3383 RVA: 0x00090204 File Offset: 0x0008E404
		public static IEnumerable NamingAdapterMaps()
		{
			foreach (INamingAdapter NA in AddressResolution._NamingAdapters)
			{
				yield return NA.Map;
			}
			yield break;
		}

		// Token: 0x06000D38 RID: 3384 RVA: 0x0009021C File Offset: 0x0008E41C
		public IEnumerator GetEnumerator()
		{
			if (this.m_tnsAddress == null || this.m_tnsAddress.Length == 0)
			{
				throw new NetworkException(12154);
			}
			if (this.m_ConnectionOption != null)
			{
				yield return this.m_ConnectionOption;
			}
			else
			{
				if (this.m_tnsAddress.StartsWith(AddressResolution.ALIAS))
				{
					string tnsAddress = this.m_tnsAddress;
					this.m_tnsAddress = tnsAddress.Substring(tnsAddress.IndexOf(AddressResolution.ALIAS) + 6, tnsAddress.Length - (tnsAddress.IndexOf(AddressResolution.ALIAS) + 6));
				}
				NVPair tnsNVP = NVFactory.CreateNVPair(this.m_tnsAddress);
				NVPair dlist = NVNavigator.FindNVPairRecurse(tnsNVP, AddressResolution.DESCRIPTION_LIST);
				if (dlist != null)
				{
					NVNavigator dlistNVN = new NVNavigator(dlist);
					dlistNVN.SetFindString(AddressResolution.DESCRIPTION);
					if ((this.m_desc = dlistNVN.FindNVPair()) == null)
					{
						throw new NetworkException(12550);
					}
					while (this.m_desc != null)
					{
						int retries;
						int delay;
						this.GetRetries(out retries, out delay);
						int num;
						do
						{
							NVNavigator descNVN = this.getAddressContainer(this.m_desc);
							descNVN.SetFindString(AddressResolution.ADDRESS);
							NVPair addr = descNVN.FindNVPair();
							if (addr == null)
							{
								goto Block_6;
							}
							while (addr != null)
							{
								yield return this.BuildCO(this.m_desc, addr);
								addr = descNVN.FindNVPair();
							}
							if (delay > 0 && retries > 0)
							{
								Thread.Sleep(delay * 1000);
							}
							retries = (num = retries) - 1;
						}
						while (num > 0);
						this.m_desc = dlistNVN.FindNVPair();
						continue;
						Block_6:
						throw new NetworkException(12550);
					}
				}
				else
				{
					this.m_desc = NVNavigator.FindNVPairRecurse(tnsNVP, AddressResolution.DESCRIPTION);
					if (this.m_desc == null)
					{
						this.m_tnsAddress = string.Concat(new string[]
						{
							AddressResolution.BEG_DESC,
							AddressResolution.BEG_CONDATA,
							AddressResolution.END_ONE_BRACE,
							this.m_tnsAddress,
							AddressResolution.END_ONE_BRACE
						});
						tnsNVP = NVFactory.CreateNVPair(this.m_tnsAddress);
						this.m_desc = NVNavigator.FindNVPairRecurse(tnsNVP, AddressResolution.DESCRIPTION);
					}
					if (this.m_desc == null)
					{
						throw new NetworkException(12550);
					}
					int retries;
					int delay;
					this.GetRetries(out retries, out delay);
					for (;;)
					{
						NVNavigator descNVN = this.getAddressContainer(this.m_desc);
						descNVN.SetFindString(AddressResolution.ADDRESS);
						NVPair addr = descNVN.FindNVPair();
						if (addr == null)
						{
							break;
						}
						while (addr != null)
						{
							yield return this.BuildCO(this.m_desc, addr);
							addr = descNVN.FindNVPair();
						}
						if (delay > 0 && retries > 0)
						{
							Thread.Sleep(delay * 1000);
						}
						int num2;
						retries = (num2 = retries) - 1;
						if (num2 <= 0)
						{
							goto IL_41F;
						}
					}
					throw new NetworkException(12550);
				}
			}
			IL_41F:
			yield break;
		}

		// Token: 0x06000D39 RID: 3385 RVA: 0x00090238 File Offset: 0x0008E438
		private void GetRetries(out int retries, out int delay)
		{
			retries = (delay = 0);
			NVPair nvpair;
			if ((nvpair = NVNavigator.FindNVPair(this.m_desc, AddressResolution.RETRY_COUNT)) != null)
			{
				retries = int.Parse(nvpair.Atom);
				if ((nvpair = NVNavigator.FindNVPair(this.m_desc, AddressResolution.RETRY_DELAY)) != null)
				{
					delay = int.Parse(nvpair.Atom);
				}
			}
		}

		// Token: 0x06000D3A RID: 3386 RVA: 0x00090290 File Offset: 0x0008E490
		private NVNavigator getAddressContainer(NVPair m_desc)
		{
			NVPair nvpair = NVNavigator.FindNVPair(m_desc, AddressResolution.ADDRESS_LIST);
			if (nvpair != null)
			{
				return new NVNavigator(nvpair);
			}
			return new NVNavigator(m_desc);
		}

		// Token: 0x06000D3B RID: 3387 RVA: 0x000902BC File Offset: 0x0008E4BC
		internal static string Resolve(string TNSAlias, out ConnectionOption CO, string instanceName = null)
		{
			string text = null;
			CO = null;
			if (TNSAlias == null || TNSAlias.Length == 0)
			{
				string text2 = ConfigBaseClass.m_configParameters[AddressResolution.SID_ENV] as string;
				if (string.IsNullOrEmpty(text2))
				{
					text2 = Environment.GetEnvironmentVariable(AddressResolution.SID_ENV);
				}
				if (!string.IsNullOrEmpty(text2))
				{
					text = AddressResolution.DEFAULT_ADDRESS + text2 + ")))";
				}
			}
			else if (TNSAlias.IndexOf('(') != -1)
			{
				text = TNSAlias;
			}
			else
			{
				if (!string.IsNullOrEmpty(SqlNetOraConfig.NamesDefaultDomain) && TNSAlias.IndexOf(':') == -1 && TNSAlias.IndexOf('/') == -1)
				{
					int num = TNSAlias.IndexOf('.');
					if (num == -1)
					{
						TNSAlias = TNSAlias + "." + SqlNetOraConfig.NamesDefaultDomain;
					}
					else if (num == TNSAlias.Length - 1)
					{
						TNSAlias = TNSAlias.Substring(0, TNSAlias.Length - 1);
					}
				}
				foreach (INamingAdapter namingAdapter in AddressResolution._NamingAdapters)
				{
					text = namingAdapter.Resolve(TNSAlias, out CO, instanceName);
					if (text != null)
					{
						break;
					}
				}
			}
			return text;
		}

		// Token: 0x06000D3C RID: 3388 RVA: 0x000903E0 File Offset: 0x0008E5E0
		internal ConnectionOption ResolveConnectionString()
		{
			if (this.m_tnsAddress == null || this.m_tnsAddress.Length == 0)
			{
				throw new NetworkException(-6500);
			}
			if (this.m_ConnectionOption != null)
			{
				return this.m_ConnectionOption;
			}
			if (this.m_tnsAddress.IndexOf(')') == -1)
			{
				return this.ResolveSimple(this.m_tnsAddress);
			}
			return this.ResolveAddr(this.m_tnsAddress);
		}

		// Token: 0x06000D3D RID: 3389 RVA: 0x00090448 File Offset: 0x0008E648
		private string GetValue(string TNSdesc, ref int POS)
		{
			POS++;
			int num = TNSdesc.IndexOfAny(AddressResolution.colonORslash, POS);
			if (num == -1)
			{
				num = TNSdesc.Length;
			}
			string result = TNSdesc.Substring(POS, num - POS);
			POS = num;
			return result;
		}

		// Token: 0x06000D3E RID: 3390 RVA: 0x00090488 File Offset: 0x0008E688
		private ConnectionOption ResolveSimple(string TNSdesc)
		{
			ConnectionOption connectionOption = new ConnectionOption();
			int num = 0;
			connectionOption.Protocol = AddressResolution.DEFAULT_CONNECT_PROTOCOL;
			if (TNSdesc.StartsWith(AddressResolution.BEGIN_DBL_SLASH))
			{
				num = 2;
			}
			if (TNSdesc[num] == '[')
			{
				num++;
				int num2 = TNSdesc.IndexOf(']', num);
				if (num2 == -1)
				{
					throw new NetworkException(12550);
				}
				connectionOption.Host = TNSdesc.Substring(num, num2 - num);
				num = num2 + 1;
			}
			else
			{
				int num2 = TNSdesc.IndexOfAny(AddressResolution.colonORslash, num);
				if (num2 == -1)
				{
					num2 = TNSdesc.Length - 1;
				}
				connectionOption.Host = TNSdesc.Substring(num, num2 - num);
				num = num2;
			}
			if (num < TNSdesc.Length)
			{
				if (TNSdesc[num] == ':')
				{
					string value = this.GetValue(TNSdesc, ref num);
					if (!string.IsNullOrEmpty(value) && !int.TryParse(value, out connectionOption.m_portNumber))
					{
						throw new NetworkException(12545);
					}
				}
				if (num < TNSdesc.Length && TNSdesc[num] == '/')
				{
					connectionOption.ServiceName = this.GetValue(TNSdesc, ref num);
					if (num < TNSdesc.Length)
					{
						if (TNSdesc[num] == ':')
						{
							connectionOption.Server = this.GetValue(TNSdesc, ref num);
						}
						if (num < TNSdesc.Length && TNSdesc[num] == '/')
						{
							connectionOption.InstanceName = this.GetValue(TNSdesc, ref num);
						}
					}
				}
			}
			if (connectionOption.Port == -1)
			{
				connectionOption.Port = 1521;
			}
			if (connectionOption.ServiceName == null && SqlNetOraConfig.HostnameDefaultServiceIsHost)
			{
				connectionOption.ServiceName = connectionOption.Host;
			}
			connectionOption.Address = new StringBuilder().Append(AddressResolution.BEG_ADDR).Append(connectionOption.Protocol).Append(AddressResolution.BEG_HOST).Append(connectionOption.Host).Append(AddressResolution.BEG_PORT).Append(connectionOption.Port).Append(AddressResolution.END_TWO_BRACES).ToString();
			StringBuilder stringBuilder;
			if (connectionOption.ServiceName != null)
			{
				stringBuilder = new StringBuilder().Append(AddressResolution.BEG_CONDATA_SVC).Append(connectionOption.ServiceName).Append(AddressResolution.END_ONE_BRACE);
			}
			else
			{
				stringBuilder = new StringBuilder().Append(AddressResolution.BEG_CONDATA);
			}
			if (connectionOption.Server != null)
			{
				stringBuilder = stringBuilder.Append(AddressResolution.BEG_SRVR).Append(connectionOption.Server).Append(AddressResolution.END_ONE_BRACE);
			}
			if (this.m_instanceName != null)
			{
				stringBuilder = stringBuilder.Append(AddressResolution.BEG_INST).Append(this.m_instanceName).Append(AddressResolution.END_ONE_BRACE);
			}
			else if (connectionOption.InstanceName != null)
			{
				stringBuilder = stringBuilder.Append(AddressResolution.BEG_INST).Append(connectionOption.InstanceName).Append(AddressResolution.END_ONE_BRACE);
			}
			if (AddressResolution.ful_CID != null)
			{
				stringBuilder = stringBuilder.Append(AddressResolution.ful_CID);
			}
			connectionOption.ConnectData = new StringBuilder().Append(AddressResolution.BEG_DESC).Append(connectionOption.Address).Append(stringBuilder).Append(AddressResolution.END_TWO_BRACES).ToString();
			return connectionOption;
		}

		// Token: 0x06000D3F RID: 3391 RVA: 0x00090760 File Offset: 0x0008E960
		private ConnectionOption ResolveAddr(string TNSdesc)
		{
			new ConnectionOption();
			if (TNSdesc.StartsWith(AddressResolution.ALIAS))
			{
				string text = TNSdesc;
				TNSdesc = text.Substring(text.IndexOf(AddressResolution.ALIAS) + 6, text.Length - (text.IndexOf(AddressResolution.ALIAS) + 6));
			}
			NVPair nvp = NVFactory.CreateNVPair(TNSdesc);
			NVNavigator.FindNVPairRecurse(nvp, AddressResolution.DESCRIPTION_LIST);
			if (this.m_dlist != null)
			{
				this.m_desc = NVNavigator.FindNVPair(this.m_dlist, AddressResolution.DESCRIPTION);
			}
			else
			{
				this.m_desc = NVNavigator.FindNVPairRecurse(nvp, AddressResolution.DESCRIPTION);
			}
			NVPair addr;
			if (this.m_desc != null)
			{
				addr = NVNavigator.FindNVPair(this.m_desc, AddressResolution.ADDRESS);
			}
			else
			{
				addr = NVNavigator.FindNVPairRecurse(nvp, AddressResolution.ADDRESS);
			}
			return this.BuildCO(this.m_desc, addr);
		}

		// Token: 0x06000D40 RID: 3392 RVA: 0x00090824 File Offset: 0x0008EA24
		internal void BuildCO_Redirect(string cndr, ref ConnectionOption co)
		{
			if (cndr == null)
			{
				throw new NetworkException(12533);
			}
			NVPair nvpair = NVFactory.CreateNVPair(cndr);
			NVPair addr;
			if (nvpair.Name.ToUpperInvariant() == AddressResolution.DESCRIPTION)
			{
				addr = NVNavigator.FindNVPair(nvpair, AddressResolution.ADDRESS);
			}
			else
			{
				addr = nvpair;
			}
			this.BuildCO_Addr(addr, ref co);
		}

		// Token: 0x06000D41 RID: 3393 RVA: 0x00090878 File Offset: 0x0008EA78
		internal void BuildCO_Addr(NVPair addr, ref ConnectionOption co)
		{
			NVPair nvpair = NVNavigator.FindNVPair(addr, AddressResolution.PROTOCOL);
			if (nvpair == null)
			{
				throw new NetworkException(12533);
			}
			co.Protocol = nvpair.Atom;
			if (!string.Equals(co.Protocol, AddressResolution.DEFAULT_CONNECT_PROTOCOL, StringComparison.InvariantCultureIgnoreCase) && !string.Equals(co.Protocol, AddressResolution.SSL_CONNECT_PROTOCOL, StringComparison.InvariantCultureIgnoreCase))
			{
				throw new NetworkException(12533);
			}
			nvpair = NVNavigator.FindNVPair(addr, AddressResolution.HOST);
			if (nvpair == null || nvpair.Atom == null)
			{
				co.Host = "";
			}
			else
			{
				co.Host = nvpair.Atom;
			}
			nvpair = NVNavigator.FindNVPair(addr, AddressResolution.PORT);
			if (nvpair == null || nvpair.Atom == null)
			{
				co.Port = 0;
			}
			else
			{
				co.Port = int.Parse(nvpair.Atom);
			}
			nvpair = NVNavigator.FindNVPair(addr, AddressResolution.IP);
			if (nvpair == null || nvpair.Atom == null)
			{
				co.IP = null;
			}
			else
			{
				co.IP = nvpair.Atom;
			}
			nvpair = (NVNavigator.FindNVPair(addr, AddressResolution.SBS) ?? NVNavigator.FindNVPair(addr, AddressResolution.SendBufSize));
			if (nvpair != null && nvpair.Atom != null && (co.SBS = int.Parse(nvpair.Atom)) <= 0)
			{
				co.SBS = 0;
			}
			nvpair = (NVNavigator.FindNVPair(addr, AddressResolution.RBS) ?? NVNavigator.FindNVPair(addr, AddressResolution.RecvBufSize));
			if (nvpair != null && nvpair.Atom != null && (co.RBS = int.Parse(nvpair.Atom)) <= 0)
			{
				co.RBS = 0;
			}
		}

		// Token: 0x06000D42 RID: 3394 RVA: 0x00090A00 File Offset: 0x0008EC00
		private ConnectionOption BuildCO(NVPair desc, NVPair addr)
		{
			ConnectionOption connectionOption = new ConnectionOption();
			NVPair nvpair2;
			if (desc != null)
			{
				NVPair nvpair = NVNavigator.FindNVPair(desc, AddressResolution.SECURITY);
				if (nvpair != null)
				{
					if ((nvpair2 = NVNavigator.FindNVPair(nvpair, AddressResolution.SSL_VERSION)) != null)
					{
						connectionOption.SSL_Version = nvpair2.Atom;
					}
					if ((nvpair2 = NVNavigator.FindNVPair(nvpair, AddressResolution.WALLETDIR)) != null)
					{
						connectionOption.SSL_WALLET_DIRECTORY = nvpair2.Atom;
					}
					if ((nvpair2 = NVNavigator.FindNVPair(nvpair, AddressResolution.SSLServerDN)) != null)
					{
						connectionOption.SSLServerDN = nvpair2.Atom;
					}
				}
				if ((nvpair2 = NVNavigator.FindNVPair(desc, AddressResolution.TRANSPORT_CONNECT_TO)) != null)
				{
					connectionOption.TransportConnectTO = int.Parse(nvpair2.Atom);
				}
				else
				{
					connectionOption.TransportConnectTO = SqlNetOraConfig.TCPCTimeOut;
				}
			}
			nvpair2 = (NVNavigator.FindNVPair(desc, AddressResolution.SBS) ?? NVNavigator.FindNVPair(desc, AddressResolution.SendBufSize));
			int num;
			if (nvpair2 != null && nvpair2.Atom != null && (num = int.Parse(nvpair2.Atom)) > 0)
			{
				connectionOption.SBS = num;
			}
			nvpair2 = (NVNavigator.FindNVPair(desc, AddressResolution.RBS) ?? NVNavigator.FindNVPair(desc, AddressResolution.RecvBufSize));
			if (nvpair2 != null && nvpair2.Atom != null && (num = int.Parse(nvpair2.Atom)) > 0)
			{
				connectionOption.RBS = num;
			}
			this.BuildCO_Addr(addr, ref connectionOption);
			nvpair2 = NVNavigator.FindNVPair(desc, AddressResolution.SDU);
			if (nvpair2 != null)
			{
				num = int.Parse(nvpair2.Atom);
				if (num > 0 && num >= ConnectionOption.NSPMNSDULN)
				{
					if (num > ConnectionOption.NSPMXSDULN)
					{
						num = ConnectionOption.NSPMXSDULN;
					}
					connectionOption.SessionDataUnitSize = num;
				}
			}
			nvpair2 = NVNavigator.FindNVPair(desc, AddressResolution.TDU);
			if (nvpair2 != null)
			{
				num = int.Parse(nvpair2.Atom);
				if (num > 0 && num >= ConnectionOption.NSPMNTDULN)
				{
					if (num > ConnectionOption.NSPMXTDULN)
					{
						num = ConnectionOption.NSPMXTDULN;
					}
					connectionOption.TransportDataUnitSize = num;
				}
			}
			NVPair nvpair3 = NVNavigator.FindNVPair(desc, AddressResolution.CONNECT_DATA);
			bool flag = !string.IsNullOrEmpty(this.m_instanceName);
			bool flag2 = false;
			bool flag3 = false;
			if (nvpair3 == null)
			{
				desc.AddListElement(NVFactory.CreateNVPair(AddressResolution.BEG_CONDATA + AddressResolution.END_ONE_BRACE));
				nvpair3 = NVNavigator.FindNVPair(desc, AddressResolution.CONNECT_DATA);
			}
			else
			{
				for (int i = 0; i < nvpair3.ListSize; i++)
				{
					NVPair listElement = nvpair3.GetListElement(i);
					if (string.Equals(listElement.Name, AddressResolution.CID, StringComparison.InvariantCultureIgnoreCase))
					{
						nvpair3.RemoveListElement(i);
					}
					if (string.Equals(listElement.Name, AddressResolution.SERVICE_NAME, StringComparison.InvariantCultureIgnoreCase))
					{
						flag2 = true;
						connectionOption.ServiceName = listElement.Atom;
					}
					if (string.Equals(listElement.Name, AddressResolution.INSTANCE_NAME, StringComparison.InvariantCultureIgnoreCase))
					{
						flag3 = true;
						connectionOption.InstanceName = listElement.Atom;
					}
					if (string.Equals(listElement.Name, AddressResolution.SERVER, StringComparison.InvariantCultureIgnoreCase))
					{
						connectionOption.Server = listElement.Atom;
					}
				}
				if (flag && flag2 && !flag3)
				{
					nvpair3.AddListElement(NVFactory.CreateNVPair(AddressResolution.BEG_INST + this.m_instanceName + AddressResolution.END_ONE_BRACE));
				}
			}
			if (nvpair3 == null)
			{
				throw new NetworkException(12550);
			}
			try
			{
				if (AddressResolution.nvp_CID != null)
				{
					nvpair3.AddListElement(AddressResolution.nvp_CID);
				}
			}
			catch (Exception)
			{
			}
			connectionOption.ConnectData = desc.ToString();
			connectionOption.Address = new StringBuilder().Append(AddressResolution.BEG_ADDR).Append(AddressResolution.DEFAULT_CONNECT_PROTOCOL).Append(AddressResolution.BEG_HOST).Append(connectionOption.Host).Append(AddressResolution.BEG_PORT).Append(connectionOption.Port).Append(AddressResolution.END_TWO_BRACES).ToString();
			return connectionOption;
		}

		// Token: 0x04000E78 RID: 3704
		private const int length_of_alias_prefix = 6;

		// Token: 0x04000E79 RID: 3705
		internal const int DEFAULT_DATABASE_PORT = 1521;

		// Token: 0x04000E7A RID: 3706
		private NVPair m_dlist;

		// Token: 0x04000E7B RID: 3707
		internal static char[] colonORslash = new char[]
		{
			'/',
			':'
		};

		// Token: 0x04000E7C RID: 3708
		internal static string POOLED_SERVER = "POOLED";

		// Token: 0x04000E7D RID: 3709
		internal static string SHARED_SERVER = "SHARED";

		// Token: 0x04000E7E RID: 3710
		internal static string DEDICATED_SERVER = "DEDICATED";

		// Token: 0x04000E7F RID: 3711
		internal static string DEFAULT_CONNECT_PROTOCOL = "TCP";

		// Token: 0x04000E80 RID: 3712
		internal static string DEFAULT_CONNECT_PROTOCOL_LOWERCASE = "tcp";

		// Token: 0x04000E81 RID: 3713
		internal static string SSL_CONNECT_PROTOCOL = "TCPS";

		// Token: 0x04000E82 RID: 3714
		internal static string SSL_CONNECT_PROTOCOL_LOWERCASE = "tcps";

		// Token: 0x04000E83 RID: 3715
		internal static string ANO_CONNECT_PROTOCOL = "ANO";

		// Token: 0x04000E84 RID: 3716
		internal static string ANO_CONNECT_PROTOCOL_LOWERCASE = "ano";

		// Token: 0x04000E85 RID: 3717
		internal static string BEGIN_DBL_SLASH = "//";

		// Token: 0x04000E86 RID: 3718
		internal static string OPENING_SQUARE_BRACE = "[";

		// Token: 0x04000E87 RID: 3719
		internal static string BEG_CID = "(CID=";

		// Token: 0x04000E88 RID: 3720
		internal static string BEG_USER = ")(USER=";

		// Token: 0x04000E89 RID: 3721
		internal static string BEG_ADDR = "(ADDRESS=(PROTOCOL=";

		// Token: 0x04000E8A RID: 3722
		internal static string BEG_HOST = ")(HOST=";

		// Token: 0x04000E8B RID: 3723
		internal static string BEG_PORT = ")(PORT=";

		// Token: 0x04000E8C RID: 3724
		internal static string BEG_DESC = "(DESCRIPTION=";

		// Token: 0x04000E8D RID: 3725
		internal static string BEG_SRVR = "(SERVER=";

		// Token: 0x04000E8E RID: 3726
		internal static string BEG_INST = "(INSTANCE_NAME=";

		// Token: 0x04000E8F RID: 3727
		internal static string BEG_PROGRAM = "(PROGRAM=";

		// Token: 0x04000E90 RID: 3728
		internal static string BEG_CONDATA = "(CONNECT_DATA=";

		// Token: 0x04000E91 RID: 3729
		internal static string BEG_CONDATA_SVC = "(CONNECT_DATA=(SERVICE_NAME=";

		// Token: 0x04000E92 RID: 3730
		internal static string END_THREE_BRACES = ")))";

		// Token: 0x04000E93 RID: 3731
		internal static string END_TWO_BRACES = "))";

		// Token: 0x04000E94 RID: 3732
		internal static string END_ONE_BRACE = ")";

		// Token: 0x04000E95 RID: 3733
		internal static string ALIAS = "alias=";

		// Token: 0x04000E96 RID: 3734
		internal static string CID = "CID";

		// Token: 0x04000E97 RID: 3735
		internal static string SID = "SID";

		// Token: 0x04000E98 RID: 3736
		internal static string INSTANCE_NAME = "INSTANCE_NAME";

		// Token: 0x04000E99 RID: 3737
		internal static string SERVICE_NAME = "SERVICE_NAME";

		// Token: 0x04000E9A RID: 3738
		internal static string SERVER = "SERVER";

		// Token: 0x04000E9B RID: 3739
		internal static string PROTOCOL = "PROTOCOL";

		// Token: 0x04000E9C RID: 3740
		internal static string ADDRESS = "ADDRESS";

		// Token: 0x04000E9D RID: 3741
		internal static string CONNECT_DATA = "CONNECT_DATA";

		// Token: 0x04000E9E RID: 3742
		internal static string ADDRESS_LIST = "ADDRESS_LIST";

		// Token: 0x04000E9F RID: 3743
		internal static string HOST = "HOST";

		// Token: 0x04000EA0 RID: 3744
		internal static string PORT = "PORT";

		// Token: 0x04000EA1 RID: 3745
		internal static string IP = "IP";

		// Token: 0x04000EA2 RID: 3746
		internal static string SDU = "SDU";

		// Token: 0x04000EA3 RID: 3747
		internal static string TDU = "TDU";

		// Token: 0x04000EA4 RID: 3748
		internal static string SBS = "SBS";

		// Token: 0x04000EA5 RID: 3749
		internal static string RBS = "RBS";

		// Token: 0x04000EA6 RID: 3750
		internal static string SendBufSize = "SEND_BUF_SIZE";

		// Token: 0x04000EA7 RID: 3751
		internal static string RecvBufSize = "RECV_BUF_SIZE";

		// Token: 0x04000EA8 RID: 3752
		internal static string DESCRIPTION = "DESCRIPTION";

		// Token: 0x04000EA9 RID: 3753
		internal static string SOURCE_ROUTE = "SOURCE_ROUTE";

		// Token: 0x04000EAA RID: 3754
		internal static string DESCRIPTION_LIST = "DESCRIPTION_LIST";

		// Token: 0x04000EAB RID: 3755
		internal static string RETRY_COUNT = "RETRY_COUNT";

		// Token: 0x04000EAC RID: 3756
		internal static string RETRY_DELAY = "RETRY_DELAY";

		// Token: 0x04000EAD RID: 3757
		internal static string TRANSPORT_CONNECT_TO = "TRANSPORT_CONNECT_TIMEOUT";

		// Token: 0x04000EAE RID: 3758
		internal static string SECURITY = "SECURITY";

		// Token: 0x04000EAF RID: 3759
		internal static string SSL_VERSION = "SSL_VERSION";

		// Token: 0x04000EB0 RID: 3760
		internal static string WALLETDIR = "MY_WALLET_DIRECTORY";

		// Token: 0x04000EB1 RID: 3761
		internal static string SSLServerDN = "SSL_SERVER_CERT_DN";

		// Token: 0x04000EB2 RID: 3762
		internal static string SID_ENV = "ORACLE_SID";

		// Token: 0x04000EB3 RID: 3763
		internal static string DEFAULT_ADDRESS = "(Description=(Address=(Protocol=tcp)(IP=loopback)(port=1521))(CONNECT_DATA=(SID=";

		// Token: 0x04000EB4 RID: 3764
		internal static string HOSTNAME;

		// Token: 0x04000EB5 RID: 3765
		internal static string USERNAME;

		// Token: 0x04000EB6 RID: 3766
		internal static string PROGRAMNAME;

		// Token: 0x04000EB7 RID: 3767
		internal static NVPair nvp_CID;

		// Token: 0x04000EB8 RID: 3768
		internal static string val_CID;

		// Token: 0x04000EB9 RID: 3769
		internal static string ful_CID;

		// Token: 0x04000EBA RID: 3770
		internal bool connection_revised;

		// Token: 0x04000EBB RID: 3771
		internal bool connection_redirected;

		// Token: 0x04000EBC RID: 3772
		private NVPair m_desc;

		// Token: 0x04000EBD RID: 3773
		private string m_tnsAddress;

		// Token: 0x04000EBE RID: 3774
		private string m_instanceName;

		// Token: 0x04000EBF RID: 3775
		private ConnectionOption m_ConnectionOption;

		// Token: 0x04000EC0 RID: 3776
		private bool tnsnames_ora_resolved;

		// Token: 0x04000EC1 RID: 3777
		private static char NLNVBEGDE = '(';

		// Token: 0x04000EC2 RID: 3778
		private static char NLNVENDDE = ')';

		// Token: 0x04000EC3 RID: 3779
		private static char NLNVASNOP = '=';

		// Token: 0x04000EC4 RID: 3780
		private static char NLNVPTHDE = '/';

		// Token: 0x04000EC5 RID: 3781
		private static char NLNVESCAP = '\\';

		// Token: 0x04000EC6 RID: 3782
		private static char NLNVQUOTE = '"';

		// Token: 0x04000EC7 RID: 3783
		private static char NLNVSQUOTE = '\'';

		// Token: 0x04000EC8 RID: 3784
		private static List<INamingAdapter> _NamingAdapters = new List<INamingAdapter>(10);

		// Token: 0x04000EC9 RID: 3785
		private static INamingAdapter _DataSourcesAdapter = null;

		// Token: 0x04000ECA RID: 3786
		private static char[] NCS = new char[]
		{
			'A',
			'B',
			'C',
			'D',
			'E',
			'F',
			'G',
			'H',
			'I',
			'J',
			'K',
			'L',
			'M',
			'N',
			'O',
			'P',
			'Q',
			'R',
			'S',
			'T',
			'U',
			'V',
			'W',
			'X',
			'Y',
			'Z',
			'a',
			'b',
			'c',
			'd',
			'e',
			'f',
			'g',
			'h',
			'i',
			'j',
			'k',
			'l',
			'm',
			'n',
			'o',
			'p',
			'q',
			'r',
			's',
			't',
			'u',
			'v',
			'w',
			'x',
			'y',
			'z',
			'0',
			'1',
			'2',
			'3',
			'4',
			'5',
			'6',
			'7',
			'8',
			'9',
			'0',
			'(',
			')',
			'<',
			'>',
			'/',
			'\\',
			',',
			'.',
			':',
			';',
			'\'',
			'"',
			'=',
			'-',
			'_',
			'$',
			'+',
			'*',
			'#',
			'&',
			'!',
			'%',
			'?',
			'@'
		};

		// Token: 0x04000ECB RID: 3787
		private static char[] reservedNCS = new char[]
		{
			AddressResolution.NLNVBEGDE,
			AddressResolution.NLNVENDDE,
			AddressResolution.NLNVASNOP
		};

		// Token: 0x04000ECC RID: 3788
		internal static byte[] ValidChars = new byte[256];
	}
}
