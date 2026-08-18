using System;
using System.ComponentModel;

namespace Telerik.Web.UI.Diagram
{
	// Token: 0x020002B3 RID: 691
	public class Stroke : StateManager, IDefaultCheck
	{
		// Token: 0x1700084B RID: 2123
		// (get) Token: 0x0600184B RID: 6219 RVA: 0x0005035E File Offset: 0x0004E55E
		// (set) Token: 0x0600184C RID: 6220 RVA: 0x0005037E File Offset: 0x0004E57E
		[DefaultValue("Black")]
		public string Color
		{
			get
			{
				return (string)(base.ViewState["Color"] ?? "Black");
			}
			set
			{
				base.ViewState["Color"] = value;
			}
		}

		// Token: 0x1700084C RID: 2124
		// (get) Token: 0x0600184D RID: 6221 RVA: 0x00050391 File Offset: 0x0004E591
		// (set) Token: 0x0600184E RID: 6222 RVA: 0x000503B1 File Offset: 0x0004E5B1
		[DefaultValue("")]
		public string DashType
		{
			get
			{
				return (string)(base.ViewState["DashType"] ?? "");
			}
			set
			{
				base.ViewState["DashType"] = value;
			}
		}

		// Token: 0x1700084D RID: 2125
		// (get) Token: 0x0600184F RID: 6223 RVA: 0x000503C4 File Offset: 0x0004E5C4
		// (set) Token: 0x06001850 RID: 6224 RVA: 0x000503ED File Offset: 0x0004E5ED
		[DefaultValue(1.0)]
		public double Width
		{
			get
			{
				return (double)(base.ViewState["Width"] ?? 1.0);
			}
			set
			{
				base.ViewState["Width"] = value;
			}
		}

		// Token: 0x1700084E RID: 2126
		// (get) Token: 0x06001851 RID: 6225 RVA: 0x00050405 File Offset: 0x0004E605
		public bool IsDefault
		{
			get
			{
				return this.Color == "Black" && this.DashType == "" && this.Width == 1.0;
			}
		}
	}
}
