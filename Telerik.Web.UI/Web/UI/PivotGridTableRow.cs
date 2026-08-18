using System;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02000769 RID: 1897
	public class PivotGridTableRow : TableRow
	{
		// Token: 0x060042DF RID: 17119 RVA: 0x000D0A84 File Offset: 0x000CEC84
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			this.AddStyleAttributes(writer);
			if (this.AccessKey.Length > 0)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Accesskey, this.AccessKey);
			}
			if (!this.Enabled)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Disabled, "disabled");
			}
			if (this.TabIndex != 0)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Tabindex, this.TabIndex.ToString(NumberFormatInfo.InvariantInfo));
			}
			if (this.ToolTip.Length > 0)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Title, this.ToolTip);
			}
			if (base.ControlStyleCreated && !base.ControlStyle.IsEmpty)
			{
				base.ControlStyle.AddAttributesToRender(writer, this);
			}
			foreach (object obj in base.Attributes.Keys)
			{
				string text = (string)obj;
				writer.AddAttribute(text, base.Attributes[text]);
			}
		}

		// Token: 0x060042E0 RID: 17120 RVA: 0x000D0B88 File Offset: 0x000CED88
		private void AddStyleAttributes(HtmlTextWriter writer)
		{
			if (base.ControlStyle is TableItemStyle && (base.ControlStyle as TableItemStyle).HorizontalAlign != HorizontalAlign.NotSet)
			{
				base.Style["text-align"] = (base.ControlStyle as TableItemStyle).HorizontalAlign.ToString().ToLower();
				(base.ControlStyle as TableItemStyle).HorizontalAlign = HorizontalAlign.NotSet;
			}
		}
	}
}
