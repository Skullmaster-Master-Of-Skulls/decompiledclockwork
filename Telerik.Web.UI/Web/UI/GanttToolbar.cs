using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02000049 RID: 73
	public class GanttToolbar : StateManager
	{
		// Token: 0x170000DA RID: 218
		// (get) Token: 0x0600024E RID: 590 RVA: 0x0000667A File Offset: 0x0000487A
		// (set) Token: 0x0600024F RID: 591 RVA: 0x0000669A File Offset: 0x0000489A
		[Browsable(false)]
		[Description("Gets or sets the HTML template of the RadGantt Task tooltip.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[DefaultValue("")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
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

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x06000250 RID: 592 RVA: 0x000066AD File Offset: 0x000048AD
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public GanttToolbarItemsCollection Items
		{
			get
			{
				if (this._items == null)
				{
					this._items = new GanttToolbarItemsCollection();
				}
				return this._items;
			}
		}

		// Token: 0x04000051 RID: 81
		private GanttToolbarItemsCollection _items;
	}
}
