using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x020007EA RID: 2026
	internal static class DecoderHelper
	{
		// Token: 0x06004CAC RID: 19628 RVA: 0x00117BE9 File Offset: 0x00115DE9
		public static void ValidateSize(int size)
		{
			if (size <= 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("size", size, SR.GetString("ValueMustBePositive")));
			}
		}
	}
}
