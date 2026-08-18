using System;
using System.Web.UI;

namespace Telerik.Web.UI.RibbonBar.Renderers
{
	// Token: 0x0200078D RID: 1933
	internal class RibbonBarGroupClassicRenderer : RibbonBarGroupRenderer
	{
		// Token: 0x060043F6 RID: 17398 RVA: 0x000D4DD3 File Offset: 0x000D2FD3
		public RibbonBarGroupClassicRenderer(RibbonBarGroup owner) : base(owner)
		{
		}

		// Token: 0x060043F7 RID: 17399 RVA: 0x000D4DDC File Offset: 0x000D2FDC
		public override void RenderBeginTag(HtmlTextWriter writer)
		{
			string text = base.Owner.Enabled ? string.Empty : "rrbDisabled";
			string value = RibbonBarStyles.Combine(new string[]
			{
				"rrbButtonGroup",
				base.Owner.CssClass,
				text
			});
			writer.AddAttribute(HtmlTextWriterAttribute.Class, value);
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
		}

		// Token: 0x060043F8 RID: 17400 RVA: 0x000D4E3C File Offset: 0x000D303C
		public override void RenderContents(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rrbButtonGroupIn");
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			this.ResolveItemsEnabledState();
			foreach (RibbonBarItem ribbonBarItem in base.Owner.Items)
			{
				ribbonBarItem.RenderControl(writer);
			}
			this.RenderTitle(writer);
			writer.RenderEndTag();
		}

		// Token: 0x060043F9 RID: 17401 RVA: 0x000D4EBC File Offset: 0x000D30BC
		private void ResolveItemsEnabledState()
		{
			foreach (RibbonBarItem ribbonBarItem in base.Owner.Items)
			{
				ribbonBarItem.Enabled = (base.Owner.Enabled && ribbonBarItem.Enabled);
			}
		}

		// Token: 0x060043FA RID: 17402 RVA: 0x000D4F2C File Offset: 0x000D312C
		private void RenderTitle(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rrbGroupTitle");
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			if (base.Owner.EnableLauncher)
			{
				string text = base.Owner.Enabled ? "" : "rrbDisabled";
				string value = RibbonBarStyles.Combine(new string[]
				{
					"rrbGroupLauncher",
					text
				});
				writer.AddAttribute(HtmlTextWriterAttribute.Class, value);
				writer.AddAttribute(HtmlTextWriterAttribute.Href, "#");
				writer.RenderBeginTag(HtmlTextWriterTag.A);
				writer.RenderEndTag();
			}
			if (!string.IsNullOrEmpty(base.Owner.Text))
			{
				writer.Write(base.Owner.Text);
			}
			writer.RenderEndTag();
		}
	}
}
