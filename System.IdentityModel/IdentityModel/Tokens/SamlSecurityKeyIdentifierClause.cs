using System;

namespace System.IdentityModel.Tokens
{
	// Token: 0x0200015D RID: 349
	public class SamlSecurityKeyIdentifierClause : SecurityKeyIdentifierClause
	{
		// Token: 0x06000AA1 RID: 2721 RVA: 0x0003048A File Offset: 0x0002E68A
		public SamlSecurityKeyIdentifierClause(SamlAssertion assertion) : base(typeof(SamlSecurityKeyIdentifierClause).ToString())
		{
			this.assertion = assertion;
		}

		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x06000AA2 RID: 2722 RVA: 0x000304A8 File Offset: 0x0002E6A8
		public SamlAssertion Assertion
		{
			get
			{
				return this.assertion;
			}
		}

		// Token: 0x04000BD0 RID: 3024
		private SamlAssertion assertion;
	}
}
