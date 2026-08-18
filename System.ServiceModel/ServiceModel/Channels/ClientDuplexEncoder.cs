using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000802 RID: 2050
	internal class ClientDuplexEncoder : SessionEncoder
	{
		// Token: 0x06004D19 RID: 19737 RVA: 0x00119C5E File Offset: 0x00117E5E
		private ClientDuplexEncoder()
		{
		}

		// Token: 0x04003002 RID: 12290
		public static byte[] ModeBytes = new byte[]
		{
			0,
			1,
			0,
			1,
			2
		};
	}
}
