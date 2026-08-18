using System;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x02000248 RID: 584
	public class DiagramShapeEditableTool : StateManager
	{
		// Token: 0x1700074E RID: 1870
		// (get) Token: 0x06001574 RID: 5492 RVA: 0x00049A02 File Offset: 0x00047C02
		// (set) Token: 0x06001575 RID: 5493 RVA: 0x00049A22 File Offset: 0x00047C22
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

		// Token: 0x1700074F RID: 1871
		// (get) Token: 0x06001576 RID: 5494 RVA: 0x00049A35 File Offset: 0x00047C35
		// (set) Token: 0x06001577 RID: 5495 RVA: 0x00049A5E File Offset: 0x00047C5E
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
