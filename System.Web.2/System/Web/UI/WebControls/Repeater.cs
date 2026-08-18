using System;
using System.Collections;
using System.ComponentModel;
using System.Web.Util;

namespace System.Web.UI.WebControls
{
	// Token: 0x020004B2 RID: 1202
	[DefaultEvent("ItemCommand")]
	[DefaultProperty("DataSource")]
	[Designer("System.Web.UI.Design.WebControls.RepeaterDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[ParseChildren(true)]
	[PersistChildren(false)]
	public class Repeater : Control, INamingContainer
	{
		// Token: 0x17001185 RID: 4485
		// (get) Token: 0x06003BFD RID: 15357 RVA: 0x000C2C0C File Offset: 0x000C0E0C
		private bool IsUsingModelBinders
		{
			get
			{
				return !string.IsNullOrEmpty(this.SelectMethod);
			}
		}

		// Token: 0x06003BFE RID: 15358 RVA: 0x000C2C1C File Offset: 0x000C0E1C
		private void UpdateModelDataSourceProperties(ModelDataSource modelDataSource)
		{
			if (modelDataSource == null)
			{
				throw new ArgumentNullException("modelDataSource");
			}
			modelDataSource.UpdateProperties(this.ItemType, this.SelectMethod);
		}

		// Token: 0x17001186 RID: 4486
		// (get) Token: 0x06003BFF RID: 15359 RVA: 0x000C2C3E File Offset: 0x000C0E3E
		// (set) Token: 0x06003C00 RID: 15360 RVA: 0x000C2C5A File Offset: 0x000C0E5A
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

		// Token: 0x06003C01 RID: 15361 RVA: 0x000C2C74 File Offset: 0x000C0E74
		protected virtual void OnCreatingModelDataSource(CreatingModelDataSourceEventArgs e)
		{
			CreatingModelDataSourceEventHandler creatingModelDataSourceEventHandler = base.Events[Repeater.EventCreatingModelDataSource] as CreatingModelDataSourceEventHandler;
			if (creatingModelDataSourceEventHandler != null)
			{
				creatingModelDataSourceEventHandler(this, e);
			}
		}

		// Token: 0x140000E3 RID: 227
		// (add) Token: 0x06003C02 RID: 15362 RVA: 0x000C2CA2 File Offset: 0x000C0EA2
		// (remove) Token: 0x06003C03 RID: 15363 RVA: 0x000C2CB5 File Offset: 0x000C0EB5
		[WebCategory("Data")]
		[WebSysDescription("DataBoundControl_OnCreatingModelDataSource")]
		public event CreatingModelDataSourceEventHandler CreatingModelDataSource
		{
			add
			{
				base.Events.AddHandler(Repeater.EventCreatingModelDataSource, value);
			}
			remove
			{
				base.Events.RemoveHandler(Repeater.EventCreatingModelDataSource, value);
			}
		}

		// Token: 0x17001187 RID: 4487
		// (get) Token: 0x06003C04 RID: 15364 RVA: 0x000C2CC8 File Offset: 0x000C0EC8
		// (set) Token: 0x06003C05 RID: 15365 RVA: 0x000C2CD9 File Offset: 0x000C0ED9
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

		// Token: 0x17001188 RID: 4488
		// (get) Token: 0x06003C06 RID: 15366 RVA: 0x000C2CF7 File Offset: 0x000C0EF7
		// (set) Token: 0x06003C07 RID: 15367 RVA: 0x000C2D08 File Offset: 0x000C0F08
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

		// Token: 0x140000E4 RID: 228
		// (add) Token: 0x06003C08 RID: 15368 RVA: 0x000C2D26 File Offset: 0x000C0F26
		// (remove) Token: 0x06003C09 RID: 15369 RVA: 0x000C2D39 File Offset: 0x000C0F39
		[WebCategory("Data")]
		[WebSysDescription("DataBoundControl_CallingDataMethods")]
		public event CallingDataMethodsEventHandler CallingDataMethods
		{
			add
			{
				base.Events.AddHandler(Repeater.EventCallingDataMethods, value);
			}
			remove
			{
				base.Events.RemoveHandler(Repeater.EventCallingDataMethods, value);
				if (this._modelDataSource != null)
				{
					this._modelDataSource.CallingDataMethods -= value;
				}
			}
		}

		// Token: 0x17001189 RID: 4489
		// (get) Token: 0x06003C0A RID: 15370 RVA: 0x000C2D60 File Offset: 0x000C0F60
		// (set) Token: 0x06003C0B RID: 15371 RVA: 0x000C2D68 File Offset: 0x000C0F68
		[Browsable(false)]
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[TemplateContainer(typeof(RepeaterItem))]
		[WebSysDescription("Repeater_AlternatingItemTemplate")]
		public virtual ITemplate AlternatingItemTemplate
		{
			get
			{
				return this.alternatingItemTemplate;
			}
			set
			{
				this.alternatingItemTemplate = value;
			}
		}

		// Token: 0x1700118A RID: 4490
		// (get) Token: 0x06003C0C RID: 15372 RVA: 0x000856CA File Offset: 0x000838CA
		public override ControlCollection Controls
		{
			get
			{
				this.EnsureChildControls();
				return base.Controls;
			}
		}

		// Token: 0x1700118B RID: 4491
		// (get) Token: 0x06003C0D RID: 15373 RVA: 0x000C2D74 File Offset: 0x000C0F74
		// (set) Token: 0x06003C0E RID: 15374 RVA: 0x000C2DA1 File Offset: 0x000C0FA1
		[DefaultValue("")]
		[WebCategory("Data")]
		[WebSysDescription("Repeater_DataMember")]
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

		// Token: 0x1700118C RID: 4492
		// (get) Token: 0x06003C0F RID: 15375 RVA: 0x000C2DBA File Offset: 0x000C0FBA
		// (set) Token: 0x06003C10 RID: 15376 RVA: 0x000C2DC4 File Offset: 0x000C0FC4
		[Bindable(true)]
		[WebCategory("Data")]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[WebSysDescription("BaseDataBoundControl_DataSource")]
		public virtual object DataSource
		{
			get
			{
				return this.dataSource;
			}
			set
			{
				if (value == null || value is IListSource || value is IEnumerable)
				{
					this.dataSource = value;
					this.OnDataPropertyChanged();
					return;
				}
				throw new ArgumentException(SR.GetString("Invalid_DataSource_Type", new object[]
				{
					this.ID
				}));
			}
		}

		// Token: 0x1700118D RID: 4493
		// (get) Token: 0x06003C11 RID: 15377 RVA: 0x000C2E10 File Offset: 0x000C1010
		// (set) Token: 0x06003C12 RID: 15378 RVA: 0x000C2E3D File Offset: 0x000C103D
		[DefaultValue("")]
		[IDReferenceProperty(typeof(DataSourceControl))]
		[WebCategory("Data")]
		[WebSysDescription("BaseDataBoundControl_DataSourceID")]
		public virtual string DataSourceID
		{
			get
			{
				object obj = this.ViewState["DataSourceID"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["DataSourceID"] = value;
				this.OnDataPropertyChanged();
			}
		}

		// Token: 0x1700118E RID: 4494
		// (get) Token: 0x06003C13 RID: 15379 RVA: 0x00075E05 File Offset: 0x00074005
		// (set) Token: 0x06003C14 RID: 15380 RVA: 0x00075E0D File Offset: 0x0007400D
		[Browsable(true)]
		public override bool EnableTheming
		{
			get
			{
				return base.EnableTheming;
			}
			set
			{
				base.EnableTheming = value;
			}
		}

		// Token: 0x1700118F RID: 4495
		// (get) Token: 0x06003C15 RID: 15381 RVA: 0x000C2E56 File Offset: 0x000C1056
		// (set) Token: 0x06003C16 RID: 15382 RVA: 0x000C2E5E File Offset: 0x000C105E
		[Browsable(false)]
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[TemplateContainer(typeof(RepeaterItem))]
		[WebSysDescription("Repeater_FooterTemplate")]
		public virtual ITemplate FooterTemplate
		{
			get
			{
				return this.footerTemplate;
			}
			set
			{
				this.footerTemplate = value;
			}
		}

		// Token: 0x17001190 RID: 4496
		// (get) Token: 0x06003C17 RID: 15383 RVA: 0x000C2E67 File Offset: 0x000C1067
		// (set) Token: 0x06003C18 RID: 15384 RVA: 0x000C2E6F File Offset: 0x000C106F
		[Browsable(false)]
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[TemplateContainer(typeof(RepeaterItem))]
		[WebSysDescription("WebControl_HeaderTemplate")]
		public virtual ITemplate HeaderTemplate
		{
			get
			{
				return this.headerTemplate;
			}
			set
			{
				this.headerTemplate = value;
			}
		}

		// Token: 0x17001191 RID: 4497
		// (get) Token: 0x06003C19 RID: 15385 RVA: 0x000C2E78 File Offset: 0x000C1078
		protected bool Initialized
		{
			get
			{
				return this._inited;
			}
		}

		// Token: 0x17001192 RID: 4498
		// (get) Token: 0x06003C1A RID: 15386 RVA: 0x000C2E80 File Offset: 0x000C1080
		protected bool IsBoundUsingDataSourceID
		{
			get
			{
				return this.DataSourceID.Length > 0;
			}
		}

		// Token: 0x17001193 RID: 4499
		// (get) Token: 0x06003C1B RID: 15387 RVA: 0x000C2E90 File Offset: 0x000C1090
		protected bool IsDataBindingAutomatic
		{
			get
			{
				return this.IsBoundUsingDataSourceID || this.IsUsingModelBinders;
			}
		}

		// Token: 0x17001194 RID: 4500
		// (get) Token: 0x06003C1C RID: 15388 RVA: 0x000C2EA2 File Offset: 0x000C10A2
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[WebSysDescription("Repeater_Items")]
		public virtual RepeaterItemCollection Items
		{
			get
			{
				if (this.itemsCollection == null)
				{
					if (this.itemsArray == null)
					{
						this.EnsureChildControls();
					}
					this.itemsCollection = new RepeaterItemCollection(this.itemsArray);
				}
				return this.itemsCollection;
			}
		}

		// Token: 0x17001195 RID: 4501
		// (get) Token: 0x06003C1D RID: 15389 RVA: 0x000C2ED1 File Offset: 0x000C10D1
		// (set) Token: 0x06003C1E RID: 15390 RVA: 0x000C2ED9 File Offset: 0x000C10D9
		[Browsable(false)]
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[TemplateContainer(typeof(RepeaterItem))]
		[WebSysDescription("Repeater_ItemTemplate")]
		public virtual ITemplate ItemTemplate
		{
			get
			{
				return this.itemTemplate;
			}
			set
			{
				this.itemTemplate = value;
			}
		}

		// Token: 0x17001196 RID: 4502
		// (get) Token: 0x06003C1F RID: 15391 RVA: 0x000C2EE2 File Offset: 0x000C10E2
		// (set) Token: 0x06003C20 RID: 15392 RVA: 0x000C2EEA File Offset: 0x000C10EA
		protected bool RequiresDataBinding
		{
			get
			{
				return this._requiresDataBinding;
			}
			set
			{
				this._requiresDataBinding = value;
			}
		}

		// Token: 0x17001197 RID: 4503
		// (get) Token: 0x06003C21 RID: 15393 RVA: 0x000C2EF3 File Offset: 0x000C10F3
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

		// Token: 0x17001198 RID: 4504
		// (get) Token: 0x06003C22 RID: 15394 RVA: 0x000C2F0F File Offset: 0x000C110F
		// (set) Token: 0x06003C23 RID: 15395 RVA: 0x000C2F17 File Offset: 0x000C1117
		[Browsable(false)]
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[TemplateContainer(typeof(RepeaterItem))]
		[WebSysDescription("Repeater_SeparatorTemplate")]
		public virtual ITemplate SeparatorTemplate
		{
			get
			{
				return this.separatorTemplate;
			}
			set
			{
				this.separatorTemplate = value;
			}
		}

		// Token: 0x140000E5 RID: 229
		// (add) Token: 0x06003C24 RID: 15396 RVA: 0x000C2F20 File Offset: 0x000C1120
		// (remove) Token: 0x06003C25 RID: 15397 RVA: 0x000C2F33 File Offset: 0x000C1133
		[WebCategory("Action")]
		[WebSysDescription("Repeater_OnItemCommand")]
		public event RepeaterCommandEventHandler ItemCommand
		{
			add
			{
				base.Events.AddHandler(Repeater.EventItemCommand, value);
			}
			remove
			{
				base.Events.RemoveHandler(Repeater.EventItemCommand, value);
			}
		}

		// Token: 0x140000E6 RID: 230
		// (add) Token: 0x06003C26 RID: 15398 RVA: 0x000C2F46 File Offset: 0x000C1146
		// (remove) Token: 0x06003C27 RID: 15399 RVA: 0x000C2F59 File Offset: 0x000C1159
		[WebCategory("Behavior")]
		[WebSysDescription("DataControls_OnItemCreated")]
		public event RepeaterItemEventHandler ItemCreated
		{
			add
			{
				base.Events.AddHandler(Repeater.EventItemCreated, value);
			}
			remove
			{
				base.Events.RemoveHandler(Repeater.EventItemCreated, value);
			}
		}

		// Token: 0x140000E7 RID: 231
		// (add) Token: 0x06003C28 RID: 15400 RVA: 0x000C2F6C File Offset: 0x000C116C
		// (remove) Token: 0x06003C29 RID: 15401 RVA: 0x000C2F7F File Offset: 0x000C117F
		[WebCategory("Behavior")]
		[WebSysDescription("DataControls_OnItemDataBound")]
		public event RepeaterItemEventHandler ItemDataBound
		{
			add
			{
				base.Events.AddHandler(Repeater.EventItemDataBound, value);
			}
			remove
			{
				base.Events.RemoveHandler(Repeater.EventItemDataBound, value);
			}
		}

		// Token: 0x06003C2A RID: 15402 RVA: 0x000C2F94 File Offset: 0x000C1194
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
			IDataSource dataSource = null;
			if (!base.DesignMode && this.IsUsingModelBinders)
			{
				if (this.DataSourceID.Length != 0 || this.DataSource != null)
				{
					throw new InvalidOperationException(SR.GetString("DataControl_ItemType_MultipleDataSources", new object[]
					{
						this.ID
					}));
				}
				CreatingModelDataSourceEventArgs creatingModelDataSourceEventArgs = new CreatingModelDataSourceEventArgs();
				this.OnCreatingModelDataSource(creatingModelDataSourceEventArgs);
				if (creatingModelDataSourceEventArgs.ModelDataSource != null)
				{
					this.ModelDataSource = creatingModelDataSourceEventArgs.ModelDataSource;
				}
				this.UpdateModelDataSourceProperties(this.ModelDataSource);
				CallingDataMethodsEventHandler callingDataMethodsEventHandler = base.Events[Repeater.EventCallingDataMethods] as CallingDataMethodsEventHandler;
				if (callingDataMethodsEventHandler != null)
				{
					this.ModelDataSource.CallingDataMethods += callingDataMethodsEventHandler;
				}
				dataSource = this.ModelDataSource;
			}
			else
			{
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
			}
			if (dataSource == null)
			{
				dataSource = new ReadOnlyDataSource(this.DataSource, this.DataMember);
			}
			else if (this.DataSource != null)
			{
				throw new InvalidOperationException(SR.GetString("DataControl_MultipleDataSources", new object[]
				{
					this.ID
				}));
			}
			DataSourceView view = dataSource.GetView(this.DataMember);
			if (view == null)
			{
				throw new InvalidOperationException(SR.GetString("DataControl_ViewNotFound", new object[]
				{
					this.ID
				}));
			}
			this._currentViewIsFromDataSourceID = this.IsDataBindingAutomatic;
			this._currentView = view;
			if (this._currentView != null && this._currentViewIsFromDataSourceID)
			{
				this._currentView.DataSourceViewChanged += this.OnDataSourceViewChanged;
			}
			this._currentViewValid = true;
			return this._currentView;
		}

		// Token: 0x06003C2B RID: 15403 RVA: 0x000C31AE File Offset: 0x000C13AE
		protected internal override void CreateChildControls()
		{
			this.Controls.Clear();
			if (this.ViewState["_!ItemCount"] != null)
			{
				this.CreateControlHierarchy(false);
			}
			else
			{
				this.itemsArray = new ArrayList();
			}
			base.ClearChildViewState();
		}

		// Token: 0x06003C2C RID: 15404 RVA: 0x000C31E8 File Offset: 0x000C13E8
		protected virtual void CreateControlHierarchy(bool useDataSource)
		{
			IEnumerable enumerable = null;
			if (this.itemsArray != null)
			{
				this.itemsArray.Clear();
			}
			else
			{
				this.itemsArray = new ArrayList();
			}
			if (!useDataSource)
			{
				int num = (int)this.ViewState["_!ItemCount"];
				if (num != -1)
				{
					enumerable = new DummyDataSource(num);
					this.itemsArray.Capacity = num;
				}
				this.AddDataItemsIntoItemsArray(enumerable, useDataSource);
				return;
			}
			enumerable = this.GetData();
			this.PostGetDataAction(enumerable);
		}

		// Token: 0x06003C2D RID: 15405 RVA: 0x000C325F File Offset: 0x000C145F
		private void OnDataSourceViewSelectCallback(IEnumerable data)
		{
			this._asyncSelectPending = false;
			this.PostGetDataAction(data);
		}

		// Token: 0x06003C2E RID: 15406 RVA: 0x000C3270 File Offset: 0x000C1470
		private void PostGetDataAction(IEnumerable dataSource)
		{
			if (this._asyncSelectPending)
			{
				return;
			}
			ICollection collection = dataSource as ICollection;
			if (collection != null)
			{
				this.itemsArray.Capacity = collection.Count;
			}
			int num = this.AddDataItemsIntoItemsArray(dataSource, true);
			this.ViewState["_!ItemCount"] = num;
		}

		// Token: 0x06003C2F RID: 15407 RVA: 0x000C32C0 File Offset: 0x000C14C0
		private int AddDataItemsIntoItemsArray(IEnumerable dataSource, bool useDataSource)
		{
			int num = -1;
			if (dataSource != null)
			{
				int num2 = 0;
				bool flag = this.separatorTemplate != null;
				num = 0;
				if (this.headerTemplate != null)
				{
					this.CreateItem(-1, ListItemType.Header, useDataSource, null);
				}
				foreach (object dataItem in dataSource)
				{
					if (flag && num > 0)
					{
						this.CreateItem(num2 - 1, ListItemType.Separator, useDataSource, null);
					}
					ListItemType itemType = (num2 % 2 == 0) ? ListItemType.Item : ListItemType.AlternatingItem;
					RepeaterItem value = this.CreateItem(num2, itemType, useDataSource, dataItem);
					this.itemsArray.Add(value);
					num++;
					num2++;
				}
				if (this.footerTemplate != null)
				{
					this.CreateItem(-1, ListItemType.Footer, useDataSource, null);
				}
			}
			return num;
		}

		// Token: 0x06003C30 RID: 15408 RVA: 0x00085B41 File Offset: 0x00083D41
		protected virtual DataSourceSelectArguments CreateDataSourceSelectArguments()
		{
			return DataSourceSelectArguments.Empty;
		}

		// Token: 0x06003C31 RID: 15409 RVA: 0x000C3390 File Offset: 0x000C1590
		private RepeaterItem CreateItem(int itemIndex, ListItemType itemType, bool dataBind, object dataItem)
		{
			RepeaterItem repeaterItem = this.CreateItem(itemIndex, itemType);
			RepeaterItemEventArgs e = new RepeaterItemEventArgs(repeaterItem);
			this.InitializeItem(repeaterItem);
			if (dataBind)
			{
				repeaterItem.DataItem = dataItem;
			}
			this.OnItemCreated(e);
			this.Controls.Add(repeaterItem);
			if (dataBind)
			{
				repeaterItem.DataBind();
				this.OnItemDataBound(e);
				repeaterItem.DataItem = null;
			}
			return repeaterItem;
		}

		// Token: 0x06003C32 RID: 15410 RVA: 0x000C33EA File Offset: 0x000C15EA
		protected virtual RepeaterItem CreateItem(int itemIndex, ListItemType itemType)
		{
			return new RepeaterItem(itemIndex, itemType);
		}

		// Token: 0x06003C33 RID: 15411 RVA: 0x000C33F3 File Offset: 0x000C15F3
		public override void DataBind()
		{
			if (this.IsDataBindingAutomatic && base.DesignMode && base.Site == null)
			{
				return;
			}
			this.RequiresDataBinding = false;
			this.OnDataBinding(EventArgs.Empty);
		}

		// Token: 0x06003C34 RID: 15412 RVA: 0x000C3420 File Offset: 0x000C1620
		protected void EnsureDataBound()
		{
			try
			{
				this._throwOnDataPropertyChange = true;
				if (this.RequiresDataBinding && this.IsDataBindingAutomatic)
				{
					this.DataBind();
				}
			}
			finally
			{
				this._throwOnDataPropertyChange = false;
			}
		}

		// Token: 0x06003C35 RID: 15413 RVA: 0x000C3464 File Offset: 0x000C1664
		protected virtual IEnumerable GetData()
		{
			DataSourceView dataSourceView = this.ConnectToDataSourceView();
			if (dataSourceView != null)
			{
				bool flag = false;
				if (AppSettings.EnableAsyncModelBinding)
				{
					ModelDataSourceView modelDataSourceView = dataSourceView as ModelDataSourceView;
					flag = (modelDataSourceView != null && modelDataSourceView.IsSelectMethodAsync);
				}
				if (!flag)
				{
					return dataSourceView.ExecuteSelect(this.SelectArguments);
				}
				this._asyncSelectPending = true;
				dataSourceView.Select(this.SelectArguments, new DataSourceViewSelectCallback(this.OnDataSourceViewSelectCallback));
			}
			return null;
		}

		// Token: 0x06003C36 RID: 15414 RVA: 0x000C34CC File Offset: 0x000C16CC
		protected virtual void InitializeItem(RepeaterItem item)
		{
			ITemplate template = null;
			switch (item.ItemType)
			{
			case ListItemType.Header:
				template = this.headerTemplate;
				goto IL_5B;
			case ListItemType.Footer:
				template = this.footerTemplate;
				goto IL_5B;
			case ListItemType.Item:
				break;
			case ListItemType.AlternatingItem:
				template = this.alternatingItemTemplate;
				if (template != null)
				{
					goto IL_5B;
				}
				break;
			case ListItemType.SelectedItem:
			case ListItemType.EditItem:
				goto IL_5B;
			case ListItemType.Separator:
				template = this.separatorTemplate;
				goto IL_5B;
			default:
				goto IL_5B;
			}
			template = this.itemTemplate;
			IL_5B:
			if (template != null)
			{
				template.InstantiateIn(item);
			}
		}

		// Token: 0x06003C37 RID: 15415 RVA: 0x000C3540 File Offset: 0x000C1740
		protected override bool OnBubbleEvent(object sender, EventArgs e)
		{
			bool result = false;
			if (e is RepeaterCommandEventArgs)
			{
				this.OnItemCommand((RepeaterCommandEventArgs)e);
				result = true;
			}
			return result;
		}

		// Token: 0x06003C38 RID: 15416 RVA: 0x000C3566 File Offset: 0x000C1766
		protected override void OnDataBinding(EventArgs e)
		{
			base.OnDataBinding(e);
			this.Controls.Clear();
			base.ClearChildViewState();
			this.CreateControlHierarchy(true);
			base.ChildControlsCreated = true;
		}

		// Token: 0x06003C39 RID: 15417 RVA: 0x000C358E File Offset: 0x000C178E
		protected virtual void OnDataPropertyChanged()
		{
			if (this._throwOnDataPropertyChange)
			{
				throw new HttpException(SR.GetString("DataBoundControl_InvalidDataPropertyChange", new object[]
				{
					this.ID
				}));
			}
			if (this._inited)
			{
				this.RequiresDataBinding = true;
			}
			this._currentViewValid = false;
		}

		// Token: 0x06003C3A RID: 15418 RVA: 0x000C35CD File Offset: 0x000C17CD
		protected virtual void OnDataSourceViewChanged(object sender, EventArgs e)
		{
			this.RequiresDataBinding = true;
		}

		// Token: 0x06003C3B RID: 15419 RVA: 0x000C35D8 File Offset: 0x000C17D8
		protected internal override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			if (this.Page != null)
			{
				this.Page.PreLoad += this.OnPagePreLoad;
				if (!base.IsViewStateEnabled && this.Page.IsPostBack)
				{
					this.RequiresDataBinding = true;
				}
			}
			if (!base.DesignMode && !string.IsNullOrEmpty(this.ItemType))
			{
				DataBoundControlHelper.EnableDynamicData(this, this.ItemType);
			}
		}

		// Token: 0x06003C3C RID: 15420 RVA: 0x000C3648 File Offset: 0x000C1848
		protected virtual void OnItemCommand(RepeaterCommandEventArgs e)
		{
			RepeaterCommandEventHandler repeaterCommandEventHandler = (RepeaterCommandEventHandler)base.Events[Repeater.EventItemCommand];
			if (repeaterCommandEventHandler != null)
			{
				repeaterCommandEventHandler(this, e);
			}
		}

		// Token: 0x06003C3D RID: 15421 RVA: 0x000C3678 File Offset: 0x000C1878
		protected virtual void OnItemCreated(RepeaterItemEventArgs e)
		{
			RepeaterItemEventHandler repeaterItemEventHandler = (RepeaterItemEventHandler)base.Events[Repeater.EventItemCreated];
			if (repeaterItemEventHandler != null)
			{
				repeaterItemEventHandler(this, e);
			}
		}

		// Token: 0x06003C3E RID: 15422 RVA: 0x000C36A8 File Offset: 0x000C18A8
		protected virtual void OnItemDataBound(RepeaterItemEventArgs e)
		{
			RepeaterItemEventHandler repeaterItemEventHandler = (RepeaterItemEventHandler)base.Events[Repeater.EventItemDataBound];
			if (repeaterItemEventHandler != null)
			{
				repeaterItemEventHandler(this, e);
			}
		}

		// Token: 0x06003C3F RID: 15423 RVA: 0x000C36D8 File Offset: 0x000C18D8
		protected internal override void OnLoad(EventArgs e)
		{
			this._inited = true;
			this.ConnectToDataSourceView();
			if (this.Page != null && !this._pagePreLoadFired && this.ViewState["_!ItemCount"] == null)
			{
				if (!this.Page.IsPostBack)
				{
					this.RequiresDataBinding = true;
				}
				else if (base.IsViewStateEnabled)
				{
					this.RequiresDataBinding = true;
				}
			}
			base.OnLoad(e);
		}

		// Token: 0x06003C40 RID: 15424 RVA: 0x000C3744 File Offset: 0x000C1944
		private void OnPagePreLoad(object sender, EventArgs e)
		{
			this._inited = true;
			if (this.Page != null)
			{
				this.Page.PreLoad -= this.OnPagePreLoad;
				if (!this.Page.IsPostBack)
				{
					this.RequiresDataBinding = true;
				}
				if (this.Page.IsPostBack && base.IsViewStateEnabled && this.ViewState["_!ItemCount"] == null)
				{
					this.RequiresDataBinding = true;
				}
				this._pagePreLoadFired = true;
			}
		}

		// Token: 0x06003C41 RID: 15425 RVA: 0x000C37C0 File Offset: 0x000C19C0
		protected internal override void OnPreRender(EventArgs e)
		{
			this.EnsureDataBound();
			base.OnPreRender(e);
		}

		// Token: 0x06003C42 RID: 15426 RVA: 0x000C37D0 File Offset: 0x000C19D0
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

		// Token: 0x06003C43 RID: 15427 RVA: 0x000C3824 File Offset: 0x000C1A24
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

		// Token: 0x06003C44 RID: 15428 RVA: 0x000C3873 File Offset: 0x000C1A73
		protected override void TrackViewState()
		{
			base.TrackViewState();
			if (this.IsUsingModelBinders)
			{
				((IStateManager)this.ModelDataSource).TrackViewState();
			}
		}

		// Token: 0x04002359 RID: 9049
		private static readonly object EventItemCreated = new object();

		// Token: 0x0400235A RID: 9050
		private static readonly object EventItemDataBound = new object();

		// Token: 0x0400235B RID: 9051
		private static readonly object EventItemCommand = new object();

		// Token: 0x0400235C RID: 9052
		private static readonly object EventCreatingModelDataSource = new object();

		// Token: 0x0400235D RID: 9053
		private static readonly object EventCallingDataMethods = new object();

		// Token: 0x0400235E RID: 9054
		internal const string ItemCountViewStateKey = "_!ItemCount";

		// Token: 0x0400235F RID: 9055
		private object dataSource;

		// Token: 0x04002360 RID: 9056
		private ITemplate headerTemplate;

		// Token: 0x04002361 RID: 9057
		private ITemplate footerTemplate;

		// Token: 0x04002362 RID: 9058
		private ITemplate itemTemplate;

		// Token: 0x04002363 RID: 9059
		private ITemplate alternatingItemTemplate;

		// Token: 0x04002364 RID: 9060
		private ITemplate separatorTemplate;

		// Token: 0x04002365 RID: 9061
		private ArrayList itemsArray;

		// Token: 0x04002366 RID: 9062
		private RepeaterItemCollection itemsCollection;

		// Token: 0x04002367 RID: 9063
		private bool _requiresDataBinding;

		// Token: 0x04002368 RID: 9064
		private bool _inited;

		// Token: 0x04002369 RID: 9065
		private bool _throwOnDataPropertyChange;

		// Token: 0x0400236A RID: 9066
		private DataSourceView _currentView;

		// Token: 0x0400236B RID: 9067
		private bool _currentViewIsFromDataSourceID;

		// Token: 0x0400236C RID: 9068
		private bool _currentViewValid;

		// Token: 0x0400236D RID: 9069
		private DataSourceSelectArguments _arguments;

		// Token: 0x0400236E RID: 9070
		private bool _pagePreLoadFired;

		// Token: 0x0400236F RID: 9071
		private string _itemType;

		// Token: 0x04002370 RID: 9072
		private string _selectMethod;

		// Token: 0x04002371 RID: 9073
		private ModelDataSource _modelDataSource;

		// Token: 0x04002372 RID: 9074
		private bool _asyncSelectPending;
	}
}
