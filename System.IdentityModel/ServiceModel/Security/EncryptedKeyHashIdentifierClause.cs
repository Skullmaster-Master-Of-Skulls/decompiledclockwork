using System;
using System.Globalization;
using System.IdentityModel.Tokens;
using System.Runtime.CompilerServices;

namespace System.ServiceModel.Security
{
	// Token: 0x02000009 RID: 9
	[TypeForwardedFrom("System.ServiceModel, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	internal sealed class EncryptedKeyHashIdentifierClause : BinaryKeyIdentifierClause
	{
		// Token: 0x06000028 RID: 40 RVA: 0x00002692 File Offset: 0x00000892
		public EncryptedKeyHashIdentifierClause(byte[] encryptedKeyHash) : this(encryptedKeyHash, true)
		{
		}

		// Token: 0x06000029 RID: 41 RVA: 0x0000269C File Offset: 0x0000089C
		internal EncryptedKeyHashIdentifierClause(byte[] encryptedKeyHash, bool cloneBuffer) : this(encryptedKeyHash, cloneBuffer, null, 0)
		{
		}

		// Token: 0x0600002A RID: 42 RVA: 0x000026A8 File Offset: 0x000008A8
		internal EncryptedKeyHashIdentifierClause(byte[] encryptedKeyHash, bool cloneBuffer, byte[] derivationNonce, int derivationLength) : base(null, encryptedKeyHash, cloneBuffer, derivationNonce, derivationLength)
		{
		}

		// Token: 0x0600002B RID: 43 RVA: 0x0000242C File Offset: 0x0000062C
		public byte[] GetEncryptedKeyHash()
		{
			return base.GetBuffer();
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000026B6 File Offset: 0x000008B6
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "EncryptedKeyHashIdentifierClause(Hash = {0})", new object[]
			{
				Convert.ToBase64String(base.GetRawBuffer())
			});
		}
	}
}
