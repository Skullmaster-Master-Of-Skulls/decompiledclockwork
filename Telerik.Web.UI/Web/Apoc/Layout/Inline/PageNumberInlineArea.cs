using System;

namespace Telerik.Web.Apoc.Layout.Inline
{
	// Token: 0x020015F1 RID: 5617
	internal class PageNumberInlineArea : WordArea
	{
		// Token: 0x0600DAE0 RID: 56032 RVA: 0x002FE079 File Offset: 0x002FC279
		public PageNumberInlineArea(FontState fontState, float red, float green, float blue, string refid, int width) : base(fontState, red, green, blue, "?", width)
		{
			this.pageNumberId = refid;
		}
	}
}
