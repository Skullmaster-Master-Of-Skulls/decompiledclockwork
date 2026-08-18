using System;
using System.Collections.ObjectModel;

namespace System.IdentityModel.Tokens
{
	// Token: 0x0200013B RID: 315
	public class Saml2Conditions
	{
		// Token: 0x17000223 RID: 547
		// (get) Token: 0x060008F4 RID: 2292 RVA: 0x00024C51 File Offset: 0x00022E51
		public Collection<Saml2AudienceRestriction> AudienceRestrictions
		{
			get
			{
				return this.audienceRestrictions;
			}
		}

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x060008F5 RID: 2293 RVA: 0x00024C59 File Offset: 0x00022E59
		// (set) Token: 0x060008F6 RID: 2294 RVA: 0x00024C64 File Offset: 0x00022E64
		public DateTime? NotBefore
		{
			get
			{
				return this.notBefore;
			}
			set
			{
				value = DateTimeUtil.ToUniversalTime(value);
				if (value != null && this.notOnOrAfter != null && value.Value >= this.notOnOrAfter.Value)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("value", SR.GetString("ID4116"));
				}
				this.notBefore = value;
			}
		}

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x060008F7 RID: 2295 RVA: 0x00024CC9 File Offset: 0x00022EC9
		// (set) Token: 0x060008F8 RID: 2296 RVA: 0x00024CD4 File Offset: 0x00022ED4
		public DateTime? NotOnOrAfter
		{
			get
			{
				return this.notOnOrAfter;
			}
			set
			{
				value = DateTimeUtil.ToUniversalTime(value);
				if (value != null && this.notBefore != null && value.Value <= this.notBefore.Value)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("value", SR.GetString("ID4116"));
				}
				this.notOnOrAfter = value;
			}
		}

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x060008F9 RID: 2297 RVA: 0x00024D39 File Offset: 0x00022F39
		// (set) Token: 0x060008FA RID: 2298 RVA: 0x00024D41 File Offset: 0x00022F41
		public bool OneTimeUse
		{
			get
			{
				return this.oneTimeUse;
			}
			set
			{
				this.oneTimeUse = value;
			}
		}

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x060008FB RID: 2299 RVA: 0x00024D4A File Offset: 0x00022F4A
		// (set) Token: 0x060008FC RID: 2300 RVA: 0x00024D52 File Offset: 0x00022F52
		public Saml2ProxyRestriction ProxyRestriction
		{
			get
			{
				return this.proxyRestriction;
			}
			set
			{
				this.proxyRestriction = value;
			}
		}

		// Token: 0x04000B4B RID: 2891
		private Collection<Saml2AudienceRestriction> audienceRestrictions = new Collection<Saml2AudienceRestriction>();

		// Token: 0x04000B4C RID: 2892
		private DateTime? notBefore;

		// Token: 0x04000B4D RID: 2893
		private DateTime? notOnOrAfter;

		// Token: 0x04000B4E RID: 2894
		private bool oneTimeUse;

		// Token: 0x04000B4F RID: 2895
		private Saml2ProxyRestriction proxyRestriction;
	}
}
