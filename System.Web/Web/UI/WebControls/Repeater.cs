using System;
using System.Collections;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	// Token: 0x0200062B RID: 1579
	[ParseChildren(true)]
	[PersistChildren(false)]
	[DefaultEvent("ItemCommand")]
	[DefaultProperty("DataSource")]
	[Designer("System.Web.UI.Design.WebControls.RepeaterDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class Repeater : Control, INamingContainer
	{
		// Token: 0x170013BB RID: 5051
		// (get) Token: 0x06004E27 RID: 20007 RVA: 0x0013CA70 File Offset: 0x0013BA70
		// (set) Token: 0x06004E28 RID: 20008 RVA: 0x0013CA78 File Offset: 0x0013BA78
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

		// Token: 0x170013BC RID: 5052
		// (get) Token: 0x06004E29 RID: 20009 RVA: 0x0013CA81 File Offset: 0x0013BA81
		public override ControlCollection Controls
		{
			get
			{
				this.EnsureChildControls();
				return base.Controls;
			}
		}

		// Token: 0x170013BD RID: 5053
		// (get) Token: 0x06004E2A RID: 20010 RVA: 0x0013CA90 File Offset: 0x0013BA90
		// (set) Token: 0x06004E2B RID: 20011 RVA: 0x0013CABD File Offset: 0x0013BABD
		[WebSysDescription("Repeater_DataMember")]
		[DefaultValue("")]
		[WebCategory("Data")]
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

		// Token: 0x170013BE RID: 5054
		// (get) Token: 0x06004E2C RID: 20012 RVA: 0x0013CAD6 File Offset: 0x0013BAD6
		// (set) Token: 0x06004E2D RID: 20013 RVA: 0x0013CAE0 File Offset: 0x0013BAE0
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[WebSysDescription("BaseDataBoundControl_DataSource")]
		[WebCategory("Data")]
		[Bindable(true)]
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

		// Token: 0x170013BF RID: 5055
		// (get) Token: 0x06004E2E RID: 20014 RVA: 0x0013CB30 File Offset: 0x0013BB30
		// (set) Token: 0x06004E2F RID: 20015 RVA: 0x0013CB5D File Offset: 0x0013BB5D
		[IDReferenceProperty(typeof(DataSourceControl))]
		[WebCategory("Data")]
		[WebSysDescription("BaseDataBoundControl_DataSourceID")]
		[DefaultValue("")]
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

		// Token: 0x170013C0 RID: 5056
		// (get) Token: 0x06004E30 RID: 20016 RVA: 0x0013CB76 File Offset: 0x0013BB76
		// (set) Token: 0x06004E31 RID: 20017 RVA: 0x0013CB7E File Offset: 0x0013BB7E
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

		// Token: 0x170013C1 RID: 5057
		// (get) Token: 0x06004E32 RID: 20018 RVA: 0x0013CB87 File Offset: 0x0013BB87
		// (set) Token: 0x06004E33 RID: 20019 RVA: 0x0013CB8F File Offset: 0x0013BB8F
		[WebSysDescription("Repeater_FooterTemplate")]
		[Browsable(false)]
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[TemplateContainer(typeof(RepeaterItem))]
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

		// Token: 0x170013C2 RID: 5058
		// (get) Token: 0x06004E34 RID: 20020 RVA: 0x0013CB98 File Offset: 0x0013BB98
		// (set) Token: 0x06004E35 RID: 20021 RVA: 0x0013CBA0 File Offset: 0x0013BBA0
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[TemplateContainer(typeof(RepeaterItem))]
		[WebSysDescription("WebControl_HeaderTemplate")]
		[Browsable(false)]
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

		// Token: 0x170013C3 RID: 5059
		// (get) Token: 0x06004E36 RID: 20022 RVA: 0x0013CBA9 File Offset: 0x0013BBA9
		protected bool Initialized
		{
			get
			{
				return this._inited;
			}
		}

		// Token: 0x170013C4 RID: 5060
		// (get) Token: 0x06004E37 RID: 20023 RVA: 0x0013CBB1 File Offset: 0x0013BBB1
		protected bool IsBoundUsingDataSourceID
		{
			get
			{
				return this.DataSourceID.Length > 0;
			}
		}

		// Token: 0x170013C5 RID: 5061
		// (get) Token: 0x06004E38 RID: 20024 RVA: 0x0013CBC1 File Offset: 0x0013BBC1
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[WebSysDescription("Repeater_Items")]
		[Browsable(false)]
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

		// Token: 0x170013C6 RID: 5062
		// (get) Token: 0x06004E39 RID: 20025 RVA: 0x0013CBF0 File Offset: 0x0013BBF0
		// (set) Token: 0x06004E3A RID: 20026 RVA: 0x0013CBF8 File Offset: 0x0013BBF8
		[TemplateContainer(typeof(RepeaterItem))]
		[WebSysDescription("Repeater_ItemTemplate")]
		[Browsable(false)]
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
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

		// Token: 0x170013C7 RID: 5063
		// (get) Token: 0x06004E3B RID: 20027 RVA: 0x0013CC01 File Offset: 0x0013BC01
		// (set) Token: 0x06004E3C RID: 20028 RVA: 0x0013CC09 File Offset: 0x0013BC09
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

		// Token: 0x170013C8 RID: 5064
		// (get) Token: 0x06004E3D RID: 20029 RVA: 0x0013CC12 File Offset: 0x0013BC12
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

		// Token: 0x170013C9 RID: 5065
		// (get) Token: 0x06004E3E RID: 20030 RVA: 0x0013CC2E File Offset: 0x0013BC2E
		// (set) Token: 0x06004E3F RID: 20031 RVA: 0x0013CC36 File Offset: 0x0013BC36
		[DefaultValue(null)]
		[Browsable(false)]
		[TemplateContainer(typeof(RepeaterItem))]
		[WebSysDescription("Repeater_SeparatorTemplate")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
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

		// Token: 0x140000FB RID: 251
		// (add) Token: 0x06004E40 RID: 20032 RVA: 0x0013CC3F File Offset: 0x0013BC3F
		// (remove) Token: 0x06004E41 RID: 20033 RVA: 0x0013CC52 File Offset: 0x0013BC52
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

		// Token: 0x140000FC RID: 252
		// (add) Token: 0x06004E42 RID: 20034 RVA: 0x0013CC65 File Offset: 0x0013BC65
		// (remove) Token: 0x06004E43 RID: 20035 RVA: 0x0013CC78 File Offset: 0x0013BC78
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

		// Token: 0x140000FD RID: 253
		// (add) Token: 0x06004E44 RID: 20036 RVA: 0x0013CC8B File Offset: 0x0013BC8B
		// (remove) Token: 0x06004E45 RID: 20037 RVA: 0x0013CC9E File Offset: 0x0013BC9E
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

		// Token: 0x06004E46 RID: 20038 RVA: 0x0013CCB4 File Offset: 0x0013BCB4
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
			this._currentViewIsFromDataSourceID = this.IsBoundUsingDataSourceID;
			this._currentView = view;
			if (this._currentView != null && this._currentViewIsFromDataSourceID)
			{
				this._currentView.DataSourceViewChanged += this.OnDataSourceViewChanged;
			}
			this._currentViewValid = true;
			return this._currentView;
		}

		// Token: 0x06004E47 RID: 20039 RVA: 0x0013CE37 File Offset: 0x0013BE37
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

		// Token: 0x06004E48 RID: 20040 RVA: 0x0013CE70 File Offset: 0x0013BE70
		protected virtual void CreateControlHierarchy(bool useDataSource)
		{
			IEnumerable enumerable = null;
			int num = -1;
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
				num = (int)this.ViewState["_!ItemCount"];
				if (num != -1)
				{
					enumerable = new DummyDataSource(num);
					this.itemsArray.Capacity = num;
				}
			}
			else
			{
				enumerable = this.GetData();
				ICollection collection = enumerable as ICollection;
				if (collection != null)
				{
					this.itemsArray.Capacity = collection.Count;
				}
			}
			if (enumerable != null)
			{
				int num2 = 0;
				bool flag = this.separatorTemplate != null;
				num = 0;
				if (this.headerTemplate != null)
				{
					this.CreateItem(-1, ListItemType.Header, useDataSource, null);
				}
				foreach (object dataItem in enumerable)
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
			if (useDataSource)
			{
				this.ViewState["_!ItemCount"] = ((enumerable != null) ? num : -1);
			}
		}

		// Token: 0x06004E49 RID: 20041 RVA: 0x0013CFE0 File Offset: 0x0013BFE0
		protected virtual DataSourceSelectArguments CreateDataSourceSelectArguments()
		{
			return DataSourceSelectArguments.Empty;
		}

		// Token: 0x06004E4A RID: 20042 RVA: 0x0013CFE8 File Offset: 0x0013BFE8
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

		// Token: 0x06004E4B RID: 20043 RVA: 0x0013D042 File Offset: 0x0013C042
		protected virtual RepeaterItem CreateItem(int itemIndex, ListItemType itemType)
		{
			return new RepeaterItem(itemIndex, itemType);
		}

		// Token: 0x06004E4C RID: 20044 RVA: 0x0013D04B File Offset: 0x0013C04B
		public override void DataBind()
		{
			if (this.IsBoundUsingDataSourceID && base.DesignMode && base.Site == null)
			{
				return;
			}
			this.RequiresDataBinding = false;
			this.OnDataBinding(EventArgs.Empty);
		}

		// Token: 0x06004E4D RID: 20045 RVA: 0x0013D078 File Offset: 0x0013C078
		protected void EnsureDataBound()
		{
			try
			{
				this._throwOnDataPropertyChange = true;
				if (this.RequiresDataBinding && this.DataSourceID.Length > 0)
				{
					this.DataBind();
				}
			}
			finally
			{
				this._throwOnDataPropertyChange = false;
			}
		}

		// Token: 0x06004E4E RID: 20046 RVA: 0x0013D0C4 File Offset: 0x0013C0C4
		protected virtual IEnumerable GetData()
		{
			DataSourceView dataSourceView = this.ConnectToDataSourceView();
			if (dataSourceView != null)
			{
				return dataSourceView.ExecuteSelect(this.SelectArguments);
			}
			return null;
		}

		// Token: 0x06004E4F RID: 20047 RVA: 0x0013D0EC File Offset: 0x0013C0EC
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

		// Token: 0x06004E50 RID: 20048 RVA: 0x0013D160 File Offset: 0x0013C160
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

		// Token: 0x06004E51 RID: 20049 RVA: 0x0013D186 File Offset: 0x0013C186
		protected override void OnDataBinding(EventArgs e)
		{
			base.OnDataBinding(e);
			this.Controls.Clear();
			base.ClearChildViewState();
			this.CreateControlHierarchy(true);
			base.ChildControlsCreated = true;
		}

		// Token: 0x06004E52 RID: 20050 RVA: 0x0013D1B0 File Offset: 0x0013C1B0
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

		// Token: 0x06004E53 RID: 20051 RVA: 0x0013D1FC File Offset: 0x0013C1FC
		protected virtual void OnDataSourceViewChanged(object sender, EventArgs e)
		{
			this.RequiresDataBinding = true;
		}

		// Token: 0x06004E54 RID: 20052 RVA: 0x0013D208 File Offset: 0x0013C208
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
		}

		// Token: 0x06004E55 RID: 20053 RVA: 0x0013D258 File Offset: 0x0013C258
		protected virtual void OnItemCommand(RepeaterCommandEventArgs e)
		{
			RepeaterCommandEventHandler repeaterCommandEventHandler = (RepeaterCommandEventHandler)base.Events[Repeater.EventItemCommand];
			if (repeaterCommandEventHandler != null)
			{
				repeaterCommandEventHandler(this, e);
			}
		}

		// Token: 0x06004E56 RID: 20054 RVA: 0x0013D288 File Offset: 0x0013C288
		protected virtual void OnItemCreated(RepeaterItemEventArgs e)
		{
			RepeaterItemEventHandler repeaterItemEventHandler = (RepeaterItemEventHandler)base.Events[Repeater.EventItemCreated];
			if (repeaterItemEventHandler != null)
			{
				repeaterItemEventHandler(this, e);
			}
		}

		// Token: 0x06004E57 RID: 20055 RVA: 0x0013D2B8 File Offset: 0x0013C2B8
		protected virtual void OnItemDataBound(RepeaterItemEventArgs e)
		{
			RepeaterItemEventHandler repeaterItemEventHandler = (RepeaterItemEventHandler)base.Events[Repeater.EventItemDataBound];
			if (repeaterItemEventHandler != null)
			{
				repeaterItemEventHandler(this, e);
			}
		}

		// Token: 0x06004E58 RID: 20056 RVA: 0x0013D2E8 File Offset: 0x0013C2E8
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

		// Token: 0x06004E59 RID: 20057 RVA: 0x0013D354 File Offset: 0x0013C354
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

		// Token: 0x06004E5A RID: 20058 RVA: 0x0013D3D0 File Offset: 0x0013C3D0
		protected internal override void OnPreRender(EventArgs e)
		{
			this.EnsureDataBound();
			base.OnPreRender(e);
		}

		// Token: 0x04002C7F RID: 11391
		internal const string ItemCountViewStateKey = "_!ItemCount";

		// Token: 0x04002C80 RID: 11392
		private static readonly object EventItemCreated = new object();

		// Token: 0x04002C81 RID: 11393
		private static readonly object EventItemDataBound = new object();

		// Token: 0x04002C82 RID: 11394
		private static readonly object EventItemCommand = new object();

		// Token: 0x04002C83 RID: 11395
		private object dataSource;

		// Token: 0x04002C84 RID: 11396
		private ITemplate headerTemplate;

		// Token: 0x04002C85 RID: 11397
		private ITemplate footerTemplate;

		// Token: 0x04002C86 RID: 11398
		private ITemplate itemTemplate;

		// Token: 0x04002C87 RID: 11399
		private ITemplate alternatingItemTemplate;

		// Token: 0x04002C88 RID: 11400
		private ITemplate separatorTemplate;

		// Token: 0x04002C89 RID: 11401
		private ArrayList itemsArray;

		// Token: 0x04002C8A RID: 11402
		private RepeaterItemCollection itemsCollection;

		// Token: 0x04002C8B RID: 11403
		private bool _requiresDataBinding;

		// Token: 0x04002C8C RID: 11404
		private bool _inited;

		// Token: 0x04002C8D RID: 11405
		private bool _throwOnDataPropertyChange;

		// Token: 0x04002C8E RID: 11406
		private DataSourceView _currentView;

		// Token: 0x04002C8F RID: 11407
		private bool _currentViewIsFromDataSourceID;

		// Token: 0x04002C90 RID: 11408
		private bool _currentViewValid;

		// Token: 0x04002C91 RID: 11409
		private DataSourceSelectArguments _arguments;

		// Token: 0x04002C92 RID: 11410
		private bool _pagePreLoadFired;
	}
}
