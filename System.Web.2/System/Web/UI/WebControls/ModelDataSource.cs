using System;
using System.Collections;

namespace System.Web.UI.WebControls
{
	// Token: 0x0200047A RID: 1146
	public class ModelDataSource : IDataSource, IStateManager
	{
		// Token: 0x0600389A RID: 14490 RVA: 0x000B82B6 File Offset: 0x000B64B6
		public ModelDataSource(Control dataControl)
		{
			if (dataControl == null)
			{
				throw new ArgumentNullException("dataControl");
			}
			this.DataControl = dataControl;
		}

		// Token: 0x0600389B RID: 14491 RVA: 0x000B82D3 File Offset: 0x000B64D3
		public void UpdateProperties(string modelTypeName, string selectMethod)
		{
			this.UpdateProperties(modelTypeName, selectMethod, string.Empty, string.Empty, string.Empty, string.Empty);
		}

		// Token: 0x0600389C RID: 14492 RVA: 0x000B82F1 File Offset: 0x000B64F1
		public void UpdateProperties(string modelTypeName, string selectMethod, string updateMethod, string insertMethod, string deleteMethod, string dataKeyName)
		{
			this.View.UpdateProperties(modelTypeName, selectMethod, updateMethod, insertMethod, deleteMethod, dataKeyName);
		}

		// Token: 0x17001092 RID: 4242
		// (get) Token: 0x0600389D RID: 14493 RVA: 0x000B8307 File Offset: 0x000B6507
		// (set) Token: 0x0600389E RID: 14494 RVA: 0x000B830F File Offset: 0x000B650F
		public Control DataControl { get; private set; }

		// Token: 0x140000BD RID: 189
		// (add) Token: 0x0600389F RID: 14495 RVA: 0x000B8318 File Offset: 0x000B6518
		// (remove) Token: 0x060038A0 RID: 14496 RVA: 0x000B8326 File Offset: 0x000B6526
		public event CallingDataMethodsEventHandler CallingDataMethods
		{
			add
			{
				this.View.CallingDataMethods += value;
			}
			remove
			{
				this.View.CallingDataMethods -= value;
			}
		}

		// Token: 0x17001093 RID: 4243
		// (get) Token: 0x060038A1 RID: 14497 RVA: 0x000B8334 File Offset: 0x000B6534
		public virtual ModelDataSourceView View
		{
			get
			{
				if (this._view == null)
				{
					this._view = new ModelDataSourceView(this);
				}
				return this._view;
			}
		}

		// Token: 0x060038A2 RID: 14498 RVA: 0x000B8350 File Offset: 0x000B6550
		protected virtual bool IsTrackingViewState()
		{
			return ((IStateManager)this.View).IsTrackingViewState;
		}

		// Token: 0x060038A3 RID: 14499 RVA: 0x000B835D File Offset: 0x000B655D
		protected virtual void LoadViewState(object savedState)
		{
			((IStateManager)this.View).LoadViewState(savedState);
		}

		// Token: 0x060038A4 RID: 14500 RVA: 0x000B836B File Offset: 0x000B656B
		protected virtual object SaveViewState()
		{
			return ((IStateManager)this.View).SaveViewState();
		}

		// Token: 0x060038A5 RID: 14501 RVA: 0x000B8378 File Offset: 0x000B6578
		protected virtual void TrackViewState()
		{
			((IStateManager)this.View).TrackViewState();
		}

		// Token: 0x060038A6 RID: 14502 RVA: 0x000B8385 File Offset: 0x000B6585
		private DataSourceView GetView(string viewName)
		{
			return this.View;
		}

		// Token: 0x060038A7 RID: 14503 RVA: 0x000B838D File Offset: 0x000B658D
		private ICollection GetViewNames()
		{
			if (this._viewNames == null)
			{
				this._viewNames = new string[]
				{
					"DefaultView"
				};
			}
			return this._viewNames;
		}

		// Token: 0x140000BE RID: 190
		// (add) Token: 0x060038A8 RID: 14504 RVA: 0x000B83B1 File Offset: 0x000B65B1
		// (remove) Token: 0x060038A9 RID: 14505 RVA: 0x000B83CA File Offset: 0x000B65CA
		event EventHandler DataSourceChanged;

		// Token: 0x060038AA RID: 14506 RVA: 0x000B83E3 File Offset: 0x000B65E3
		DataSourceView IDataSource.GetView(string viewName)
		{
			return this.GetView(viewName);
		}

		// Token: 0x060038AB RID: 14507 RVA: 0x000B83EC File Offset: 0x000B65EC
		ICollection IDataSource.GetViewNames()
		{
			return this.GetViewNames();
		}

		// Token: 0x17001094 RID: 4244
		// (get) Token: 0x060038AC RID: 14508 RVA: 0x000B83F4 File Offset: 0x000B65F4
		bool IStateManager.IsTrackingViewState
		{
			get
			{
				return this.IsTrackingViewState();
			}
		}

		// Token: 0x060038AD RID: 14509 RVA: 0x000B83FC File Offset: 0x000B65FC
		void IStateManager.LoadViewState(object savedState)
		{
			this.LoadViewState(savedState);
		}

		// Token: 0x060038AE RID: 14510 RVA: 0x000B8405 File Offset: 0x000B6605
		object IStateManager.SaveViewState()
		{
			return this.SaveViewState();
		}

		// Token: 0x060038AF RID: 14511 RVA: 0x000B840D File Offset: 0x000B660D
		void IStateManager.TrackViewState()
		{
			this.TrackViewState();
		}

		// Token: 0x0400228F RID: 8847
		internal const string DefaultViewName = "DefaultView";

		// Token: 0x04002290 RID: 8848
		private ModelDataSourceView _view;

		// Token: 0x04002291 RID: 8849
		private ICollection _viewNames;

		// Token: 0x04002292 RID: 8850
		private EventHandler DataSourceChanged;
	}
}
