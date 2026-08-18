using System;

namespace System.Web.UI
{
	// Token: 0x02000055 RID: 85
	internal interface IControl : IClientUrlResolver
	{
		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x0600030B RID: 779
		HttpContextBase Context { get; }

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x0600030C RID: 780
		bool DesignMode { get; }
	}
}
