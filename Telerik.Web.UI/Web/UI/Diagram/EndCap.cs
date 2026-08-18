using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI.Diagram
{
	// Token: 0x02000250 RID: 592
	public class EndCap : StateManager, IDefaultCheck
	{
		// Token: 0x1700075A RID: 1882
		// (get) Token: 0x06001596 RID: 5526 RVA: 0x00049DBA File Offset: 0x00047FBA
		// (set) Token: 0x06001597 RID: 5527 RVA: 0x00049DDA File Offset: 0x00047FDA
		[DefaultValue("")]
		public string Fill
		{
			get
			{
				return (string)(base.ViewState["Fill"] ?? "");
			}
			set
			{
				base.ViewState["Fill"] = value;
			}
		}

		// Token: 0x1700075B RID: 1883
		// (get) Token: 0x06001598 RID: 5528 RVA: 0x00049DED File Offset: 0x00047FED
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public Fill FillSettings
		{
			get
			{
				if (this._fill == null)
				{
					this._fill = new Fill();
				}
				return this._fill;
			}
		}

		// Token: 0x1700075C RID: 1884
		// (get) Token: 0x06001599 RID: 5529 RVA: 0x00049E08 File Offset: 0x00048008
		// (set) Token: 0x0600159A RID: 5530 RVA: 0x00049E28 File Offset: 0x00048028
		[DefaultValue("")]
		public string Stroke
		{
			get
			{
				return (string)(base.ViewState["Stroke"] ?? "");
			}
			set
			{
				base.ViewState["Stroke"] = value;
			}
		}

		// Token: 0x1700075D RID: 1885
		// (get) Token: 0x0600159B RID: 5531 RVA: 0x00049E3B File Offset: 0x0004803B
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public Stroke StrokeSettings
		{
			get
			{
				if (this._stroke == null)
				{
					this._stroke = new Stroke();
				}
				return this._stroke;
			}
		}

		// Token: 0x1700075E RID: 1886
		// (get) Token: 0x0600159C RID: 5532 RVA: 0x00049E56 File Offset: 0x00048056
		// (set) Token: 0x0600159D RID: 5533 RVA: 0x00049E77 File Offset: 0x00048077
		[DefaultValue(ConnectionEndCap.None)]
		public ConnectionEndCap Type
		{
			get
			{
				return (ConnectionEndCap)(base.ViewState["Type"] ?? ConnectionEndCap.None);
			}
			set
			{
				base.ViewState["Type"] = value;
			}
		}

		// Token: 0x0600159E RID: 5534 RVA: 0x00049E8F File Offset: 0x0004808F
		internal override void SetDirty()
		{
			base.SetDirty();
			this.FillSettings.SetDirty();
			this.StrokeSettings.SetDirty();
		}

		// Token: 0x0600159F RID: 5535 RVA: 0x00049EB0 File Offset: 0x000480B0
		protected override void LoadViewState(object state)
		{
			object[] array = (object[])state;
			int num = 0;
			base.LoadViewState(array[num++]);
			((IStateManager)this.FillSettings).LoadViewState(array[num++]);
			((IStateManager)this.StrokeSettings).LoadViewState(array[num++]);
		}

		// Token: 0x060015A0 RID: 5536 RVA: 0x00049EF8 File Offset: 0x000480F8
		protected override object SaveViewState()
		{
			return new object[]
			{
				base.SaveViewState(),
				((IStateManager)this.FillSettings).SaveViewState(),
				((IStateManager)this.StrokeSettings).SaveViewState()
			};
		}

		// Token: 0x060015A1 RID: 5537 RVA: 0x00049F34 File Offset: 0x00048134
		protected override void TrackViewState()
		{
			base.TrackViewState();
			((IStateManager)this.FillSettings).TrackViewState();
			((IStateManager)this.StrokeSettings).TrackViewState();
		}

		// Token: 0x1700075F RID: 1887
		// (get) Token: 0x060015A2 RID: 5538 RVA: 0x00049F54 File Offset: 0x00048154
		public bool IsDefault
		{
			get
			{
				return this.Fill == "" && this.FillSettings.IsDefault && this.Stroke == "" && this.StrokeSettings.IsDefault && this.Type == ConnectionEndCap.None;
			}
		}

		// Token: 0x040005BA RID: 1466
		private Fill _fill;

		// Token: 0x040005BB RID: 1467
		private Stroke _stroke;
	}
}
