using System;

namespace Telerik.Web.UI.Common
{
	// Token: 0x02000090 RID: 144
	internal interface IHmacEnabledService
	{
		// Token: 0x0600057E RID: 1406
		string Encrypt(string input);

		// Token: 0x0600057F RID: 1407
		string Decrypt(string input);
	}
}
