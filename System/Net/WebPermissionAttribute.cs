using System;
using System.Security;
using System.Security.Permissions;

namespace System.Net
{
	// Token: 0x020004A8 RID: 1192
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[Serializable]
	public sealed class WebPermissionAttribute : CodeAccessSecurityAttribute
	{
		// Token: 0x06002481 RID: 9345 RVA: 0x0008F76A File Offset: 0x0008E76A
		public WebPermissionAttribute(SecurityAction action) : base(action)
		{
		}

		// Token: 0x17000792 RID: 1938
		// (get) Token: 0x06002482 RID: 9346 RVA: 0x0008F773 File Offset: 0x0008E773
		// (set) Token: 0x06002483 RID: 9347 RVA: 0x0008F780 File Offset: 0x0008E780
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

		// Token: 0x17000793 RID: 1939
		// (get) Token: 0x06002484 RID: 9348 RVA: 0x0008F7C5 File Offset: 0x0008E7C5
		// (set) Token: 0x06002485 RID: 9349 RVA: 0x0008F7D4 File Offset: 0x0008E7D4
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

		// Token: 0x17000794 RID: 1940
		// (get) Token: 0x06002486 RID: 9350 RVA: 0x0008F819 File Offset: 0x0008E819
		// (set) Token: 0x06002487 RID: 9351 RVA: 0x0008F858 File Offset: 0x0008E858
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

		// Token: 0x17000795 RID: 1941
		// (get) Token: 0x06002488 RID: 9352 RVA: 0x0008F8BC File Offset: 0x0008E8BC
		// (set) Token: 0x06002489 RID: 9353 RVA: 0x0008F8F8 File Offset: 0x0008E8F8
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

		// Token: 0x0600248A RID: 9354 RVA: 0x0008F95C File Offset: 0x0008E95C
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

		// Token: 0x040024C9 RID: 9417
		private object m_accept;

		// Token: 0x040024CA RID: 9418
		private object m_connect;
	}
}
