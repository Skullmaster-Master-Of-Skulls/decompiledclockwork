using System;

namespace System.IdentityModel.Tokens
{
	// Token: 0x02000141 RID: 321
	public class Saml2SecurityKeyIdentifierClause : SecurityKeyIdentifierClause
	{
		// Token: 0x0600091D RID: 2333 RVA: 0x000250F9 File Offset: 0x000232F9
		public Saml2SecurityKeyIdentifierClause(Saml2Assertion assertion) : base(typeof(Saml2SecurityKeyIdentifierClause).ToString())
		{
			this.assertion = assertion;
		}

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x0600091E RID: 2334 RVA: 0x00025117 File Offset: 0x00023317
		public Saml2Assertion Assertion
		{
			get
			{
				return this.assertion;
			}
		}

		// Token: 0x04000B5F RID: 2911
		private Saml2Assertion assertion;
	}
}
