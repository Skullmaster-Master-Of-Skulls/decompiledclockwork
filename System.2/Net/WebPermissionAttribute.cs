using System;
using System.Security;
using System.Security.Permissions;

namespace System.Net
{
	// Token: 0x02000185 RID: 389
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[Serializable]
	public sealed class WebPermissionAttribute : CodeAccessSecurityAttribute
	{
		// Token: 0x06000E7C RID: 3708 RVA: 0x0004B9BE File Offset: 0x00049BBE
		public WebPermissionAttribute(SecurityAction action) : base(action)
		{
		}

		// Token: 0x17000337 RID: 823
		// (get) Token: 0x06000E7D RID: 3709 RVA: 0x0004B9C7 File Offset: 0x00049BC7
		// (set) Token: 0x06000E7E RID: 3710 RVA: 0x0004B9D4 File Offset: 0x00049BD4
		public string Connect
		{
			get
			{
				return this.m_connect as string;
			}
			set
			{
				if (this.m_connect != null)
				{
					throw new ArgumentException(SR.GetString("net_perm_attrib_multi", new object[]
					{
						"Connect",
						value
					}), "value");
				}
				this.m_connect = value;
			}
		}

		// Token: 0x17000338 RID: 824
		// (get) Token: 0x06000E7F RID: 3711 RVA: 0x0004BA0C File Offset: 0x00049C0C
		// (set) Token: 0x06000E80 RID: 3712 RVA: 0x0004BA19 File Offset: 0x00049C19
		public string Accept
		{
			get
			{
				return this.m_accept as string;
			}
			set
			{
				if (this.m_accept != null)
				{
					throw new ArgumentException(SR.GetString("net_perm_attrib_multi", new object[]
					{
						"Accept",
						value
					}), "value");
				}
				this.m_accept = value;
			}
		}

		// Token: 0x17000339 RID: 825
		// (get) Token: 0x06000E81 RID: 3713 RVA: 0x0004BA51 File Offset: 0x00049C51
		// (set) Token: 0x06000E82 RID: 3714 RVA: 0x0004BA90 File Offset: 0x00049C90
		public string ConnectPattern
		{
			get
			{
				if (this.m_connect is DelayedRegex)
				{
					return this.m_connect.ToString();
				}
				if (!(this.m_connect is bool) || !(bool)this.m_connect)
				{
					return null;
				}
				return ".*";
			}
			set
			{
				if (this.m_connect != null)
				{
					throw new ArgumentException(SR.GetString("net_perm_attrib_multi", new object[]
					{
						"ConnectPatern",
						value
					}), "value");
				}
				if (value == ".*")
				{
					this.m_connect = true;
					return;
				}
				this.m_connect = new DelayedRegex(value);
			}
		}

		// Token: 0x1700033A RID: 826
		// (get) Token: 0x06000E83 RID: 3715 RVA: 0x0004BAF2 File Offset: 0x00049CF2
		// (set) Token: 0x06000E84 RID: 3716 RVA: 0x0004BB30 File Offset: 0x00049D30
		public string AcceptPattern
		{
			get
			{
				if (this.m_accept is DelayedRegex)
				{
					return this.m_accept.ToString();
				}
				if (!(this.m_accept is bool) || !(bool)this.m_accept)
				{
					return null;
				}
				return ".*";
			}
			set
			{
				if (this.m_accept != null)
				{
					throw new ArgumentException(SR.GetString("net_perm_attrib_multi", new object[]
					{
						"AcceptPattern",
						value
					}), "value");
				}
				if (value == ".*")
				{
					this.m_accept = true;
					return;
				}
				this.m_accept = new DelayedRegex(value);
			}
		}

		// Token: 0x06000E85 RID: 3717 RVA: 0x0004BB94 File Offset: 0x00049D94
		public override IPermission CreatePermission()
		{
			WebPermission webPermission;
			if (base.Unrestricted)
			{
				webPermission = new WebPermission(PermissionState.Unrestricted);
			}
			else
			{
				NetworkAccess networkAccess = (NetworkAccess)0;
				if (this.m_connect is bool)
				{
					if ((bool)this.m_connect)
					{
						networkAccess |= NetworkAccess.Connect;
					}
					this.m_connect = null;
				}
				if (this.m_accept is bool)
				{
					if ((bool)this.m_accept)
					{
						networkAccess |= NetworkAccess.Accept;
					}
					this.m_accept = null;
				}
				webPermission = new WebPermission(networkAccess);
				if (this.m_accept != null)
				{
					if (this.m_accept is DelayedRegex)
					{
						webPermission.AddAsPattern(NetworkAccess.Accept, (DelayedRegex)this.m_accept);
					}
					else
					{
						webPermission.AddPermission(NetworkAccess.Accept, (string)this.m_accept);
					}
				}
				if (this.m_connect != null)
				{
					if (this.m_connect is DelayedRegex)
					{
						webPermission.AddAsPattern(NetworkAccess.Connect, (DelayedRegex)this.m_connect);
					}
					else
					{
						webPermission.AddPermission(NetworkAccess.Connect, (string)this.m_connect);
					}
				}
			}
			return webPermission;
		}

		// Token: 0x04001279 RID: 4729
		private object m_accept;

		// Token: 0x0400127A RID: 4730
		private object m_connect;
	}
}
