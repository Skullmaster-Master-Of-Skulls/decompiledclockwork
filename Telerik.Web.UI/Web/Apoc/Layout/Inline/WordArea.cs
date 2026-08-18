using System;
using Telerik.Web.Apoc.Render;

namespace Telerik.Web.Apoc.Layout.Inline
{
	// Token: 0x020015F0 RID: 5616
	internal class WordArea : InlineArea
	{
		// Token: 0x0600DADD RID: 56029 RVA: 0x002FE049 File Offset: 0x002FC249
		public WordArea(FontState fontState, float red, float green, float blue, string text, int width) : base(fontState, width, red, green, blue)
		{
			this.text = text;
			this.contentRectangleWidth = width;
		}

		// Token: 0x0600DADE RID: 56030 RVA: 0x002FE068 File Offset: 0x002FC268
		public override void render(IRenderer renderer)
		{
			renderer.RenderWordArea(this);
		}

		// Token: 0x0600DADF RID: 56031 RVA: 0x002FE071 File Offset: 0x002FC271
		public string getText()
		{
			return this.text;
		}

		// Token: 0x04003CFB RID: 15611
		private string text;
	}
}
