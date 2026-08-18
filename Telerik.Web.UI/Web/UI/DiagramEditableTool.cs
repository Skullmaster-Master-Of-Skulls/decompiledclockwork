using System;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x02000237 RID: 567
	public class DiagramEditableTool : StateManager
	{
		// Token: 0x17000700 RID: 1792
		// (get) Token: 0x060014CB RID: 5323 RVA: 0x00047D82 File Offset: 0x00045F82
		// (set) Token: 0x060014CC RID: 5324 RVA: 0x00047DA2 File Offset: 0x00045FA2
		[DefaultValue("")]
		public string Name
		{
			get
			{
				return (string)(base.ViewState["Name"] ?? "");
			}
			set
			{
				base.ViewState["Name"] = value;
			}
		}

		// Token: 0x17000701 RID: 1793
		// (get) Token: 0x060014CD RID: 5325 RVA: 0x00047DB5 File Offset: 0x00045FB5
		// (set) Token: 0x060014CE RID: 5326 RVA: 0x00047DDE File Offset: 0x00045FDE
		[DefaultValue(90.0)]
		public double Step
		{
			get
			{
				return (double)(base.ViewState["Step"] ?? 90.0);
			}
			set
			{
				base.ViewState["Step"] = value;
			}
		}
	}
}
