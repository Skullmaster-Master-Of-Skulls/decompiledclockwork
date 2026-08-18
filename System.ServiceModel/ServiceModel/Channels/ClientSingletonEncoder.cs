using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000805 RID: 2053
	internal class ClientSingletonEncoder : SingletonEncoder
	{
		// Token: 0x06004D20 RID: 19744 RVA: 0x00119D6B File Offset: 0x00117F6B
		private ClientSingletonEncoder()
		{
		}

		// Token: 0x06004D21 RID: 19745 RVA: 0x00119D73 File Offset: 0x00117F73
		public static int CalcStartSize(EncodedVia via, EncodedContentType contentType)
		{
			return via.EncodedBytes.Length + contentType.EncodedBytes.Length;
		}

		// Token: 0x06004D22 RID: 19746 RVA: 0x00119D86 File Offset: 0x00117F86
		public static void EncodeStart(byte[] buffer, int offset, EncodedVia via, EncodedContentType contentType)
		{
			Buffer.BlockCopy(via.EncodedBytes, 0, buffer, offset, via.EncodedBytes.Length);
			Buffer.BlockCopy(contentType.EncodedBytes, 0, buffer, offset + via.EncodedBytes.Length, contentType.EncodedBytes.Length);
		}

		// Token: 0x04003008 RID: 12296
		public static byte[] PreambleEndBytes = new byte[]
		{
			12
		};

		// Token: 0x04003009 RID: 12297
		public static byte[] ModeBytes = new byte[]
		{
			0,
			1,
			0,
			1,
			1
		};
	}
}
