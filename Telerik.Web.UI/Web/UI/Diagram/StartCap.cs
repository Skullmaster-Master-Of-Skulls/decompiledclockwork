using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI.Diagram
{
	// Token: 0x02000269 RID: 617
	public class StartCap : StateManager, IDefaultCheck
	{
		// Token: 0x170007A4 RID: 1956
		// (get) Token: 0x0600164F RID: 5711 RVA: 0x0004BDC2 File Offset: 0x00049FC2
		// (set) Token: 0x06001650 RID: 5712 RVA: 0x0004BDE2 File Offset: 0x00049FE2
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

		// Token: 0x170007A5 RID: 1957
		// (get) Token: 0x06001651 RID: 5713 RVA: 0x0004BDF5 File Offset: 0x00049FF5
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

		// Token: 0x170007A6 RID: 1958
		// (get) Token: 0x06001652 RID: 5714 RVA: 0x0004BE10 File Offset: 0x0004A010
		// (set) Token: 0x06001653 RID: 5715 RVA: 0x0004BE30 File Offset: 0x0004A030
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

		// Token: 0x170007A7 RID: 1959
		// (get) Token: 0x06001654 RID: 5716 RVA: 0x0004BE43 File Offset: 0x0004A043
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

		// Token: 0x170007A8 RID: 1960
		// (get) Token: 0x06001655 RID: 5717 RVA: 0x0004BE5E File Offset: 0x0004A05E
		// (set) Token: 0x06001656 RID: 5718 RVA: 0x0004BE7F File Offset: 0x0004A07F
		[DefaultValue(ConnectionStartCap.None)]
		public ConnectionStartCap Type
		{
			get
			{
				return (ConnectionStartCap)(base.ViewState["Type"] ?? ConnectionStartCap.None);
			}
			set
			{
				base.ViewState["Type"] = value;
			}
		}

		// Token: 0x06001657 RID: 5719 RVA: 0x0004BE97 File Offset: 0x0004A097
		internal override void SetDirty()
		{
			base.SetDirty();
			this.FillSettings.SetDirty();
			this.StrokeSettings.SetDirty();
		}

		// Token: 0x06001658 RID: 5720 RVA: 0x0004BEB8 File Offset: 0x0004A0B8
		protected override void LoadViewState(object state)
		{
			object[] array = (object[])state;
			int num = 0;
			base.LoadViewState(array[num++]);
			((IStateManager)this.FillSettings).LoadViewState(array[num++]);
			((IStateManager)this.StrokeSettings).LoadViewState(array[num++]);
		}

		// Token: 0x06001659 RID: 5721 RVA: 0x0004BF00 File Offset: 0x0004A100
		protected override object SaveViewState()
		{
			return new object[]
			{
				base.SaveViewState(),
				((IStateManager)this.FillSettings).SaveViewState(),
				((IStateManager)this.StrokeSettings).SaveViewState()
			};
		}

		// Token: 0x0600165A RID: 5722 RVA: 0x0004BF3C File Offset: 0x0004A13C
		protected override void TrackViewState()
		{
			base.TrackViewState();
			((IStateManager)this.FillSettings).TrackViewState();
			((IStateManager)this.StrokeSettings).TrackViewState();
		}

		// Token: 0x170007A9 RID: 1961
		// (get) Token: 0x0600165B RID: 5723 RVA: 0x0004BF5C File Offset: 0x0004A15C
		public bool IsDefault
		{
			get
			{
				return this.Fill == "" && this.FillSettings.IsDefault && this.Stroke == "" && this.StrokeSettings.IsDefault && this.Type == ConnectionStartCap.None;
			}
		}

		// Token: 0x040005EA RID: 1514
		private Fill _fill;

		// Token: 0x040005EB RID: 1515
		private Stroke _stroke;
	}
}
