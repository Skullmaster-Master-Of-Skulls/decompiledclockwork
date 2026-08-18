using System;

namespace System.Web.Helpers.AntiXsrf
{
	// Token: 0x02000022 RID: 34
	internal interface ICryptoSystem
	{
		// Token: 0x06000101 RID: 257
		string Protect(byte[] data);

		// Token: 0x06000102 RID: 258
		byte[] Unprotect(string protectedData);
	}
}
