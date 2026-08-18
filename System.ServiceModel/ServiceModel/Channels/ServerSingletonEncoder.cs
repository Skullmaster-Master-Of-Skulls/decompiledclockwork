using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000806 RID: 2054
	internal class ServerSingletonEncoder : SingletonEncoder
	{
		// Token: 0x06004D24 RID: 19748 RVA: 0x00119DE5 File Offset: 0x00117FE5
		private ServerSingletonEncoder()
		{
		}

		// Token: 0x0400300A RID: 12298
		public static byte[] AckResponseBytes = new byte[]
		{
			11
		};

		// Token: 0x0400300B RID: 12299
		public static byte[] UpgradeResponseBytes = new byte[]
		{
			10
		};
	}
}
