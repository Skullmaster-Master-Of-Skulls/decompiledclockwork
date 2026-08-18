using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000800 RID: 2048
	internal abstract class SessionEncoder
	{
		// Token: 0x06004D13 RID: 19731 RVA: 0x00119B2E File Offset: 0x00117D2E
		public static int CalcStartSize(EncodedVia via, EncodedContentType contentType)
		{
			return via.EncodedBytes.Length + contentType.EncodedBytes.Length;
		}

		// Token: 0x06004D14 RID: 19732 RVA: 0x00119B41 File Offset: 0x00117D41
		public static void EncodeStart(byte[] buffer, int offset, EncodedVia via, EncodedContentType contentType)
		{
			Buffer.BlockCopy(via.EncodedBytes, 0, buffer, offset, via.EncodedBytes.Length);
			Buffer.BlockCopy(contentType.EncodedBytes, 0, buffer, offset + via.EncodedBytes.Length, contentType.EncodedBytes.Length);
		}

		// Token: 0x06004D15 RID: 19733 RVA: 0x00119B78 File Offset: 0x00117D78
		public static ArraySegment<byte> EncodeMessageFrame(ArraySegment<byte> messageFrame)
		{
			int num = 1 + IntEncoder.GetEncodedSize(messageFrame.Count);
			int num2 = messageFrame.Offset - num;
			if (num2 < 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("messageFrame.Offset", messageFrame.Offset, SR.GetString("SpaceNeededExceedsMessageFrameOffset", new object[]
				{
					num
				})));
			}
			byte[] array = messageFrame.Array;
			array[num2++] = 6;
			IntEncoder.Encode(messageFrame.Count, array, num2);
			return new ArraySegment<byte>(array, messageFrame.Offset - num, messageFrame.Count + num);
		}

		// Token: 0x04002FFD RID: 12285
		public const int MaxMessageFrameSize = 6;

		// Token: 0x04002FFE RID: 12286
		public static byte[] PreambleEndBytes = new byte[]
		{
			12
		};

		// Token: 0x04002FFF RID: 12287
		public static byte[] EndBytes = new byte[]
		{
			7
		};
	}
}
