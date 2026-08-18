using System;
using System.Web.UI;

namespace Telerik.Web.UI.ComboBox
{
	// Token: 0x02000A17 RID: 2583
	public class NativeRenderer : ComboRendererBase
	{
		// Token: 0x060061EC RID: 25068 RVA: 0x00171CA2 File Offset: 0x0016FEA2
		public NativeRenderer(RadComboBox owner) : base(owner)
		{
		}

		// Token: 0x060061ED RID: 25069 RVA: 0x00171CAC File Offset: 0x0016FEAC
		public override void RenderContents(HtmlTextWriter writer)
		{
			base.RenderContents(writer);
			if (!base.Owner.IsControlEnabled)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Disabled, "disabled");
			}
			writer.AddStyleAttribute(HtmlTextWriterStyle.Width, "100%");
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "radPreventDecorate");
			if (!string.IsNullOrEmpty(base.Owner.ToolTip))
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Title, base.Owner.ToolTip);
			}
			if (base.Owner.TabIndex != 0)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Tabindex, base.Owner.TabIndex.ToString());
			}
			writer.RenderBeginTag(HtmlTextWriterTag.Select);
			this.RenderItems(writer);
			writer.RenderEndTag();
		}
	}
}
