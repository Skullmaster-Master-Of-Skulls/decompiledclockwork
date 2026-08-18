using System;
using System.ComponentModel;

namespace Telerik.Web.UI.Diagram
{
	// Token: 0x02000453 RID: 1107
	public class ShapeStroke : StateManager, IDefaultCheck
	{
		// Token: 0x17000CEE RID: 3310
		// (get) Token: 0x060027E8 RID: 10216 RVA: 0x00081956 File Offset: 0x0007FB56
		// (set) Token: 0x060027E9 RID: 10217 RVA: 0x00081976 File Offset: 0x0007FB76
		[DefaultValue("")]
		public string Color
		{
			get
			{
				return (string)(base.ViewState["Color"] ?? "");
			}
			set
			{
				base.ViewState["Color"] = value;
			}
		}

		// Token: 0x17000CEF RID: 3311
		// (get) Token: 0x060027EA RID: 10218 RVA: 0x00081989 File Offset: 0x0007FB89
		// (set) Token: 0x060027EB RID: 10219 RVA: 0x000819B2 File Offset: 0x0007FBB2
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

		// Token: 0x17000CF0 RID: 3312
		// (get) Token: 0x060027EC RID: 10220 RVA: 0x000819CA File Offset: 0x0007FBCA
		// (set) Token: 0x060027ED RID: 10221 RVA: 0x000819EB File Offset: 0x0007FBEB
		[DefaultValue(StrokeDashType.Solid)]
		public StrokeDashType DashType
		{
			get
			{
				return (StrokeDashType)(base.ViewState["DashType"] ?? StrokeDashType.Solid);
			}
			set
			{
				base.ViewState["DashType"] = value;
			}
		}

		// Token: 0x17000CF1 RID: 3313
		// (get) Token: 0x060027EE RID: 10222 RVA: 0x00081A03 File Offset: 0x0007FC03
		public bool IsDefault
		{
			get
			{
				return this.Color == "" && this.Width == 1.0 && this.DashType == StrokeDashType.Solid;
			}
		}
	}
}
