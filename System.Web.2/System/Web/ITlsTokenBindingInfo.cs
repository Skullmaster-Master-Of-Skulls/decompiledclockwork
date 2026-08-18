using System;

namespace System.Web
{
	// Token: 0x020000AE RID: 174
	public interface ITlsTokenBindingInfo
	{
		// Token: 0x06000B24 RID: 2852
		byte[] GetProvidedTokenBindingId();

		// Token: 0x06000B25 RID: 2853
		byte[] GetReferredTokenBindingId();
	}
}
