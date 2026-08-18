using System;
using System.Collections;
using System.ComponentModel;
using System.Web.UI.WebControls.Adapters;

namespace System.Web.UI.WebControls
{
	// Token: 0x020003AD RID: 941
	[Designer("System.Web.UI.Design.WebControls.DataBoundControlDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public abstract class DataBoundControl : BaseDataBoundControl
	{
		// Token: 0x17000CF5 RID: 3317
		// (get) Token: 0x06002D6A RID: 11626 RVA: 0x00094858 File Offset: 0x00092A58
		// (set) Token: 0x06002D6B RID: 11627 RVA: 0x00094885 File Offset: 0x00092A85
		[DefaultValue("")]
		[Themeable(false)]
		[WebCategory("Data")]
		[WebSysDescription("DataBoundControl_DataMember")]
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

		// Token: 0x17000CF6 RID: 3318
		// (get) Token: 0x06002D6C RID: 11628 RVA: 0x0009489E File Offset: 0x00092A9E
		protected override bool IsUsingModelBinders
		{
			get
			{
				return !string.IsNullOrEmpty(this.SelectMethod);
			}
		}

		// Token: 0x17000CF7 RID: 3319
		// (get) Token: 0x06002D6D RID: 11629 RVA: 0x000948AE File Offset: 0x00092AAE
		// (set) Token: 0x06002D6E RID: 11630 RVA: 0x000948CA File Offset: 0x00092ACA
		private ModelDataSource ModelDataSource
		{
			get
			{
				if (this._modelDataSource == null)
				{
					this._modelDataSource = new ModelDataSource(this);
				}
				return this._modelDataSource;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this._modelDataSource = value;
			}
		}

		// Token: 0x14000068 RID: 104
		// (add) Token: 0x06002D6F RID: 11631 RVA: 0x000948E1 File Offset: 0x00092AE1
		// (remove) Token: 0x06002D70 RID: 11632 RVA: 0x000948F4 File Offset: 0x00092AF4
		[WebCategory("Data")]
		[WebSysDescription("DataBoundControl_OnCreatingModelDataSource")]
		public event CreatingModelDataSourceEventHandler CreatingModelDataSource
		{
			add
			{
				base.Events.AddHandler(DataBoundControl.EventCreatingModelDataSource, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataBoundControl.EventCreatingModelDataSource, value);
			}
		}

		// Token: 0x06002D71 RID: 11633 RVA: 0x00094908 File Offset: 0x00092B08
		protected virtual void OnCreatingModelDataSource(CreatingModelDataSourceEventArgs e)
		{
			CreatingModelDataSourceEventHandler creatingModelDataSourceEventHandler = base.Events[DataBoundControl.EventCreatingModelDataSource] as CreatingModelDataSourceEventHandler;
			if (creatingModelDataSourceEventHandler != null)
			{
				creatingModelDataSourceEventHandler(this, e);
			}
		}

		// Token: 0x17000CF8 RID: 3320
		// (get) Token: 0x06002D72 RID: 11634 RVA: 0x00094936 File Offset: 0x00092B36
		// (set) Token: 0x06002D73 RID: 11635 RVA: 0x00094947 File Offset: 0x00092B47
		[DefaultValue("")]
		[Themeable(false)]
		[WebCategory("Data")]
		[WebSysDescription("DataBoundControl_ItemType")]
		public virtual string ItemType
		{
			get
			{
				return this._itemType ?? string.Empty;
			}
			set
			{
				if (!string.Equals(this._itemType, value, StringComparison.OrdinalIgnoreCase))
				{
					this._itemType = value;
					this.OnDataPropertyChanged();
				}
			}
		}

		// Token: 0x17000CF9 RID: 3321
		// (get) Token: 0x06002D74 RID: 11636 RVA: 0x00094965 File Offset: 0x00092B65
		// (set) Token: 0x06002D75 RID: 11637 RVA: 0x00094976 File Offset: 0x00092B76
		[DefaultValue("")]
		[Themeable(false)]
		[WebCategory("Data")]
		[WebSysDescription("DataBoundControl_SelectMethod")]
		public virtual string SelectMethod
		{
			get
			{
				return this._selectMethod ?? string.Empty;
			}
			set
			{
				if (!string.Equals(this._selectMethod, value, StringComparison.OrdinalIgnoreCase))
				{
					this._selectMethod = value;
					this.OnDataPropertyChanged();
				}
			}
		}

		// Token: 0x14000069 RID: 105
		// (add) Token: 0x06002D76 RID: 11638 RVA: 0x00094994 File Offset: 0x00092B94
		// (remove) Token: 0x06002D77 RID: 11639 RVA: 0x000949A7 File Offset: 0x00092BA7
		[WebCategory("Data")]
		[WebSysDescription("DataBoundControl_CallingDataMethods")]
		public event CallingDataMethodsEventHandler CallingDataMethods
		{
			add
			{
				base.Events.AddHandler(DataBoundControl.EventCallingDataMethods, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataBoundControl.EventCallingDataMethods, value);
				if (this._modelDataSource != null)
				{
					this._modelDataSource.CallingDataMethods -= value;
				}
			}
		}

		// Token: 0x17000CFA RID: 3322
		// (get) Token: 0x06002D78 RID: 11640 RVA: 0x000949CE File Offset: 0x00092BCE
		// (set) Token: 0x06002D79 RID: 11641 RVA: 0x000949D6 File Offset: 0x00092BD6
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

		// Token: 0x17000CFB RID: 3323
		// (get) Token: 0x06002D7A RID: 11642 RVA: 0x000949DF File Offset: 0x00092BDF
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public IDataSource DataSourceObject
		{
			get
			{
				return this.GetDataSource();
			}
		}

		// Token: 0x17000CFC RID: 3324
		// (get) Token: 0x06002D7B RID: 11643 RVA: 0x000949E7 File Offset: 0x00092BE7
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

		// Token: 0x06002D7C RID: 11644 RVA: 0x00094A04 File Offset: 0x00092C04
		internal void EnsureSingleDataSource()
		{
			if (!base.DesignMode)
			{
				if (this.IsUsingModelBinders)
				{
					if (this.DataSourceID.Length != 0 || this.DataSource != null)
					{
						throw new InvalidOperationException(SR.GetString("DataControl_ItemType_MultipleDataSources", new object[]
						{
							this.ID
						}));
					}
				}
				else if (this.DataSourceID.Length != 0 && this.DataSource != null)
				{
					throw new InvalidOperationException(SR.GetString("DataControl_MultipleDataSources", new object[]
					{
						this.ID
					}));
				}
			}
		}

		// Token: 0x06002D7D RID: 11645 RVA: 0x00094A8C File Offset: 0x00092C8C
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
			this.EnsureSingleDataSource();
			this._currentDataSource = this.GetDataSource();
			string dataMember = this.DataMember;
			if (this._currentDataSource == null)
			{
				this._currentDataSource = new ReadOnlyDataSource(this.DataSource, dataMember);
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
			this._currentViewIsFromDataSourceID = base.IsDataBindingAutomatic;
			this._currentView = view;
			if (this._currentView != null && this._currentViewIsFromDataSourceID)
			{
				this._currentView.DataSourceViewChanged += this.OnDataSourceViewChanged;
			}
			this._currentViewValid = true;
			return this._currentView;
		}

		// Token: 0x06002D7E RID: 11646 RVA: 0x00085B41 File Offset: 0x00083D41
		protected virtual DataSourceSelectArguments CreateDataSourceSelectArguments()
		{
			return DataSourceSelectArguments.Empty;
		}

		// Token: 0x06002D7F RID: 11647 RVA: 0x00094B8C File Offset: 0x00092D8C
		protected virtual DataSourceView GetData()
		{
			return this.ConnectToDataSourceView();
		}

		// Token: 0x06002D80 RID: 11648 RVA: 0x00094BA4 File Offset: 0x00092DA4
		protected virtual IDataSource GetDataSource()
		{
			if (!base.DesignMode && this.IsUsingModelBinders)
			{
				CreatingModelDataSourceEventArgs creatingModelDataSourceEventArgs = new CreatingModelDataSourceEventArgs();
				this.OnCreatingModelDataSource(creatingModelDataSourceEventArgs);
				if (creatingModelDataSourceEventArgs.ModelDataSource != null)
				{
					this.ModelDataSource = creatingModelDataSourceEventArgs.ModelDataSource;
				}
				this.UpdateModelDataSourceProperties(this.ModelDataSource);
				CallingDataMethodsEventHandler callingDataMethodsEventHandler = base.Events[DataBoundControl.EventCallingDataMethods] as CallingDataMethodsEventHandler;
				if (callingDataMethodsEventHandler != null)
				{
					this.ModelDataSource.CallingDataMethods += callingDataMethodsEventHandler;
				}
				return this.ModelDataSource;
			}
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

		// Token: 0x06002D81 RID: 11649 RVA: 0x00094CA9 File Offset: 0x00092EA9
		protected void MarkAsDataBound()
		{
			this.ViewState["_!DataBound"] = true;
		}

		// Token: 0x06002D82 RID: 11650 RVA: 0x00094CC1 File Offset: 0x00092EC1
		protected override void OnDataPropertyChanged()
		{
			this._currentViewValid = false;
			this._currentDataSourceValid = false;
			base.OnDataPropertyChanged();
		}

		// Token: 0x06002D83 RID: 11651 RVA: 0x00094CD7 File Offset: 0x00092ED7
		protected virtual void OnDataSourceViewChanged(object sender, EventArgs e)
		{
			if (!this._ignoreDataSourceViewChanged)
			{
				base.RequiresDataBinding = true;
			}
		}

		// Token: 0x06002D84 RID: 11652 RVA: 0x00094CE8 File Offset: 0x00092EE8
		private void OnDataSourceViewSelectCallback(IEnumerable data)
		{
			this._ignoreDataSourceViewChanged = false;
			if (base.IsDataBindingAutomatic)
			{
				this.OnDataBinding(EventArgs.Empty);
			}
			if (base.AdapterInternal != null)
			{
				DataBoundControlAdapter dataBoundControlAdapter = base.AdapterInternal as DataBoundControlAdapter;
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

		// Token: 0x06002D85 RID: 11653 RVA: 0x00094D4C File Offset: 0x00092F4C
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

		// Token: 0x06002D86 RID: 11654 RVA: 0x00094DB4 File Offset: 0x00092FB4
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

		// Token: 0x06002D87 RID: 11655 RVA: 0x00006164 File Offset: 0x00004364
		protected internal virtual void PerformDataBinding(IEnumerable data)
		{
		}

		// Token: 0x06002D88 RID: 11656 RVA: 0x00094E10 File Offset: 0x00093010
		protected override void PerformSelect()
		{
			if (!base.IsDataBindingAutomatic)
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

		// Token: 0x06002D89 RID: 11657 RVA: 0x00094E6F File Offset: 0x0009306F
		protected override void ValidateDataSource(object dataSource)
		{
			if (dataSource == null || dataSource is IListSource || dataSource is IEnumerable || dataSource is IDataSource)
			{
				return;
			}
			throw new InvalidOperationException(SR.GetString("DataBoundControl_InvalidDataSourceType"));
		}

		// Token: 0x06002D8A RID: 11658 RVA: 0x00094E9C File Offset: 0x0009309C
		protected override void LoadViewState(object savedState)
		{
			if (this.IsUsingModelBinders)
			{
				Pair pair = (Pair)savedState;
				if (savedState == null)
				{
					base.LoadViewState(null);
					return;
				}
				base.LoadViewState(pair.First);
				if (pair.Second != null)
				{
					((IStateManager)this.ModelDataSource).LoadViewState(pair.Second);
					return;
				}
			}
			else
			{
				base.LoadViewState(savedState);
			}
		}

		// Token: 0x06002D8B RID: 11659 RVA: 0x00094EF0 File Offset: 0x000930F0
		protected override object SaveViewState()
		{
			object obj = base.SaveViewState();
			if (!this.IsUsingModelBinders)
			{
				return obj;
			}
			Pair pair = new Pair();
			pair.First = obj;
			pair.Second = ((IStateManager)this.ModelDataSource).SaveViewState();
			if (pair.First == null && pair.Second == null)
			{
				return null;
			}
			return pair;
		}

		// Token: 0x06002D8C RID: 11660 RVA: 0x00094F3F File Offset: 0x0009313F
		protected override void TrackViewState()
		{
			base.TrackViewState();
			if (this.IsUsingModelBinders)
			{
				((IStateManager)this.ModelDataSource).TrackViewState();
			}
		}

		// Token: 0x06002D8D RID: 11661 RVA: 0x00094F5A File Offset: 0x0009315A
		internal virtual void UpdateModelDataSourceProperties(ModelDataSource modelDataSource)
		{
			modelDataSource.UpdateProperties(this.ItemType, this.SelectMethod);
		}

		// Token: 0x04001F83 RID: 8067
		private DataSourceView _currentView;

		// Token: 0x04001F84 RID: 8068
		private bool _currentViewIsFromDataSourceID;

		// Token: 0x04001F85 RID: 8069
		private bool _currentViewValid;

		// Token: 0x04001F86 RID: 8070
		private IDataSource _currentDataSource;

		// Token: 0x04001F87 RID: 8071
		private bool _currentDataSourceValid;

		// Token: 0x04001F88 RID: 8072
		private DataSourceSelectArguments _arguments;

		// Token: 0x04001F89 RID: 8073
		private bool _pagePreLoadFired;

		// Token: 0x04001F8A RID: 8074
		private bool _ignoreDataSourceViewChanged;

		// Token: 0x04001F8B RID: 8075
		private string _itemType;

		// Token: 0x04001F8C RID: 8076
		private string _selectMethod;

		// Token: 0x04001F8D RID: 8077
		private ModelDataSource _modelDataSource;

		// Token: 0x04001F8E RID: 8078
		private const string DataBoundViewStateKey = "_!DataBound";

		// Token: 0x04001F8F RID: 8079
		private static readonly object EventCreatingModelDataSource = new object();

		// Token: 0x04001F90 RID: 8080
		private static readonly object EventCallingDataMethods = new object();
	}
}
