using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000801 RID: 2049
	internal abstract class ServerSessionEncoder : SessionEncoder
	{
		// Token: 0x04003000 RID: 12288
		public static byte[] AckResponseBytes = new byte[]
		{
			11
		};

		// Token: 0x04003001 RID: 12289
		public static byte[] UpgradeResponseBytes = new byte[]
		{
			10
		};
	}
}
