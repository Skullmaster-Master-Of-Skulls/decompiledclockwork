using System;
using System.Web.UI;

namespace Telerik.Web.UI.Editor
{
	// Token: 0x020002D3 RID: 723
	internal class LiteSplitButtonRenderer : LiteDropDownRenderer
	{
		// Token: 0x0600192B RID: 6443 RVA: 0x00052E12 File Offset: 0x00051012
		public LiteSplitButtonRenderer(EditorSplitButton owner) : base(owner)
		{
			this.Owner = owner;
		}

		// Token: 0x17000880 RID: 2176
		// (get) Token: 0x0600192C RID: 6444 RVA: 0x00052E22 File Offset: 0x00051022
		// (set) Token: 0x0600192D RID: 6445 RVA: 0x00052E2A File Offset: 0x0005102A
		public new EditorSplitButton Owner { get; private set; }

		// Token: 0x0600192E RID: 6446 RVA: 0x00052E33 File Offset: 0x00051033
		public override void RenderContents(HtmlTextWriter writer)
		{
			base.RenderContents(writer);
			this.RenderSplitButtonArrow(writer);
		}

		// Token: 0x0600192F RID: 6447 RVA: 0x00052E44 File Offset: 0x00051044
		public override void AddAttributesToRender(HtmlTextWriter writer)
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

		// Token: 0x06001930 RID: 6448 RVA: 0x00052EB7 File Offset: 0x000510B7
		public override void RenderText(HtmlTextWriter writer)
		{
			if (this.Owner.Type != EditorToolType.ToolStrip)
			{
				base.RenderText(writer);
			}
		}

		// Token: 0x17000881 RID: 2177
		// (get) Token: 0x06001931 RID: 6449 RVA: 0x00052ECF File Offset: 0x000510CF
		public override string CssClassFormatString
		{
			get
			{
				return string.Format("reTool reToolIcon {0}{1}", base.CssClassFormatString, this.Owner.ShowText ? " reToolText" : "");
			}
		}

		// Token: 0x06001932 RID: 6450 RVA: 0x00052EFA File Offset: 0x000510FA
		public override string GetCssClassString()
		{
			return string.Format(this.CssClassFormatString, "reSplitButton", this.Owner.Name);
		}

		// Token: 0x06001933 RID: 6451 RVA: 0x00052F17 File Offset: 0x00051117
		public override void RenderSplitButtonArrow(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "reSplitArrow");
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			writer.RenderEndTag();
		}
	}
}
