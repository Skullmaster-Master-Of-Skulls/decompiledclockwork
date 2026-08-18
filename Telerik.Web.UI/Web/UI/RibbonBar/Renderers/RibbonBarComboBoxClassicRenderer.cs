using System;
using System.Web.UI;

namespace Telerik.Web.UI.RibbonBar.Renderers
{
	// Token: 0x020007A4 RID: 1956
	internal class RibbonBarComboBoxClassicRenderer : RibbonBarDropDownClassicRenderer
	{
		// Token: 0x0600447C RID: 17532 RVA: 0x000D79DC File Offset: 0x000D5BDC
		public RibbonBarComboBoxClassicRenderer(RibbonBarItem owner) : base(owner)
		{
		}

		// Token: 0x0600447D RID: 17533 RVA: 0x000D79E8 File Offset: 0x000D5BE8
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
