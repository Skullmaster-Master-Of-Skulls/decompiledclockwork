using System;
using System.IdentityModel;
using System.IdentityModel.Tokens;
using System.Runtime.CompilerServices;

namespace System.ServiceModel.Security
{
	// Token: 0x02000010 RID: 16
	[TypeForwardedFrom("System.ServiceModel, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	internal class SamlAssertionDirectKeyIdentifierClause : SecurityKeyIdentifierClause
	{
		// Token: 0x06000073 RID: 115 RVA: 0x00002F3C File Offset: 0x0000113C
		public SamlAssertionDirectKeyIdentifierClause(string samlUri, byte[] derivationNonce, int derivationLength) : base(null, derivationNonce, derivationLength)
		{
			if (string.IsNullOrEmpty(samlUri))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException("SamlUriCannotBeNullOrEmpty"));
			}
			this.samlUri = samlUri;
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000074 RID: 116 RVA: 0x00002F6B File Offset: 0x0000116B
		public string SamlUri
		{
			get
			{
				return this.samlUri;
			}
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00002F74 File Offset: 0x00001174
		public override bool Matches(SecurityKeyIdentifierClause keyIdentifierClause)
		{
			SamlAssertionDirectKeyIdentifierClause samlAssertionDirectKeyIdentifierClause = keyIdentifierClause as SamlAssertionDirectKeyIdentifierClause;
			return this == samlAssertionDirectKeyIdentifierClause || (samlAssertionDirectKeyIdentifierClause != null && samlAssertionDirectKeyIdentifierClause.SamlUri == this.SamlUri);
		}

		// Token: 0x04000074 RID: 116
		private string samlUri;
	}
}
