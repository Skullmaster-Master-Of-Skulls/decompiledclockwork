using System;
using System.ComponentModel;

namespace Telerik.Web.UI.MultiColumnComboBox
{
	// Token: 0x020005F6 RID: 1526
	public class Virtual : StateManager, IDefaultCheck
	{
		// Token: 0x17001219 RID: 4633
		// (get) Token: 0x0600372C RID: 14124 RVA: 0x000B6B9A File Offset: 0x000B4D9A
		// (set) Token: 0x0600372D RID: 14125 RVA: 0x000B6BC3 File Offset: 0x000B4DC3
		[DefaultValue(0.0)]
		public double ItemHeight
		{
			get
			{
				return (double)(base.ViewState["ItemHeight"] ?? 0.0);
			}
			set
			{
				base.ViewState["ItemHeight"] = value;
			}
		}

		// Token: 0x1700121A RID: 4634
		// (get) Token: 0x0600372E RID: 14126 RVA: 0x000B6BDB File Offset: 0x000B4DDB
		// (set) Token: 0x0600372F RID: 14127 RVA: 0x000B6BFB File Offset: 0x000B4DFB
		[DefaultValue("index")]
		public string MapValueTo
		{
			get
			{
				return (string)(base.ViewState["MapValueTo"] ?? "index");
			}
			set
			{
				base.ViewState["MapValueTo"] = value;
			}
		}

		// Token: 0x1700121B RID: 4635
		// (get) Token: 0x06003730 RID: 14128 RVA: 0x000B6C0E File Offset: 0x000B4E0E
		// (set) Token: 0x06003731 RID: 14129 RVA: 0x000B6C2A File Offset: 0x000B4E2A
		[DefaultValue(null)]
		public string ValueMapper
		{
			get
			{
				return (string)(base.ViewState["ValueMapper"] ?? null);
			}
			set
			{
				base.ViewState["ValueMapper"] = value;
			}
		}

		// Token: 0x1700121C RID: 4636
		// (get) Token: 0x06003732 RID: 14130 RVA: 0x000B6C3D File Offset: 0x000B4E3D
		public bool IsDefault
		{
			get
			{
				return this.ItemHeight == 0.0 && this.MapValueTo == "index" && this.ValueMapper == null;
			}
		}
	}
}
