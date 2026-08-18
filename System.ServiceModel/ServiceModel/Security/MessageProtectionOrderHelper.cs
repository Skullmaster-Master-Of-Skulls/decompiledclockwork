using System;

namespace System.ServiceModel.Security
{
	// Token: 0x020002D8 RID: 728
	internal static class MessageProtectionOrderHelper
	{
		// Token: 0x060017D5 RID: 6101 RVA: 0x0005AE4D File Offset: 0x0005904D
		internal static bool IsDefined(MessageProtectionOrder value)
		{
			return value == MessageProtectionOrder.SignBeforeEncrypt || value == MessageProtectionOrder.SignBeforeEncryptAndEncryptSignature || value == MessageProtectionOrder.EncryptBeforeSign;
		}
	}
}
