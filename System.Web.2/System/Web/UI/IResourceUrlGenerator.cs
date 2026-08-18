using System;

namespace System.Web.UI
{
	// Token: 0x020002B3 RID: 691
	public interface IResourceUrlGenerator
	{
		// Token: 0x06001FB6 RID: 8118
		string GetResourceUrl(Type type, string resourceName);
	}
}
