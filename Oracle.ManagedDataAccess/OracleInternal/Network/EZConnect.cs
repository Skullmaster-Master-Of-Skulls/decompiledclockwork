using System;
using System.Collections;
using System.Net;
using System.Text;
using OracleInternal.Common;

namespace OracleInternal.Network
{
	// Token: 0x02000161 RID: 353
	internal class EZConnect : INamingAdapter
	{
		// Token: 0x1700029A RID: 666
		// (get) Token: 0x06000DF9 RID: 3577 RVA: 0x00093AC8 File Offset: 0x00091CC8
		public string ID
		{
			get
			{
				return "EZConnect";
			}
		}

		// Token: 0x1700029B RID: 667
		// (get) Token: 0x06000DFA RID: 3578 RVA: 0x00093AD0 File Offset: 0x00091CD0
		public Hashtable Map
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06000DFC RID: 3580 RVA: 0x00093B04 File Offset: 0x00091D04
		public string Resolve(string TNSAlias, out ConnectionOption CO, string InstanceName = null)
		{
			if (TNSAlias == null)
			{
				CO = null;
				return null;
			}
			CO = this.ResolveSimple(TNSAlias, InstanceName);
			try
			{
				if (Dns.GetHostAddresses(CO.Host) == null)
				{
					CO = null;
				}
			}
			catch (Exception)
			{
				CO = null;
			}
			if (CO == null)
			{
				return null;
			}
			return CO.ConnectData;
		}

		// Token: 0x06000DFD RID: 3581 RVA: 0x00093B5C File Offset: 0x00091D5C
		private string GetValue(string TNSdesc, ref int POS)
		{
			POS++;
			int num = TNSdesc.IndexOfAny(EZConnect.colonORslash, POS);
			if (num == -1)
			{
				num = TNSdesc.Length;
			}
			string result = TNSdesc.Substring(POS, num - POS);
			POS = num;
			return result;
		}

		// Token: 0x06000DFE RID: 3582 RVA: 0x00093B9C File Offset: 0x00091D9C
		private ConnectionOption ResolveSimple(string TNSdesc, string InstanceName)
		{
			ConnectionOption connectionOption = new ConnectionOption();
			int num = 0;
			connectionOption.Protocol = AddressResolution.DEFAULT_CONNECT_PROTOCOL;
			if (TNSdesc.StartsWith(EZConnect.BEGIN_DBL_SLASH))
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
				int num2 = TNSdesc.IndexOfAny(EZConnect.colonORslash, num);
				if (num2 == -1)
				{
					num2 = TNSdesc.Length;
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
			if (InstanceName != null)
			{
				stringBuilder = stringBuilder.Append(AddressResolution.BEG_INST).Append(InstanceName).Append(AddressResolution.END_ONE_BRACE);
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

		// Token: 0x06000DFF RID: 3583 RVA: 0x00093E68 File Offset: 0x00092068
		public void Refresh()
		{
		}

		// Token: 0x04000F71 RID: 3953
		private const int DEFAULT_DATABASE_PORT = 1521;

		// Token: 0x04000F72 RID: 3954
		private static char[] colonORslash = new char[]
		{
			'/',
			':'
		};

		// Token: 0x04000F73 RID: 3955
		private static string BEGIN_DBL_SLASH = "//";
	}
}
