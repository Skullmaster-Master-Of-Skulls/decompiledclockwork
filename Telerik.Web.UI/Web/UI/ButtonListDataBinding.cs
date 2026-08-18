using System;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x020000B1 RID: 177
	public class ButtonListDataBinding : StateManager
	{
		// Token: 0x1700026F RID: 623
		// (get) Token: 0x06000728 RID: 1832 RVA: 0x0001C070 File Offset: 0x0001A270
		// (set) Token: 0x06000729 RID: 1833 RVA: 0x0001C090 File Offset: 0x0001A290
		[Description("Gets or sets the field of the data source that provides the text content of the list items.")]
		[Category("Data")]
		[DefaultValue("")]
		public string DataTextField
		{
			get
			{
				return (string)(base.ViewState["DataTextField"] ?? string.Empty);
			}
			set
			{
				base.ViewState["DataTextField"] = value;
			}
		}

		// Token: 0x17000270 RID: 624
		// (get) Token: 0x0600072A RID: 1834 RVA: 0x0001C0A3 File Offset: 0x0001A2A3
		// (set) Token: 0x0600072B RID: 1835 RVA: 0x0001C0C3 File Offset: 0x0001A2C3
		[DefaultValue("")]
		[Description("Gets or sets the field of the data source that provides the value of each list item.")]
		[Category("Data")]
		public string DataValueField
		{
			get
			{
				return (string)(base.ViewState["DataValueField"] ?? string.Empty);
			}
			set
			{
				base.ViewState["DataValueField"] = value;
			}
		}

		// Token: 0x17000271 RID: 625
		// (get) Token: 0x0600072C RID: 1836 RVA: 0x0001C0D6 File Offset: 0x0001A2D6
		// (set) Token: 0x0600072D RID: 1837 RVA: 0x0001C0F6 File Offset: 0x0001A2F6
		[DefaultValue("")]
		[Description("Gets or sets the field of the data source that provides the selected state of each list item.")]
		[Category("Data")]
		public string DataSelectedField
		{
			get
			{
				return (string)(base.ViewState["DataSelectedField"] ?? string.Empty);
			}
			set
			{
				base.ViewState["DataSelectedField"] = value;
			}
		}

		// Token: 0x17000272 RID: 626
		// (get) Token: 0x0600072E RID: 1838 RVA: 0x0001C109 File Offset: 0x0001A309
		// (set) Token: 0x0600072F RID: 1839 RVA: 0x0001C129 File Offset: 0x0001A329
		[Description("Gets or sets the field of the data source that provides the enabled state of each list item.")]
		[DefaultValue("")]
		[Category("Data")]
		public string DataEnabledField
		{
			get
			{
				return (string)(base.ViewState["DataEnabledField"] ?? string.Empty);
			}
			set
			{
				base.ViewState["DataEnabledField"] = value;
			}
		}

		// Token: 0x17000273 RID: 627
		// (get) Token: 0x06000730 RID: 1840 RVA: 0x0001C13C File Offset: 0x0001A33C
		// (set) Token: 0x06000731 RID: 1841 RVA: 0x0001C15C File Offset: 0x0001A35C
		[DefaultValue("")]
		[Description("Gets or sets the field of the data source that provides the tooltip text of each list item.")]
		[Category("Data")]
		public string DataToolTipField
		{
			get
			{
				return (string)(base.ViewState["DataToolTipField"] ?? string.Empty);
			}
			set
			{
				base.ViewState["DataToolTipField"] = value;
			}
		}
	}
}
