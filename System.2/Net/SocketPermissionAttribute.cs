using System;
using System.Globalization;
using System.Security;
using System.Security.Permissions;
using System.Threading;

namespace System.Net
{
	// Token: 0x02000162 RID: 354
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[Serializable]
	public sealed class SocketPermissionAttribute : CodeAccessSecurityAttribute
	{
		// Token: 0x06000CD1 RID: 3281 RVA: 0x00044281 File Offset: 0x00042481
		public SocketPermissionAttribute(SecurityAction action) : base(action)
		{
		}

		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x06000CD2 RID: 3282 RVA: 0x0004428A File Offset: 0x0004248A
		// (set) Token: 0x06000CD3 RID: 3283 RVA: 0x00044292 File Offset: 0x00042492
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

		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x06000CD4 RID: 3284 RVA: 0x000442CA File Offset: 0x000424CA
		// (set) Token: 0x06000CD5 RID: 3285 RVA: 0x000442D2 File Offset: 0x000424D2
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

		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x06000CD6 RID: 3286 RVA: 0x0004430A File Offset: 0x0004250A
		// (set) Token: 0x06000CD7 RID: 3287 RVA: 0x00044312 File Offset: 0x00042512
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

		// Token: 0x170002FA RID: 762
		// (get) Token: 0x06000CD8 RID: 3288 RVA: 0x0004434A File Offset: 0x0004254A
		// (set) Token: 0x06000CD9 RID: 3289 RVA: 0x00044352 File Offset: 0x00042552
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

		// Token: 0x06000CDA RID: 3290 RVA: 0x0004438C File Offset: 0x0004258C
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

		// Token: 0x06000CDB RID: 3291 RVA: 0x00044458 File Offset: 0x00042658
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
			if (!ValidationHelper.ValidateTcpPort(num) && num != -1)
			{
				throw new ArgumentOutOfRangeException("port", num, SR.GetString("net_perm_invalid_val", new object[]
				{
					"Port",
					this.m_port
				}));
			}
			perm.AddPermission(access, transport, this.m_host, num);
		}

		// Token: 0x040011C4 RID: 4548
		private string m_access;

		// Token: 0x040011C5 RID: 4549
		private string m_host;

		// Token: 0x040011C6 RID: 4550
		private string m_port;

		// Token: 0x040011C7 RID: 4551
		private string m_transport;

		// Token: 0x040011C8 RID: 4552
		private const string strAccess = "Access";

		// Token: 0x040011C9 RID: 4553
		private const string strConnect = "Connect";

		// Token: 0x040011CA RID: 4554
		private const string strAccept = "Accept";

		// Token: 0x040011CB RID: 4555
		private const string strHost = "Host";

		// Token: 0x040011CC RID: 4556
		private const string strTransport = "Transport";

		// Token: 0x040011CD RID: 4557
		private const string strPort = "Port";
	}
}
