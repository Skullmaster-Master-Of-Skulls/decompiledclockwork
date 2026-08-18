using System;
using System.Collections;
using System.Globalization;

namespace System.Web.Compilation
{
	// Token: 0x02000843 RID: 2115
	public interface IImplicitResourceProvider
	{
		// Token: 0x0600649B RID: 25755
		object GetObject(ImplicitResourceKey key, CultureInfo culture);

		// Token: 0x0600649C RID: 25756
		ICollection GetImplicitResourceKeys(string keyPrefix);
	}
}
