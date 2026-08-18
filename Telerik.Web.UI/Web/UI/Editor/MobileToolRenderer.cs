using System;
using System.Text;
using System.Web.UI;

namespace Telerik.Web.UI.Editor
{
	// Token: 0x020002D4 RID: 724
	internal class MobileToolRenderer : ToolRendererBase
	{
		// Token: 0x06001934 RID: 6452 RVA: 0x00052F34 File Offset: 0x00051134
		public MobileToolRenderer(EditorTool editor) : base(editor)
		{
		}

		// Token: 0x17000882 RID: 2178
		// (get) Token: 0x06001935 RID: 6453 RVA: 0x00052F3D File Offset: 0x0005113D
		public override string CssClassString
		{
			get
			{
				return "reButton";
			}
		}

		// Token: 0x06001936 RID: 6454 RVA: 0x00052F44 File Offset: 0x00051144
		public override string GetCssClassString()
		{
			StringBuilder stringBuilder = new StringBuilder("{0} re{1}");
			if (base.Owner.ShowText)
			{
				stringBuilder.Append(" reVisibleText");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x17000883 RID: 2179
		// (get) Token: 0x06001937 RID: 6455 RVA: 0x00052F7B File Offset: 0x0005117B
		public virtual string IconCssClassStirng
		{
			get
			{
				return "reIcon";
			}
		}

		// Token: 0x06001938 RID: 6456 RVA: 0x00052F82 File Offset: 0x00051182
		public virtual string GetIconCssClassString()
		{
			return "{0} {0}{1}";
		}

		// Token: 0x17000884 RID: 2180
		// (get) Token: 0x06001939 RID: 6457 RVA: 0x00052F89 File Offset: 0x00051189
		public override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Span;
			}
		}

		// Token: 0x0600193A RID: 6458 RVA: 0x00052F8D File Offset: 0x0005118D
		public override void RenderToolIcon(HtmlTextWriter writer)
		{
			if (base.Owner.ShowIcon)
			{
				this.AddIconAttributesToRender(writer);
				writer.RenderBeginTag(HtmlTextWriterTag.Span);
				writer.RenderEndTag();
			}
		}

		// Token: 0x0600193B RID: 6459 RVA: 0x00052FB4 File Offset: 0x000511B4
		public override void AddAttributesToRender(HtmlTextWriter writer)
		{
			string text = base.Owner.Text;
			if (!string.IsNullOrEmpty(text))
			{
				if (!string.IsNullOrEmpty(base.Owner.ShortCut))
				{
					text = text + " (" + base.Owner.ShortCut + ")";
				}
				writer.AddAttribute(HtmlTextWriterAttribute.Title, text);
			}
			writer.AddAttribute("role", "button");
			writer.AddAttribute(HtmlTextWriterAttribute.Class, string.Format(this.GetCssClassString(), this.CssClassString, base.Owner.Name));
		}

		// Token: 0x0600193C RID: 6460 RVA: 0x00053040 File Offset: 0x00051240
		public override void AddIconAttributesToRender(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, string.Format(this.GetIconCssClassString(), this.IconCssClassStirng, base.Owner.Name));
		}
	}
}
