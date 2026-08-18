using System;
using System.ServiceModel.Security.Tokens;

namespace TechnoPro.Common.Security.Saml
{
	// Token: 0x02000009 RID: 9
	public sealed class NonceToken : BinarySecretSecurityToken
	{
		// Token: 0x06000030 RID: 48 RVA: 0x00002787 File Offset: 0x00000987
		public NonceToken(byte[] key) : this(NonceToken.GenerateUniqueId(), key)
		{
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002797 File Offset: 0x00000997
		private NonceToken(string id, byte[] key) : base(id, key, false)
		{
		}

		// Token: 0x06000032 RID: 50 RVA: 0x000027A4 File Offset: 0x000009A4
		private static string GenerateUniqueId()
		{
			return "uuid-" + Guid.NewGuid().ToString() + "-icc";
		}
	}
}
