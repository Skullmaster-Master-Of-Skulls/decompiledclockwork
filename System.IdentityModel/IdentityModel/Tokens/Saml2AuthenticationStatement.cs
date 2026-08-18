using System;

namespace System.IdentityModel.Tokens
{
	// Token: 0x02000139 RID: 313
	public class Saml2AuthenticationStatement : Saml2Statement
	{
		// Token: 0x060008DD RID: 2269 RVA: 0x000249EE File Offset: 0x00022BEE
		public Saml2AuthenticationStatement(Saml2AuthenticationContext authenticationContext) : this(authenticationContext, DateTime.UtcNow)
		{
		}

		// Token: 0x060008DE RID: 2270 RVA: 0x000249FC File Offset: 0x00022BFC
		public Saml2AuthenticationStatement(Saml2AuthenticationContext authenticationContext, DateTime authenticationInstant)
		{
			if (authenticationContext == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("authenticationContext");
			}
			this.authnContext = authenticationContext;
			this.authnInstant = DateTimeUtil.ToUniversalTime(authenticationInstant);
		}

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x060008DF RID: 2271 RVA: 0x00024A2A File Offset: 0x00022C2A
		// (set) Token: 0x060008E0 RID: 2272 RVA: 0x00024A32 File Offset: 0x00022C32
		public Saml2AuthenticationContext AuthenticationContext
		{
			get
			{
				return this.authnContext;
			}
			set
			{
				if (value == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("value");
				}
				this.authnContext = value;
			}
		}

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x060008E1 RID: 2273 RVA: 0x00024A4E File Offset: 0x00022C4E
		// (set) Token: 0x060008E2 RID: 2274 RVA: 0x00024A56 File Offset: 0x00022C56
		public DateTime AuthenticationInstant
		{
			get
			{
				return this.authnInstant;
			}
			set
			{
				this.authnInstant = DateTimeUtil.ToUniversalTime(value);
			}
		}

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x060008E3 RID: 2275 RVA: 0x00024A64 File Offset: 0x00022C64
		// (set) Token: 0x060008E4 RID: 2276 RVA: 0x00024A6C File Offset: 0x00022C6C
		public string SessionIndex
		{
			get
			{
				return this.sessionIndex;
			}
			set
			{
				this.sessionIndex = XmlUtil.NormalizeEmptyString(value);
			}
		}

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x060008E5 RID: 2277 RVA: 0x00024A7A File Offset: 0x00022C7A
		// (set) Token: 0x060008E6 RID: 2278 RVA: 0x00024A82 File Offset: 0x00022C82
		public DateTime? SessionNotOnOrAfter
		{
			get
			{
				return this.sessionNotOnOrAfter;
			}
			set
			{
				this.sessionNotOnOrAfter = DateTimeUtil.ToUniversalTime(value);
			}
		}

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x060008E7 RID: 2279 RVA: 0x00024A90 File Offset: 0x00022C90
		// (set) Token: 0x060008E8 RID: 2280 RVA: 0x00024A98 File Offset: 0x00022C98
		public Saml2SubjectLocality SubjectLocality
		{
			get
			{
				return this.subjectLocality;
			}
			set
			{
				this.subjectLocality = value;
			}
		}

		// Token: 0x04000B41 RID: 2881
		private Saml2AuthenticationContext authnContext;

		// Token: 0x04000B42 RID: 2882
		private DateTime authnInstant;

		// Token: 0x04000B43 RID: 2883
		private string sessionIndex;

		// Token: 0x04000B44 RID: 2884
		private DateTime? sessionNotOnOrAfter;

		// Token: 0x04000B45 RID: 2885
		private Saml2SubjectLocality subjectLocality;
	}
}
