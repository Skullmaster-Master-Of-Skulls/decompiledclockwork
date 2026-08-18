using System;
using System.Collections;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02000DED RID: 3565
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class PivotGridClientSettings : StateManager
	{
		// Token: 0x06008470 RID: 33904 RVA: 0x001E2FFC File Offset: 0x001E11FC
		public PivotGridClientSettings(RadPivotGrid owner)
		{
			this.owner = owner;
		}

		// Token: 0x170029E0 RID: 10720
		// (get) Token: 0x06008471 RID: 33905 RVA: 0x001E300B File Offset: 0x001E120B
		// (set) Token: 0x06008472 RID: 33906 RVA: 0x001E302C File Offset: 0x001E122C
		[Description("")]
		[DefaultValue(false)]
		[NotifyParentProperty(true)]
		[Category("Client")]
		public bool EnableFieldsDragDrop
		{
			get
			{
				return (bool)(base.ViewState["EnableFieldsDragDrop"] ?? false);
			}
			set
			{
				base.ViewState["EnableFieldsDragDrop"] = value;
			}
		}

		// Token: 0x170029E1 RID: 10721
		// (get) Token: 0x06008473 RID: 33907 RVA: 0x001E3044 File Offset: 0x001E1244
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[NotifyParentProperty(true)]
		[Category("Client")]
		public PivotGridScrolling Scrolling
		{
			get
			{
				if (this.scrolling == null)
				{
					this.scrolling = new PivotGridScrolling();
					if (this.IsTrackingViewState)
					{
						((IStateManager)this.scrolling).TrackViewState();
					}
				}
				return this.scrolling;
			}
		}

		// Token: 0x170029E2 RID: 10722
		// (get) Token: 0x06008474 RID: 33908 RVA: 0x001E3072 File Offset: 0x001E1272
		[Category("Client")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public PivotGridClientEvents ClientEvents
		{
			get
			{
				if (this.clientEvents == null)
				{
					this.clientEvents = new PivotGridClientEvents();
					if (this.IsTrackingViewState)
					{
						((IStateManager)this.clientEvents).TrackViewState();
					}
				}
				return this.clientEvents;
			}
		}

		// Token: 0x170029E3 RID: 10723
		// (get) Token: 0x06008475 RID: 33909 RVA: 0x001E30A0 File Offset: 0x001E12A0
		[NotifyParentProperty(true)]
		[Category("Client")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public PivotGridClientMessages ClientMessages
		{
			get
			{
				if (this.clientMessages == null)
				{
					this.clientMessages = new PivotGridClientMessages(this.owner);
					if (this.IsTrackingViewState)
					{
						((IStateManager)this.clientMessages).TrackViewState();
					}
				}
				return this.clientMessages;
			}
		}

		// Token: 0x170029E4 RID: 10724
		// (get) Token: 0x06008476 RID: 33910 RVA: 0x001E30D4 File Offset: 0x001E12D4
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[Category("Client")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public PivotGridResizing Resizing
		{
			get
			{
				if (this.resizing == null)
				{
					this.resizing = new PivotGridResizing();
					if (this.IsTrackingViewState)
					{
						((IStateManager)this.resizing).TrackViewState();
					}
				}
				return this.resizing;
			}
		}

		// Token: 0x06008477 RID: 33911 RVA: 0x001E3104 File Offset: 0x001E1304
		protected override void LoadViewState(object state)
		{
			if (state != null)
			{
				object[] array = (object[])state;
				base.LoadViewState(array[0]);
				((IStateManager)this.Scrolling).LoadViewState(array[1]);
				((IStateManager)this.ClientEvents).LoadViewState(array[2]);
				((IStateManager)this.ClientMessages).LoadViewState(array[3]);
				((IStateManager)this.Resizing).LoadViewState(array[4]);
			}
		}

		// Token: 0x06008478 RID: 33912 RVA: 0x001E315C File Offset: 0x001E135C
		protected override object SaveViewState()
		{
			ArrayList arrayList = new ArrayList
			{
				base.SaveViewState(),
				((IStateManager)this.Scrolling).SaveViewState(),
				((IStateManager)this.ClientEvents).SaveViewState(),
				((IStateManager)this.ClientMessages).SaveViewState(),
				((IStateManager)this.Resizing).SaveViewState()
			};
			return arrayList.ToArray(typeof(object));
		}

		// Token: 0x06008479 RID: 33913 RVA: 0x001E31D6 File Offset: 0x001E13D6
		protected override void TrackViewState()
		{
			((IStateManager)this.Scrolling).TrackViewState();
			base.TrackViewState();
		}

		// Token: 0x040024C6 RID: 9414
		private readonly RadPivotGrid owner;

		// Token: 0x040024C7 RID: 9415
		private PivotGridScrolling scrolling;

		// Token: 0x040024C8 RID: 9416
		private PivotGridClientEvents clientEvents;

		// Token: 0x040024C9 RID: 9417
		private PivotGridClientMessages clientMessages;

		// Token: 0x040024CA RID: 9418
		private PivotGridResizing resizing;
	}
}
