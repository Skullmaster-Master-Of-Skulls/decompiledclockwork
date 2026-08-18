using System;
using System.ComponentModel;

namespace Telerik.Web.UI.Diagram
{
	// Token: 0x02000451 RID: 1105
	public class ShapeRotation : StateManager, IDefaultCheck
	{
		// Token: 0x17000CEB RID: 3307
		// (get) Token: 0x060027E1 RID: 10209 RVA: 0x00081896 File Offset: 0x0007FA96
		// (set) Token: 0x060027E2 RID: 10210 RVA: 0x000818BF File Offset: 0x0007FABF
		[DefaultValue(0.0)]
		public double Angle
		{
			get
			{
				return (double)(base.ViewState["Angle"] ?? 0.0);
			}
			set
			{
				base.ViewState["Angle"] = value;
			}
		}

		// Token: 0x17000CEC RID: 3308
		// (get) Token: 0x060027E3 RID: 10211 RVA: 0x000818D7 File Offset: 0x0007FAD7
		public bool IsDefault
		{
			get
			{
				return this.Angle == 0.0;
			}
		}
	}
}
