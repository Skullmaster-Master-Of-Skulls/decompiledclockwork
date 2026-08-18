using System;

namespace System.IdentityModel.Tokens
{
	// Token: 0x0200018B RID: 395
	internal class WrappedSaml2AssertionKeyIdentifierClause : SamlAssertionKeyIdentifierClause
	{
		// Token: 0x06000CEF RID: 3311 RVA: 0x0003C060 File Offset: 0x0003A260
		public WrappedSaml2AssertionKeyIdentifierClause(Saml2AssertionKeyIdentifierClause clause) : base(clause.Id)
		{
			this.clause = clause;
		}

		// Token: 0x17000339 RID: 825
		// (get) Token: 0x06000CF0 RID: 3312 RVA: 0x0003C075 File Offset: 0x0003A275
		public override bool CanCreateKey
		{
			get
			{
				return this.clause.CanCreateKey;
			}
		}

		// Token: 0x1700033A RID: 826
		// (get) Token: 0x06000CF1 RID: 3313 RVA: 0x0003C082 File Offset: 0x0003A282
		public Saml2AssertionKeyIdentifierClause WrappedClause
		{
			get
			{
				return this.clause;
			}
		}

		// Token: 0x06000CF2 RID: 3314 RVA: 0x0003C08A File Offset: 0x0003A28A
		public override SecurityKey CreateKey()
		{
			return this.clause.CreateKey();
		}

		// Token: 0x06000CF3 RID: 3315 RVA: 0x0003C097 File Offset: 0x0003A297
		public override bool Matches(SecurityKeyIdentifierClause keyIdentifierClause)
		{
			return this.clause.Matches(keyIdentifierClause);
		}

		// Token: 0x04000CA3 RID: 3235
		private Saml2AssertionKeyIdentifierClause clause;
	}
}
