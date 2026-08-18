using System;
using System.Collections;
using System.ComponentModel;
using System.Web.Util;

namespace System.Web.UI.WebControls
{
	// Token: 0x0200037D RID: 893
	[DefaultEvent("SelectedIndexChanged")]
	[DefaultProperty("DataSource")]
	[Designer("System.Web.UI.Design.WebControls.BaseDataListDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public abstract class BaseDataList : WebControl
	{
		// Token: 0x17000B6D RID: 2925
		// (get) Token: 0x06002936 RID: 10550 RVA: 0x000855D8 File Offset: 0x000837D8
		// (set) Token: 0x06002937 RID: 10551 RVA: 0x00085605 File Offset: 0x00083805
		[DefaultValue("")]
		[Localizable(true)]
		[WebCategory("Accessibility")]
		[WebSysDescription("DataControls_Caption")]
		public virtual string Caption
		{
			get
			{
				string text = (string)this.ViewState["Caption"];
				if (text == null)
				{
					return string.Empty;
				}
				return text;
			}
			set
			{
				this.ViewState["Caption"] = value;
			}
		}

		// Token: 0x17000B6E RID: 2926
		// (get) Token: 0x06002938 RID: 10552 RVA: 0x00085618 File Offset: 0x00083818
		// (set) Token: 0x06002939 RID: 10553 RVA: 0x00085641 File Offset: 0x00083841
		[DefaultValue(TableCaptionAlign.NotSet)]
		[WebCategory("Accessibility")]
		[WebSysDescription("WebControl_CaptionAlign")]
		public virtual TableCaptionAlign CaptionAlign
		{
			get
			{
				object obj = this.ViewState["CaptionAlign"];
				if (obj == null)
				{
					return TableCaptionAlign.NotSet;
				}
				return (TableCaptionAlign)obj;
			}
			set
			{
				if (value < TableCaptionAlign.NotSet || value > TableCaptionAlign.Right)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.ViewState["CaptionAlign"] = value;
			}
		}

		// Token: 0x17000B6F RID: 2927
		// (get) Token: 0x0600293A RID: 10554 RVA: 0x0008566C File Offset: 0x0008386C
		// (set) Token: 0x0600293B RID: 10555 RVA: 0x00085688 File Offset: 0x00083888
		[WebCategory("Layout")]
		[DefaultValue(-1)]
		[WebSysDescription("BaseDataList_CellPadding")]
		public virtual int CellPadding
		{
			get
			{
				if (!base.ControlStyleCreated)
				{
					return -1;
				}
				return ((TableStyle)base.ControlStyle).CellPadding;
			}
			set
			{
				((TableStyle)base.ControlStyle).CellPadding = value;
			}
		}

		// Token: 0x17000B70 RID: 2928
		// (get) Token: 0x0600293C RID: 10556 RVA: 0x0008569B File Offset: 0x0008389B
		// (set) Token: 0x0600293D RID: 10557 RVA: 0x000856B7 File Offset: 0x000838B7
		[WebCategory("Layout")]
		[DefaultValue(0)]
		[WebSysDescription("BaseDataList_CellSpacing")]
		public virtual int CellSpacing
		{
			get
			{
				if (!base.ControlStyleCreated)
				{
					return 0;
				}
				return ((TableStyle)base.ControlStyle).CellSpacing;
			}
			set
			{
				((TableStyle)base.ControlStyle).CellSpacing = value;
			}
		}

		// Token: 0x17000B71 RID: 2929
		// (get) Token: 0x0600293E RID: 10558 RVA: 0x000856CA File Offset: 0x000838CA
		public override ControlCollection Controls
		{
			get
			{
				this.EnsureChildControls();
				return base.Controls;
			}
		}

		// Token: 0x17000B72 RID: 2930
		// (get) Token: 0x0600293F RID: 10559 RVA: 0x000856D8 File Offset: 0x000838D8
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[WebSysDescription("BaseDataList_DataKeys")]
		public DataKeyCollection DataKeys
		{
			get
			{
				if (this.dataKeysCollection == null)
				{
					this.dataKeysCollection = new DataKeyCollection(this.DataKeysArray);
				}
				return this.dataKeysCollection;
			}
		}

		// Token: 0x17000B73 RID: 2931
		// (get) Token: 0x06002940 RID: 10560 RVA: 0x000856FC File Offset: 0x000838FC
		protected ArrayList DataKeysArray
		{
			get
			{
				object obj = this.ViewState["DataKeys"];
				if (obj == null)
				{
					obj = new ArrayList();
					this.ViewState["DataKeys"] = obj;
				}
				return (ArrayList)obj;
			}
		}

		// Token: 0x17000B74 RID: 2932
		// (get) Token: 0x06002941 RID: 10561 RVA: 0x0008573C File Offset: 0x0008393C
		// (set) Token: 0x06002942 RID: 10562 RVA: 0x00085769 File Offset: 0x00083969
		[DefaultValue("")]
		[Themeable(false)]
		[WebCategory("Data")]
		[WebSysDescription("BaseDataList_DataKeyField")]
		public virtual string DataKeyField
		{
			get
			{
				object obj = this.ViewState["DataKeyField"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["DataKeyField"] = value;
			}
		}

		// Token: 0x17000B75 RID: 2933
		// (get) Token: 0x06002943 RID: 10563 RVA: 0x0008577C File Offset: 0x0008397C
		// (set) Token: 0x06002944 RID: 10564 RVA: 0x000857A9 File Offset: 0x000839A9
		[DefaultValue("")]
		[Themeable(false)]
		[WebCategory("Data")]
		[WebSysDescription("BaseDataList_DataMember")]
		public string DataMember
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

		// Token: 0x17000B76 RID: 2934
		// (get) Token: 0x06002945 RID: 10565 RVA: 0x000857C2 File Offset: 0x000839C2
		// (set) Token: 0x06002946 RID: 10566 RVA: 0x000857CC File Offset: 0x000839CC
		[Bindable(true)]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Themeable(false)]
		[WebCategory("Data")]
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

		// Token: 0x17000B77 RID: 2935
		// (get) Token: 0x06002947 RID: 10567 RVA: 0x00085818 File Offset: 0x00083A18
		// (set) Token: 0x06002948 RID: 10568 RVA: 0x00085845 File Offset: 0x00083A45
		[DefaultValue("")]
		[IDReferenceProperty(typeof(DataSourceControl))]
		[Themeable(false)]
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

		// Token: 0x17000B78 RID: 2936
		// (get) Token: 0x06002949 RID: 10569 RVA: 0x0008585E File Offset: 0x00083A5E
		// (set) Token: 0x0600294A RID: 10570 RVA: 0x0008587A File Offset: 0x00083A7A
		[WebCategory("Appearance")]
		[DefaultValue(GridLines.Both)]
		[WebSysDescription("DataControls_GridLines")]
		public virtual GridLines GridLines
		{
			get
			{
				if (!base.ControlStyleCreated)
				{
					return GridLines.Both;
				}
				return ((TableStyle)base.ControlStyle).GridLines;
			}
			set
			{
				((TableStyle)base.ControlStyle).GridLines = value;
			}
		}

		// Token: 0x17000B79 RID: 2937
		// (get) Token: 0x0600294B RID: 10571 RVA: 0x0008588D File Offset: 0x00083A8D
		// (set) Token: 0x0600294C RID: 10572 RVA: 0x000858A9 File Offset: 0x00083AA9
		[Category("Layout")]
		[DefaultValue(HorizontalAlign.NotSet)]
		[WebSysDescription("WebControl_HorizontalAlign")]
		public virtual HorizontalAlign HorizontalAlign
		{
			get
			{
				if (!base.ControlStyleCreated)
				{
					return HorizontalAlign.NotSet;
				}
				return ((TableStyle)base.ControlStyle).HorizontalAlign;
			}
			set
			{
				((TableStyle)base.ControlStyle).HorizontalAlign = value;
			}
		}

		// Token: 0x17000B7A RID: 2938
		// (get) Token: 0x0600294D RID: 10573 RVA: 0x000858BC File Offset: 0x00083ABC
		protected bool Initialized
		{
			get
			{
				return this._inited;
			}
		}

		// Token: 0x17000B7B RID: 2939
		// (get) Token: 0x0600294E RID: 10574 RVA: 0x000858C4 File Offset: 0x00083AC4
		protected bool IsBoundUsingDataSourceID
		{
			get
			{
				return this.DataSourceID.Length > 0;
			}
		}

		// Token: 0x17000B7C RID: 2940
		// (get) Token: 0x0600294F RID: 10575 RVA: 0x000853AC File Offset: 0x000835AC
		public override bool SupportsDisabledAttribute
		{
			get
			{
				return this.RenderingCompatibility < VersionUtil.Framework40;
			}
		}

		// Token: 0x17000B7D RID: 2941
		// (get) Token: 0x06002950 RID: 10576 RVA: 0x000858D4 File Offset: 0x00083AD4
		// (set) Token: 0x06002951 RID: 10577 RVA: 0x000858DC File Offset: 0x00083ADC
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

		// Token: 0x17000B7E RID: 2942
		// (get) Token: 0x06002952 RID: 10578 RVA: 0x000858E5 File Offset: 0x00083AE5
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

		// Token: 0x17000B7F RID: 2943
		// (get) Token: 0x06002953 RID: 10579 RVA: 0x00085904 File Offset: 0x00083B04
		// (set) Token: 0x06002954 RID: 10580 RVA: 0x0008592D File Offset: 0x00083B2D
		[DefaultValue(false)]
		[WebCategory("Accessibility")]
		[WebSysDescription("Table_UseAccessibleHeader")]
		public virtual bool UseAccessibleHeader
		{
			get
			{
				object obj = this.ViewState["UseAccessibleHeader"];
				return obj != null && (bool)obj;
			}
			set
			{
				this.ViewState["UseAccessibleHeader"] = value;
			}
		}

		// Token: 0x1400004C RID: 76
		// (add) Token: 0x06002955 RID: 10581 RVA: 0x00085945 File Offset: 0x00083B45
		// (remove) Token: 0x06002956 RID: 10582 RVA: 0x00085958 File Offset: 0x00083B58
		[WebCategory("Action")]
		[WebSysDescription("BaseDataList_OnSelectedIndexChanged")]
		public event EventHandler SelectedIndexChanged
		{
			add
			{
				base.Events.AddHandler(BaseDataList.EventSelectedIndexChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(BaseDataList.EventSelectedIndexChanged, value);
			}
		}

		// Token: 0x06002957 RID: 10583 RVA: 0x00006164 File Offset: 0x00004364
		protected override void AddParsedSubObject(object obj)
		{
		}

		// Token: 0x06002958 RID: 10584 RVA: 0x0008596C File Offset: 0x00083B6C
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

		// Token: 0x06002959 RID: 10585 RVA: 0x00085AD9 File Offset: 0x00083CD9
		protected internal override void CreateChildControls()
		{
			this.Controls.Clear();
			if (this.ViewState["_!ItemCount"] == null)
			{
				if (this.RequiresDataBinding)
				{
					this.EnsureDataBound();
					return;
				}
			}
			else
			{
				this.CreateControlHierarchy(false);
				base.ClearChildViewState();
			}
		}

		// Token: 0x0600295A RID: 10586
		protected abstract void CreateControlHierarchy(bool useDataSource);

		// Token: 0x0600295B RID: 10587 RVA: 0x00085B14 File Offset: 0x00083D14
		public override void DataBind()
		{
			if (this.IsBoundUsingDataSourceID && base.DesignMode && base.Site == null)
			{
				return;
			}
			this.RequiresDataBinding = false;
			this.OnDataBinding(EventArgs.Empty);
		}

		// Token: 0x0600295C RID: 10588 RVA: 0x00085B41 File Offset: 0x00083D41
		protected virtual DataSourceSelectArguments CreateDataSourceSelectArguments()
		{
			return DataSourceSelectArguments.Empty;
		}

		// Token: 0x0600295D RID: 10589 RVA: 0x00085B48 File Offset: 0x00083D48
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

		// Token: 0x0600295E RID: 10590 RVA: 0x00085B94 File Offset: 0x00083D94
		protected virtual IEnumerable GetData()
		{
			this.ConnectToDataSourceView();
			if (this._currentView != null)
			{
				return this._currentView.ExecuteSelect(this.SelectArguments);
			}
			return null;
		}

		// Token: 0x0600295F RID: 10591 RVA: 0x00085BB8 File Offset: 0x00083DB8
		public static bool IsBindableType(Type type)
		{
			return type.IsPrimitive || type == typeof(string) || type == typeof(DateTime) || type == typeof(decimal);
		}

		// Token: 0x06002960 RID: 10592 RVA: 0x00085BF8 File Offset: 0x00083DF8
		protected override void OnDataBinding(EventArgs e)
		{
			base.OnDataBinding(e);
			this.Controls.Clear();
			base.ClearChildViewState();
			this.dataKeysCollection = null;
			this.CreateControlHierarchy(true);
			base.ChildControlsCreated = true;
			this.TrackViewState();
		}

		// Token: 0x06002961 RID: 10593 RVA: 0x00085C2D File Offset: 0x00083E2D
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

		// Token: 0x06002962 RID: 10594 RVA: 0x00085C6C File Offset: 0x00083E6C
		protected virtual void OnDataSourceViewChanged(object sender, EventArgs e)
		{
			this.RequiresDataBinding = true;
		}

		// Token: 0x06002963 RID: 10595 RVA: 0x00085C78 File Offset: 0x00083E78
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

		// Token: 0x06002964 RID: 10596 RVA: 0x00085CC8 File Offset: 0x00083EC8
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

		// Token: 0x06002965 RID: 10597 RVA: 0x00085D34 File Offset: 0x00083F34
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
			}
			this._pagePreLoadFired = true;
		}

		// Token: 0x06002966 RID: 10598 RVA: 0x00085DB0 File Offset: 0x00083FB0
		protected internal override void OnPreRender(EventArgs e)
		{
			this.EnsureDataBound();
			base.OnPreRender(e);
		}

		// Token: 0x06002967 RID: 10599 RVA: 0x00085DC0 File Offset: 0x00083FC0
		protected virtual void OnSelectedIndexChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[BaseDataList.EventSelectedIndexChanged];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x06002968 RID: 10600
		protected internal abstract void PrepareControlHierarchy();

		// Token: 0x06002969 RID: 10601 RVA: 0x00085DEE File Offset: 0x00083FEE
		protected internal override void Render(HtmlTextWriter writer)
		{
			this.PrepareControlHierarchy();
			this.RenderContents(writer);
		}

		// Token: 0x04001E4E RID: 7758
		private static readonly object EventSelectedIndexChanged = new object();

		// Token: 0x04001E4F RID: 7759
		internal const string ItemCountViewStateKey = "_!ItemCount";

		// Token: 0x04001E50 RID: 7760
		private object dataSource;

		// Token: 0x04001E51 RID: 7761
		private DataKeyCollection dataKeysCollection;

		// Token: 0x04001E52 RID: 7762
		private bool _requiresDataBinding;

		// Token: 0x04001E53 RID: 7763
		private bool _inited;

		// Token: 0x04001E54 RID: 7764
		private bool _throwOnDataPropertyChange;

		// Token: 0x04001E55 RID: 7765
		private DataSourceView _currentView;

		// Token: 0x04001E56 RID: 7766
		private bool _currentViewIsFromDataSourceID;

		// Token: 0x04001E57 RID: 7767
		private bool _currentViewValid;

		// Token: 0x04001E58 RID: 7768
		private DataSourceSelectArguments _arguments;

		// Token: 0x04001E59 RID: 7769
		private bool _pagePreLoadFired;
	}
}
