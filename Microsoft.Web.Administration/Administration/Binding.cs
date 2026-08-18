using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net;
using Microsoft.Web.Management.Utility;

namespace Microsoft.Web.Administration
{
	// Token: 0x02000013 RID: 19
	[DebuggerDisplay("Protocol = {Protocol}, BindingInformation = {BindingInformation}")]
	public class Binding : ConfigurationElement
	{
		// Token: 0x060000F1 RID: 241 RVA: 0x000045D1 File Offset: 0x000035D1
		internal Binding(ServerManager serverManager)
		{
			this._serverManager = serverManager;
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060000F2 RID: 242 RVA: 0x000045E0 File Offset: 0x000035E0
		// (set) Token: 0x060000F3 RID: 243 RVA: 0x000045F2 File Offset: 0x000035F2
		public string BindingInformation
		{
			get
			{
				return (string)base["bindingInformation"];
			}
			set
			{
				this.SetBindingProperty("bindingInformation", value);
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060000F4 RID: 244 RVA: 0x00004600 File Offset: 0x00003600
		// (set) Token: 0x060000F5 RID: 245 RVA: 0x00004654 File Offset: 0x00003654
		public byte[] CertificateHash
		{
			get
			{
				if (this._certificateHash == null && this.IsHttps)
				{
					IPEndPoint endPoint = this.EndPoint;
					if (endPoint != null)
					{
						string text = (string)base.GetAttributeValue("certificateHash");
						if (!string.IsNullOrEmpty(text))
						{
							this._certificateHash = HttpApiWrapper.ConvertCertificateHexStringToBytes(text);
						}
					}
				}
				return this._certificateHash;
			}
			set
			{
				if (this.CertificateHash != null)
				{
					this._certificateHash = value;
					IPEndPoint endPoint = this.EndPoint;
					if (string.IsNullOrEmpty(this.Protocol) || endPoint == null)
					{
						return;
					}
					if (this.IsHttps)
					{
						this._serverManager.BindingManager.AddModifyBindingTransaction(this, this.Protocol, endPoint, this.Protocol, endPoint, this._certificateHash, this.CertificateStoreName);
						return;
					}
				}
				else
				{
					this._certificateHash = value;
				}
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060000F6 RID: 246 RVA: 0x000046C4 File Offset: 0x000036C4
		// (set) Token: 0x060000F7 RID: 247 RVA: 0x00004708 File Offset: 0x00003708
		public string CertificateStoreName
		{
			get
			{
				if (this._certificateStoreName == null && this.IsHttps)
				{
					IPEndPoint endPoint = this.EndPoint;
					if (endPoint != null)
					{
						this._certificateStoreName = (string)base.GetAttributeValue("certificateStoreName");
					}
				}
				return this._certificateStoreName;
			}
			set
			{
				if (!string.IsNullOrEmpty(this.CertificateStoreName))
				{
					this._certificateStoreName = value;
					IPEndPoint endPoint = this.EndPoint;
					if (string.IsNullOrEmpty(this.Protocol) || endPoint == null)
					{
						return;
					}
					if (this.IsHttps)
					{
						this._serverManager.BindingManager.AddModifyBindingTransaction(this, this.Protocol, endPoint, this.Protocol, endPoint, this.CertificateHash, this._certificateStoreName);
						return;
					}
				}
				else
				{
					this._certificateStoreName = value;
				}
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060000F8 RID: 248 RVA: 0x0000477C File Offset: 0x0000377C
		public IPEndPoint EndPoint
		{
			get
			{
				this.LoadBindingInfo();
				return this._endPoint;
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060000F9 RID: 249 RVA: 0x0000478A File Offset: 0x0000378A
		public string Host
		{
			get
			{
				this.LoadBindingInfo();
				if (this._host == null)
				{
					return string.Empty;
				}
				return this._host;
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060000FA RID: 250 RVA: 0x000047A6 File Offset: 0x000037A6
		private bool IsHttp
		{
			get
			{
				return string.Equals(this.Protocol, Uri.UriSchemeHttp, StringComparison.OrdinalIgnoreCase);
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060000FB RID: 251 RVA: 0x000047B9 File Offset: 0x000037B9
		private bool IsHttps
		{
			get
			{
				return string.Equals(this.Protocol, Uri.UriSchemeHttps, StringComparison.OrdinalIgnoreCase);
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060000FC RID: 252 RVA: 0x000047CC File Offset: 0x000037CC
		public bool IsIPPortHostBinding
		{
			get
			{
				this.LoadBindingInfo();
				return this._isIPPortHostBinding;
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060000FD RID: 253 RVA: 0x000047DC File Offset: 0x000037DC
		// (set) Token: 0x060000FE RID: 254 RVA: 0x00004820 File Offset: 0x00003820
		public bool UseDsMapper
		{
			get
			{
				if (!this._useDsMapperSet && this.IsHttps)
				{
					IPEndPoint endPoint = this.EndPoint;
					if (endPoint != null)
					{
						this._useDsMapper = (bool)base.GetAttributeValue("isDsMapperEnabled");
					}
				}
				return this._useDsMapper;
			}
			set
			{
				this._useDsMapperSet = true;
				this._useDsMapper = value;
				if (string.IsNullOrEmpty(this.Protocol) || this.EndPoint == null)
				{
					return;
				}
				if (this.IsHttps)
				{
					this._serverManager.BindingManager.AddModifyDSMapperPropertyTransaction(this, this._useDsMapper);
				}
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060000FF RID: 255 RVA: 0x00004870 File Offset: 0x00003870
		internal bool UseDsMapperInternal
		{
			get
			{
				return this._useDsMapper;
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000100 RID: 256 RVA: 0x00004878 File Offset: 0x00003878
		internal bool UseDsMapperInternalSet
		{
			get
			{
				return this._useDsMapperSet;
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000101 RID: 257 RVA: 0x00004880 File Offset: 0x00003880
		// (set) Token: 0x06000102 RID: 258 RVA: 0x00004892 File Offset: 0x00003892
		public string Protocol
		{
			get
			{
				return (string)base["protocol"];
			}
			set
			{
				this.SetBindingProperty("protocol", value);
			}
		}

		// Token: 0x06000103 RID: 259 RVA: 0x000048A0 File Offset: 0x000038A0
		private void LoadBindingInfo()
		{
			this._isIPPortHostBinding = false;
			this._host = string.Empty;
			this._endPoint = null;
			if (string.IsNullOrEmpty(this.BindingInformation))
			{
				return;
			}
			if (this.IsHttp || this.IsHttps)
			{
				string empty = string.Empty;
				IPEndPoint ipendPoint = BindingUtility.EndPointFromBindingInformation(this.BindingInformation, out empty);
				if (ipendPoint != null)
				{
					this._host = empty;
					this._endPoint = ipendPoint;
					this._isIPPortHostBinding = true;
				}
			}
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00004910 File Offset: 0x00003910
		private void SetBindingProperty(string attributeName, string value)
		{
			IPEndPoint endPoint = this.EndPoint;
			string protocol = this.Protocol;
			byte[] certificateHash = this.CertificateHash;
			string certificateStoreName = this.CertificateStoreName;
			base[attributeName] = value;
			this.LoadBindingInfo();
			if (string.IsNullOrEmpty(protocol) || endPoint == null)
			{
				return;
			}
			if (string.Equals(protocol, Uri.UriSchemeHttps, StringComparison.OrdinalIgnoreCase) || string.Equals(this.Protocol, Uri.UriSchemeHttps, StringComparison.OrdinalIgnoreCase))
			{
				this._serverManager.BindingManager.AddModifyBindingTransaction(this, protocol, endPoint, this.Protocol, this.EndPoint, certificateHash, certificateStoreName);
			}
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00004998 File Offset: 0x00003998
		public override string ToString()
		{
			if (!this._isIPPortHostBinding)
			{
				return "[" + this.Protocol + "] " + this.BindingInformation;
			}
			string text;
			if (object.Equals(this._endPoint.Address, IPAddress.Any))
			{
				text = "*";
			}
			else
			{
				text = this._endPoint.Address.ToString();
			}
			if (this._host == "*")
			{
				this._host = string.Empty;
			}
			return string.Format(CultureInfo.InvariantCulture.NumberFormat, "{0}:{1}:{2}", new object[]
			{
				text,
				this._endPoint.Port,
				this._host
			});
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00004A54 File Offset: 0x00003A54
		internal void RemoveSslCertificate()
		{
			try
			{
				base.ExecuteMethod("RemoveSslCertificate");
			}
			catch (FileNotFoundException)
			{
			}
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00004A84 File Offset: 0x00003A84
		internal void SetDsMapper(bool useDsMapper)
		{
			if (useDsMapper)
			{
				base.ExecuteMethod("EnableDsMapper");
				return;
			}
			base.ExecuteMethod("DisableDsMapper");
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00004AA0 File Offset: 0x00003AA0
		internal void AddSslCertificate(byte[] certificateHash, string certificateStoreName)
		{
			if (string.IsNullOrEmpty(certificateStoreName))
			{
				certificateStoreName = "MY";
			}
			string value = HttpApiWrapper.ConvertBytesToCertificateHexString(certificateHash);
			ConfigurationMethodInstance configurationMethodInstance = base.Methods["AddSslCertificate"].CreateInstance();
			configurationMethodInstance.Input.SetAttributeValue("certificateHash", value);
			configurationMethodInstance.Input.SetAttributeValue("certificateStoreName", certificateStoreName);
			configurationMethodInstance.Execute();
		}

		// Token: 0x0400002F RID: 47
		private ServerManager _serverManager;

		// Token: 0x04000030 RID: 48
		private IPEndPoint _endPoint;

		// Token: 0x04000031 RID: 49
		private string _host;

		// Token: 0x04000032 RID: 50
		private bool _isIPPortHostBinding;

		// Token: 0x04000033 RID: 51
		private byte[] _certificateHash;

		// Token: 0x04000034 RID: 52
		private string _certificateStoreName;

		// Token: 0x04000035 RID: 53
		private bool _useDsMapper;

		// Token: 0x04000036 RID: 54
		private bool _useDsMapperSet;
	}
}
