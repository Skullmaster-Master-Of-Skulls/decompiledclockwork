using System;

namespace System.Web.WebPages
{
	// Token: 0x0200000A RID: 10
	public interface IVirtualPathFactory
	{
		// Token: 0x0600004A RID: 74
		bool Exists(string virtualPath);

		// Token: 0x0600004B RID: 75
		object CreateInstance(string virtualPath);
	}
}
