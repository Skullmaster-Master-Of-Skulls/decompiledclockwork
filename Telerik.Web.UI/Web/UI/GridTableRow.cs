using System;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02000387 RID: 903
	public class GridTableRow : TableRow
	{
		// Token: 0x17000A4C RID: 2636
		// (get) Token: 0x06001EF1 RID: 7921 RVA: 0x00061C04 File Offset: 0x0005FE04
		public string OriginalClientID
		{
			get
			{
				return base.ClientID;
			}
		}

		// Token: 0x17000A4D RID: 2637
		// (get) Token: 0x06001EF2 RID: 7922 RVA: 0x00061C0C File Offset: 0x0005FE0C
		public override string ClientID
		{
			get
			{
				return string.Format("{0}__{1}", this.Parent.Parent.ClientID, ((GridItem)this).ItemIndexHierarchical);
			}
		}

		// Token: 0x06001EF3 RID: 7923 RVA: 0x00061C34 File Offset: 0x0005FE34
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

		// Token: 0x06001EF4 RID: 7924 RVA: 0x00061D38 File Offset: 0x0005FF38
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
