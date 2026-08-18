using System;
using System.ComponentModel;
using System.Web.UI.WebControls.Adapters;

namespace System.Web.UI.WebControls
{
	// Token: 0x0200042F RID: 1071
	[Designer("System.Web.UI.Design.WebControls.HierarchicalDataBoundControlDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public abstract class HierarchicalDataBoundControl : BaseDataBoundControl
	{
		// Token: 0x17000F0F RID: 3855
		// (get) Token: 0x060033F2 RID: 13298 RVA: 0x000949CE File Offset: 0x00092BCE
		// (set) Token: 0x060033F3 RID: 13299 RVA: 0x000949D6 File Offset: 0x00092BD6
		[IDReferenceProperty(typeof(HierarchicalDataSourceControl))]
		public override string DataSourceID
		{
			get
			{
				return base.DataSourceID;
			}
			set
			{
				base.DataSourceID = value;
			}
		}

		// Token: 0x060033F4 RID: 13300 RVA: 0x000A9500 File Offset: 0x000A7700
		private IHierarchicalDataSource ConnectToHierarchicalDataSource()
		{
			if (!this._currentDataSourceValid || base.DesignMode)
			{
				if (this._currentHierarchicalDataSource != null && this._currentDataSourceIsFromControl)
				{
					this._currentHierarchicalDataSource.DataSourceChanged -= this.OnDataSourceChanged;
				}
				this._currentHierarchicalDataSource = this.GetDataSource();
				this._currentDataSourceIsFromControl = base.IsBoundUsingDataSourceID;
				if (this._currentHierarchicalDataSource == null)
				{
					this._currentHierarchicalDataSource = new ReadOnlyHierarchicalDataSource(this.DataSource);
				}
				else if (this.DataSource != null)
				{
					throw new InvalidOperationException(SR.GetString("DataControl_MultipleDataSources", new object[]
					{
						this.ID
					}));
				}
				this._currentDataSourceValid = true;
				if (this._currentHierarchicalDataSource != null && this._currentDataSourceIsFromControl)
				{
					this._currentHierarchicalDataSource.DataSourceChanged += this.OnDataSourceChanged;
				}
				return this._currentHierarchicalDataSource;
			}
			if (!this._currentDataSourceIsFromControl && this.DataSourceID != null && this.DataSourceID.Length != 0)
			{
				throw new InvalidOperationException(SR.GetString("DataControl_MultipleDataSources", new object[]
				{
					this.ID
				}));
			}
			return this._currentHierarchicalDataSource;
		}

		// Token: 0x060033F5 RID: 13301 RVA: 0x000A9618 File Offset: 0x000A7818
		protected virtual HierarchicalDataSourceView GetData(string viewPath)
		{
			IHierarchicalDataSource hierarchicalDataSource = this.ConnectToHierarchicalDataSource();
			HierarchicalDataSourceView hierarchicalView = hierarchicalDataSource.GetHierarchicalView(viewPath);
			if (hierarchicalView == null)
			{
				throw new InvalidOperationException(SR.GetString("HierarchicalDataControl_ViewNotFound", new object[]
				{
					this.ID
				}));
			}
			return hierarchicalView;
		}

		// Token: 0x060033F6 RID: 13302 RVA: 0x000A965C File Offset: 0x000A785C
		protected virtual IHierarchicalDataSource GetDataSource()
		{
			if (!base.DesignMode && this._currentDataSourceValid && this._currentHierarchicalDataSource != null)
			{
				return this._currentHierarchicalDataSource;
			}
			IHierarchicalDataSource hierarchicalDataSource = null;
			string dataSourceID = this.DataSourceID;
			if (dataSourceID.Length != 0)
			{
				Control control = DataBoundControlHelper.FindControl(this, dataSourceID);
				if (control == null)
				{
					throw new HttpException(SR.GetString("HierarchicalDataControl_DataSourceDoesntExist", new object[]
					{
						this.ID,
						dataSourceID
					}));
				}
				hierarchicalDataSource = (control as IHierarchicalDataSource);
				if (hierarchicalDataSource == null)
				{
					throw new HttpException(SR.GetString("HierarchicalDataControl_DataSourceIDMustBeHierarchicalDataControl", new object[]
					{
						this.ID,
						dataSourceID
					}));
				}
			}
			return hierarchicalDataSource;
		}

		// Token: 0x060033F7 RID: 13303 RVA: 0x000A96F5 File Offset: 0x000A78F5
		protected void MarkAsDataBound()
		{
			this.ViewState["_!DataBound"] = true;
		}

		// Token: 0x060033F8 RID: 13304 RVA: 0x000A970D File Offset: 0x000A790D
		protected override void OnDataPropertyChanged()
		{
			this._currentDataSourceValid = false;
			base.OnDataPropertyChanged();
		}

		// Token: 0x060033F9 RID: 13305 RVA: 0x000A971C File Offset: 0x000A791C
		protected virtual void OnDataSourceChanged(object sender, EventArgs e)
		{
			base.RequiresDataBinding = true;
		}

		// Token: 0x060033FA RID: 13306 RVA: 0x000A9728 File Offset: 0x000A7928
		protected internal override void OnLoad(EventArgs e)
		{
			base.ConfirmInitState();
			this.ConnectToHierarchicalDataSource();
			if (this.Page != null && !this._pagePreLoadFired && this.ViewState["_!DataBound"] == null)
			{
				if (!this.Page.IsPostBack)
				{
					base.RequiresDataBinding = true;
				}
				else if (base.IsViewStateEnabled)
				{
					base.RequiresDataBinding = true;
				}
			}
			base.OnLoad(e);
		}

		// Token: 0x060033FB RID: 13307 RVA: 0x000A9790 File Offset: 0x000A7990
		protected override void OnPagePreLoad(object sender, EventArgs e)
		{
			base.OnPagePreLoad(sender, e);
			if (this.Page != null)
			{
				if (!this.Page.IsPostBack)
				{
					base.RequiresDataBinding = true;
				}
				else if (base.IsViewStateEnabled && this.ViewState["_!DataBound"] == null)
				{
					base.RequiresDataBinding = true;
				}
			}
			this._pagePreLoadFired = true;
		}

		// Token: 0x060033FC RID: 13308 RVA: 0x00006164 File Offset: 0x00004364
		protected internal virtual void PerformDataBinding()
		{
		}

		// Token: 0x060033FD RID: 13309 RVA: 0x000A97EC File Offset: 0x000A79EC
		protected override void PerformSelect()
		{
			this.OnDataBinding(EventArgs.Empty);
			if (base.AdapterInternal != null)
			{
				HierarchicalDataBoundControlAdapter hierarchicalDataBoundControlAdapter = base.AdapterInternal as HierarchicalDataBoundControlAdapter;
				if (hierarchicalDataBoundControlAdapter != null)
				{
					hierarchicalDataBoundControlAdapter.PerformDataBinding();
				}
				else
				{
					this.PerformDataBinding();
				}
			}
			else
			{
				this.PerformDataBinding();
			}
			base.RequiresDataBinding = false;
			this.MarkAsDataBound();
			this.OnDataBound(EventArgs.Empty);
		}

		// Token: 0x060033FE RID: 13310 RVA: 0x000A9849 File Offset: 0x000A7A49
		protected override void ValidateDataSource(object dataSource)
		{
			if (dataSource == null || dataSource is IHierarchicalEnumerable || dataSource is IHierarchicalDataSource)
			{
				return;
			}
			throw new InvalidOperationException(SR.GetString("HierarchicalDataBoundControl_InvalidDataSource"));
		}

		// Token: 0x04002184 RID: 8580
		private IHierarchicalDataSource _currentHierarchicalDataSource;

		// Token: 0x04002185 RID: 8581
		private bool _currentDataSourceIsFromControl;

		// Token: 0x04002186 RID: 8582
		private bool _currentDataSourceValid;

		// Token: 0x04002187 RID: 8583
		private bool _pagePreLoadFired;

		// Token: 0x04002188 RID: 8584
		private const string DataBoundViewStateKey = "_!DataBound";
	}
}
