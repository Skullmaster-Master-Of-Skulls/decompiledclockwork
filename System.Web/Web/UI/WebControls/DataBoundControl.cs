using System;
using System.Collections;
using System.ComponentModel;
using System.Security.Permissions;
using System.Web.UI.WebControls.Adapters;

namespace System.Web.UI.WebControls
{
	// Token: 0x020004BC RID: 1212
	[Designer("System.Web.UI.Design.WebControls.DataBoundControlDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public abstract class DataBoundControl : BaseDataBoundControl
	{
		// Token: 0x17000D07 RID: 3335
		// (get) Token: 0x06003990 RID: 14736 RVA: 0x000F3888 File Offset: 0x000F2888
		// (set) Token: 0x06003991 RID: 14737 RVA: 0x000F38B5 File Offset: 0x000F28B5
		[DefaultValue("")]
		[WebCategory("Data")]
		[WebSysDescription("DataBoundControl_DataMember")]
		[Themeable(false)]
		public virtual string DataMember
		{
			get
			{
				object obj = this.ViewState["DataMember"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["DataMember"] = value;
				this.OnDataPropertyChanged();
			}
		}

		// Token: 0x17000D08 RID: 3336
		// (get) Token: 0x06003992 RID: 14738 RVA: 0x000F38CE File Offset: 0x000F28CE
		// (set) Token: 0x06003993 RID: 14739 RVA: 0x000F38D6 File Offset: 0x000F28D6
		[IDReferenceProperty(typeof(DataSourceControl))]
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

		// Token: 0x17000D09 RID: 3337
		// (get) Token: 0x06003994 RID: 14740 RVA: 0x000F38DF File Offset: 0x000F28DF
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public IDataSource DataSourceObject
		{
			get
			{
				return this.GetDataSource();
			}
		}

		// Token: 0x17000D0A RID: 3338
		// (get) Token: 0x06003995 RID: 14741 RVA: 0x000F38E7 File Offset: 0x000F28E7
		protected DataSourceSelectArguments SelectArguments
		{
			get
			{
				if (this._arguments == null)
				{
					this._arguments = this.CreateDataSourceSelectArguments();
				}
				return this._arguments;
			}
		}

		// Token: 0x06003996 RID: 14742 RVA: 0x000F3904 File Offset: 0x000F2904
		private DataSourceView ConnectToDataSourceView()
		{
			if (this._currentViewValid && !base.DesignMode)
			{
				return this._currentView;
			}
			if (this._currentView != null && this._currentViewIsFromDataSourceID)
			{
				this._currentView.DataSourceViewChanged -= this.OnDataSourceViewChanged;
			}
			this._currentDataSource = this.GetDataSource();
			string dataMember = this.DataMember;
			if (this._currentDataSource == null)
			{
				this._currentDataSource = new ReadOnlyDataSource(this.DataSource, dataMember);
			}
			else if (this.DataSource != null)
			{
				throw new InvalidOperationException(SR.GetString("DataControl_MultipleDataSources", new object[]
				{
					this.ID
				}));
			}
			this._currentDataSourceValid = true;
			DataSourceView view = this._currentDataSource.GetView(dataMember);
			if (view == null)
			{
				throw new InvalidOperationException(SR.GetString("DataControl_ViewNotFound", new object[]
				{
					this.ID
				}));
			}
			this._currentViewIsFromDataSourceID = base.IsBoundUsingDataSourceID;
			this._currentView = view;
			if (this._currentView != null && this._currentViewIsFromDataSourceID)
			{
				this._currentView.DataSourceViewChanged += this.OnDataSourceViewChanged;
			}
			this._currentViewValid = true;
			return this._currentView;
		}

		// Token: 0x06003997 RID: 14743 RVA: 0x000F3A28 File Offset: 0x000F2A28
		protected virtual DataSourceSelectArguments CreateDataSourceSelectArguments()
		{
			return DataSourceSelectArguments.Empty;
		}

		// Token: 0x06003998 RID: 14744 RVA: 0x000F3A30 File Offset: 0x000F2A30
		protected virtual DataSourceView GetData()
		{
			return this.ConnectToDataSourceView();
		}

		// Token: 0x06003999 RID: 14745 RVA: 0x000F3A48 File Offset: 0x000F2A48
		protected virtual IDataSource GetDataSource()
		{
			if (!base.DesignMode && this._currentDataSourceValid && this._currentDataSource != null)
			{
				return this._currentDataSource;
			}
			IDataSource dataSource = null;
			string dataSourceID = this.DataSourceID;
			if (dataSourceID.Length != 0)
			{
				Control control = DataBoundControlHelper.FindControl(this, dataSourceID);
				if (control == null)
				{
					throw new HttpException(SR.GetString("DataControl_DataSourceDoesntExist", new object[]
					{
						this.ID,
						dataSourceID
					}));
				}
				dataSource = (control as IDataSource);
				if (dataSource == null)
				{
					throw new HttpException(SR.GetString("DataControl_DataSourceIDMustBeDataControl", new object[]
					{
						this.ID,
						dataSourceID
					}));
				}
			}
			return dataSource;
		}

		// Token: 0x0600399A RID: 14746 RVA: 0x000F3AE9 File Offset: 0x000F2AE9
		protected void MarkAsDataBound()
		{
			this.ViewState["_!DataBound"] = true;
		}

		// Token: 0x0600399B RID: 14747 RVA: 0x000F3B01 File Offset: 0x000F2B01
		protected override void OnDataPropertyChanged()
		{
			this._currentViewValid = false;
			this._currentDataSourceValid = false;
			base.OnDataPropertyChanged();
		}

		// Token: 0x0600399C RID: 14748 RVA: 0x000F3B17 File Offset: 0x000F2B17
		protected virtual void OnDataSourceViewChanged(object sender, EventArgs e)
		{
			if (!this._ignoreDataSourceViewChanged)
			{
				base.RequiresDataBinding = true;
			}
		}

		// Token: 0x0600399D RID: 14749 RVA: 0x000F3B28 File Offset: 0x000F2B28
		private void OnDataSourceViewSelectCallback(IEnumerable data)
		{
			this._ignoreDataSourceViewChanged = false;
			if (this.DataSourceID.Length > 0)
			{
				this.OnDataBinding(EventArgs.Empty);
			}
			if (this._adapter != null)
			{
				DataBoundControlAdapter dataBoundControlAdapter = this._adapter as DataBoundControlAdapter;
				if (dataBoundControlAdapter != null)
				{
					dataBoundControlAdapter.PerformDataBinding(data);
				}
				else
				{
					this.PerformDataBinding(data);
				}
			}
			else
			{
				this.PerformDataBinding(data);
			}
			this.OnDataBound(EventArgs.Empty);
		}

		// Token: 0x0600399E RID: 14750 RVA: 0x000F3B90 File Offset: 0x000F2B90
		protected internal override void OnLoad(EventArgs e)
		{
			base.ConfirmInitState();
			this.ConnectToDataSourceView();
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

		// Token: 0x0600399F RID: 14751 RVA: 0x000F3BF8 File Offset: 0x000F2BF8
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

		// Token: 0x060039A0 RID: 14752 RVA: 0x000F3C53 File Offset: 0x000F2C53
		protected internal virtual void PerformDataBinding(IEnumerable data)
		{
		}

		// Token: 0x060039A1 RID: 14753 RVA: 0x000F3C58 File Offset: 0x000F2C58
		protected override void PerformSelect()
		{
			if (this.DataSourceID.Length == 0)
			{
				this.OnDataBinding(EventArgs.Empty);
			}
			DataSourceView data = this.GetData();
			this._arguments = this.CreateDataSourceSelectArguments();
			this._ignoreDataSourceViewChanged = true;
			base.RequiresDataBinding = false;
			this.MarkAsDataBound();
			data.Select(this._arguments, new DataSourceViewSelectCallback(this.OnDataSourceViewSelectCallback));
		}

		// Token: 0x060039A2 RID: 14754 RVA: 0x000F3CBC File Offset: 0x000F2CBC
		protected override void ValidateDataSource(object dataSource)
		{
			if (dataSource == null || dataSource is IListSource || dataSource is IEnumerable || dataSource is IDataSource)
			{
				return;
			}
			throw new InvalidOperationException(SR.GetString("DataBoundControl_InvalidDataSourceType"));
		}

		// Token: 0x0400262E RID: 9774
		private const string DataBoundViewStateKey = "_!DataBound";

		// Token: 0x0400262F RID: 9775
		private DataSourceView _currentView;

		// Token: 0x04002630 RID: 9776
		private bool _currentViewIsFromDataSourceID;

		// Token: 0x04002631 RID: 9777
		private bool _currentViewValid;

		// Token: 0x04002632 RID: 9778
		private IDataSource _currentDataSource;

		// Token: 0x04002633 RID: 9779
		private bool _currentDataSourceValid;

		// Token: 0x04002634 RID: 9780
		private DataSourceSelectArguments _arguments;

		// Token: 0x04002635 RID: 9781
		private bool _pagePreLoadFired;

		// Token: 0x04002636 RID: 9782
		private bool _ignoreDataSourceViewChanged;
	}
}
