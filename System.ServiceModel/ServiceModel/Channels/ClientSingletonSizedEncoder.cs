using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000807 RID: 2055
	internal static class ClientSingletonSizedEncoder
	{
		// Token: 0x06004D26 RID: 19750 RVA: 0x00119E0F File Offset: 0x0011800F
		public static int CalcStartSize(EncodedVia via, EncodedContentType contentType)
		{
			return via.EncodedBytes.Length + contentType.EncodedBytes.Length;
		}

		// Token: 0x06004D27 RID: 19751 RVA: 0x00119E22 File Offset: 0x00118022
		public static void EncodeStart(byte[] buffer, int offset, EncodedVia via, EncodedContentType contentType)
		{
			Buffer.BlockCopy(via.EncodedBytes, 0, buffer, offset, via.EncodedBytes.Length);
			Buffer.BlockCopy(contentType.EncodedBytes, 0, buffer, offset + via.EncodedBytes.Length, contentType.EncodedBytes.Length);
		}

		// Token: 0x0400300C RID: 12300
		public static byte[] ModeBytes = new byte[]
		{
			0,
			1,
			0,
			1,
			4
		};
	}
}
