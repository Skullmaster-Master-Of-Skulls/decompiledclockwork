using System;

namespace System.ServiceModel
{
	// Token: 0x02000175 RID: 373
	internal static class TransactionFlowOptionHelper
	{
		// Token: 0x06000AFB RID: 2811 RVA: 0x00028B4E File Offset: 0x00026D4E
		public static bool IsDefined(TransactionFlowOption option)
		{
			return option == TransactionFlowOption.NotAllowed || option == TransactionFlowOption.Allowed || option == TransactionFlowOption.Mandatory;
		}

		// Token: 0x06000AFC RID: 2812 RVA: 0x00028B5D File Offset: 0x00026D5D
		internal static bool AllowedOrRequired(TransactionFlowOption option)
		{
			return option == TransactionFlowOption.Allowed || option == TransactionFlowOption.Mandatory;
		}
	}
}
