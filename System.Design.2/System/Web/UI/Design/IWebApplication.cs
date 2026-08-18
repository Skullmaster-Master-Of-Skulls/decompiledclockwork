using System;
using System.Configuration;
using System.Runtime.InteropServices;

namespace System.Web.UI.Design
{
	// Token: 0x0200005C RID: 92
	[Guid("cff39fa8-5607-4b6d-86f3-cc80b3cfe2dd")]
	public interface IWebApplication : IServiceProvider
	{
		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x060002D9 RID: 729
		IProjectItem RootProjectItem { get; }

		// Token: 0x060002DA RID: 730
		IProjectItem GetProjectItemFromUrl(string appRelativeUrl);

		// Token: 0x060002DB RID: 731
		Configuration OpenWebConfiguration(bool isReadOnly);
	}
}
