using System;
using System.Runtime.Remoting.Metadata.W3cXsd2001;

namespace System.IdentityModel.Tokens
{
	// Token: 0x02000113 RID: 275
	public abstract class BinaryKeyIdentifierClause : SecurityKeyIdentifierClause
	{
		// Token: 0x06000783 RID: 1923 RVA: 0x0001FB24 File Offset: 0x0001DD24
		protected BinaryKeyIdentifierClause(string clauseType, byte[] identificationData, bool cloneBuffer) : this(clauseType, identificationData, cloneBuffer, null, 0)
		{
		}

		// Token: 0x06000784 RID: 1924 RVA: 0x0001FB34 File Offset: 0x0001DD34
		protected BinaryKeyIdentifierClause(string clauseType, byte[] identificationData, bool cloneBuffer, byte[] derivationNonce, int derivationLength) : base(clauseType, derivationNonce, derivationLength)
		{
			if (identificationData == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("identificationData"));
			}
			if (identificationData.Length == 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("identificationData", SR.GetString("LengthMustBeGreaterThanZero")));
			}
			if (cloneBuffer)
			{
				this.identificationData = SecurityUtils.CloneBuffer(identificationData);
				return;
			}
			this.identificationData = identificationData;
		}

		// Token: 0x06000785 RID: 1925 RVA: 0x0001FB9E File Offset: 0x0001DD9E
		public byte[] GetBuffer()
		{
			return SecurityUtils.CloneBuffer(this.identificationData);
		}

		// Token: 0x06000786 RID: 1926 RVA: 0x0001FBAB File Offset: 0x0001DDAB
		protected byte[] GetRawBuffer()
		{
			return this.identificationData;
		}

		// Token: 0x06000787 RID: 1927 RVA: 0x0001FBB4 File Offset: 0x0001DDB4
		public override bool Matches(SecurityKeyIdentifierClause keyIdentifierClause)
		{
			BinaryKeyIdentifierClause binaryKeyIdentifierClause = keyIdentifierClause as BinaryKeyIdentifierClause;
			return this == binaryKeyIdentifierClause || (binaryKeyIdentifierClause != null && binaryKeyIdentifierClause.Matches(this.identificationData));
		}

		// Token: 0x06000788 RID: 1928 RVA: 0x0001FBDF File Offset: 0x0001DDDF
		public bool Matches(byte[] data)
		{
			return this.Matches(data, 0);
		}

		// Token: 0x06000789 RID: 1929 RVA: 0x0001FBE9 File Offset: 0x0001DDE9
		public bool Matches(byte[] data, int offset)
		{
			if (offset < 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("offset", SR.GetString("ValueMustBeNonNegative")));
			}
			return SecurityUtils.MatchesBuffer(this.identificationData, 0, data, offset);
		}

		// Token: 0x0600078A RID: 1930 RVA: 0x0001FC1C File Offset: 0x0001DE1C
		internal string ToBase64String()
		{
			return Convert.ToBase64String(this.identificationData);
		}

		// Token: 0x0600078B RID: 1931 RVA: 0x0001FC29 File Offset: 0x0001DE29
		internal string ToHexString()
		{
			return new SoapHexBinary(this.identificationData).ToString();
		}

		// Token: 0x04000AC4 RID: 2756
		private readonly byte[] identificationData;
	}
}
