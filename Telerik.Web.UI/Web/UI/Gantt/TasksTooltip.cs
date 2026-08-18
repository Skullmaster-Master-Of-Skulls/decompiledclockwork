using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI.Gantt
{
	// Token: 0x0200004B RID: 75
	public class TasksTooltip : StateManager, IDefaultCheck
	{
		// Token: 0x170000DF RID: 223
		// (get) Token: 0x06000259 RID: 601 RVA: 0x00006771 File Offset: 0x00004971
		// (set) Token: 0x0600025A RID: 602 RVA: 0x00006791 File Offset: 0x00004991
		[ClientPropertyName("template")]
		[ClientControlProperty]
		[DefaultValue("")]
		[Description("Gets or sets the HTML template of the RadGantt Task tooltip.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Browsable(false)]
		public string ClientTemplate
		{
			get
			{
				return (string)(base.ViewState["ClientTemplate"] ?? "");
			}
			set
			{
				base.ViewState["ClientTemplate"] = value;
			}
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x0600025B RID: 603 RVA: 0x000067A4 File Offset: 0x000049A4
		// (set) Token: 0x0600025C RID: 604 RVA: 0x000067C5 File Offset: 0x000049C5
		[DefaultValue(true)]
		public bool Visible
		{
			get
			{
				return (bool)(base.ViewState["Visible"] ?? true);
			}
			set
			{
				base.ViewState["Visible"] = value;
			}
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x0600025D RID: 605 RVA: 0x000067DD File Offset: 0x000049DD
		public bool IsDefault
		{
			get
			{
				return this.ClientTemplate == "" && this.Visible;
			}
		}
	}
}
