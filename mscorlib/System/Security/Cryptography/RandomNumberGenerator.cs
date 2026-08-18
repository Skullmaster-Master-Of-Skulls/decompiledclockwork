using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	// Token: 0x02000866 RID: 2150
	[ComVisible(true)]
	public abstract class RandomNumberGenerator
	{
		// Token: 0x06004E7D RID: 20093 RVA: 0x00110034 File Offset: 0x0010F034
		public static RandomNumberGenerator Create()
		{
			return RandomNumberGenerator.Create("System.Security.Cryptography.RandomNumberGenerator");
		}

		// Token: 0x06004E7E RID: 20094 RVA: 0x00110040 File Offset: 0x0010F040
		public static RandomNumberGenerator Create(string rngName)
		{
			return (RandomNumberGenerator)CryptoConfig.CreateFromName(rngName);
		}

		// Token: 0x06004E7F RID: 20095
		public abstract void GetBytes(byte[] data);

		// Token: 0x06004E80 RID: 20096
		public abstract void GetNonZeroBytes(byte[] data);
	}
}
