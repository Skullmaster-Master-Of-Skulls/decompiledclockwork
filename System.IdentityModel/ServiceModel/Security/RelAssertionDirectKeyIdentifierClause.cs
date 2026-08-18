using System;
using System.IdentityModel;
using System.IdentityModel.Tokens;
using System.Runtime.CompilerServices;

namespace System.ServiceModel.Security
{
	// Token: 0x0200000F RID: 15
	[TypeForwardedFrom("System.ServiceModel, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	internal class RelAssertionDirectKeyIdentifierClause : SecurityKeyIdentifierClause
	{
		// Token: 0x06000070 RID: 112 RVA: 0x00002ED3 File Offset: 0x000010D3
		public RelAssertionDirectKeyIdentifierClause(string assertionId, byte[] derivationNonce, int derivationLength) : base(null, derivationNonce, derivationLength)
		{
			if (string.IsNullOrEmpty(assertionId))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException("AssertionIdCannotBeNullOrEmpty"));
			}
			this.assertionId = assertionId;
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000071 RID: 113 RVA: 0x00002F02 File Offset: 0x00001102
		public string AssertionId
		{
			get
			{
				return this.assertionId;
			}
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00002F0C File Offset: 0x0000110C
		public override bool Matches(SecurityKeyIdentifierClause keyIdentifierClause)
		{
			RelAssertionDirectKeyIdentifierClause relAssertionDirectKeyIdentifierClause = keyIdentifierClause as RelAssertionDirectKeyIdentifierClause;
			return this == relAssertionDirectKeyIdentifierClause || (relAssertionDirectKeyIdentifierClause != null && relAssertionDirectKeyIdentifierClause.AssertionId == this.AssertionId);
		}

		// Token: 0x04000073 RID: 115
		private string assertionId;
	}
}
