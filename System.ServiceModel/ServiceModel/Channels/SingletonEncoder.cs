using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000804 RID: 2052
	internal abstract class SingletonEncoder
	{
		// Token: 0x06004D1E RID: 19742 RVA: 0x00119CA8 File Offset: 0x00117EA8
		public static ArraySegment<byte> EncodeMessageFrame(ArraySegment<byte> messageFrame)
		{
			int encodedSize = IntEncoder.GetEncodedSize(messageFrame.Count);
			int num = messageFrame.Offset - encodedSize;
			if (num < 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("messageFrame.Offset", messageFrame.Offset, SR.GetString("SpaceNeededExceedsMessageFrameOffset", new object[]
				{
					encodedSize
				})));
			}
			byte[] array = messageFrame.Array;
			IntEncoder.Encode(messageFrame.Count, array, num);
			return new ArraySegment<byte>(array, num, messageFrame.Count + encodedSize);
		}

		// Token: 0x04003004 RID: 12292
		public static byte[] EnvelopeStartBytes = new byte[]
		{
			5
		};

		// Token: 0x04003005 RID: 12293
		public static byte[] EnvelopeEndBytes = new byte[1];

		// Token: 0x04003006 RID: 12294
		public static byte[] EnvelopeEndFramingEndBytes = new byte[]
		{
			0,
			7
		};

		// Token: 0x04003007 RID: 12295
		public static byte[] EndBytes = new byte[]
		{
			7
		};
	}
}
