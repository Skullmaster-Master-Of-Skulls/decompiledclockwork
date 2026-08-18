using System;

namespace System.IdentityModel.Tokens
{
	// Token: 0x0200016A RID: 362
	public abstract class SecurityKeyIdentifierClause
	{
		// Token: 0x06000B6D RID: 2925 RVA: 0x00036B57 File Offset: 0x00034D57
		protected SecurityKeyIdentifierClause(string clauseType) : this(clauseType, null, 0)
		{
		}

		// Token: 0x06000B6E RID: 2926 RVA: 0x00036B62 File Offset: 0x00034D62
		protected SecurityKeyIdentifierClause(string clauseType, byte[] nonce, int length)
		{
			this.clauseType = clauseType;
			this.derivationNonce = nonce;
			this.derivationLength = length;
		}

		// Token: 0x170002CA RID: 714
		// (get) Token: 0x06000B6F RID: 2927 RVA: 0x00002D09 File Offset: 0x00000F09
		public virtual bool CanCreateKey
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170002CB RID: 715
		// (get) Token: 0x06000B70 RID: 2928 RVA: 0x00036B7F File Offset: 0x00034D7F
		public string ClauseType
		{
			get
			{
				return this.clauseType;
			}
		}

		// Token: 0x170002CC RID: 716
		// (get) Token: 0x06000B71 RID: 2929 RVA: 0x00036B87 File Offset: 0x00034D87
		// (set) Token: 0x06000B72 RID: 2930 RVA: 0x00036B8F File Offset: 0x00034D8F
		public string Id
		{
			get
			{
				return this.id;
			}
			set
			{
				this.id = value;
			}
		}

		// Token: 0x06000B73 RID: 2931 RVA: 0x00036B98 File Offset: 0x00034D98
		public virtual SecurityKey CreateKey()
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("KeyIdentifierClauseDoesNotSupportKeyCreation")));
		}

		// Token: 0x06000B74 RID: 2932 RVA: 0x00036BB3 File Offset: 0x00034DB3
		public virtual bool Matches(SecurityKeyIdentifierClause keyIdentifierClause)
		{
			return this == keyIdentifierClause;
		}

		// Token: 0x06000B75 RID: 2933 RVA: 0x00036BB9 File Offset: 0x00034DB9
		public byte[] GetDerivationNonce()
		{
			if (this.derivationNonce == null)
			{
				return null;
			}
			return (byte[])this.derivationNonce.Clone();
		}

		// Token: 0x170002CD RID: 717
		// (get) Token: 0x06000B76 RID: 2934 RVA: 0x00036BD5 File Offset: 0x00034DD5
		public int DerivationLength
		{
			get
			{
				return this.derivationLength;
			}
		}

		// Token: 0x04000C21 RID: 3105
		private readonly string clauseType;

		// Token: 0x04000C22 RID: 3106
		private byte[] derivationNonce;

		// Token: 0x04000C23 RID: 3107
		private int derivationLength;

		// Token: 0x04000C24 RID: 3108
		private string id;
	}
}
