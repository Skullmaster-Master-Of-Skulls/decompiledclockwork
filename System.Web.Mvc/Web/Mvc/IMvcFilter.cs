using System;

namespace System.Web.Mvc
{
	// Token: 0x0200008E RID: 142
	public interface IMvcFilter
	{
		// Token: 0x17000172 RID: 370
		// (get) Token: 0x06000414 RID: 1044
		bool AllowMultiple { get; }

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x06000415 RID: 1045
		int Order { get; }
	}
}
