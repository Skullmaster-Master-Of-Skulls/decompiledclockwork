using System;
using System.Globalization;
using System.Security;
using System.Security.Permissions;
using System.Threading;

namespace System.Net
{
	// Token: 0x02000440 RID: 1088
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[Serializable]
	public sealed class SocketPermissionAttribute : CodeAccessSecurityAttribute
	{
		// Token: 0x06002229 RID: 8745 RVA: 0x00086CD4 File Offset: 0x00085CD4
		public SocketPermissionAttribute(SecurityAction action) : base(action)
		{
		}

		// Token: 0x17000745 RID: 1861
		// (get) Token: 0x0600222A RID: 8746 RVA: 0x00086CDD File Offset: 0x00085CDD
		// (set) Token: 0x0600222B RID: 8747 RVA: 0x00086CE8 File Offset: 0x00085CE8
		public string Access
		{
			get
			{
				return this.m_access;
			}
			set
			{
				if (this.m_access != null)
				{
					throw new ArgumentException(SR.GetString("net_perm_attrib_multi", new object[]
					{
						"Access",
						value
					}), "value");
				}
				this.m_access = value;
			}
		}

		// Token: 0x17000746 RID: 1862
		// (get) Token: 0x0600222C RID: 8748 RVA: 0x00086D2D File Offset: 0x00085D2D
		// (set) Token: 0x0600222D RID: 8749 RVA: 0x00086D38 File Offset: 0x00085D38
		public string Host
		{
			get
			{
				return this.m_host;
			}
			set
			{
				if (this.m_host != null)
				{
					throw new ArgumentException(SR.GetString("net_perm_attrib_multi", new object[]
					{
						"Host",
						value
					}), "value");
				}
				this.m_host = value;
			}
		}

		// Token: 0x17000747 RID: 1863
		// (get) Token: 0x0600222E RID: 8750 RVA: 0x00086D7D File Offset: 0x00085D7D
		// (set) Token: 0x0600222F RID: 8751 RVA: 0x00086D88 File Offset: 0x00085D88
		public string Transport
		{
			get
			{
				return this.m_transport;
			}
			set
			{
				if (this.m_transport != null)
				{
					throw new ArgumentException(SR.GetString("net_perm_attrib_multi", new object[]
					{
						"Transport",
						value
					}), "value");
				}
				this.m_transport = value;
			}
		}

		// Token: 0x17000748 RID: 1864
		// (get) Token: 0x06002230 RID: 8752 RVA: 0x00086DCD File Offset: 0x00085DCD
		// (set) Token: 0x06002231 RID: 8753 RVA: 0x00086DD8 File Offset: 0x00085DD8
		public string Port
		{
			get
			{
				return this.m_port;
			}
			set
			{
				if (this.m_port != null)
				{
					throw new ArgumentException(SR.GetString("net_perm_attrib_multi", new object[]
					{
						"Port",
						value
					}), "value");
				}
				this.m_port = value;
			}
		}

		// Token: 0x06002232 RID: 8754 RVA: 0x00086E20 File Offset: 0x00085E20
		public override IPermission CreatePermission()
		{
			SocketPermission socketPermission;
			if (base.Unrestricted)
			{
				socketPermission = new SocketPermission(PermissionState.Unrestricted);
			}
			else
			{
				socketPermission = new SocketPermission(PermissionState.None);
				if (this.m_access == null)
				{
					throw new ArgumentException(SR.GetString("net_perm_attrib_count", new object[]
					{
						"Access"
					}));
				}
				if (this.m_host == null)
				{
					throw new ArgumentException(SR.GetString("net_perm_attrib_count", new object[]
					{
						"Host"
					}));
				}
				if (this.m_transport == null)
				{
					throw new ArgumentException(SR.GetString("net_perm_attrib_count", new object[]
					{
						"Transport"
					}));
				}
				if (this.m_port == null)
				{
					throw new ArgumentException(SR.GetString("net_perm_attrib_count", new object[]
					{
						"Port"
					}));
				}
				this.ParseAddPermissions(socketPermission);
			}
			return socketPermission;
		}

		// Token: 0x06002233 RID: 8755 RVA: 0x00086EF8 File Offset: 0x00085EF8
		private void ParseAddPermissions(SocketPermission perm)
		{
			NetworkAccess access;
			if (string.Compare(this.m_access, "Connect", StringComparison.OrdinalIgnoreCase) == 0)
			{
				access = NetworkAccess.Connect;
			}
			else
			{
				if (string.Compare(this.m_access, "Accept", StringComparison.OrdinalIgnoreCase) != 0)
				{
					throw new ArgumentException(SR.GetString("net_perm_invalid_val", new object[]
					{
						"Access",
						this.m_access
					}));
				}
				access = NetworkAccess.Accept;
			}
			TransportType transport;
			try
			{
				transport = (TransportType)Enum.Parse(typeof(TransportType), this.m_transport, true);
			}
			catch (Exception ex)
			{
				if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
				{
					throw;
				}
				throw new ArgumentException(SR.GetString("net_perm_invalid_val", new object[]
				{
					"Transport",
					this.m_transport
				}), ex);
			}
			catch
			{
				throw new ArgumentException(SR.GetString("net_perm_invalid_val", new object[]
				{
					"Transport",
					this.m_transport
				}), new Exception(SR.GetString("net_nonClsCompliantException")));
			}
			if (string.Compare(this.m_port, "All", StringComparison.OrdinalIgnoreCase) == 0)
			{
				this.m_port = "-1";
			}
			int num;
			try
			{
				num = int.Parse(this.m_port, NumberFormatInfo.InvariantInfo);
			}
			catch (Exception ex2)
			{
				if (ex2 is ThreadAbortException || ex2 is StackOverflowException || ex2 is OutOfMemoryException)
				{
					throw;
				}
				throw new ArgumentException(SR.GetString("net_perm_invalid_val", new object[]
				{
					"Port",
					this.m_port
				}), ex2);
			}
			catch
			{
				throw new ArgumentException(SR.GetString("net_perm_invalid_val", new object[]
				{
					"Port",
					this.m_port
				}), new Exception(SR.GetString("net_nonClsCompliantException")));
			}
			if (!ValidationHelper.ValidateTcpPort(num) && num != -1)
			{
				throw new ArgumentOutOfRangeException(SR.GetString("net_perm_invalid_val", new object[]
				{
					"Port",
					this.m_port
				}));
			}
			perm.AddPermission(access, transport, this.m_host, num);
		}

		// Token: 0x04002217 RID: 8727
		private const string strAccess = "Access";

		// Token: 0x04002218 RID: 8728
		private const string strConnect = "Connect";

		// Token: 0x04002219 RID: 8729
		private const string strAccept = "Accept";

		// Token: 0x0400221A RID: 8730
		private const string strHost = "Host";

		// Token: 0x0400221B RID: 8731
		private const string strTransport = "Transport";

		// Token: 0x0400221C RID: 8732
		private const string strPort = "Port";

		// Token: 0x0400221D RID: 8733
		private string m_access;

		// Token: 0x0400221E RID: 8734
		private string m_host;

		// Token: 0x0400221F RID: 8735
		private string m_port;

		// Token: 0x04002220 RID: 8736
		private string m_transport;
	}
}
