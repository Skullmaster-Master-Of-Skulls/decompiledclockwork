using System;

namespace Telerik.Web.UI
{
	// Token: 0x0200008E RID: 142
	internal interface ICryptoExceptionThrower
	{
		// Token: 0x06000579 RID: 1401
		T ThrowGenericCryptoException<T>();

		// Token: 0x0600057A RID: 1402
		T ThrowIfFails<T>(Func<T> function);
	}
}
