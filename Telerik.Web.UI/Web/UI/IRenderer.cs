using System;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02000014 RID: 20
	public interface IRenderer
	{
		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000115 RID: 277
		HtmlTextWriterTag TagKey { get; }

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000116 RID: 278
		string CssClassFormatString { get; }

		// Token: 0x06000117 RID: 279
		void AddAttributesToRender(HtmlTextWriter writer);

		// Token: 0x06000118 RID: 280
		void RenderContents(HtmlTextWriter writer);
	}
}
