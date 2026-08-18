using System;

namespace Microsoft.Owin.Security
{
	// Token: 0x02000006 RID: 6
	public interface ISecureDataFormat<TData>
	{
		// Token: 0x0600000A RID: 10
		string Protect(TData data);

		// Token: 0x0600000B RID: 11
		TData Unprotect(string protectedText);
	}
}
