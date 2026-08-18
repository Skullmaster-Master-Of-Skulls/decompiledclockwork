using System;
using System.ComponentModel;

namespace Telerik.Web.UI.MultiColumnComboBox
{
	// Token: 0x02000066 RID: 102
	public class Messages : StateManager, IDefaultCheck
	{
		// Token: 0x1700019A RID: 410
		// (get) Token: 0x0600044C RID: 1100 RVA: 0x0000B3F1 File Offset: 0x000095F1
		// (set) Token: 0x0600044D RID: 1101 RVA: 0x0000B411 File Offset: 0x00009611
		[DefaultValue("clear")]
		public string Clear
		{
			get
			{
				return (string)(base.ViewState["Clear"] ?? "clear");
			}
			set
			{
				base.ViewState["Clear"] = value;
			}
		}

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x0600044E RID: 1102 RVA: 0x0000B424 File Offset: 0x00009624
		// (set) Token: 0x0600044F RID: 1103 RVA: 0x0000B444 File Offset: 0x00009644
		[DefaultValue("No data found.")]
		public string NoData
		{
			get
			{
				return (string)(base.ViewState["NoData"] ?? "No data found.");
			}
			set
			{
				base.ViewState["NoData"] = value;
			}
		}

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x06000450 RID: 1104 RVA: 0x0000B457 File Offset: 0x00009657
		public bool IsDefault
		{
			get
			{
				return this.Clear == "clear" && this.NoData == "No data found.";
			}
		}
	}
}
