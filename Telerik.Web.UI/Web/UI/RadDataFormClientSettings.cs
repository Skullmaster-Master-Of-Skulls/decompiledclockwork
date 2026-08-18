using System;
using System.Collections;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02000213 RID: 531
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class RadDataFormClientSettings : StateManager
	{
		// Token: 0x17000667 RID: 1639
		// (get) Token: 0x06001389 RID: 5001 RVA: 0x00044C29 File Offset: 0x00042E29
		[NotifyParentProperty(true)]
		[Category("Client")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public RadDataFormClientEvents ClientEvents
		{
			get
			{
				if (this._events == null)
				{
					this._events = new RadDataFormClientEvents();
					if (((IStateManager)this).IsTrackingViewState)
					{
						((IStateManager)this._events).TrackViewState();
					}
				}
				return this._events;
			}
		}

		// Token: 0x0600138A RID: 5002 RVA: 0x00044C58 File Offset: 0x00042E58
		protected override void LoadViewState(object state)
		{
			if (state != null)
			{
				object[] array = (object[])state;
				base.LoadViewState(array[0]);
				((IStateManager)this.ClientEvents).LoadViewState(array[1]);
			}
		}

		// Token: 0x0600138B RID: 5003 RVA: 0x00044C88 File Offset: 0x00042E88
		protected override object SaveViewState()
		{
			return new ArrayList
			{
				base.SaveViewState(),
				((IStateManager)this.ClientEvents).SaveViewState()
			}.ToArray(typeof(object));
		}

		// Token: 0x0600138C RID: 5004 RVA: 0x00044CCA File Offset: 0x00042ECA
		protected override void TrackViewState()
		{
			((IStateManager)this.ClientEvents).TrackViewState();
			base.TrackViewState();
		}

		// Token: 0x0400057B RID: 1403
		private RadDataFormClientEvents _events;
	}
}
