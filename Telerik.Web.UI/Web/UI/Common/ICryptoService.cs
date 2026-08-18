using System;

namespace Telerik.Web.UI.Common
{
	// Token: 0x02000182 RID: 386
	internal interface ICryptoService
	{
		// Token: 0x06000D74 RID: 3444
		string DecryptWithMachineKey(string encryptedText);

		// Token: 0x06000D75 RID: 3445
		string Decrypt(string encryptedString);

		// Token: 0x06000D76 RID: 3446
		string EncryptWithMachineKey(string clearText);

		// Token: 0x06000D77 RID: 3447
		string Encrypt(string plainString);

		// Token: 0x06000D78 RID: 3448
		void CheckWhitelistTypes(Type type, string allowedCustomMetaTypes, string uploadMetaDataFullName);
	}
}
