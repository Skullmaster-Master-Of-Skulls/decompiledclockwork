using System;

namespace System.ServiceModel.Security
{
	// Token: 0x02000297 RID: 663
	[__DynamicallyInvokable]
	public abstract class SecurityPolicyVersion
	{
		// Token: 0x06001422 RID: 5154 RVA: 0x0004C0E9 File Offset: 0x0004A2E9
		internal SecurityPolicyVersion(string ns, string prefix)
		{
			this.spNamespace = ns;
			this.prefix = prefix;
		}

		// Token: 0x1700046F RID: 1135
		// (get) Token: 0x06001423 RID: 5155 RVA: 0x0004C0FF File Offset: 0x0004A2FF
		[__DynamicallyInvokable]
		public string Namespace
		{
			[__DynamicallyInvokable]
			get
			{
				return this.spNamespace;
			}
		}

		// Token: 0x17000470 RID: 1136
		// (get) Token: 0x06001424 RID: 5156 RVA: 0x0004C107 File Offset: 0x0004A307
		[__DynamicallyInvokable]
		public string Prefix
		{
			[__DynamicallyInvokable]
			get
			{
				return this.prefix;
			}
		}

		// Token: 0x17000471 RID: 1137
		// (get) Token: 0x06001425 RID: 5157 RVA: 0x0004C10F File Offset: 0x0004A30F
		[__DynamicallyInvokable]
		public static SecurityPolicyVersion WSSecurityPolicy11
		{
			[__DynamicallyInvokable]
			get
			{
				return SecurityPolicyVersion.WSSecurityPolicyVersion11.Instance;
			}
		}

		// Token: 0x17000472 RID: 1138
		// (get) Token: 0x06001426 RID: 5158 RVA: 0x0004C116 File Offset: 0x0004A316
		public static SecurityPolicyVersion WSSecurityPolicy12
		{
			get
			{
				return SecurityPolicyVersion.WSSecurityPolicyVersion12.Instance;
			}
		}

		// Token: 0x04001A9E RID: 6814
		private readonly string spNamespace;

		// Token: 0x04001A9F RID: 6815
		private readonly string prefix;

		// Token: 0x02000B30 RID: 2864
		private class WSSecurityPolicyVersion11 : SecurityPolicyVersion
		{
			// Token: 0x0600702B RID: 28715 RVA: 0x001A0307 File Offset: 0x0019E507
			protected WSSecurityPolicyVersion11() : base("http://schemas.xmlsoap.org/ws/2005/07/securitypolicy", "sp")
			{
			}

			// Token: 0x17001A33 RID: 6707
			// (get) Token: 0x0600702C RID: 28716 RVA: 0x001A0319 File Offset: 0x0019E519
			public static SecurityPolicyVersion Instance
			{
				get
				{
					return SecurityPolicyVersion.WSSecurityPolicyVersion11.instance;
				}
			}

			// Token: 0x04003FFC RID: 16380
			private static readonly SecurityPolicyVersion.WSSecurityPolicyVersion11 instance = new SecurityPolicyVersion.WSSecurityPolicyVersion11();
		}

		// Token: 0x02000B31 RID: 2865
		private class WSSecurityPolicyVersion12 : SecurityPolicyVersion
		{
			// Token: 0x0600702E RID: 28718 RVA: 0x001A032C File Offset: 0x0019E52C
			protected WSSecurityPolicyVersion12() : base("http://docs.oasis-open.org/ws-sx/ws-securitypolicy/200702", "sp")
			{
			}

			// Token: 0x17001A34 RID: 6708
			// (get) Token: 0x0600702F RID: 28719 RVA: 0x001A033E File Offset: 0x0019E53E
			public static SecurityPolicyVersion Instance
			{
				get
				{
					return SecurityPolicyVersion.WSSecurityPolicyVersion12.instance;
				}
			}

			// Token: 0x04003FFD RID: 16381
			private static readonly SecurityPolicyVersion.WSSecurityPolicyVersion12 instance = new SecurityPolicyVersion.WSSecurityPolicyVersion12();
		}
	}
}
