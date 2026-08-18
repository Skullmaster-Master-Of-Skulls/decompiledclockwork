using System;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x0200022E RID: 558
	public class DiagramConnectionEditableTool : StateManager
	{
		// Token: 0x170006EC RID: 1772
		// (get) Token: 0x0600149E RID: 5278 RVA: 0x00047676 File Offset: 0x00045876
		// (set) Token: 0x0600149F RID: 5279 RVA: 0x00047696 File Offset: 0x00045896
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
	}
}
