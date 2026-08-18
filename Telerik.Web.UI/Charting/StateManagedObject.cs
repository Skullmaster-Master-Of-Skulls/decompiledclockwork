using System;
using System.Collections;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Charting
{
	// Token: 0x020016D7 RID: 5847
	public abstract class StateManagedObject : IChartingStateManagedItem, IChartingStateManager, IDisposable
	{
		// Token: 0x17004538 RID: 17720
		// (get) Token: 0x0600E1BC RID: 57788 RVA: 0x00323046 File Offset: 0x00321246
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		protected virtual bool ViewStateIgnoresCase
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17004539 RID: 17721
		// (get) Token: 0x0600E1BD RID: 57789 RVA: 0x00323049 File Offset: 0x00321249
		// (set) Token: 0x0600E1BE RID: 57790 RVA: 0x0032307D File Offset: 0x0032127D
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		internal StateBag ViewState
		{
			get
			{
				if (this.viewState == null)
				{
					this.viewState = new StateBag(this.ViewStateIgnoresCase);
					if (((IChartingStateManager)this).IsTrackingViewState)
					{
						((IStateManager)this.viewState).TrackViewState();
					}
				}
				return this.viewState;
			}
			set
			{
				this.viewState = value;
			}
		}

		// Token: 0x1700453A RID: 17722
		// (get) Token: 0x0600E1BF RID: 57791 RVA: 0x00323086 File Offset: 0x00321286
		bool IChartingStateManager.IsTrackingViewState
		{
			get
			{
				return ((IStateManager)this.ViewState).IsTrackingViewState;
			}
		}

		// Token: 0x0600E1C0 RID: 57792 RVA: 0x00323093 File Offset: 0x00321293
		void IChartingStateManager.LoadViewState(object state)
		{
			this.LoadViewState(state);
		}

		// Token: 0x0600E1C1 RID: 57793 RVA: 0x0032309C File Offset: 0x0032129C
		object IChartingStateManager.SaveViewState()
		{
			return this.SaveViewState();
		}

		// Token: 0x0600E1C2 RID: 57794 RVA: 0x003230A4 File Offset: 0x003212A4
		void IChartingStateManager.TrackViewState()
		{
			this.TrackViewState();
		}

		// Token: 0x0600E1C3 RID: 57795 RVA: 0x003230AC File Offset: 0x003212AC
		protected StateBag CloneState()
		{
			StateBag stateBag = new StateBag(this.ViewStateIgnoresCase);
			IDictionaryEnumerator enumerator = this.ViewState.GetEnumerator();
			while (enumerator.MoveNext())
			{
				stateBag[(string)enumerator.Key] = ((StateItem)enumerator.Value).Value;
			}
			return stateBag;
		}

		// Token: 0x0600E1C4 RID: 57796 RVA: 0x003230FD File Offset: 0x003212FD
		protected virtual object SaveViewState()
		{
			this.SetDirty();
			return ((IStateManager)this.ViewState).SaveViewState();
		}

		// Token: 0x0600E1C5 RID: 57797 RVA: 0x00323110 File Offset: 0x00321310
		protected virtual void TrackViewState()
		{
			((IStateManager)this.ViewState).TrackViewState();
		}

		// Token: 0x0600E1C6 RID: 57798 RVA: 0x0032311D File Offset: 0x0032131D
		protected virtual void LoadViewState(object state)
		{
			((IStateManager)this.ViewState).LoadViewState(state);
		}

		// Token: 0x0600E1C7 RID: 57799 RVA: 0x0032312C File Offset: 0x0032132C
		public void SetDirty()
		{
			if (this.ViewState.Count > 0)
			{
				foreach (object obj in this.ViewState.Keys)
				{
					string text = (string)obj;
					PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(this)[text];
					DefaultValueAttribute defaultValueAttribute = null;
					if (propertyDescriptor != null)
					{
						System.ComponentModel.AttributeCollection attributes = propertyDescriptor.Attributes;
						defaultValueAttribute = (DefaultValueAttribute)attributes[typeof(DefaultValueAttribute)];
					}
					if (defaultValueAttribute == null)
					{
						this.ViewState.SetItemDirty(text, true);
					}
					else if (!defaultValueAttribute.Value.Equals(this.ViewState[text]))
					{
						this.ViewState.SetItemDirty(text, true);
					}
				}
			}
		}

		// Token: 0x0600E1C8 RID: 57800 RVA: 0x00323204 File Offset: 0x00321404
		public override string ToString()
		{
			return string.Empty;
		}

		// Token: 0x0600E1C9 RID: 57801 RVA: 0x0032320B File Offset: 0x0032140B
		public void Dispose()
		{
			if (!this.isDisposed)
			{
				this.Dispose(true);
				this.isDisposed = true;
				GC.SuppressFinalize(this);
			}
		}

		// Token: 0x0600E1CA RID: 57802 RVA: 0x00323229 File Offset: 0x00321429
		protected virtual void Dispose(bool disposing)
		{
		}

		// Token: 0x0400416E RID: 16750
		private StateBag viewState;

		// Token: 0x0400416F RID: 16751
		private bool isDisposed;
	}
}
