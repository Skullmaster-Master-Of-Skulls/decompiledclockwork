using System;

namespace Microsoft.Owin.Security.DataProtection
{
	// Token: 0x0200000B RID: 11
	public interface IDataProtectionProvider
	{
		// Token: 0x06000016 RID: 22
		IDataProtector Create(params string[] purposes);
	}
}
