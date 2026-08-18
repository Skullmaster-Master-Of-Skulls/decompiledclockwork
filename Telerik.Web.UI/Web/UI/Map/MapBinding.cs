using System;
using System.Web.UI;

namespace Telerik.Web.UI.Map
{
	// Token: 0x02000440 RID: 1088
	public class MapBinding : StateManager
	{
		// Token: 0x17000C88 RID: 3208
		// (get) Token: 0x060026F6 RID: 9974 RVA: 0x0007EDA2 File Offset: 0x0007CFA2
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public MarkerBinding MarkerBinding
		{
			get
			{
				if (this._markerBinding == null)
				{
					this._markerBinding = new MarkerBinding();
					if (this.IsTrackingViewState)
					{
						((IStateManager)this._markerBinding).TrackViewState();
					}
				}
				return this._markerBinding;
			}
		}

		// Token: 0x17000C89 RID: 3209
		// (get) Token: 0x060026F7 RID: 9975 RVA: 0x0007EDD0 File Offset: 0x0007CFD0
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public LayerBinding LayerBinding
		{
			get
			{
				if (this._layerBinding == null)
				{
					this._layerBinding = new LayerBinding();
					if (this.IsTrackingViewState)
					{
						((IStateManager)this._layerBinding).TrackViewState();
					}
				}
				return this._layerBinding;
			}
		}

		// Token: 0x04000A04 RID: 2564
		private MarkerBinding _markerBinding;

		// Token: 0x04000A05 RID: 2565
		private LayerBinding _layerBinding;
	}
}
