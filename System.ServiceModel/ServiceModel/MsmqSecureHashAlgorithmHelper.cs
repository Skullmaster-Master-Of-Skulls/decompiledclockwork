using System;

namespace System.ServiceModel
{
	// Token: 0x020000AF RID: 175
	internal static class MsmqSecureHashAlgorithmHelper
	{
		// Token: 0x060002F9 RID: 761 RVA: 0x00011D69 File Offset: 0x0000FF69
		public static bool IsDefined(MsmqSecureHashAlgorithm algorithm)
		{
			return algorithm == MsmqSecureHashAlgorithm.MD5 || algorithm == MsmqSecureHashAlgorithm.Sha1 || algorithm == MsmqSecureHashAlgorithm.Sha256 || algorithm == MsmqSecureHashAlgorithm.Sha512;
		}

		// Token: 0x060002FA RID: 762 RVA: 0x00011D7C File Offset: 0x0000FF7C
		public static int ToInt32(MsmqSecureHashAlgorithm algorithm)
		{
			switch (algorithm)
			{
			case MsmqSecureHashAlgorithm.MD5:
				return 32771;
			case MsmqSecureHashAlgorithm.Sha1:
				return 32772;
			case MsmqSecureHashAlgorithm.Sha256:
				return 32780;
			case MsmqSecureHashAlgorithm.Sha512:
				return 32782;
			default:
				return -1;
			}
		}
	}
}
