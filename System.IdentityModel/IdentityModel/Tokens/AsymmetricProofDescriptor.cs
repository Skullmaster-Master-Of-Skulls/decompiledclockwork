using System;
using System.IdentityModel.Protocols.WSTrust;
using System.Security.Cryptography;

namespace System.IdentityModel.Tokens
{
	// Token: 0x0200010B RID: 267
	public class AsymmetricProofDescriptor : ProofDescriptor
	{
		// Token: 0x0600075C RID: 1884 RVA: 0x0001F558 File Offset: 0x0001D758
		public AsymmetricProofDescriptor()
		{
		}

		// Token: 0x0600075D RID: 1885 RVA: 0x0001F560 File Offset: 0x0001D760
		public AsymmetricProofDescriptor(RSA rsaAlgorithm)
		{
			if (rsaAlgorithm == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("rsaAlgorithm");
			}
			this._keyIdentifier = new SecurityKeyIdentifier(new SecurityKeyIdentifierClause[]
			{
				new RsaKeyIdentifierClause(rsaAlgorithm)
			});
		}

		// Token: 0x0600075E RID: 1886 RVA: 0x0001F595 File Offset: 0x0001D795
		public AsymmetricProofDescriptor(SecurityKeyIdentifier keyIdentifier)
		{
			if (keyIdentifier == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("keyIdentifier");
			}
			this._keyIdentifier = keyIdentifier;
		}

		// Token: 0x0600075F RID: 1887 RVA: 0x0001F5B7 File Offset: 0x0001D7B7
		public override void ApplyTo(RequestSecurityTokenResponse response)
		{
			if (response == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("response");
			}
		}

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x06000760 RID: 1888 RVA: 0x0001F5CC File Offset: 0x0001D7CC
		public override SecurityKeyIdentifier KeyIdentifier
		{
			get
			{
				return this._keyIdentifier;
			}
		}

		// Token: 0x04000AA9 RID: 2729
		private SecurityKeyIdentifier _keyIdentifier;
	}
}
