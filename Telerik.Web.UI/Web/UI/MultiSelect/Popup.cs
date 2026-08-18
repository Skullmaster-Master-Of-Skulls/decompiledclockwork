using System;
using System.ComponentModel;

namespace Telerik.Web.UI.MultiSelect
{
	// Token: 0x02000611 RID: 1553
	public class Popup : StateManager, IDefaultCheck
	{
		// Token: 0x17001288 RID: 4744
		// (get) Token: 0x06003871 RID: 14449 RVA: 0x000B9A82 File Offset: 0x000B7C82
		// (set) Token: 0x06003872 RID: 14450 RVA: 0x000B9AA2 File Offset: 0x000B7CA2
		[DefaultValue("")]
		public string AppendTo
		{
			get
			{
				return (string)(base.ViewState["AppendTo"] ?? "");
			}
			set
			{
				base.ViewState["AppendTo"] = value;
			}
		}

		// Token: 0x17001289 RID: 4745
		// (get) Token: 0x06003873 RID: 14451 RVA: 0x000B9AB5 File Offset: 0x000B7CB5
		// (set) Token: 0x06003874 RID: 14452 RVA: 0x000B9AD5 File Offset: 0x000B7CD5
		[DefaultValue("")]
		public string Origin
		{
			get
			{
				return (string)(base.ViewState["Origin"] ?? "");
			}
			set
			{
				base.ViewState["Origin"] = value;
			}
		}

		// Token: 0x1700128A RID: 4746
		// (get) Token: 0x06003875 RID: 14453 RVA: 0x000B9AE8 File Offset: 0x000B7CE8
		// (set) Token: 0x06003876 RID: 14454 RVA: 0x000B9B08 File Offset: 0x000B7D08
		[DefaultValue("")]
		public string Position
		{
			get
			{
				return (string)(base.ViewState["Position"] ?? "");
			}
			set
			{
				base.ViewState["Position"] = value;
			}
		}

		// Token: 0x1700128B RID: 4747
		// (get) Token: 0x06003877 RID: 14455 RVA: 0x000B9B1B File Offset: 0x000B7D1B
		public bool IsDefault
		{
			get
			{
				return this.AppendTo == "" && this.Origin == "" && this.Position == "";
			}
		}
	}
}
