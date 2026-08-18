using System;
using System.Collections;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02000BBB RID: 3003
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class RadListViewClientDataBinding : StateManager
	{
		// Token: 0x17002571 RID: 9585
		// (get) Token: 0x06007314 RID: 29460 RVA: 0x001AF563 File Offset: 0x001AD763
		// (set) Token: 0x06007315 RID: 29461 RVA: 0x001AF583 File Offset: 0x001AD783
		[DefaultValue("")]
		[Category("Client")]
		[NotifyParentProperty(true)]
		[Description("Gets or sets the ID of an HTML element in which RadListView will be rendered.")]
		public virtual string ContainerID
		{
			get
			{
				return (base.ViewState["ContainerID"] as string) ?? string.Empty;
			}
			set
			{
				base.ViewState["ContainerID"] = value;
			}
		}

		// Token: 0x17002572 RID: 9586
		// (get) Token: 0x06007316 RID: 29462 RVA: 0x001AF596 File Offset: 0x001AD796
		// (set) Token: 0x06007317 RID: 29463 RVA: 0x001AF5B6 File Offset: 0x001AD7B6
		[NotifyParentProperty(true)]
		[Category("Client")]
		[DefaultValue("")]
		[Description("Gets or sets the ID of an HTML element in the LayoutTemplate that will contain the data items in RadListView when using client-side databinding.")]
		public virtual string ItemPlaceHolderID
		{
			get
			{
				return (base.ViewState["ItemPlaceHolderID"] as string) ?? string.Empty;
			}
			set
			{
				base.ViewState["ItemPlaceHolderID"] = value;
			}
		}

		// Token: 0x17002573 RID: 9587
		// (get) Token: 0x06007318 RID: 29464 RVA: 0x001AF5C9 File Offset: 0x001AD7C9
		// (set) Token: 0x06007319 RID: 29465 RVA: 0x001AF5E9 File Offset: 0x001AD7E9
		[Category("Client")]
		[Browsable(false)]
		[DefaultValue("")]
		[Description("Gets or sets the HTML template of the RadListView container when using client-side databinding.")]
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public virtual string LayoutTemplate
		{
			get
			{
				return (base.ViewState["LayoutTemplate"] as string) ?? string.Empty;
			}
			set
			{
				base.ViewState["LayoutTemplate"] = value;
			}
		}

		// Token: 0x17002574 RID: 9588
		// (get) Token: 0x0600731A RID: 29466 RVA: 0x001AF5FC File Offset: 0x001AD7FC
		// (set) Token: 0x0600731B RID: 29467 RVA: 0x001AF61C File Offset: 0x001AD81C
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Description("Gets or sets the HTML template of a RadListView item when using client-side databinding.")]
		[NotifyParentProperty(true)]
		[Browsable(false)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Category("Client")]
		[DefaultValue("")]
		public virtual string ItemTemplate
		{
			get
			{
				return (base.ViewState["ItemTemplate"] as string) ?? string.Empty;
			}
			set
			{
				base.ViewState["ItemTemplate"] = value;
			}
		}

		// Token: 0x17002575 RID: 9589
		// (get) Token: 0x0600731C RID: 29468 RVA: 0x001AF62F File Offset: 0x001AD82F
		// (set) Token: 0x0600731D RID: 29469 RVA: 0x001AF64F File Offset: 0x001AD84F
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Browsable(false)]
		[DefaultValue("")]
		[Description("Gets or sets the HTML template of a RadListView alternating item when using client-side databinding.")]
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Category("Client")]
		public virtual string AlternatingItemTemplate
		{
			get
			{
				return (base.ViewState["AlternatingItemTemplate"] as string) ?? string.Empty;
			}
			set
			{
				base.ViewState["AlternatingItemTemplate"] = value;
			}
		}

		// Token: 0x17002576 RID: 9590
		// (get) Token: 0x0600731E RID: 29470 RVA: 0x001AF662 File Offset: 0x001AD862
		// (set) Token: 0x0600731F RID: 29471 RVA: 0x001AF682 File Offset: 0x001AD882
		[DefaultValue("")]
		[Browsable(false)]
		[Category("Client")]
		[Description("Gets or sets the HTML template of the RadListView empty item when using client-side databinding.")]
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public virtual string EmptyDataTemplate
		{
			get
			{
				return (base.ViewState["EmptyDataTemplate"] as string) ?? string.Empty;
			}
			set
			{
				base.ViewState["EmptyDataTemplate"] = value;
			}
		}

		// Token: 0x17002577 RID: 9591
		// (get) Token: 0x06007320 RID: 29472 RVA: 0x001AF695 File Offset: 0x001AD895
		// (set) Token: 0x06007321 RID: 29473 RVA: 0x001AF6B5 File Offset: 0x001AD8B5
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Category("Client")]
		[DefaultValue("")]
		[Description("Gets or sets the HTML template of the RadListView separator item that is rendered between two bound items when using client-side databinding.")]
		[NotifyParentProperty(true)]
		public virtual string ItemSeparatorTemplate
		{
			get
			{
				return (base.ViewState["ItemSeparatorTemplate"] as string) ?? string.Empty;
			}
			set
			{
				base.ViewState["ItemSeparatorTemplate"] = value;
			}
		}

		// Token: 0x17002578 RID: 9592
		// (get) Token: 0x06007322 RID: 29474 RVA: 0x001AF6C8 File Offset: 0x001AD8C8
		// (set) Token: 0x06007323 RID: 29475 RVA: 0x001AF6E8 File Offset: 0x001AD8E8
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Browsable(false)]
		[DefaultValue("")]
		[Description("Gets or sets the HTML template of a selected RadListView item when using client-side databinding.")]
		[Category("Client")]
		public virtual string SelectedItemTemplate
		{
			get
			{
				return (base.ViewState["SelectedItemTemplate"] as string) ?? string.Empty;
			}
			set
			{
				base.ViewState["SelectedItemTemplate"] = value;
			}
		}

		// Token: 0x17002579 RID: 9593
		// (get) Token: 0x06007324 RID: 29476 RVA: 0x001AF6FB File Offset: 0x001AD8FB
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Category("Client")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Description("Contains data service settings for client-bound RadListView.")]
		public virtual RadListViewDataServiceSettings DataService
		{
			get
			{
				if (this._dataService == null)
				{
					this._dataService = new RadListViewDataServiceSettings();
					if (((IStateManager)this._dataService).IsTrackingViewState)
					{
						((IStateManager)this._dataService).TrackViewState();
					}
				}
				return this._dataService;
			}
		}

		// Token: 0x06007325 RID: 29477 RVA: 0x001AF730 File Offset: 0x001AD930
		protected override void LoadViewState(object state)
		{
			if (state != null)
			{
				object[] array = (object[])state;
				base.LoadViewState(array[0]);
				((IStateManager)this.DataService).LoadViewState(array[1]);
			}
		}

		// Token: 0x06007326 RID: 29478 RVA: 0x001AF760 File Offset: 0x001AD960
		protected override object SaveViewState()
		{
			return new ArrayList
			{
				base.SaveViewState(),
				((IStateManager)this.DataService).SaveViewState()
			}.ToArray(typeof(object));
		}

		// Token: 0x06007327 RID: 29479 RVA: 0x001AF7A2 File Offset: 0x001AD9A2
		protected override void TrackViewState()
		{
			((IStateManager)this.DataService).TrackViewState();
			base.TrackViewState();
		}

		// Token: 0x04001F31 RID: 7985
		private RadListViewDataServiceSettings _dataService;
	}
}
