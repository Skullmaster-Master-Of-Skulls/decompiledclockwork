using System;
using System.Collections.ObjectModel;

namespace System.Web.Http.Description
{
	// Token: 0x020000BB RID: 187
	public interface IApiExplorer
	{
		// Token: 0x170001BF RID: 447
		// (get) Token: 0x06000438 RID: 1080
		Collection<ApiDescription> ApiDescriptions { get; }
	}
}
