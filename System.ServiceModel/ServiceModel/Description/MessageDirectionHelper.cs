using System;

namespace System.ServiceModel.Description
{
	// Token: 0x020003DB RID: 987
	internal static class MessageDirectionHelper
	{
		// Token: 0x06002530 RID: 9520 RVA: 0x00085599 File Offset: 0x00083799
		internal static bool IsDefined(MessageDirection value)
		{
			return value == MessageDirection.Input || value == MessageDirection.Output;
		}

		// Token: 0x06002531 RID: 9521 RVA: 0x000855A4 File Offset: 0x000837A4
		internal static MessageDirection Opposite(MessageDirection d)
		{
			if (d != MessageDirection.Input)
			{
				return MessageDirection.Input;
			}
			return MessageDirection.Output;
		}
	}
}
