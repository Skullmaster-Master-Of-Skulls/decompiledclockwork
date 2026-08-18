using System;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;

namespace System.ServiceModel.Security.Tokens
{
	// Token: 0x020003A8 RID: 936
	internal class WrappedKeySecurityTokenParameters : SecurityTokenParameters
	{
		// Token: 0x06002319 RID: 8985 RVA: 0x00080428 File Offset: 0x0007E628
		protected WrappedKeySecurityTokenParameters(WrappedKeySecurityTokenParameters other) : base(other)
		{
		}

		// Token: 0x0600231A RID: 8986 RVA: 0x00080431 File Offset: 0x0007E631
		public WrappedKeySecurityTokenParameters()
		{
			base.InclusionMode = SecurityTokenInclusionMode.Once;
		}

		// Token: 0x170008B6 RID: 2230
		// (get) Token: 0x0600231B RID: 8987 RVA: 0x00080440 File Offset: 0x0007E640
		protected internal override bool HasAsymmetricKey
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170008B7 RID: 2231
		// (get) Token: 0x0600231C RID: 8988 RVA: 0x00080443 File Offset: 0x0007E643
		protected internal override bool SupportsClientAuthentication
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170008B8 RID: 2232
		// (get) Token: 0x0600231D RID: 8989 RVA: 0x00080446 File Offset: 0x0007E646
		protected internal override bool SupportsServerAuthentication
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170008B9 RID: 2233
		// (get) Token: 0x0600231E RID: 8990 RVA: 0x00080449 File Offset: 0x0007E649
		protected internal override bool SupportsClientWindowsIdentity
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600231F RID: 8991 RVA: 0x0008044C File Offset: 0x0007E64C
		protected override SecurityTokenParameters CloneCore()
		{
			return new WrappedKeySecurityTokenParameters(this);
		}

		// Token: 0x06002320 RID: 8992 RVA: 0x00080454 File Offset: 0x0007E654
		protected internal override SecurityKeyIdentifierClause CreateKeyIdentifierClause(SecurityToken token, SecurityTokenReferenceStyle referenceStyle)
		{
			return base.CreateKeyIdentifierClause<EncryptedKeyHashIdentifierClause, LocalIdKeyIdentifierClause>(token, referenceStyle);
		}

		// Token: 0x06002321 RID: 8993 RVA: 0x0008045E File Offset: 0x0007E65E
		protected internal override void InitializeSecurityTokenRequirement(SecurityTokenRequirement requirement)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotImplementedException());
		}
	}
}
