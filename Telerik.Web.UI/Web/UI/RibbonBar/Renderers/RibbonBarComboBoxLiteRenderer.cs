using System;
using System.Web.UI;

namespace Telerik.Web.UI.RibbonBar.Renderers
{
	// Token: 0x020007B2 RID: 1970
	internal class RibbonBarComboBoxLiteRenderer : RibbonBarDropDownLiteRenderer
	{
		// Token: 0x060044C9 RID: 17609 RVA: 0x000D9320 File Offset: 0x000D7520
		public RibbonBarComboBoxLiteRenderer(RibbonBarItem owner) : base(owner)
		{
		}

		// Token: 0x060044CA RID: 17610 RVA: 0x000D932C File Offset: 0x000D752C
		protected override void RenderInput(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, ((RibbonBarComboBox)base.Owner).InputCssClass);
			writer.AddAttribute(HtmlTextWriterAttribute.Type, "text");
			if (!base.Owner.Enabled)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.ReadOnly, "readonly");
			}
			if (!string.IsNullOrEmpty(((RibbonBarComboBox)base.Owner).Text))
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Value, ((RibbonBarComboBox)base.Owner).Text);
			}
			writer.AddAttribute(HtmlTextWriterAttribute.Name, base.Owner.RibbonBar.ClientID + ":" + base.Owner.RibbonBar.GetItemHierarchicalIndex(base.Owner));
			writer.RenderBeginTag(HtmlTextWriterTag.Input);
			writer.RenderEndTag();
		}
	}
}
