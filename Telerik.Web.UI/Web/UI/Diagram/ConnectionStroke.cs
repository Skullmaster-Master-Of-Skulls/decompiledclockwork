using System;
using System.ComponentModel;

namespace Telerik.Web.UI.Diagram
{
	// Token: 0x0200021E RID: 542
	public class ConnectionStroke : StateManager, IDefaultCheck
	{
		// Token: 0x17000689 RID: 1673
		// (get) Token: 0x060013D4 RID: 5076 RVA: 0x0004590A File Offset: 0x00043B0A
		// (set) Token: 0x060013D5 RID: 5077 RVA: 0x0004592A File Offset: 0x00043B2A
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

		// Token: 0x1700068A RID: 1674
		// (get) Token: 0x060013D6 RID: 5078 RVA: 0x0004593D File Offset: 0x00043B3D
		// (set) Token: 0x060013D7 RID: 5079 RVA: 0x00045966 File Offset: 0x00043B66
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

		// Token: 0x1700068B RID: 1675
		// (get) Token: 0x060013D8 RID: 5080 RVA: 0x0004597E File Offset: 0x00043B7E
		// (set) Token: 0x060013D9 RID: 5081 RVA: 0x0004599F File Offset: 0x00043B9F
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

		// Token: 0x1700068C RID: 1676
		// (get) Token: 0x060013DA RID: 5082 RVA: 0x000459B7 File Offset: 0x00043BB7
		public bool IsDefault
		{
			get
			{
				return this.Color == "" && this.Width == 1.0 && this.DashType == StrokeDashType.Solid;
			}
		}
	}
}
