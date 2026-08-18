using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit.Design;
using AjaxControlToolkit.ToolboxIcons;

namespace AjaxControlToolkit
{
	// Token: 0x02000002 RID: 2
	[ToolboxBitmap(typeof(Accessor), "Accordion.bmp")]
	[ToolboxData("<{0}:Accordion runat=server></{0}:Accordion>")]
	[Designer(typeof(AccordionDesigner))]
	public class Accordion : WebControl
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		// (remove) Token: 0x06000002 RID: 2 RVA: 0x00002108 File Offset: 0x00000308
		public event EventHandler<AccordionItemEventArgs> ItemCreated;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000003 RID: 3 RVA: 0x00002140 File Offset: 0x00000340
		// (remove) Token: 0x06000004 RID: 4 RVA: 0x00002178 File Offset: 0x00000378
		public event EventHandler<AccordionItemEventArgs> ItemDataBound;

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06000005 RID: 5 RVA: 0x000021B0 File Offset: 0x000003B0
		// (remove) Token: 0x06000006 RID: 6 RVA: 0x000021E8 File Offset: 0x000003E8
		public event CommandEventHandler ItemCommand;

		// Token: 0x06000007 RID: 7 RVA: 0x0000221D File Offset: 0x0000041D
		public Accordion() : base(HtmlTextWriterTag.Div)
		{
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000008 RID: 8 RVA: 0x00002228 File Offset: 0x00000428
		private AccordionExtender AccordionExtender
		{
			get
			{
				if (this._extender == null)
				{
					this._extender = new AccordionExtender();
					this._extender.ID = this.ID + "_AccordionExtender";
					this._extender.TargetControlID = this.ID;
					this.Controls.AddAt(0, this._extender);
				}
				return this._extender;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000009 RID: 9 RVA: 0x0000228C File Offset: 0x0000048C
		// (set) Token: 0x0600000A RID: 10 RVA: 0x00002299 File Offset: 0x00000499
		[Category("Behavior")]
		[Description("Length of the transition animation in milliseconds")]
		[DefaultValue(500)]
		[Browsable(true)]
		public int TransitionDuration
		{
			get
			{
				return this.AccordionExtender.TransitionDuration;
			}
			set
			{
				this.AccordionExtender.TransitionDuration = value;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000B RID: 11 RVA: 0x000022A7 File Offset: 0x000004A7
		// (set) Token: 0x0600000C RID: 12 RVA: 0x000022B4 File Offset: 0x000004B4
		[DefaultValue(30)]
		[Category("Behavior")]
		[Browsable(true)]
		[Description("Number of frames per second used in the transition animation")]
		public int FramesPerSecond
		{
			get
			{
				return this.AccordionExtender.FramesPerSecond;
			}
			set
			{
				this.AccordionExtender.FramesPerSecond = value;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000D RID: 13 RVA: 0x000022C2 File Offset: 0x000004C2
		// (set) Token: 0x0600000E RID: 14 RVA: 0x000022CF File Offset: 0x000004CF
		[Description("Whether or not to use a fade effect in the transition animations")]
		[Category("Behavior")]
		[DefaultValue(false)]
		[Browsable(true)]
		public bool FadeTransitions
		{
			get
			{
				return this.AccordionExtender.FadeTransitions;
			}
			set
			{
				this.AccordionExtender.FadeTransitions = value;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000F RID: 15 RVA: 0x000022DD File Offset: 0x000004DD
		// (set) Token: 0x06000010 RID: 16 RVA: 0x000022EA File Offset: 0x000004EA
		[Browsable(true)]
		[Description("Default CSS class for Accordion Pane Headers")]
		[Category("Appearance")]
		public string HeaderCssClass
		{
			get
			{
				return this.AccordionExtender.HeaderCssClass;
			}
			set
			{
				this.AccordionExtender.HeaderCssClass = value;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000011 RID: 17 RVA: 0x000022F8 File Offset: 0x000004F8
		// (set) Token: 0x06000012 RID: 18 RVA: 0x00002305 File Offset: 0x00000505
		[Category("Appearance")]
		[Description("Default CSS class for the selected Accordion Pane Headers")]
		[Browsable(true)]
		public string HeaderSelectedCssClass
		{
			get
			{
				return this.AccordionExtender.HeaderSelectedCssClass;
			}
			set
			{
				this.AccordionExtender.HeaderSelectedCssClass = value;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000013 RID: 19 RVA: 0x00002313 File Offset: 0x00000513
		// (set) Token: 0x06000014 RID: 20 RVA: 0x00002320 File Offset: 0x00000520
		[Category("Appearance")]
		[Browsable(true)]
		[Description("Default CSS class for Accordion Pane Content")]
		public string ContentCssClass
		{
			get
			{
				return this.AccordionExtender.ContentCssClass;
			}
			set
			{
				this.AccordionExtender.ContentCssClass = value;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000015 RID: 21 RVA: 0x0000232E File Offset: 0x0000052E
		// (set) Token: 0x06000016 RID: 22 RVA: 0x0000233B File Offset: 0x0000053B
		[Description("Determine how the growth of the Accordion will be controlled")]
		[DefaultValue(AutoSize.None)]
		[Browsable(true)]
		[Category("Behavior")]
		public AutoSize AutoSize
		{
			get
			{
				return this.AccordionExtender.AutoSize;
			}
			set
			{
				this.AccordionExtender.AutoSize = value;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000017 RID: 23 RVA: 0x00002349 File Offset: 0x00000549
		// (set) Token: 0x06000018 RID: 24 RVA: 0x00002356 File Offset: 0x00000556
		[DefaultValue(0)]
		[Browsable(true)]
		[Category("Behavior")]
		[Description("Index of the AccordionPane to be displayed")]
		public int SelectedIndex
		{
			get
			{
				return this.AccordionExtender.SelectedIndex;
			}
			set
			{
				this.AccordionExtender.SelectedIndex = value;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000019 RID: 25 RVA: 0x00002364 File Offset: 0x00000564
		// (set) Token: 0x0600001A RID: 26 RVA: 0x00002371 File Offset: 0x00000571
		[Description("Whether or not clicking the header will close the currently opened pane (leaving all the Accordion's panes closed)")]
		[Browsable(true)]
		[DefaultValue(true)]
		[Category("Behavior")]
		public bool RequireOpenedPane
		{
			get
			{
				return this.AccordionExtender.RequireOpenedPane;
			}
			set
			{
				this.AccordionExtender.RequireOpenedPane = value;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600001B RID: 27 RVA: 0x0000237F File Offset: 0x0000057F
		// (set) Token: 0x0600001C RID: 28 RVA: 0x0000238C File Offset: 0x0000058C
		[Category("Behavior")]
		[Description("Whether or not we suppress the client-side click handlers of any elements in the header sections")]
		[Browsable(true)]
		[DefaultValue(false)]
		public bool SuppressHeaderPostbacks
		{
			get
			{
				return this.AccordionExtender.SuppressHeaderPostbacks;
			}
			set
			{
				this.AccordionExtender.SuppressHeaderPostbacks = value;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600001D RID: 29 RVA: 0x0000239A File Offset: 0x0000059A
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public AccordionPaneCollection Panes
		{
			get
			{
				if (this._panes == null)
				{
					this._panes = new AccordionPaneCollection(this);
				}
				return this._panes;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600001E RID: 30 RVA: 0x000023B6 File Offset: 0x000005B6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override ControlCollection Controls
		{
			get
			{
				return base.Controls;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600001F RID: 31 RVA: 0x000023BE File Offset: 0x000005BE
		// (set) Token: 0x06000020 RID: 32 RVA: 0x000023C6 File Offset: 0x000005C6
		[Browsable(false)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[TemplateContainer(typeof(AccordionContentPanel))]
		[DefaultValue(null)]
		public virtual ITemplate HeaderTemplate
		{
			get
			{
				return this._headerTemplate;
			}
			set
			{
				this._headerTemplate = value;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000021 RID: 33 RVA: 0x000023CF File Offset: 0x000005CF
		// (set) Token: 0x06000022 RID: 34 RVA: 0x000023D7 File Offset: 0x000005D7
		[TemplateContainer(typeof(AccordionContentPanel))]
		[Browsable(false)]
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public virtual ITemplate ContentTemplate
		{
			get
			{
				return this._contentTemplate;
			}
			set
			{
				this._contentTemplate = value;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000023 RID: 35 RVA: 0x000023E0 File Offset: 0x000005E0
		// (set) Token: 0x06000024 RID: 36 RVA: 0x000023E8 File Offset: 0x000005E8
		[Category("Data")]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Bindable(true)]
		public virtual object DataSource
		{
			get
			{
				return this._dataSource;
			}
			set
			{
				if (value == null || value is IListSource || value is IEnumerable)
				{
					this._dataSource = value;
					this.OnDataPropertyChanged();
					return;
				}
				throw new ArgumentException("Can't bind to value that is not an IListSource or an IEnumerable.");
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000025 RID: 37 RVA: 0x00002415 File Offset: 0x00000615
		// (set) Token: 0x06000026 RID: 38 RVA: 0x00002435 File Offset: 0x00000635
		[IDReferenceProperty(typeof(DataSourceControl))]
		[DefaultValue("")]
		[Category("Data")]
		public virtual string DataSourceID
		{
			get
			{
				return (this.ViewState["DataSourceID"] as string) ?? string.Empty;
			}
			set
			{
				this.ViewState["DataSourceID"] = value;
				this.OnDataPropertyChanged();
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000027 RID: 39 RVA: 0x0000244E File Offset: 0x0000064E
		// (set) Token: 0x06000028 RID: 40 RVA: 0x0000246E File Offset: 0x0000066E
		[DefaultValue("")]
		[Category("Data")]
		public virtual string DataMember
		{
			get
			{
				return (this.ViewState["DataMember"] as string) ?? string.Empty;
			}
			set
			{
				this.ViewState["DataMember"] = value;
				this.OnDataPropertyChanged();
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000029 RID: 41 RVA: 0x00002487 File Offset: 0x00000687
		protected bool IsBoundUsingDataSourceID
		{
			get
			{
				return !string.IsNullOrEmpty(this.DataSourceID);
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600002A RID: 42 RVA: 0x00002497 File Offset: 0x00000697
		// (set) Token: 0x0600002B RID: 43 RVA: 0x0000249F File Offset: 0x0000069F
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

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600002C RID: 44 RVA: 0x000024A8 File Offset: 0x000006A8
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

		// Token: 0x0600002D RID: 45 RVA: 0x000024C4 File Offset: 0x000006C4
		protected override void OnInit(EventArgs e)
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

		// Token: 0x0600002E RID: 46 RVA: 0x00002514 File Offset: 0x00000714
		private void OnPagePreLoad(object sender, EventArgs e)
		{
			this._initialized = true;
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
			this.EnsureChildControls();
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002598 File Offset: 0x00000798
		protected override void OnLoad(EventArgs e)
		{
			this._initialized = true;
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

		// Token: 0x06000030 RID: 48 RVA: 0x00002604 File Offset: 0x00000804
		protected override void CreateChildControls()
		{
			base.CreateChildControls();
			if (this.AccordionExtender != null && this.ViewState["_!ItemCount"] != null)
			{
				this.CreateControlHierarchy(false);
			}
			base.ClearChildViewState();
			foreach (AccordionPane accordionPane in this.Panes)
			{
				ControlCollection controls = accordionPane.Controls;
			}
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002680 File Offset: 0x00000880
		protected override void OnPreRender(EventArgs e)
		{
			this.EnsureDataBound();
			base.OnPreRender(e);
			if (this.AutoSize != AutoSize.None)
			{
				base.Style[HtmlTextWriterStyle.Overflow] = "hidden";
				base.Style[HtmlTextWriterStyle.OverflowX] = "auto";
			}
			foreach (AccordionPane accordionPane in this.Panes)
			{
				if (accordionPane.HeaderCssClass == this.HeaderSelectedCssClass)
				{
					accordionPane.HeaderCssClass = string.Empty;
				}
				if (!string.IsNullOrEmpty(this.HeaderCssClass) && string.IsNullOrEmpty(accordionPane.HeaderCssClass))
				{
					accordionPane.HeaderCssClass = this.HeaderCssClass;
				}
				if (!string.IsNullOrEmpty(this.ContentCssClass) && string.IsNullOrEmpty(accordionPane.ContentCssClass))
				{
					accordionPane.ContentCssClass = this.ContentCssClass;
				}
			}
			int num = this.AccordionExtender.SelectedIndex;
			num = (((num < 0 || num >= this.Panes.Count) && this.AccordionExtender.RequireOpenedPane) ? 0 : num);
			if (num >= 0 && num < this.Panes.Count)
			{
				AccordionContentPanel contentContainer = this.Panes[num].ContentContainer;
				if (contentContainer != null)
				{
					contentContainer.Collapsed = false;
				}
				if (!string.IsNullOrEmpty(this.HeaderSelectedCssClass))
				{
					this.Panes[num].HeaderCssClass = this.HeaderSelectedCssClass;
				}
			}
		}

		// Token: 0x06000032 RID: 50 RVA: 0x000027EC File Offset: 0x000009EC
		public override Control FindControl(string id)
		{
			Control control = base.FindControl(id);
			if (control == null)
			{
				foreach (AccordionPane accordionPane in this.Panes)
				{
					control = accordionPane.FindControl(id);
					if (control != null)
					{
						break;
					}
				}
			}
			return control;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x0000284C File Offset: 0x00000A4C
		internal void ClearPanes()
		{
			for (int i = this.Controls.Count - 1; i >= 0; i--)
			{
				if (this.Controls[i] is AccordionPane)
				{
					this.Controls.RemoveAt(i);
				}
			}
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002890 File Offset: 0x00000A90
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
			if (!string.IsNullOrEmpty(dataSourceID))
			{
				Control control = this.NamingContainer.FindControl(dataSourceID);
				if (control == null)
				{
					throw new HttpException(string.Format(CultureInfo.CurrentCulture, "DataSource '{1}' for control '{0}' doesn't exist", new object[]
					{
						this.ID,
						dataSourceID
					}));
				}
				dataSource = (control as IDataSource);
				if (dataSource == null)
				{
					throw new HttpException(string.Format(CultureInfo.CurrentCulture, "'{1}' is not a data source for control '{0}'.", new object[]
					{
						this.ID,
						dataSourceID
					}));
				}
			}
			if (dataSource == null)
			{
				return null;
			}
			if (this.DataSource != null)
			{
				throw new InvalidOperationException("DataSourceID and DataSource can't be set at the same time.");
			}
			DataSourceView view = dataSource.GetView(this.DataMember);
			if (view == null)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, "DataSourceView not found for control '{0}'", new object[]
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

		// Token: 0x06000035 RID: 53 RVA: 0x000029FC File Offset: 0x00000BFC
		public override void DataBind()
		{
			if (this.IsBoundUsingDataSourceID && base.DesignMode && base.Site == null)
			{
				return;
			}
			this.RequiresDataBinding = false;
			this.OnDataBinding(EventArgs.Empty);
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002A29 File Offset: 0x00000C29
		protected override void OnDataBinding(EventArgs e)
		{
			base.OnDataBinding(e);
			if (this.DataSource != null || this.IsBoundUsingDataSourceID)
			{
				this.ClearPanes();
				base.ClearChildViewState();
				this.CreateControlHierarchy(true);
				base.ChildControlsCreated = true;
			}
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002A5C File Offset: 0x00000C5C
		protected virtual void CreateControlHierarchy(bool useDataSource)
		{
			int num = -1;
			IEnumerable enumerable = null;
			List<AccordionPane> list = new List<AccordionPane>();
			if (!useDataSource)
			{
				object obj = this.ViewState["_!ItemCount"];
				if (obj != null)
				{
					num = (int)obj;
					if (num != -1)
					{
						List<object> list2 = new List<object>(num);
						for (int i = 0; i < num; i++)
						{
							list2.Add(null);
						}
						enumerable = list2;
						list.Capacity = num;
					}
				}
			}
			else
			{
				enumerable = this.GetData();
				num = 0;
				ICollection collection = enumerable as ICollection;
				if (collection != null)
				{
					list.Capacity = collection.Count;
				}
			}
			if (enumerable != null)
			{
				int num2 = 0;
				foreach (object dataItem in enumerable)
				{
					AccordionPane accordionPane = new AccordionPane();
					accordionPane.ID = string.Format(CultureInfo.InvariantCulture, "{0}_Pane_{1}", new object[]
					{
						this.ID,
						num2.ToString(CultureInfo.InvariantCulture)
					});
					this.Controls.Add(accordionPane);
					this.CreateItem(dataItem, num2, AccordionItemType.Header, accordionPane.HeaderContainer, this.HeaderTemplate, useDataSource);
					this.CreateItem(dataItem, num2, AccordionItemType.Content, accordionPane.ContentContainer, this.ContentTemplate, useDataSource);
					list.Add(accordionPane);
					num++;
					num2++;
				}
			}
			if (useDataSource)
			{
				this.ViewState["_!ItemCount"] = ((enumerable != null) ? num : -1);
			}
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002BE8 File Offset: 0x00000DE8
		private void CreateItem(object dataItem, int index, AccordionItemType itemType, AccordionContentPanel container, ITemplate template, bool dataBind)
		{
			if (template == null)
			{
				return;
			}
			AccordionItemEventArgs args = new AccordionItemEventArgs(container, itemType);
			this.OnItemCreated(args);
			container.SetDataItemProperties(dataItem, index, itemType);
			template.InstantiateIn(container);
			if (dataBind)
			{
				container.DataBind();
				this.OnItemDataBound(args);
			}
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002C30 File Offset: 0x00000E30
		protected void EnsureDataBound()
		{
			try
			{
				this._throwOnDataPropertyChange = true;
				if (this.RequiresDataBinding && !string.IsNullOrEmpty(this.DataSourceID))
				{
					this.DataBind();
				}
			}
			finally
			{
				this._throwOnDataPropertyChange = false;
			}
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002C7C File Offset: 0x00000E7C
		protected virtual IEnumerable GetData()
		{
			this._selectResult = null;
			DataSourceView dataSourceView = this.ConnectToDataSourceView();
			if (dataSourceView != null)
			{
				this._selectWait = new EventWaitHandle(false, EventResetMode.AutoReset);
				dataSourceView.Select(this.SelectArguments, new DataSourceViewSelectCallback(this.DoSelect));
				this._selectWait.WaitOne();
			}
			else if (this.DataSource != null)
			{
				this._selectResult = (this.DataSource as IEnumerable);
			}
			return this._selectResult;
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002CEC File Offset: 0x00000EEC
		protected virtual DataSourceSelectArguments CreateDataSourceSelectArguments()
		{
			return DataSourceSelectArguments.Empty;
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002CF3 File Offset: 0x00000EF3
		private void DoSelect(IEnumerable data)
		{
			this._selectResult = data;
			this._selectWait.Set();
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002D08 File Offset: 0x00000F08
		protected override bool OnBubbleEvent(object source, EventArgs args)
		{
			bool result = false;
			AccordionCommandEventArgs accordionCommandEventArgs = args as AccordionCommandEventArgs;
			if (accordionCommandEventArgs != null)
			{
				this.OnItemCommand(accordionCommandEventArgs);
				result = true;
			}
			return result;
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002D2B File Offset: 0x00000F2B
		protected virtual void OnDataPropertyChanged()
		{
			if (this._throwOnDataPropertyChange)
			{
				throw new HttpException("Invalid data property change");
			}
			if (this._initialized)
			{
				this.RequiresDataBinding = true;
			}
			this._currentViewValid = false;
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002D56 File Offset: 0x00000F56
		protected virtual void OnDataSourceViewChanged(object sender, EventArgs args)
		{
			this.RequiresDataBinding = true;
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002D5F File Offset: 0x00000F5F
		protected virtual void OnItemCommand(AccordionCommandEventArgs args)
		{
			if (this.ItemCommand != null)
			{
				this.ItemCommand(this, args);
			}
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002D76 File Offset: 0x00000F76
		protected virtual void OnItemCreated(AccordionItemEventArgs args)
		{
			if (this.ItemCreated != null)
			{
				this.ItemCreated(this, args);
			}
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002D8D File Offset: 0x00000F8D
		protected virtual void OnItemDataBound(AccordionItemEventArgs args)
		{
			if (this.ItemDataBound != null)
			{
				this.ItemDataBound(this, args);
			}
		}

		// Token: 0x04000001 RID: 1
		internal const string ItemCountViewStateKey = "_!ItemCount";

		// Token: 0x04000005 RID: 5
		private AccordionExtender _extender;

		// Token: 0x04000006 RID: 6
		private AccordionPaneCollection _panes;

		// Token: 0x04000007 RID: 7
		private object _dataSource;

		// Token: 0x04000008 RID: 8
		private ITemplate _headerTemplate;

		// Token: 0x04000009 RID: 9
		private ITemplate _contentTemplate;

		// Token: 0x0400000A RID: 10
		private bool _initialized;

		// Token: 0x0400000B RID: 11
		private bool _pagePreLoadFired;

		// Token: 0x0400000C RID: 12
		private bool _requiresDataBinding;

		// Token: 0x0400000D RID: 13
		private bool _throwOnDataPropertyChange;

		// Token: 0x0400000E RID: 14
		private DataSourceView _currentView;

		// Token: 0x0400000F RID: 15
		private bool _currentViewIsFromDataSourceID;

		// Token: 0x04000010 RID: 16
		private bool _currentViewValid;

		// Token: 0x04000011 RID: 17
		private DataSourceSelectArguments _arguments;

		// Token: 0x04000012 RID: 18
		private IEnumerable _selectResult;

		// Token: 0x04000013 RID: 19
		private EventWaitHandle _selectWait;
	}
}
