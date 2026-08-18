using System;
using System.Collections;

namespace Novell.Directory.Ldap.Utilclass
{
	// Token: 0x020000EC RID: 236
	public class BindProperties
	{
		// Token: 0x1700015E RID: 350
		// (get) Token: 0x060005C6 RID: 1478 RVA: 0x0001B664 File Offset: 0x0001A664
		public virtual int ProtocolVersion
		{
			get
			{
				return this.version;
			}
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x060005C7 RID: 1479 RVA: 0x0001B67C File Offset: 0x0001A67C
		public virtual string AuthenticationDN
		{
			get
			{
				return this.dn;
			}
		}

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x060005C8 RID: 1480 RVA: 0x0001B694 File Offset: 0x0001A694
		public virtual string AuthenticationMethod
		{
			get
			{
				return this.method;
			}
		}

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x060005C9 RID: 1481 RVA: 0x0001B6AC File Offset: 0x0001A6AC
		public virtual Hashtable SaslBindProperties
		{
			get
			{
				return this.bindProperties;
			}
		}

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x060005CA RID: 1482 RVA: 0x0001B6C4 File Offset: 0x0001A6C4
		public virtual object SaslCallbackHandler
		{
			get
			{
				return this.bindCallbackHandler;
			}
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x060005CB RID: 1483 RVA: 0x0001B6DC File Offset: 0x0001A6DC
		public virtual bool Anonymous
		{
			get
			{
				return this.anonymous;
			}
		}

		// Token: 0x060005CC RID: 1484 RVA: 0x0001B6F4 File Offset: 0x0001A6F4
		public BindProperties(int version, string dn, string method, bool anonymous, Hashtable bindProperties, object bindCallbackHandler)
		{
			this.version = version;
			this.dn = dn;
			this.method = method;
			this.anonymous = anonymous;
			this.bindProperties = bindProperties;
			this.bindCallbackHandler = bindCallbackHandler;
		}

		// Token: 0x04000430 RID: 1072
		private int version = 3;

		// Token: 0x04000431 RID: 1073
		private string dn = null;

		// Token: 0x04000432 RID: 1074
		private string method = null;

		// Token: 0x04000433 RID: 1075
		private bool anonymous;

		// Token: 0x04000434 RID: 1076
		private Hashtable bindProperties = null;

		// Token: 0x04000435 RID: 1077
		private object bindCallbackHandler = null;
	}
}
