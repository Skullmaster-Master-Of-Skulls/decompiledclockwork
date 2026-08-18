using System;

namespace Microsoft.Owin.Security.DataProtection
{
	// Token: 0x0200000D RID: 13
	public interface IDataProtector
	{
		// Token: 0x06000019 RID: 25
		byte[] Protect(byte[] userData);

		// Token: 0x0600001A RID: 26
		byte[] Unprotect(byte[] protectedData);
	}
}
