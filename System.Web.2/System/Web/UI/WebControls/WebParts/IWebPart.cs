using System;

namespace System.Web.UI.WebControls.WebParts
{
	// Token: 0x02000547 RID: 1351
	public interface IWebPart
	{
		// Token: 0x1700143A RID: 5178
		// (get) Token: 0x060044D3 RID: 17619
		// (set) Token: 0x060044D4 RID: 17620
		string CatalogIconImageUrl { get; set; }

		// Token: 0x1700143B RID: 5179
		// (get) Token: 0x060044D5 RID: 17621
		// (set) Token: 0x060044D6 RID: 17622
		string Description { get; set; }

		// Token: 0x1700143C RID: 5180
		// (get) Token: 0x060044D7 RID: 17623
		string Subtitle { get; }

		// Token: 0x1700143D RID: 5181
		// (get) Token: 0x060044D8 RID: 17624
		// (set) Token: 0x060044D9 RID: 17625
		string Title { get; set; }

		// Token: 0x1700143E RID: 5182
		// (get) Token: 0x060044DA RID: 17626
		// (set) Token: 0x060044DB RID: 17627
		string TitleIconImageUrl { get; set; }

		// Token: 0x1700143F RID: 5183
		// (get) Token: 0x060044DC RID: 17628
		// (set) Token: 0x060044DD RID: 17629
		string TitleUrl { get; set; }
	}
}
