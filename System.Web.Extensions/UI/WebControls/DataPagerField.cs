using System;
using System.ComponentModel;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000091 RID: 145
	public abstract class DataPagerField : IStateManager
	{
		// Token: 0x14000011 RID: 17
		// (add) Token: 0x0600065A RID: 1626 RVA: 0x0001BF7C File Offset: 0x0001A17C
		// (remove) Token: 0x0600065B RID: 1627 RVA: 0x0001BFB4 File Offset: 0x0001A1B4
		internal event EventHandler FieldChanged;

		// Token: 0x0600065C RID: 1628 RVA: 0x0001BFE9 File Offset: 0x0001A1E9
		protected DataPagerField()
		{
			this._stateBag = new StateBag();
		}

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x0600065D RID: 1629 RVA: 0x0001BFFC File Offset: 0x0001A1FC
		protected StateBag ViewState
		{
			get
			{
				return this._stateBag;
			}
		}

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x0600065E RID: 1630 RVA: 0x0001C004 File Offset: 0x0001A204
		protected bool IsTrackingViewState
		{
			get
			{
				return this._trackViewState;
			}
		}

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x0600065F RID: 1631 RVA: 0x0001C00C File Offset: 0x0001A20C
		protected DataPager DataPager
		{
			get
			{
				return this._dataPager;
			}
		}

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x06000660 RID: 1632 RVA: 0x0001C014 File Offset: 0x0001A214
		// (set) Token: 0x06000661 RID: 1633 RVA: 0x0001C021 File Offset: 0x0001A221
		protected bool QueryStringHandled
		{
			get
			{
				return this.DataPager.QueryStringHandled;
			}
			set
			{
				this.DataPager.QueryStringHandled = value;
			}
		}

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x06000662 RID: 1634 RVA: 0x0001C02F File Offset: 0x0001A22F
		protected string QueryStringValue
		{
			get
			{
				return this.DataPager.QueryStringValue;
			}
		}

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x06000663 RID: 1635 RVA: 0x0001C03C File Offset: 0x0001A23C
		// (set) Token: 0x06000664 RID: 1636 RVA: 0x0001C065 File Offset: 0x0001A265
		[Category("Behavior")]
		[DefaultValue(true)]
		[ResourceDescription("DataPagerField_Visible")]
		public bool Visible
		{
			get
			{
				object obj = this.ViewState["Visible"];
				return obj == null || (bool)obj;
			}
			set
			{
				if (value != this.Visible)
				{
					this.ViewState["Visible"] = value;
					this.OnFieldChanged();
				}
			}
		}

		// Token: 0x06000665 RID: 1637 RVA: 0x0001C08C File Offset: 0x0001A28C
		protected internal DataPagerField CloneField()
		{
			DataPagerField dataPagerField = this.CreateField();
			this.CopyProperties(dataPagerField);
			return dataPagerField;
		}

		// Token: 0x06000666 RID: 1638 RVA: 0x0001C0A8 File Offset: 0x0001A2A8
		protected virtual void CopyProperties(DataPagerField newField)
		{
			newField.Visible = this.Visible;
		}

		// Token: 0x06000667 RID: 1639
		public abstract void CreateDataPagers(DataPagerFieldItem container, int startRowIndex, int maximumRows, int totalRowCount, int fieldIndex);

		// Token: 0x06000668 RID: 1640
		protected abstract DataPagerField CreateField();

		// Token: 0x06000669 RID: 1641 RVA: 0x0001C0B6 File Offset: 0x0001A2B6
		protected string GetQueryStringNavigateUrl(int pageNumber)
		{
			return this.DataPager.GetQueryStringNavigateUrl(pageNumber);
		}

		// Token: 0x0600066A RID: 1642
		public abstract void HandleEvent(CommandEventArgs e);

		// Token: 0x0600066B RID: 1643 RVA: 0x0001C0C4 File Offset: 0x0001A2C4
		protected virtual void LoadViewState(object savedState)
		{
			if (savedState != null)
			{
				object[] array = (object[])savedState;
				if (array[0] != null)
				{
					((IStateManager)this.ViewState).LoadViewState(array[0]);
				}
			}
		}

		// Token: 0x0600066C RID: 1644 RVA: 0x0001C0EE File Offset: 0x0001A2EE
		protected virtual void OnFieldChanged()
		{
			if (this.FieldChanged != null)
			{
				this.FieldChanged(this, EventArgs.Empty);
			}
		}

		// Token: 0x0600066D RID: 1645 RVA: 0x0001C10C File Offset: 0x0001A30C
		protected virtual object SaveViewState()
		{
			object obj = ((IStateManager)this.ViewState).SaveViewState();
			if (obj != null)
			{
				return new object[]
				{
					obj
				};
			}
			return null;
		}

		// Token: 0x0600066E RID: 1646 RVA: 0x0001C134 File Offset: 0x0001A334
		internal void SetDirty()
		{
			this._stateBag.SetDirty(true);
		}

		// Token: 0x0600066F RID: 1647 RVA: 0x0001C142 File Offset: 0x0001A342
		internal void SetDataPager(DataPager dataPager)
		{
			this._dataPager = dataPager;
		}

		// Token: 0x06000670 RID: 1648 RVA: 0x0001C14B File Offset: 0x0001A34B
		protected virtual void TrackViewState()
		{
			this._trackViewState = true;
			((IStateManager)this.ViewState).TrackViewState();
		}

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x06000671 RID: 1649 RVA: 0x0001C15F File Offset: 0x0001A35F
		bool IStateManager.IsTrackingViewState
		{
			get
			{
				return this.IsTrackingViewState;
			}
		}

		// Token: 0x06000672 RID: 1650 RVA: 0x0001C167 File Offset: 0x0001A367
		void IStateManager.LoadViewState(object state)
		{
			this.LoadViewState(state);
		}

		// Token: 0x06000673 RID: 1651 RVA: 0x0001C170 File Offset: 0x0001A370
		void IStateManager.TrackViewState()
		{
			this.TrackViewState();
		}

		// Token: 0x06000674 RID: 1652 RVA: 0x0001C178 File Offset: 0x0001A378
		object IStateManager.SaveViewState()
		{
			return this.SaveViewState();
		}

		// Token: 0x04000249 RID: 585
		private StateBag _stateBag;

		// Token: 0x0400024A RID: 586
		private bool _trackViewState;

		// Token: 0x0400024B RID: 587
		private DataPager _dataPager;
	}
}
