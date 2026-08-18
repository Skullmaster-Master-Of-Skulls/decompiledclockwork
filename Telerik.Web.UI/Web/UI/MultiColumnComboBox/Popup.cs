using System;
using System.ComponentModel;

namespace Telerik.Web.UI.MultiColumnComboBox
{
	// Token: 0x020005F2 RID: 1522
	public class Popup : StateManager, IDefaultCheck
	{
		// Token: 0x1700120F RID: 4623
		// (get) Token: 0x06003715 RID: 14101 RVA: 0x000B645E File Offset: 0x000B465E
		// (set) Token: 0x06003716 RID: 14102 RVA: 0x000B647E File Offset: 0x000B467E
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

		// Token: 0x17001210 RID: 4624
		// (get) Token: 0x06003717 RID: 14103 RVA: 0x000B6491 File Offset: 0x000B4691
		// (set) Token: 0x06003718 RID: 14104 RVA: 0x000B64B1 File Offset: 0x000B46B1
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

		// Token: 0x17001211 RID: 4625
		// (get) Token: 0x06003719 RID: 14105 RVA: 0x000B64C4 File Offset: 0x000B46C4
		// (set) Token: 0x0600371A RID: 14106 RVA: 0x000B64E4 File Offset: 0x000B46E4
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

		// Token: 0x17001212 RID: 4626
		// (get) Token: 0x0600371B RID: 14107 RVA: 0x000B64F7 File Offset: 0x000B46F7
		public bool IsDefault
		{
			get
			{
				return this.AppendTo == "" && this.Origin == "" && this.Position == "";
			}
		}
	}
}
