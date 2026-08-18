using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI.Calendar.Persistence
{
	// Token: 0x02000FF6 RID: 4086
	public class PropertiesObject : IStateManager
	{
		// Token: 0x1700327E RID: 12926
		// (get) Token: 0x06009FD1 RID: 40913 RVA: 0x00239E37 File Offset: 0x00238037
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		internal PropertyBag Properties
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

		// Token: 0x06009FD2 RID: 40914 RVA: 0x00239E52 File Offset: 0x00238052
		void IStateManager.LoadViewState(object state)
		{
			this.LoadViewState(state);
		}

		// Token: 0x06009FD3 RID: 40915 RVA: 0x00239E5B File Offset: 0x0023805B
		private void LoadViewState(object state)
		{
			((IStateManager)this._ObjectProperties).LoadViewState(state);
		}

		// Token: 0x06009FD4 RID: 40916 RVA: 0x00239E69 File Offset: 0x00238069
		object IStateManager.SaveViewState()
		{
			return this.SaveViewState();
		}

		// Token: 0x06009FD5 RID: 40917 RVA: 0x00239E71 File Offset: 0x00238071
		private object SaveViewState()
		{
			return ((IStateManager)this._ObjectProperties).SaveViewState();
		}

		// Token: 0x06009FD6 RID: 40918 RVA: 0x00239E7E File Offset: 0x0023807E
		void IStateManager.TrackViewState()
		{
			this.TrackViewState();
		}

		// Token: 0x06009FD7 RID: 40919 RVA: 0x00239E86 File Offset: 0x00238086
		internal void TrackViewState()
		{
			((IStateManager)this._ObjectProperties).TrackViewState();
		}

		// Token: 0x1700327F RID: 12927
		// (get) Token: 0x06009FD8 RID: 40920 RVA: 0x00239E93 File Offset: 0x00238093
		bool IStateManager.IsTrackingViewState
		{
			get
			{
				return this.IsTrackingViewState;
			}
		}

		// Token: 0x17003280 RID: 12928
		// (get) Token: 0x06009FD9 RID: 40921 RVA: 0x00239E9B File Offset: 0x0023809B
		internal bool IsTrackingViewState
		{
			get
			{
				return ((IStateManager)this._ObjectProperties).IsTrackingViewState;
			}
		}

		// Token: 0x04002CC0 RID: 11456
		private PropertyBag _ObjectProperties = new PropertyBag();
	}
}
