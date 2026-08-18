using System;

namespace System.Web.UI
{
	// Token: 0x02000059 RID: 89
	internal interface IHtmlForm
	{
		// Token: 0x170000BD RID: 189
		// (get) Token: 0x06000312 RID: 786
		string ClientID { get; }

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x06000313 RID: 787
		string Method { get; }

		// Token: 0x06000314 RID: 788
		void RenderControl(HtmlTextWriter writer);

		// Token: 0x06000315 RID: 789
		void SetRenderMethodDelegate(RenderMethod renderMethod);
	}
}
