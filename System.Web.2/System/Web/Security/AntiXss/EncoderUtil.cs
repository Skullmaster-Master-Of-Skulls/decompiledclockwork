using System;
using System.Text;

namespace System.Web.Security.AntiXss
{
	// Token: 0x02000611 RID: 1553
	internal static class EncoderUtil
	{
		// Token: 0x06004DE5 RID: 19941 RVA: 0x0010E7F8 File Offset: 0x0010C9F8
		internal static StringBuilder GetOutputStringBuilder(int inputLength, int worstCaseOutputCharsPerInputChar)
		{
			int capacity;
			if (inputLength >= 16384)
			{
				capacity = inputLength;
			}
			else
			{
				long val = (long)inputLength * (long)worstCaseOutputCharsPerInputChar;
				capacity = (int)Math.Min(16384L, val);
			}
			return new StringBuilder(capacity);
		}
	}
}
