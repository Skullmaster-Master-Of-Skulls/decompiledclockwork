using System;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x02000F4D RID: 3917
	public class SliderItemBinding : StateManager
	{
		// Token: 0x17002F51 RID: 12113
		// (get) Token: 0x0600957F RID: 38271 RVA: 0x002167E4 File Offset: 0x002149E4
		// (set) Token: 0x06009580 RID: 38272 RVA: 0x00216804 File Offset: 0x00214A04
		[DefaultValue("")]
		[Category("Data")]
		[Description("Gets/Sets the field of the data source that provides the value content (Value property of the Slider item) of the Slider items.")]
		public string ValueField
		{
			get
			{
				return (string)(base.ViewState["ValueField"] ?? string.Empty);
			}
			set
			{
				base.ViewState["ValueField"] = value;
			}
		}

		// Token: 0x17002F52 RID: 12114
		// (get) Token: 0x06009581 RID: 38273 RVA: 0x00216817 File Offset: 0x00214A17
		// (set) Token: 0x06009582 RID: 38274 RVA: 0x00216837 File Offset: 0x00214A37
		[Description("Gets/Sets the field of the data source that provides the ToolTip content (ToolTip property of the Slider item) of the Slider items")]
		[DefaultValue("")]
		[Category("Data")]
		public string ToolTipField
		{
			get
			{
				return (string)(base.ViewState["ToolTipField"] ?? string.Empty);
			}
			set
			{
				base.ViewState["ToolTipField"] = value;
			}
		}

		// Token: 0x17002F53 RID: 12115
		// (get) Token: 0x06009583 RID: 38275 RVA: 0x0021684A File Offset: 0x00214A4A
		// (set) Token: 0x06009584 RID: 38276 RVA: 0x0021686A File Offset: 0x00214A6A
		[DefaultValue("")]
		[Category("Data")]
		[Description("Gets/Sets the field of the data source that provides the Text content (Text property of the Slider item) of the Slider items.")]
		public string TextField
		{
			get
			{
				return (string)(base.ViewState["TextField"] ?? string.Empty);
			}
			set
			{
				base.ViewState["TextField"] = value;
			}
		}
	}
}
