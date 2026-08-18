using System;
using System.ServiceModel.Security.Tokens;

namespace System.ServiceModel.Security
{
	// Token: 0x020002BE RID: 702
	internal sealed class NonceToken : BinarySecretSecurityToken
	{
		// Token: 0x06001630 RID: 5680 RVA: 0x00054618 File Offset: 0x00052818
		public NonceToken(byte[] key) : this(SecurityUniqueId.Create().Value, key)
		{
		}

		// Token: 0x06001631 RID: 5681 RVA: 0x00054639 File Offset: 0x00052839
		public NonceToken(string id, byte[] key) : base(id, key, false)
		{
		}

		// Token: 0x06001632 RID: 5682 RVA: 0x00054644 File Offset: 0x00052844
		public NonceToken(int keySizeInBits) : base(SecurityUniqueId.Create().Value, keySizeInBits, false)
		{
		}
	}
}
