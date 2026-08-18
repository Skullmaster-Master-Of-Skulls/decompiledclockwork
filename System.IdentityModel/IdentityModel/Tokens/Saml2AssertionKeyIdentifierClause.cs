using System;

namespace System.IdentityModel.Tokens
{
	// Token: 0x02000134 RID: 308
	public class Saml2AssertionKeyIdentifierClause : SecurityKeyIdentifierClause
	{
		// Token: 0x060008BA RID: 2234 RVA: 0x0002444A File Offset: 0x0002264A
		public Saml2AssertionKeyIdentifierClause(string id) : this(id, null, 0)
		{
		}

		// Token: 0x060008BB RID: 2235 RVA: 0x00024455 File Offset: 0x00022655
		public Saml2AssertionKeyIdentifierClause(string id, byte[] derivationNonce, int derivationLength) : base(null, derivationNonce, derivationLength)
		{
			if (string.IsNullOrEmpty(id))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("id");
			}
			base.Id = id;
		}

		// Token: 0x060008BC RID: 2236 RVA: 0x00024480 File Offset: 0x00022680
		public static bool Matches(string assertionId, SecurityKeyIdentifierClause keyIdentifierClause)
		{
			if (string.IsNullOrEmpty(assertionId))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("assertionId");
			}
			if (keyIdentifierClause == null)
			{
				return false;
			}
			Saml2AssertionKeyIdentifierClause saml2AssertionKeyIdentifierClause = keyIdentifierClause as Saml2AssertionKeyIdentifierClause;
			if (saml2AssertionKeyIdentifierClause != null && StringComparer.Ordinal.Equals(assertionId, saml2AssertionKeyIdentifierClause.Id))
			{
				return true;
			}
			SamlAssertionKeyIdentifierClause samlAssertionKeyIdentifierClause = keyIdentifierClause as SamlAssertionKeyIdentifierClause;
			return samlAssertionKeyIdentifierClause != null && StringComparer.Ordinal.Equals(assertionId, samlAssertionKeyIdentifierClause.AssertionId);
		}

		// Token: 0x060008BD RID: 2237 RVA: 0x000244E9 File Offset: 0x000226E9
		public override bool Matches(SecurityKeyIdentifierClause keyIdentifierClause)
		{
			return this == keyIdentifierClause || Saml2AssertionKeyIdentifierClause.Matches(base.Id, keyIdentifierClause);
		}

		// Token: 0x060008BE RID: 2238 RVA: 0x000244FD File Offset: 0x000226FD
		public override string ToString()
		{
			return "Saml2AssertionKeyIdentifierClause( Id = '" + base.Id + "' )";
		}
	}
}
