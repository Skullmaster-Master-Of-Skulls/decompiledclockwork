using System;
using System.Web.UI;

namespace Telerik.Web.UI.Editor
{
	// Token: 0x020002CA RID: 714
	internal abstract class ToolRendererBase : IEditorToolRenderer, IEditorRenderer, IRenderer
	{
		// Token: 0x060018D3 RID: 6355 RVA: 0x000526C1 File Offset: 0x000508C1
		public ToolRendererBase(EditorTool owner)
		{
			this.Owner = owner;
		}

		// Token: 0x1700086A RID: 2154
		// (get) Token: 0x060018D4 RID: 6356 RVA: 0x000526D0 File Offset: 0x000508D0
		// (set) Token: 0x060018D5 RID: 6357 RVA: 0x000526D8 File Offset: 0x000508D8
		private protected EditorTool Owner { protected get; private set; }

		// Token: 0x1700086B RID: 2155
		// (get) Token: 0x060018D6 RID: 6358 RVA: 0x000526E1 File Offset: 0x000508E1
		public virtual HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.A;
			}
		}

		// Token: 0x1700086C RID: 2156
		// (get) Token: 0x060018D7 RID: 6359 RVA: 0x000526E4 File Offset: 0x000508E4
		public virtual string CssClassFormatString
		{
			get
			{
				return "{0}";
			}
		}

		// Token: 0x060018D8 RID: 6360 RVA: 0x000526EB File Offset: 0x000508EB
		public void Render(HtmlTextWriter writer)
		{
			this.RenderBeginTag(writer);
			this.RenderContents(writer);
			this.RenderEndTag(writer);
		}

		// Token: 0x060018D9 RID: 6361 RVA: 0x00052702 File Offset: 0x00050902
		public virtual void RenderBeginTag(HtmlTextWriter writer)
		{
			if (!this.Owner.Visible)
			{
				writer.AddStyleAttribute(HtmlTextWriterStyle.Display, "none");
			}
			this.AddAttributesToRender(writer);
			writer.RenderBeginTag(this.TagKey);
		}

		// Token: 0x060018DA RID: 6362 RVA: 0x00052731 File Offset: 0x00050931
		public virtual void RenderEndTag(HtmlTextWriter writer)
		{
			writer.RenderEndTag();
		}

		// Token: 0x060018DB RID: 6363 RVA: 0x00052739 File Offset: 0x00050939
		public virtual void RenderContents(HtmlTextWriter writer)
		{
			this.RenderToolIcon(writer);
			this.RenderToolText(writer);
		}

		// Token: 0x060018DC RID: 6364 RVA: 0x00052749 File Offset: 0x00050949
		public virtual void RenderChildren(HtmlTextWriter writer)
		{
		}

		// Token: 0x060018DD RID: 6365 RVA: 0x0005274B File Offset: 0x0005094B
		public virtual void RenderToolIcon(HtmlTextWriter writer)
		{
			if (this.Owner.ShowIcon)
			{
				this.AddIconAttributesToRender(writer);
				writer.RenderBeginTag(HtmlTextWriterTag.Span);
				writer.Write("&nbsp;");
				writer.RenderEndTag();
			}
		}

		// Token: 0x060018DE RID: 6366 RVA: 0x0005277C File Offset: 0x0005097C
		public virtual void RenderToolText(HtmlTextWriter writer)
		{
			if (this.Owner.ShowText)
			{
				this.AddTextAttributesToRender(writer);
				writer.RenderBeginTag(HtmlTextWriterTag.Span);
				writer.Write((this.Owner.Text.Trim().Length == 0) ? "&nbsp;" : this.Owner.Text);
				writer.RenderEndTag();
			}
		}

		// Token: 0x060018DF RID: 6367 RVA: 0x000527DA File Offset: 0x000509DA
		public virtual void RenderSplitButtonArrow(HtmlTextWriter writer)
		{
		}

		// Token: 0x060018E0 RID: 6368
		public abstract string GetCssClassString();

		// Token: 0x1700086D RID: 2157
		// (get) Token: 0x060018E1 RID: 6369
		public abstract string CssClassString { get; }

		// Token: 0x060018E2 RID: 6370 RVA: 0x000527DC File Offset: 0x000509DC
		public virtual void AddIconAttributesToRender(HtmlTextWriter writer)
		{
		}

		// Token: 0x060018E3 RID: 6371 RVA: 0x000527DE File Offset: 0x000509DE
		public virtual void AddTextAttributesToRender(HtmlTextWriter writer)
		{
		}

		// Token: 0x060018E4 RID: 6372 RVA: 0x000527E0 File Offset: 0x000509E0
		public virtual void AddAttributesToRender(HtmlTextWriter writer)
		{
			string text = this.Owner.Text;
			if (!string.IsNullOrEmpty(text))
			{
				if (!string.IsNullOrEmpty(this.Owner.ShortCut))
				{
					text = text + " (" + this.Owner.ShortCut + ")";
				}
				writer.AddAttribute(HtmlTextWriterAttribute.Title, text);
			}
			writer.AddAttribute(HtmlTextWriterAttribute.Class, this.CssClassString);
			writer.AddAttribute(HtmlTextWriterAttribute.Href, "#");
		}
	}
}
