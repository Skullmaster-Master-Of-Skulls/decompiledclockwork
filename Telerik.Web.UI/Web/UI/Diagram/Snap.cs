using System;
using System.ComponentModel;

namespace Telerik.Web.UI.Diagram
{
	// Token: 0x02000267 RID: 615
	public class Snap : StateManager, IDefaultCheck
	{
		// Token: 0x170007A1 RID: 1953
		// (get) Token: 0x06001648 RID: 5704 RVA: 0x0004BD02 File Offset: 0x00049F02
		// (set) Token: 0x06001649 RID: 5705 RVA: 0x0004BD2B File Offset: 0x00049F2B
		[DefaultValue(10.0)]
		public double Size
		{
			get
			{
				return (double)(base.ViewState["Size"] ?? 10.0);
			}
			set
			{
				base.ViewState["Size"] = value;
			}
		}

		// Token: 0x170007A2 RID: 1954
		// (get) Token: 0x0600164A RID: 5706 RVA: 0x0004BD43 File Offset: 0x00049F43
		public bool IsDefault
		{
			get
			{
				return this.Size == 10.0;
			}
		}
	}
}
