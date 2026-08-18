using System;

namespace System.Web.UI
{
	// Token: 0x02000053 RID: 83
	internal interface IClientUrlResolver
	{
		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000308 RID: 776
		string AppRelativeTemplateSourceDirectory { get; }

		// Token: 0x06000309 RID: 777
		string ResolveClientUrl(string relativeUrl);
	}
}
