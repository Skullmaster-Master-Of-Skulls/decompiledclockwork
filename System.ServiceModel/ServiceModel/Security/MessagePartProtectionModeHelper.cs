using System;

namespace System.ServiceModel.Security
{
	// Token: 0x020002A4 RID: 676
	internal static class MessagePartProtectionModeHelper
	{
		// Token: 0x06001477 RID: 5239 RVA: 0x0004CB10 File Offset: 0x0004AD10
		public static MessagePartProtectionMode GetProtectionMode(bool sign, bool encrypt, bool signThenEncrypt)
		{
			if (sign)
			{
				if (!encrypt)
				{
					return MessagePartProtectionMode.Sign;
				}
				if (signThenEncrypt)
				{
					return MessagePartProtectionMode.SignThenEncrypt;
				}
				return MessagePartProtectionMode.EncryptThenSign;
			}
			else
			{
				if (encrypt)
				{
					return MessagePartProtectionMode.Encrypt;
				}
				return MessagePartProtectionMode.None;
			}
		}
	}
}
