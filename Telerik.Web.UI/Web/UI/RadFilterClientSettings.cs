using System;
using System.Collections;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x020018C4 RID: 6340
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class RadFilterClientSettings : StateManager
	{
		// Token: 0x0600F575 RID: 62837 RVA: 0x0037BF00 File Offset: 0x0037A100
		protected override void LoadViewState(object state)
		{
			if (state != null)
			{
				object[] array = (object[])state;
				base.LoadViewState(array[0]);
				((IStateManager)this.ClientEvents).LoadViewState(array[1]);
			}
		}

		// Token: 0x0600F576 RID: 62838 RVA: 0x0037BF30 File Offset: 0x0037A130
		protected override object SaveViewState()
		{
			ArrayList arrayList = new ArrayList
			{
				base.SaveViewState(),
				((IStateManager)this.ClientEvents).SaveViewState()
			};
			return arrayList.ToArray(typeof(object));
		}

		// Token: 0x170049FB RID: 18939
		// (get) Token: 0x0600F577 RID: 62839 RVA: 0x0037BF74 File Offset: 0x0037A174
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Category("Client")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		public RadFilterClientEvents ClientEvents
		{
			get
			{
				if (this._events == null)
				{
					this._events = new RadFilterClientEvents();
					if (((IStateManager)this).IsTrackingViewState)
					{
						((IStateManager)this._events).TrackViewState();
					}
				}
				return this._events;
			}
		}

		// Token: 0x04004649 RID: 17993
		private RadFilterClientEvents _events;
	}
}
