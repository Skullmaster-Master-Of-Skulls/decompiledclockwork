using System;
using System.Collections;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02001286 RID: 4742
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class TreeListClientSettings : StateManager
	{
		// Token: 0x0600C5BB RID: 50619 RVA: 0x002C2D5F File Offset: 0x002C0F5F
		public TreeListClientSettings(RadTreeList owner)
		{
			this._owner = owner;
		}

		// Token: 0x17003FCE RID: 16334
		// (get) Token: 0x0600C5BC RID: 50620 RVA: 0x002C2D6E File Offset: 0x002C0F6E
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Category("Client")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public TreeListSelecting Selecting
		{
			get
			{
				if (this._selecting == null)
				{
					this._selecting = new TreeListSelecting();
					if (this.IsTrackingViewState)
					{
						((IStateManager)this._selecting).TrackViewState();
					}
				}
				return this._selecting;
			}
		}

		// Token: 0x17003FCF RID: 16335
		// (get) Token: 0x0600C5BD RID: 50621 RVA: 0x002C2D9C File Offset: 0x002C0F9C
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Category("Client")]
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public TreeListClientEvents ClientEvents
		{
			get
			{
				if (this._events == null)
				{
					this._events = new TreeListClientEvents();
					if (this.IsTrackingViewState)
					{
						((IStateManager)this._events).TrackViewState();
					}
				}
				return this._events;
			}
		}

		// Token: 0x17003FD0 RID: 16336
		// (get) Token: 0x0600C5BE RID: 50622 RVA: 0x002C2DCC File Offset: 0x002C0FCC
		// (set) Token: 0x0600C5BF RID: 50623 RVA: 0x002C2DF5 File Offset: 0x002C0FF5
		[NotifyParentProperty(true)]
		[Description("Gets or sets the property determining if the RadTreeList columns could be hidden.")]
		[DefaultValue(true)]
		[Category("Client")]
		public virtual bool AllowColumnHide
		{
			get
			{
				object obj = base.ViewState["AllowColumnHide"];
				return obj == null || (bool)obj;
			}
			set
			{
				base.ViewState["AllowColumnHide"] = value;
			}
		}

		// Token: 0x17003FD1 RID: 16337
		// (get) Token: 0x0600C5C0 RID: 50624 RVA: 0x002C2E10 File Offset: 0x002C1010
		// (set) Token: 0x0600C5C1 RID: 50625 RVA: 0x002C2E39 File Offset: 0x002C1039
		[DefaultValue(false)]
		[Category("Client")]
		[NotifyParentProperty(true)]
		[Description("Gets or sets a value indicating whether RadTreeList should postback on row click.")]
		public virtual bool AllowPostBackOnItemClick
		{
			get
			{
				object obj = base.ViewState["AllowPostBackOnItemClick"];
				return obj != null && (bool)obj;
			}
			set
			{
				base.ViewState["AllowPostBackOnItemClick"] = value;
			}
		}

		// Token: 0x17003FD2 RID: 16338
		// (get) Token: 0x0600C5C2 RID: 50626 RVA: 0x002C2E51 File Offset: 0x002C1051
		[Category("Client")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public TreeListScrolling Scrolling
		{
			get
			{
				if (this._scrolling == null)
				{
					this._scrolling = new TreeListScrolling();
					if (this.IsTrackingViewState)
					{
						((IStateManager)this._scrolling).TrackViewState();
					}
				}
				return this._scrolling;
			}
		}

		// Token: 0x17003FD3 RID: 16339
		// (get) Token: 0x0600C5C3 RID: 50627 RVA: 0x002C2E7F File Offset: 0x002C107F
		internal bool ShouldSerializeAllowPostBackOnItemClick
		{
			get
			{
				return this.AllowPostBackOnItemClick;
			}
		}

		// Token: 0x17003FD4 RID: 16340
		// (get) Token: 0x0600C5C4 RID: 50628 RVA: 0x002C2E87 File Offset: 0x002C1087
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual string PostBackFunction
		{
			get
			{
				return "__doPostBack('{0}','{1}')";
			}
		}

		// Token: 0x17003FD5 RID: 16341
		// (get) Token: 0x0600C5C5 RID: 50629 RVA: 0x002C2E8E File Offset: 0x002C108E
		internal bool ShouldSerializePostBackFunction
		{
			get
			{
				return !string.IsNullOrEmpty(this.PostBackFunction) && this.PostBackFunction != "__doPostBack('{0}','{1}')";
			}
		}

		// Token: 0x17003FD6 RID: 16342
		// (get) Token: 0x0600C5C6 RID: 50630 RVA: 0x002C2EAF File Offset: 0x002C10AF
		// (set) Token: 0x0600C5C7 RID: 50631 RVA: 0x002C2EDA File Offset: 0x002C10DA
		[DefaultValue(false)]
		[Description("Gets or sets a value indicating whether the RadTreeList items can be dragged and dropped")]
		[NotifyParentProperty(true)]
		[Category("Behavior")]
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

		// Token: 0x17003FD7 RID: 16343
		// (get) Token: 0x0600C5C8 RID: 50632 RVA: 0x002C2EF2 File Offset: 0x002C10F2
		// (set) Token: 0x0600C5C9 RID: 50633 RVA: 0x002C2F1D File Offset: 0x002C111D
		[DefaultValue(false)]
		[Description("")]
		[NotifyParentProperty(true)]
		[Category("Client")]
		public virtual bool AllowKeyboardNavigation
		{
			get
			{
				return base.ViewState["AllowKeyboardNavigation"] != null && (bool)base.ViewState["AllowKeyboardNavigation"];
			}
			set
			{
				base.ViewState["AllowKeyboardNavigation"] = value;
			}
		}

		// Token: 0x17003FD8 RID: 16344
		// (get) Token: 0x0600C5CA RID: 50634 RVA: 0x002C2F35 File Offset: 0x002C1135
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Category("Client")]
		public TreeListKeyboardNavigationSettings KeyboardNavigationSettings
		{
			get
			{
				if (this._keyboardNavigationSettings == null)
				{
					this._keyboardNavigationSettings = new TreeListKeyboardNavigationSettings();
					if (this.IsTrackingViewState)
					{
						((IStateManager)this._keyboardNavigationSettings).TrackViewState();
					}
				}
				return this._keyboardNavigationSettings;
			}
		}

		// Token: 0x17003FD9 RID: 16345
		// (get) Token: 0x0600C5CB RID: 50635 RVA: 0x002C2F63 File Offset: 0x002C1163
		[Category("Client")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[NotifyParentProperty(true)]
		public TreeListResizing Resizing
		{
			get
			{
				if (this._resizing == null)
				{
					this._resizing = new TreeListResizing();
					if (this.IsTrackingViewState)
					{
						((IStateManager)this._resizing).TrackViewState();
					}
				}
				return this._resizing;
			}
		}

		// Token: 0x17003FDA RID: 16346
		// (get) Token: 0x0600C5CC RID: 50636 RVA: 0x002C2F91 File Offset: 0x002C1191
		[NotifyParentProperty(true)]
		[Category("Client")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public TreeListReordering Reordering
		{
			get
			{
				if (this._reordering == null)
				{
					this._reordering = new TreeListReordering();
					if (this.IsTrackingViewState)
					{
						((IStateManager)this._resizing).TrackViewState();
					}
				}
				return this._reordering;
			}
		}

		// Token: 0x17003FDB RID: 16347
		// (get) Token: 0x0600C5CD RID: 50637 RVA: 0x002C2FBF File Offset: 0x002C11BF
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Category("Client")]
		public TreeListClientMessages ClientMessages
		{
			get
			{
				if (this._clientMessages == null)
				{
					this._clientMessages = new TreeListClientMessages(this._owner);
					if (this.IsTrackingViewState)
					{
						((IStateManager)this._clientMessages).TrackViewState();
					}
				}
				return this._clientMessages;
			}
		}

		// Token: 0x17003FDC RID: 16348
		// (get) Token: 0x0600C5CE RID: 50638 RVA: 0x002C2FF4 File Offset: 0x002C11F4
		// (set) Token: 0x0600C5CF RID: 50639 RVA: 0x002C3021 File Offset: 0x002C1221
		[NotifyParentProperty(true)]
		[Category("Client")]
		[Description("")]
		[DefaultValue("")]
		public string ActiveRowIndex
		{
			get
			{
				object obj = base.ViewState["ActiveRowIndex"];
				if (obj != null)
				{
					return (string)obj;
				}
				return "";
			}
			set
			{
				base.ViewState["ActiveRowIndex"] = value;
			}
		}

		// Token: 0x17003FDD RID: 16349
		// (get) Token: 0x0600C5D0 RID: 50640 RVA: 0x002C3034 File Offset: 0x002C1234
		internal bool ShouldSerializeAllowItemsDragDrop
		{
			get
			{
				return this.AllowItemsDragDrop;
			}
		}

		// Token: 0x17003FDE RID: 16350
		// (get) Token: 0x0600C5D1 RID: 50641 RVA: 0x002C303C File Offset: 0x002C123C
		internal bool ShouldSerializeAllowColumnHide
		{
			get
			{
				return this.AllowColumnHide;
			}
		}

		// Token: 0x0600C5D2 RID: 50642 RVA: 0x002C3044 File Offset: 0x002C1244
		protected override void LoadViewState(object state)
		{
			if (state != null)
			{
				object[] array = (object[])state;
				base.LoadViewState(array[0]);
				((IStateManager)this.Selecting).LoadViewState(array[1]);
				((IStateManager)this.ClientEvents).LoadViewState(array[2]);
				((IStateManager)this.Scrolling).LoadViewState(array[3]);
				((IStateManager)this.KeyboardNavigationSettings).LoadViewState(array[4]);
				((IStateManager)this.Resizing).LoadViewState(array[5]);
				((IStateManager)this.ClientMessages).LoadViewState(array[6]);
				((IStateManager)this.Reordering).LoadViewState(array[7]);
			}
		}

		// Token: 0x0600C5D3 RID: 50643 RVA: 0x002C30C8 File Offset: 0x002C12C8
		protected override object SaveViewState()
		{
			ArrayList arrayList = new ArrayList
			{
				base.SaveViewState(),
				((IStateManager)this.Selecting).SaveViewState(),
				((IStateManager)this.ClientEvents).SaveViewState(),
				((IStateManager)this.Scrolling).SaveViewState(),
				((IStateManager)this.KeyboardNavigationSettings).SaveViewState(),
				((IStateManager)this.Resizing).SaveViewState(),
				((IStateManager)this.ClientMessages).SaveViewState(),
				((IStateManager)this.Reordering).SaveViewState()
			};
			return arrayList.ToArray(typeof(object));
		}

		// Token: 0x0600C5D4 RID: 50644 RVA: 0x002C3178 File Offset: 0x002C1378
		protected override void TrackViewState()
		{
			((IStateManager)this.Selecting).TrackViewState();
			((IStateManager)this.ClientEvents).TrackViewState();
			((IStateManager)this.Scrolling).TrackViewState();
			((IStateManager)this.KeyboardNavigationSettings).TrackViewState();
			((IStateManager)this.Resizing).TrackViewState();
			((IStateManager)this.ClientMessages).TrackViewState();
			((IStateManager)this.Reordering).TrackViewState();
			base.TrackViewState();
		}

		// Token: 0x04003441 RID: 13377
		private const string _doPostbackValue = "__doPostBack('{0}','{1}')";

		// Token: 0x04003442 RID: 13378
		private readonly RadTreeList _owner;

		// Token: 0x04003443 RID: 13379
		private TreeListSelecting _selecting;

		// Token: 0x04003444 RID: 13380
		private TreeListClientEvents _events;

		// Token: 0x04003445 RID: 13381
		private TreeListScrolling _scrolling;

		// Token: 0x04003446 RID: 13382
		private TreeListKeyboardNavigationSettings _keyboardNavigationSettings;

		// Token: 0x04003447 RID: 13383
		private TreeListResizing _resizing;

		// Token: 0x04003448 RID: 13384
		private TreeListReordering _reordering;

		// Token: 0x04003449 RID: 13385
		private TreeListClientMessages _clientMessages;
	}
}
