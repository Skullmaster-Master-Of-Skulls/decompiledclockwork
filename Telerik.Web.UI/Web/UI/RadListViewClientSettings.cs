using System;
using System.Collections;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x020019B4 RID: 6580
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class RadListViewClientSettings : StateManager
	{
		// Token: 0x17004CCE RID: 19662
		// (get) Token: 0x0600FE70 RID: 65136 RVA: 0x00392469 File Offset: 0x00390669
		[NotifyParentProperty(true)]
		[Category("Client")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public RadListViewClientEvents ClientEvents
		{
			get
			{
				if (this._events == null)
				{
					this._events = new RadListViewClientEvents();
					if (((IStateManager)this).IsTrackingViewState)
					{
						((IStateManager)this._events).TrackViewState();
					}
				}
				return this._events;
			}
		}

		// Token: 0x17004CCF RID: 19663
		// (get) Token: 0x0600FE71 RID: 65137 RVA: 0x00392497 File Offset: 0x00390697
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[Category("Client")]
		[Description("Contains client-side databinding settings for RadListView.")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public RadListViewClientDataBinding DataBinding
		{
			get
			{
				if (this._dataBinding == null)
				{
					this._dataBinding = new RadListViewClientDataBinding();
					if (((IStateManager)this._dataBinding).IsTrackingViewState)
					{
						((IStateManager)this._dataBinding).TrackViewState();
					}
				}
				return this._dataBinding;
			}
		}

		// Token: 0x17004CD0 RID: 19664
		// (get) Token: 0x0600FE72 RID: 65138 RVA: 0x003924CA File Offset: 0x003906CA
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual string PostBackFunction
		{
			get
			{
				return "__doPostBack('{0}','{1}')";
			}
		}

		// Token: 0x17004CD1 RID: 19665
		// (get) Token: 0x0600FE73 RID: 65139 RVA: 0x003924D1 File Offset: 0x003906D1
		internal bool ShouldSerializePostBackFunction
		{
			get
			{
				return !string.IsNullOrEmpty(this.PostBackFunction) && this.PostBackFunction != "__doPostBack('{0}','{1}')";
			}
		}

		// Token: 0x17004CD2 RID: 19666
		// (get) Token: 0x0600FE74 RID: 65140 RVA: 0x003924F2 File Offset: 0x003906F2
		// (set) Token: 0x0600FE75 RID: 65141 RVA: 0x0039251D File Offset: 0x0039071D
		[DefaultValue(false)]
		[Category("Behavior")]
		[NotifyParentProperty(true)]
		[Description("AllowItemsDragDrop")]
		public virtual bool AllowItemsDragDrop
		{
			get
			{
				return base.ViewState["AllowItemsDragDrop"] != null && (bool)base.ViewState["AllowItemsDragDrop"];
			}
			set
			{
				base.ViewState["AllowItemsDragDrop"] = value;
			}
		}

		// Token: 0x17004CD3 RID: 19667
		// (get) Token: 0x0600FE76 RID: 65142 RVA: 0x00392535 File Offset: 0x00390735
		internal bool ShouldSerializeAllowItemsDragDrop
		{
			get
			{
				return this.AllowItemsDragDrop;
			}
		}

		// Token: 0x0600FE77 RID: 65143 RVA: 0x00392540 File Offset: 0x00390740
		protected override void LoadViewState(object state)
		{
			if (state != null)
			{
				object[] array = (object[])state;
				base.LoadViewState(array[0]);
				((IStateManager)this.ClientEvents).LoadViewState(array[1]);
				((IStateManager)this.DataBinding).LoadViewState(array[2]);
			}
		}

		// Token: 0x0600FE78 RID: 65144 RVA: 0x0039257C File Offset: 0x0039077C
		protected override object SaveViewState()
		{
			return new ArrayList
			{
				base.SaveViewState(),
				((IStateManager)this.ClientEvents).SaveViewState(),
				((IStateManager)this.DataBinding).SaveViewState()
			}.ToArray(typeof(object));
		}

		// Token: 0x0600FE79 RID: 65145 RVA: 0x003925D0 File Offset: 0x003907D0
		protected override void TrackViewState()
		{
			((IStateManager)this.ClientEvents).TrackViewState();
			((IStateManager)this.DataBinding).TrackViewState();
			base.TrackViewState();
		}

		// Token: 0x0400482F RID: 18479
		private const string _doPostbackValue = "__doPostBack('{0}','{1}')";

		// Token: 0x04004830 RID: 18480
		private RadListViewClientEvents _events;

		// Token: 0x04004831 RID: 18481
		private RadListViewClientDataBinding _dataBinding;
	}
}
