using System;

namespace System.ServiceModel
{
	// Token: 0x020000AB RID: 171
	internal static class MsmqEncryptionAlgorithmHelper
	{
		// Token: 0x060002ED RID: 749 RVA: 0x000114C9 File Offset: 0x0000F6C9
		public static bool IsDefined(MsmqEncryptionAlgorithm algorithm)
		{
			return algorithm == MsmqEncryptionAlgorithm.RC4Stream || algorithm == MsmqEncryptionAlgorithm.Aes;
		}

		// Token: 0x060002EE RID: 750 RVA: 0x000114D4 File Offset: 0x0000F6D4
		public static int ToInt32(MsmqEncryptionAlgorithm algorithm)
		{
			if (algorithm == MsmqEncryptionAlgorithm.RC4Stream)
			{
				return 26625;
			}
			if (algorithm != MsmqEncryptionAlgorithm.Aes)
			{
				return -1;
			}
			return 26129;
		}
	}
}
