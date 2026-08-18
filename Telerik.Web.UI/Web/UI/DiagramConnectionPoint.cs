using System;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x02000231 RID: 561
	public class DiagramConnectionPoint : StateManager
	{
		// Token: 0x170006EF RID: 1775
		// (get) Token: 0x060014A6 RID: 5286 RVA: 0x0004771A File Offset: 0x0004591A
		// (set) Token: 0x060014A7 RID: 5287 RVA: 0x00047743 File Offset: 0x00045943
		[DefaultValue(0.0)]
		public double X
		{
			get
			{
				return (double)(base.ViewState["X"] ?? 0.0);
			}
			set
			{
				base.ViewState["X"] = value;
			}
		}

		// Token: 0x170006F0 RID: 1776
		// (get) Token: 0x060014A8 RID: 5288 RVA: 0x0004775B File Offset: 0x0004595B
		// (set) Token: 0x060014A9 RID: 5289 RVA: 0x00047784 File Offset: 0x00045984
		[DefaultValue(0.0)]
		public double Y
		{
			get
			{
				return (double)(base.ViewState["Y"] ?? 0.0);
			}
			set
			{
				base.ViewState["Y"] = value;
			}
		}
	}
}
