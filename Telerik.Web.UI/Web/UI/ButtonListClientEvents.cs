using System;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x020000B0 RID: 176
	public class ButtonListClientEvents : StateManager
	{
		// Token: 0x17000265 RID: 613
		// (get) Token: 0x06000713 RID: 1811 RVA: 0x0001BE6A File Offset: 0x0001A06A
		// (set) Token: 0x06000714 RID: 1812 RVA: 0x0001BE8A File Offset: 0x0001A08A
		[ClientPropertyName("selectedIndexChanging")]
		[Category("Client-side events")]
		[Description("Gets or sets the name of the JavaScript function that will be called when different than the currently selected item is about to be checked.")]
		[ClientControlEvent]
		[DefaultValue("")]
		public string OnSelectedIndexChanging
		{
			get
			{
				return (string)(base.ViewState["OnSelectedIndexChanging"] ?? "");
			}
			set
			{
				base.ViewState["OnSelectedIndexChanging"] = value;
			}
		}

		// Token: 0x17000266 RID: 614
		// (get) Token: 0x06000715 RID: 1813 RVA: 0x0001BE9D File Offset: 0x0001A09D
		// (set) Token: 0x06000716 RID: 1814 RVA: 0x0001BEBD File Offset: 0x0001A0BD
		[ClientPropertyName("selectedIndexChanged")]
		[Category("Client-side events")]
		[Description("Gets or sets the name of the JavaScript function that will be called when different than the currently selected item is checked.")]
		[ClientControlEvent]
		[DefaultValue("")]
		public string OnSelectedIndexChanged
		{
			get
			{
				return (string)(base.ViewState["OnSelectedIndexChanged"] ?? "");
			}
			set
			{
				base.ViewState["OnSelectedIndexChanged"] = value;
			}
		}

		// Token: 0x17000267 RID: 615
		// (get) Token: 0x06000717 RID: 1815 RVA: 0x0001BED0 File Offset: 0x0001A0D0
		// (set) Token: 0x06000718 RID: 1816 RVA: 0x0001BEF0 File Offset: 0x0001A0F0
		[Description("Gets or sets the name of the JavaScript function that will be called when the control is loaded on the page.")]
		[Category("Client-side events")]
		[DefaultValue("")]
		[ClientControlEvent]
		[ClientPropertyName("load")]
		public string OnLoad
		{
			get
			{
				return (string)(base.ViewState["OnLoad"] ?? "");
			}
			set
			{
				base.ViewState["OnLoad"] = value;
			}
		}

		// Token: 0x17000268 RID: 616
		// (get) Token: 0x06000719 RID: 1817 RVA: 0x0001BF03 File Offset: 0x0001A103
		// (set) Token: 0x0600071A RID: 1818 RVA: 0x0001BF23 File Offset: 0x0001A123
		[Description("Gets or sets the name of the JavaScript function that will be called when an item is loaded on the page.")]
		[Category("Client-side events")]
		[DefaultValue("")]
		[ClientControlEvent]
		[ClientPropertyName("itemLoad")]
		public string OnItemLoad
		{
			get
			{
				return (string)(base.ViewState["OnItemLoad"] ?? "");
			}
			set
			{
				base.ViewState["OnItemLoad"] = value;
			}
		}

		// Token: 0x17000269 RID: 617
		// (get) Token: 0x0600071B RID: 1819 RVA: 0x0001BF36 File Offset: 0x0001A136
		// (set) Token: 0x0600071C RID: 1820 RVA: 0x0001BF56 File Offset: 0x0001A156
		[ClientPropertyName("itemCheckedChanging")]
		[Description("Gets or sets the name of the JavaScript function that will be called when an item checked state is about to be changed.")]
		[Category("Client-side events")]
		[DefaultValue("")]
		[ClientControlEvent]
		public string OnItemCheckedChanging
		{
			get
			{
				return (string)(base.ViewState["OnItemCheckedChanging"] ?? "");
			}
			set
			{
				base.ViewState["OnItemCheckedChanging"] = value;
			}
		}

		// Token: 0x1700026A RID: 618
		// (get) Token: 0x0600071D RID: 1821 RVA: 0x0001BF69 File Offset: 0x0001A169
		// (set) Token: 0x0600071E RID: 1822 RVA: 0x0001BF89 File Offset: 0x0001A189
		[DefaultValue("")]
		[ClientPropertyName("itemCheckedChanged")]
		[Category("Client-side events")]
		[ClientControlEvent]
		[Description("Gets or sets the name of the JavaScript function that will be called when an item checked state was changed.")]
		public string OnItemCheckedChanged
		{
			get
			{
				return (string)(base.ViewState["OnItemCheckedChanged"] ?? "");
			}
			set
			{
				base.ViewState["OnItemCheckedChanged"] = value;
			}
		}

		// Token: 0x1700026B RID: 619
		// (get) Token: 0x0600071F RID: 1823 RVA: 0x0001BF9C File Offset: 0x0001A19C
		// (set) Token: 0x06000720 RID: 1824 RVA: 0x0001BFBC File Offset: 0x0001A1BC
		[DefaultValue("")]
		[Description("Gets or sets the name of the JavaScript function that will be called when an item is about to be clicked.")]
		[ClientControlEvent]
		[ClientPropertyName("itemClicking")]
		[Category("Client-side events")]
		public string OnItemClicking
		{
			get
			{
				return (string)(base.ViewState["OnItemClicking"] ?? "");
			}
			set
			{
				base.ViewState["OnItemClicking"] = value;
			}
		}

		// Token: 0x1700026C RID: 620
		// (get) Token: 0x06000721 RID: 1825 RVA: 0x0001BFCF File Offset: 0x0001A1CF
		// (set) Token: 0x06000722 RID: 1826 RVA: 0x0001BFEF File Offset: 0x0001A1EF
		[ClientPropertyName("itemClicked")]
		[ClientControlEvent]
		[DefaultValue("")]
		[Category("Client-side events")]
		[Description("Gets or sets the name of the JavaScript function that will be called when an item was clicked.")]
		public string OnItemClicked
		{
			get
			{
				return (string)(base.ViewState["OnItemClicked"] ?? "");
			}
			set
			{
				base.ViewState["OnItemClicked"] = value;
			}
		}

		// Token: 0x1700026D RID: 621
		// (get) Token: 0x06000723 RID: 1827 RVA: 0x0001C002 File Offset: 0x0001A202
		// (set) Token: 0x06000724 RID: 1828 RVA: 0x0001C022 File Offset: 0x0001A222
		[ClientControlEvent]
		[ClientPropertyName("itemMouseOver")]
		[Category("Client-side events")]
		[Description("Gets or sets the name of the JavaScript function that will be called when the mouse hovers over an item.")]
		[DefaultValue("")]
		public string OnItemMouseOver
		{
			get
			{
				return (string)(base.ViewState["OnItemMouseOver"] ?? "");
			}
			set
			{
				base.ViewState["OnItemMouseOver"] = value;
			}
		}

		// Token: 0x1700026E RID: 622
		// (get) Token: 0x06000725 RID: 1829 RVA: 0x0001C035 File Offset: 0x0001A235
		// (set) Token: 0x06000726 RID: 1830 RVA: 0x0001C055 File Offset: 0x0001A255
		[ClientPropertyName("itemMouseOut")]
		[DefaultValue("")]
		[Category("Client-side events")]
		[Description("Gets or sets the name of the JavaScript function that will be called when the mouse leaves a radio button item.")]
		[ClientControlEvent]
		public string OnItemMouseOut
		{
			get
			{
				return (string)(base.ViewState["OnItemMouseOut"] ?? "");
			}
			set
			{
				base.ViewState["OnItemMouseOut"] = value;
			}
		}
	}
}
