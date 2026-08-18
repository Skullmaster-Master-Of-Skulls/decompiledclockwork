using System;
using System.IdentityModel;
using System.IdentityModel.Tokens;
using System.Runtime.CompilerServices;

namespace System.ServiceModel.Security
{
	// Token: 0x02000007 RID: 7
	[TypeForwardedFrom("System.ServiceModel, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	public class BinarySecretKeyIdentifierClause : BinaryKeyIdentifierClause
	{
		// Token: 0x0600000E RID: 14 RVA: 0x000023FA File Offset: 0x000005FA
		public BinarySecretKeyIdentifierClause(byte[] key) : this(key, true)
		{
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002404 File Offset: 0x00000604
		public BinarySecretKeyIdentifierClause(byte[] key, bool cloneBuffer) : this(key, cloneBuffer, null, 0)
		{
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002410 File Offset: 0x00000610
		public BinarySecretKeyIdentifierClause(byte[] key, bool cloneBuffer, byte[] derivationNonce, int derivationLength) : base(XD.TrustFeb2005Dictionary.BinarySecretClauseType.Value, key, cloneBuffer, derivationNonce, derivationLength)
		{
		}

		// Token: 0x06000011 RID: 17 RVA: 0x0000242C File Offset: 0x0000062C
		public byte[] GetKeyBytes()
		{
			return base.GetBuffer();
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000012 RID: 18 RVA: 0x00002434 File Offset: 0x00000634
		public override bool CanCreateKey
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002437 File Offset: 0x00000637
		public override SecurityKey CreateKey()
		{
			if (this.symmetricKey == null)
			{
				this.symmetricKey = new InMemorySymmetricSecurityKey(base.GetBuffer(), false);
			}
			return this.symmetricKey;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x0000245C File Offset: 0x0000065C
		public override bool Matches(SecurityKeyIdentifierClause keyIdentifierClause)
		{
			BinarySecretKeyIdentifierClause binarySecretKeyIdentifierClause = keyIdentifierClause as BinarySecretKeyIdentifierClause;
			return this == binarySecretKeyIdentifierClause || (binarySecretKeyIdentifierClause != null && binarySecretKeyIdentifierClause.Matches(base.GetRawBuffer()));
		}

		// Token: 0x04000055 RID: 85
		private InMemorySymmetricSecurityKey symmetricKey;
	}
}
