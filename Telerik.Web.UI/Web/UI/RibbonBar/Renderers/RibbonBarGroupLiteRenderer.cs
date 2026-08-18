using System;
using System.Web.UI;

namespace Telerik.Web.UI.RibbonBar.Renderers
{
	// Token: 0x02000791 RID: 1937
	internal class RibbonBarGroupLiteRenderer : RibbonBarGroupRenderer
	{
		// Token: 0x06004407 RID: 17415 RVA: 0x000D5309 File Offset: 0x000D3509
		public RibbonBarGroupLiteRenderer(RibbonBarGroup owner) : base(owner)
		{
		}

		// Token: 0x06004408 RID: 17416 RVA: 0x000D5314 File Offset: 0x000D3514
		public override void RenderBeginTag(HtmlTextWriter writer)
		{
			string text = base.Owner.Enabled ? string.Empty : "rrbDisabled";
			string value = RibbonBarStyles.Combine(new string[]
			{
				"rrbCommandGroup",
				base.Owner.CssClass,
				text
			});
			writer.AddAttribute(HtmlTextWriterAttribute.Class, value);
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
		}

		// Token: 0x06004409 RID: 17417 RVA: 0x000D5374 File Offset: 0x000D3574
		public override void RenderContents(HtmlTextWriter writer)
		{
			this.ResolveItemsEnabledState();
			foreach (RibbonBarItem ribbonBarItem in base.Owner.Items)
			{
				ribbonBarItem.RenderControl(writer);
			}
			this.RenderTitle(writer);
		}

		// Token: 0x0600440A RID: 17418 RVA: 0x000D53DC File Offset: 0x000D35DC
		private void ResolveItemsEnabledState()
		{
			foreach (RibbonBarItem ribbonBarItem in base.Owner.Items)
			{
				ribbonBarItem.Enabled = (base.Owner.Enabled && ribbonBarItem.Enabled);
			}
		}

		// Token: 0x0600440B RID: 17419 RVA: 0x000D544C File Offset: 0x000D364C
		private void RenderTitle(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rrbTitle");
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			if (base.Owner.EnableLauncher)
			{
				string arg = base.Owner.Enabled ? "" : "rrbDisabled";
				string value = string.Format("{0} {1}", "rrbGroupLauncher", arg).Trim();
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
