using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02000587 RID: 1415
	public class ListViewDataGroup : StateManager, INamingContainer
	{
		// Token: 0x1700108C RID: 4236
		// (get) Token: 0x0600330F RID: 13071 RVA: 0x000AA360 File Offset: 0x000A8560
		// (set) Token: 0x06003310 RID: 13072 RVA: 0x000AA368 File Offset: 0x000A8568
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[TemplateContainer(typeof(RadListViewDataGroupItem), BindingDirection.OneWay)]
		[DefaultValue(null)]
		[Browsable(false)]
		[Description("Gets or sets the custom content for the data group item in a RadListView control")]
		public virtual ITemplate DataGroupTemplate { get; set; }

		// Token: 0x1700108D RID: 4237
		// (get) Token: 0x06003311 RID: 13073 RVA: 0x000AA374 File Offset: 0x000A8574
		// (set) Token: 0x06003312 RID: 13074 RVA: 0x000AA3A1 File Offset: 0x000A85A1
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		[Description("GroupField")]
		[Category("Data")]
		public virtual string GroupField
		{
			get
			{
				object obj = base.ViewState["GroupField"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["GroupField"] = value;
			}
		}

		// Token: 0x1700108E RID: 4238
		// (get) Token: 0x06003313 RID: 13075 RVA: 0x000AA3B4 File Offset: 0x000A85B4
		// (set) Token: 0x06003314 RID: 13076 RVA: 0x000AA3EE File Offset: 0x000A85EE
		[DefaultValue("dataGroupPlaceholder")]
		[Category("Behavior")]
		[Description("Gets or sets the ID for the data group placeholder in a RadListView control. ")]
		public virtual string DataGroupPlaceholderID
		{
			get
			{
				object obj = base.ViewState["DataGroupPlaceholderID"];
				if (obj == null || string.IsNullOrEmpty((string)obj))
				{
					obj = "dataGroupPlaceholder";
				}
				return (string)obj;
			}
			set
			{
				base.ViewState["DataGroupPlaceholderID"] = value;
			}
		}

		// Token: 0x1700108F RID: 4239
		// (get) Token: 0x06003315 RID: 13077 RVA: 0x000AA404 File Offset: 0x000A8604
		// (set) Token: 0x06003316 RID: 13078 RVA: 0x000AA432 File Offset: 0x000A8632
		[DefaultValue(typeof(RadListViewSortOrder), "None")]
		[NotifyParentProperty(true)]
		public RadListViewSortOrder SortOrder
		{
			get
			{
				object obj = base.ViewState["SortOrder"] ?? RadListViewSortOrder.None;
				return (RadListViewSortOrder)obj;
			}
			set
			{
				base.ViewState["SortOrder"] = value;
			}
		}

		// Token: 0x17001090 RID: 4240
		// (get) Token: 0x06003317 RID: 13079 RVA: 0x000AA44A File Offset: 0x000A864A
		[Category("Default")]
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[NotifyParentProperty(true)]
		public List<ListViewDataGroupAggregate> GroupAggregates
		{
			get
			{
				if (this._groupAggregates == null)
				{
					this._groupAggregates = new List<ListViewDataGroupAggregate>();
				}
				return this._groupAggregates;
			}
		}

		// Token: 0x04000E05 RID: 3589
		private List<ListViewDataGroupAggregate> _groupAggregates;
	}
}
