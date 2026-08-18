using System;
using System.Collections.ObjectModel;
using System.IdentityModel.Tokens;

namespace System.ServiceModel.Security.Tokens
{
	// Token: 0x02000384 RID: 900
	internal sealed class DerivedKeySecurityTokenStub : SecurityToken
	{
		// Token: 0x0600214E RID: 8526 RVA: 0x0007B794 File Offset: 0x00079994
		public DerivedKeySecurityTokenStub(int generation, int offset, int length, string label, byte[] nonce, SecurityKeyIdentifierClause tokenToDeriveIdentifier, string derivationAlgorithm, string id)
		{
			this.id = id;
			this.generation = generation;
			this.offset = offset;
			this.length = length;
			this.label = label;
			this.nonce = nonce;
			this.tokenToDeriveIdentifier = tokenToDeriveIdentifier;
			this.derivationAlgorithm = derivationAlgorithm;
		}

		// Token: 0x170007FB RID: 2043
		// (get) Token: 0x0600214F RID: 8527 RVA: 0x0007B7E4 File Offset: 0x000799E4
		public override string Id
		{
			get
			{
				return this.id;
			}
		}

		// Token: 0x170007FC RID: 2044
		// (get) Token: 0x06002150 RID: 8528 RVA: 0x0007B7EC File Offset: 0x000799EC
		public override DateTime ValidFrom
		{
			get
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotImplementedException());
			}
		}

		// Token: 0x170007FD RID: 2045
		// (get) Token: 0x06002151 RID: 8529 RVA: 0x0007B7FD File Offset: 0x000799FD
		public override DateTime ValidTo
		{
			get
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotImplementedException());
			}
		}

		// Token: 0x170007FE RID: 2046
		// (get) Token: 0x06002152 RID: 8530 RVA: 0x0007B80E File Offset: 0x00079A0E
		public override ReadOnlyCollection<SecurityKey> SecurityKeys
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170007FF RID: 2047
		// (get) Token: 0x06002153 RID: 8531 RVA: 0x0007B811 File Offset: 0x00079A11
		public SecurityKeyIdentifierClause TokenToDeriveIdentifier
		{
			get
			{
				return this.tokenToDeriveIdentifier;
			}
		}

		// Token: 0x06002154 RID: 8532 RVA: 0x0007B81C File Offset: 0x00079A1C
		public DerivedKeySecurityToken CreateToken(SecurityToken tokenToDerive, int maxKeyLength)
		{
			DerivedKeySecurityToken derivedKeySecurityToken = new DerivedKeySecurityToken(this.generation, this.offset, this.length, this.label, this.nonce, tokenToDerive, this.tokenToDeriveIdentifier, this.derivationAlgorithm, this.Id);
			derivedKeySecurityToken.InitializeDerivedKey(maxKeyLength);
			return derivedKeySecurityToken;
		}

		// Token: 0x04001F4A RID: 8010
		private string id;

		// Token: 0x04001F4B RID: 8011
		private string derivationAlgorithm;

		// Token: 0x04001F4C RID: 8012
		private string label;

		// Token: 0x04001F4D RID: 8013
		private int length;

		// Token: 0x04001F4E RID: 8014
		private byte[] nonce;

		// Token: 0x04001F4F RID: 8015
		private int offset;

		// Token: 0x04001F50 RID: 8016
		private int generation;

		// Token: 0x04001F51 RID: 8017
		private SecurityKeyIdentifierClause tokenToDeriveIdentifier;
	}
}
