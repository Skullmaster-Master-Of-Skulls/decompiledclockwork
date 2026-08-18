using System;
using System.Web.UI;

namespace Telerik.Web.UI.Editor
{
	// Token: 0x020002CD RID: 717
	internal class ClassicSplitButtonRenderer : ClassicDropDownRenderer
	{
		// Token: 0x060018F0 RID: 6384 RVA: 0x00052958 File Offset: 0x00050B58
		public ClassicSplitButtonRenderer(EditorSplitButton owner) : base(owner)
		{
			this.Owner = owner;
		}

		// Token: 0x17000871 RID: 2161
		// (get) Token: 0x060018F1 RID: 6385 RVA: 0x00052968 File Offset: 0x00050B68
		// (set) Token: 0x060018F2 RID: 6386 RVA: 0x00052970 File Offset: 0x00050B70
		public new EditorSplitButton Owner { get; private set; }

		// Token: 0x17000872 RID: 2162
		// (get) Token: 0x060018F3 RID: 6387 RVA: 0x00052979 File Offset: 0x00050B79
		public override string CssClassString
		{
			get
			{
				return string.Format(this.CssClassFormatString, this.GetCssClassString());
			}
		}

		// Token: 0x060018F4 RID: 6388 RVA: 0x0005298C File Offset: 0x00050B8C
		public override string GetCssClassString()
		{
			return string.Format("{0} reSplitButton", base.GetCssClassString());
		}

		// Token: 0x060018F5 RID: 6389 RVA: 0x0005299E File Offset: 0x00050B9E
		public override void AddTextAttributesToRender(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, this.Owner.Name);
		}

		// Token: 0x060018F6 RID: 6390 RVA: 0x000529B3 File Offset: 0x00050BB3
		public override void RenderContents(HtmlTextWriter writer)
		{
			base.RenderContents(writer);
			this.RenderToolIcon(writer);
			this.RenderSplitButtonArrow(writer);
		}

		// Token: 0x060018F7 RID: 6391 RVA: 0x000529CA File Offset: 0x00050BCA
		public override void RenderToolText(HtmlTextWriter writer)
		{
			if (this.Owner.ShowText)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Class, "reButton_text");
				base.RenderToolText(writer);
			}
		}

		// Token: 0x060018F8 RID: 6392 RVA: 0x000529ED File Offset: 0x00050BED
		public override void RenderSplitButtonArrow(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "split_arrow");
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			writer.Write("&nbsp;");
			writer.RenderEndTag();
		}
	}
}
