using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x020007FA RID: 2042
	internal static class IntEncoder
	{
		// Token: 0x06004D03 RID: 19715 RVA: 0x00119858 File Offset: 0x00117A58
		public static int Encode(int value, byte[] bytes, int offset)
		{
			int num = 1;
			while (((long)value & (long)((ulong)-128)) != 0L)
			{
				bytes[offset++] = (byte)((value & 127) | 128);
				num++;
				value >>= 7;
			}
			bytes[offset] = (byte)value;
			return num;
		}

		// Token: 0x06004D04 RID: 19716 RVA: 0x00119894 File Offset: 0x00117A94
		public static int GetEncodedSize(int value)
		{
			if (value < 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value", value, SR.GetString("ValueMustBeNonNegative")));
			}
			int num = 1;
			while (((long)value & (long)((ulong)-128)) != 0L)
			{
				num++;
				value >>= 7;
			}
			return num;
		}

		// Token: 0x04002FFB RID: 12283
		public const int MaxEncodedSize = 5;
	}
}
