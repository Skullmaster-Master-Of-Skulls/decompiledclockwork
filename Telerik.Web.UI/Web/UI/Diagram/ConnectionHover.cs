using System;
using System.Web.UI;

namespace Telerik.Web.UI.Diagram
{
	// Token: 0x0200021B RID: 539
	public class ConnectionHover : StateManager, IDefaultCheck
	{
		// Token: 0x17000686 RID: 1670
		// (get) Token: 0x060013CA RID: 5066 RVA: 0x000457FA File Offset: 0x000439FA
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public ConnectionStroke StrokeSettings
		{
			get
			{
				if (this._stroke == null)
				{
					this._stroke = new ConnectionStroke();
				}
				return this._stroke;
			}
		}

		// Token: 0x060013CB RID: 5067 RVA: 0x00045815 File Offset: 0x00043A15
		internal override void SetDirty()
		{
			base.SetDirty();
			this.StrokeSettings.SetDirty();
		}

		// Token: 0x060013CC RID: 5068 RVA: 0x00045828 File Offset: 0x00043A28
		protected override void LoadViewState(object state)
		{
			object[] array = (object[])state;
			int num = 0;
			base.LoadViewState(array[num++]);
			((IStateManager)this.StrokeSettings).LoadViewState(array[num++]);
		}

		// Token: 0x060013CD RID: 5069 RVA: 0x00045860 File Offset: 0x00043A60
		protected override object SaveViewState()
		{
			return new object[]
			{
				base.SaveViewState(),
				((IStateManager)this.StrokeSettings).SaveViewState()
			};
		}

		// Token: 0x060013CE RID: 5070 RVA: 0x0004588E File Offset: 0x00043A8E
		protected override void TrackViewState()
		{
			base.TrackViewState();
			((IStateManager)this.StrokeSettings).TrackViewState();
		}

		// Token: 0x17000687 RID: 1671
		// (get) Token: 0x060013CF RID: 5071 RVA: 0x000458A1 File Offset: 0x00043AA1
		public bool IsDefault
		{
			get
			{
				return this.StrokeSettings.IsDefault;
			}
		}

		// Token: 0x04000587 RID: 1415
		private ConnectionStroke _stroke;
	}
}
