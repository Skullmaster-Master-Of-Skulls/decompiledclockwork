using System;

namespace System.ServiceModel
{
	// Token: 0x020000A6 RID: 166
	internal static class DeadLetterQueueHelper
	{
		// Token: 0x060002CC RID: 716 RVA: 0x00011314 File Offset: 0x0000F514
		public static bool IsDefined(DeadLetterQueue mode)
		{
			return mode >= DeadLetterQueue.None && mode <= DeadLetterQueue.Custom;
		}
	}
}
