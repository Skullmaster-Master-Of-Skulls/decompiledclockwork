using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI.Calendar.Persistence
{
	// Token: 0x02000197 RID: 407
	public abstract class PropertiesControl : RadWebControl, IStateManager
	{
		// Token: 0x06000DD0 RID: 3536 RVA: 0x000345A8 File Offset: 0x000327A8
		public PropertiesControl()
		{
		}

		// Token: 0x170004A4 RID: 1188
		// (get) Token: 0x06000DD1 RID: 3537 RVA: 0x000345BB File Offset: 0x000327BB
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		internal virtual PropertyBag Properties
		{
			get
			{
				if (this._ObjectProperties == null)
				{
					this._ObjectProperties = new PropertyBag();
				}
				return this._ObjectProperties;
			}
		}

		// Token: 0x06000DD2 RID: 3538 RVA: 0x000345D6 File Offset: 0x000327D6
		void IStateManager.LoadViewState(object state)
		{
			this.LoadViewState(state);
		}

		// Token: 0x06000DD3 RID: 3539 RVA: 0x000345E0 File Offset: 0x000327E0
		protected override void LoadViewState(object state)
		{
			Pair pair = state as Pair;
			if (pair != null)
			{
				((IStateManager)this._ObjectProperties).LoadViewState(pair.First);
				base.LoadViewState(pair.Second);
			}
		}

		// Token: 0x06000DD4 RID: 3540 RVA: 0x00034614 File Offset: 0x00032814
		object IStateManager.SaveViewState()
		{
			return this.SaveViewState();
		}

		// Token: 0x06000DD5 RID: 3541 RVA: 0x0003461C File Offset: 0x0003281C
		protected override object SaveViewState()
		{
			return new Pair
			{
				First = ((IStateManager)this._ObjectProperties).SaveViewState(),
				Second = base.SaveViewState()
			};
		}

		// Token: 0x06000DD6 RID: 3542 RVA: 0x0003464D File Offset: 0x0003284D
		void IStateManager.TrackViewState()
		{
			this.TrackViewState();
		}

		// Token: 0x06000DD7 RID: 3543 RVA: 0x00034655 File Offset: 0x00032855
		protected override void TrackViewState()
		{
			((IStateManager)this._ObjectProperties).TrackViewState();
			base.TrackViewState();
		}

		// Token: 0x170004A5 RID: 1189
		// (get) Token: 0x06000DD8 RID: 3544 RVA: 0x00034668 File Offset: 0x00032868
		bool IStateManager.IsTrackingViewState
		{
			get
			{
				return this.IsTrackingViewState;
			}
		}

		// Token: 0x170004A6 RID: 1190
		// (get) Token: 0x06000DD9 RID: 3545 RVA: 0x00034670 File Offset: 0x00032870
		internal new bool IsTrackingViewState
		{
			get
			{
				return ((IStateManager)this._ObjectProperties).IsTrackingViewState;
			}
		}

		// Token: 0x040003F5 RID: 1013
		private PropertyBag _ObjectProperties = new PropertyBag();
	}
}
