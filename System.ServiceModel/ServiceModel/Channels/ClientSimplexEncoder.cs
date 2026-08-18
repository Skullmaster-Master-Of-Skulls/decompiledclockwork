using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000803 RID: 2051
	internal class ClientSimplexEncoder : SessionEncoder
	{
		// Token: 0x06004D1B RID: 19739 RVA: 0x00119C7E File Offset: 0x00117E7E
		private ClientSimplexEncoder()
		{
		}

		// Token: 0x04003003 RID: 12291
		public static byte[] ModeBytes = new byte[]
		{
			0,
			1,
			0,
			1,
			3
		};
	}
}
