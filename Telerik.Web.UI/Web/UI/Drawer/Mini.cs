using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI.Drawer
{
	// Token: 0x02000040 RID: 64
	public class Mini : StateManager, IDefaultCheck
	{
		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x0600020F RID: 527 RVA: 0x00005AED File Offset: 0x00003CED
		// (set) Token: 0x06000210 RID: 528 RVA: 0x00005B16 File Offset: 0x00003D16
		[DefaultValue(0.0)]
		public double Width
		{
			get
			{
				return (double)(base.ViewState["Width"] ?? 0.0);
			}
			set
			{
				base.ViewState["Width"] = value;
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x06000211 RID: 529 RVA: 0x00005B2E File Offset: 0x00003D2E
		// (set) Token: 0x06000212 RID: 530 RVA: 0x00005B4E File Offset: 0x00003D4E
		[Browsable(true)]
		[Bindable(true)]
		[DefaultValue("")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public string MiniTemplate
		{
			get
			{
				return (string)(base.ViewState["MiniTemplate"] ?? "");
			}
			set
			{
				base.ViewState["MiniTemplate"] = value;
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x06000213 RID: 531 RVA: 0x00005B61 File Offset: 0x00003D61
		public bool IsDefault
		{
			get
			{
				return this.Width == 0.0 && this.MiniTemplate == "";
			}
		}
	}
}
