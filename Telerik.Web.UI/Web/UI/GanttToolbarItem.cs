using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x0200004A RID: 74
	public class GanttToolbarItem : StateManager
	{
		// Token: 0x170000DC RID: 220
		// (get) Token: 0x06000252 RID: 594 RVA: 0x000066D0 File Offset: 0x000048D0
		// (set) Token: 0x06000253 RID: 595 RVA: 0x000066F0 File Offset: 0x000048F0
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

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x06000254 RID: 596 RVA: 0x00006703 File Offset: 0x00004903
		// (set) Token: 0x06000255 RID: 597 RVA: 0x00006723 File Offset: 0x00004923
		[Description("The template which renders the command. By default renders a button.")]
		[DefaultValue("")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
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

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x06000256 RID: 598 RVA: 0x00006736 File Offset: 0x00004936
		// (set) Token: 0x06000257 RID: 599 RVA: 0x00006756 File Offset: 0x00004956
		[DefaultValue("")]
		public string Text
		{
			get
			{
				return (string)(base.ViewState["Text"] ?? "");
			}
			set
			{
				base.ViewState["Text"] = value;
			}
		}
	}
}
