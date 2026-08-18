using System;
using System.IdentityModel.Tokens;

namespace System.ServiceModel.Security
{
	// Token: 0x020002A8 RID: 680
	internal class TokenTracker
	{
		// Token: 0x06001501 RID: 5377 RVA: 0x0004F059 File Offset: 0x0004D259
		public TokenTracker(SupportingTokenAuthenticatorSpecification spec) : this(spec, null, false)
		{
		}

		// Token: 0x06001502 RID: 5378 RVA: 0x0004F064 File Offset: 0x0004D264
		public TokenTracker(SupportingTokenAuthenticatorSpecification spec, SecurityToken token, bool allowFirstTokenMismatch)
		{
			this.spec = spec;
			this.token = token;
			this.allowFirstTokenMismatch = allowFirstTokenMismatch;
		}

		// Token: 0x06001503 RID: 5379 RVA: 0x0004F084 File Offset: 0x0004D284
		public void RecordToken(SecurityToken token)
		{
			if (this.token == null)
			{
				this.token = token;
				return;
			}
			if (this.allowFirstTokenMismatch)
			{
				if (!TokenTracker.AreTokensEqual(this.token, token))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MessageSecurityException(SR.GetString("MismatchInSecurityOperationToken")));
				}
				this.token = token;
				this.allowFirstTokenMismatch = false;
				return;
			}
			else
			{
				if (this.token != token)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MessageSecurityException(SR.GetString("MismatchInSecurityOperationToken")));
				}
				return;
			}
		}

		// Token: 0x06001504 RID: 5380 RVA: 0x0004F104 File Offset: 0x0004D304
		private static bool AreTokensEqual(SecurityToken outOfBandToken, SecurityToken replyToken)
		{
			if (outOfBandToken is X509SecurityToken && replyToken is X509SecurityToken)
			{
				byte[] certHash = ((X509SecurityToken)outOfBandToken).Certificate.GetCertHash();
				byte[] certHash2 = ((X509SecurityToken)replyToken).Certificate.GetCertHash();
				return CryptoHelper.IsEqual(certHash, certHash2);
			}
			return false;
		}

		// Token: 0x04001B03 RID: 6915
		public SecurityToken token;

		// Token: 0x04001B04 RID: 6916
		public bool IsDerivedFrom;

		// Token: 0x04001B05 RID: 6917
		public bool IsSigned;

		// Token: 0x04001B06 RID: 6918
		public bool IsEncrypted;

		// Token: 0x04001B07 RID: 6919
		public bool IsEndorsing;

		// Token: 0x04001B08 RID: 6920
		public bool AlreadyReadEndorsingSignature;

		// Token: 0x04001B09 RID: 6921
		private bool allowFirstTokenMismatch;

		// Token: 0x04001B0A RID: 6922
		public SupportingTokenAuthenticatorSpecification spec;
	}
}
