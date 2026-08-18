using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;
using System.Web.UI.WebControls;

namespace System.Web.UI.HtmlControls
{
	// Token: 0x0200035A RID: 858
	[DefaultEvent("ServerChange")]
	[ValidationProperty("Value")]
	[ControlBuilder(typeof(HtmlSelectBuilder))]
	[SupportsEventValidation]
	public class HtmlSelect : HtmlContainerControl, IPostBackDataHandler, IParserAccessor
	{
		// Token: 0x06002768 RID: 10088 RVA: 0x000800E3 File Offset: 0x0007E2E3
		public HtmlSelect() : base("select")
		{
			this.cachedSelectedIndex = -1;
		}

		// Token: 0x17000ADE RID: 2782
		// (get) Token: 0x06002769 RID: 10089 RVA: 0x000800F8 File Offset: 0x0007E2F8
		// (set) Token: 0x0600276A RID: 10090 RVA: 0x00080125 File Offset: 0x0007E325
		[DefaultValue("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[WebCategory("Data")]
		[WebSysDescription("HtmlSelect_DataMember")]
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
				base.Attributes["DataMember"] = HtmlControl.MapStringAttributeToString(value);
				this.OnDataPropertyChanged();
			}
		}

		// Token: 0x17000ADF RID: 2783
		// (get) Token: 0x0600276B RID: 10091 RVA: 0x00080143 File Offset: 0x0007E343
		// (set) Token: 0x0600276C RID: 10092 RVA: 0x0008014C File Offset: 0x0007E34C
		[WebCategory("Data")]
		[DefaultValue(null)]
		[WebSysDescription("BaseDataBoundControl_DataSource")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

		// Token: 0x17000AE0 RID: 2784
		// (get) Token: 0x0600276D RID: 10093 RVA: 0x00080198 File Offset: 0x0007E398
		// (set) Token: 0x0600276E RID: 10094 RVA: 0x000801C5 File Offset: 0x0007E3C5
		[DefaultValue("")]
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

		// Token: 0x17000AE1 RID: 2785
		// (get) Token: 0x0600276F RID: 10095 RVA: 0x000801E0 File Offset: 0x0007E3E0
		// (set) Token: 0x06002770 RID: 10096 RVA: 0x00080208 File Offset: 0x0007E408
		[WebCategory("Data")]
		[DefaultValue("")]
		[WebSysDescription("HtmlSelect_DataTextField")]
		public virtual string DataTextField
		{
			get
			{
				string text = base.Attributes["DataTextField"];
				if (text != null)
				{
					return text;
				}
				return string.Empty;
			}
			set
			{
				base.Attributes["DataTextField"] = HtmlControl.MapStringAttributeToString(value);
				if (this._inited)
				{
					this.RequiresDataBinding = true;
				}
			}
		}

		// Token: 0x17000AE2 RID: 2786
		// (get) Token: 0x06002771 RID: 10097 RVA: 0x00080230 File Offset: 0x0007E430
		// (set) Token: 0x06002772 RID: 10098 RVA: 0x00080258 File Offset: 0x0007E458
		[WebCategory("Data")]
		[DefaultValue("")]
		[WebSysDescription("HtmlSelect_DataValueField")]
		public virtual string DataValueField
		{
			get
			{
				string text = base.Attributes["DataValueField"];
				if (text != null)
				{
					return text;
				}
				return string.Empty;
			}
			set
			{
				base.Attributes["DataValueField"] = HtmlControl.MapStringAttributeToString(value);
				if (this._inited)
				{
					this.RequiresDataBinding = true;
				}
			}
		}

		// Token: 0x17000AE3 RID: 2787
		// (get) Token: 0x06002773 RID: 10099 RVA: 0x0008027F File Offset: 0x0007E47F
		// (set) Token: 0x06002774 RID: 10100 RVA: 0x0008027F File Offset: 0x0007E47F
		public override string InnerHtml
		{
			get
			{
				throw new NotSupportedException(SR.GetString("InnerHtml_not_supported", new object[]
				{
					base.GetType().Name
				}));
			}
			set
			{
				throw new NotSupportedException(SR.GetString("InnerHtml_not_supported", new object[]
				{
					base.GetType().Name
				}));
			}
		}

		// Token: 0x17000AE4 RID: 2788
		// (get) Token: 0x06002775 RID: 10101 RVA: 0x000802A4 File Offset: 0x0007E4A4
		// (set) Token: 0x06002776 RID: 10102 RVA: 0x000802A4 File Offset: 0x0007E4A4
		public override string InnerText
		{
			get
			{
				throw new NotSupportedException(SR.GetString("InnerText_not_supported", new object[]
				{
					base.GetType().Name
				}));
			}
			set
			{
				throw new NotSupportedException(SR.GetString("InnerText_not_supported", new object[]
				{
					base.GetType().Name
				}));
			}
		}

		// Token: 0x17000AE5 RID: 2789
		// (get) Token: 0x06002777 RID: 10103 RVA: 0x000802C9 File Offset: 0x0007E4C9
		protected bool IsBoundUsingDataSourceID
		{
			get
			{
				return this.DataSourceID.Length > 0;
			}
		}

		// Token: 0x17000AE6 RID: 2790
		// (get) Token: 0x06002778 RID: 10104 RVA: 0x000802D9 File Offset: 0x0007E4D9
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public ListItemCollection Items
		{
			get
			{
				if (this.items == null)
				{
					this.items = new ListItemCollection();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this.items).TrackViewState();
					}
				}
				return this.items;
			}
		}

		// Token: 0x17000AE7 RID: 2791
		// (get) Token: 0x06002779 RID: 10105 RVA: 0x00080308 File Offset: 0x0007E508
		// (set) Token: 0x0600277A RID: 10106 RVA: 0x00080336 File Offset: 0x0007E536
		[WebCategory("Behavior")]
		[DefaultValue("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool Multiple
		{
			get
			{
				string text = base.Attributes["multiple"];
				return text != null && text.Equals("multiple");
			}
			set
			{
				if (value)
				{
					base.Attributes["multiple"] = "multiple";
					return;
				}
				base.Attributes["multiple"] = null;
			}
		}

		// Token: 0x17000AE8 RID: 2792
		// (get) Token: 0x0600277B RID: 10107 RVA: 0x0007F357 File Offset: 0x0007D557
		// (set) Token: 0x0600277C RID: 10108 RVA: 0x00006164 File Offset: 0x00004364
		[WebCategory("Behavior")]
		[DefaultValue("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string Name
		{
			get
			{
				return this.UniqueID;
			}
			set
			{
			}
		}

		// Token: 0x17000AE9 RID: 2793
		// (get) Token: 0x0600277D RID: 10109 RVA: 0x00080362 File Offset: 0x0007E562
		internal string RenderedNameAttribute
		{
			get
			{
				return this.Name;
			}
		}

		// Token: 0x17000AEA RID: 2794
		// (get) Token: 0x0600277E RID: 10110 RVA: 0x0008036A File Offset: 0x0007E56A
		// (set) Token: 0x0600277F RID: 10111 RVA: 0x00080372 File Offset: 0x0007E572
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

		// Token: 0x17000AEB RID: 2795
		// (get) Token: 0x06002780 RID: 10112 RVA: 0x0008037C File Offset: 0x0007E57C
		// (set) Token: 0x06002781 RID: 10113 RVA: 0x000803E8 File Offset: 0x0007E5E8
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[HtmlControlPersistable(false)]
		public virtual int SelectedIndex
		{
			get
			{
				for (int i = 0; i < this.Items.Count; i++)
				{
					if (this.Items[i].Selected)
					{
						return i;
					}
				}
				if (this.Size <= 1 && !this.Multiple)
				{
					if (this.Items.Count > 0)
					{
						this.Items[0].Selected = true;
					}
					return 0;
				}
				return -1;
			}
			set
			{
				if (this.Items.Count == 0)
				{
					this.cachedSelectedIndex = value;
					return;
				}
				if (value < -1 || value >= this.Items.Count)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.ClearSelection();
				if (value >= 0)
				{
					this.Items[value].Selected = true;
				}
			}
		}

		// Token: 0x17000AEC RID: 2796
		// (get) Token: 0x06002782 RID: 10114 RVA: 0x00080444 File Offset: 0x0007E644
		protected virtual int[] SelectedIndices
		{
			get
			{
				int num = 0;
				int[] array = new int[3];
				for (int i = 0; i < this.Items.Count; i++)
				{
					if (this.Items[i].Selected)
					{
						if (num == array.Length)
						{
							int[] array2 = new int[num + num];
							array.CopyTo(array2, 0);
							array = array2;
						}
						array[num++] = i;
					}
				}
				int[] array3 = new int[num];
				Array.Copy(array, 0, array3, 0, num);
				return array3;
			}
		}

		// Token: 0x17000AED RID: 2797
		// (get) Token: 0x06002783 RID: 10115 RVA: 0x000804BC File Offset: 0x0007E6BC
		// (set) Token: 0x06002784 RID: 10116 RVA: 0x0007F54E File Offset: 0x0007D74E
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int Size
		{
			get
			{
				string text = base.Attributes["size"];
				if (text == null)
				{
					return -1;
				}
				return int.Parse(text, CultureInfo.InvariantCulture);
			}
			set
			{
				base.Attributes["size"] = HtmlControl.MapIntegerAttributeToString(value);
			}
		}

		// Token: 0x17000AEE RID: 2798
		// (get) Token: 0x06002785 RID: 10117 RVA: 0x000804EC File Offset: 0x0007E6EC
		// (set) Token: 0x06002786 RID: 10118 RVA: 0x0008052C File Offset: 0x0007E72C
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string Value
		{
			get
			{
				int selectedIndex = this.SelectedIndex;
				if (selectedIndex >= 0 && selectedIndex < this.Items.Count)
				{
					return this.Items[selectedIndex].Value;
				}
				return string.Empty;
			}
			set
			{
				int num = this.Items.FindByValueInternal(value, true);
				if (num >= 0)
				{
					this.SelectedIndex = num;
				}
			}
		}

		// Token: 0x14000048 RID: 72
		// (add) Token: 0x06002787 RID: 10119 RVA: 0x00080552 File Offset: 0x0007E752
		// (remove) Token: 0x06002788 RID: 10120 RVA: 0x00080565 File Offset: 0x0007E765
		[WebCategory("Action")]
		[WebSysDescription("HtmlSelect_OnServerChange")]
		public event EventHandler ServerChange
		{
			add
			{
				base.Events.AddHandler(HtmlSelect.EventServerChange, value);
			}
			remove
			{
				base.Events.RemoveHandler(HtmlSelect.EventServerChange, value);
			}
		}

		// Token: 0x06002789 RID: 10121 RVA: 0x00080578 File Offset: 0x0007E778
		protected override void AddParsedSubObject(object obj)
		{
			if (obj is ListItem)
			{
				this.Items.Add((ListItem)obj);
				return;
			}
			throw new HttpException(SR.GetString("Cannot_Have_Children_Of_Type", new object[]
			{
				"HtmlSelect",
				obj.GetType().Name
			}));
		}

		// Token: 0x0600278A RID: 10122 RVA: 0x000805CC File Offset: 0x0007E7CC
		protected virtual void ClearSelection()
		{
			for (int i = 0; i < this.Items.Count; i++)
			{
				this.Items[i].Selected = false;
			}
		}

		// Token: 0x0600278B RID: 10123 RVA: 0x00080604 File Offset: 0x0007E804
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

		// Token: 0x0600278C RID: 10124 RVA: 0x00060B2F File Offset: 0x0005ED2F
		protected override ControlCollection CreateControlCollection()
		{
			return new EmptyControlCollection(this);
		}

		// Token: 0x0600278D RID: 10125 RVA: 0x00080774 File Offset: 0x0007E974
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

		// Token: 0x0600278E RID: 10126 RVA: 0x000807C0 File Offset: 0x0007E9C0
		protected virtual IEnumerable GetData()
		{
			DataSourceView dataSourceView = this.ConnectToDataSourceView();
			if (dataSourceView != null)
			{
				return dataSourceView.ExecuteSelect(DataSourceSelectArguments.Empty);
			}
			return null;
		}

		// Token: 0x0600278F RID: 10127 RVA: 0x000807E4 File Offset: 0x0007E9E4
		protected override void LoadViewState(object savedState)
		{
			if (savedState != null)
			{
				Triplet triplet = (Triplet)savedState;
				base.LoadViewState(triplet.First);
				((IStateManager)this.Items).LoadViewState(triplet.Second);
				object third = triplet.Third;
				if (third != null)
				{
					this.Select((int[])third);
				}
			}
		}

		// Token: 0x06002790 RID: 10128 RVA: 0x00080830 File Offset: 0x0007EA30
		protected override void OnDataBinding(EventArgs e)
		{
			base.OnDataBinding(e);
			IEnumerable data = this.GetData();
			if (data != null)
			{
				bool flag = false;
				string dataTextField = this.DataTextField;
				string dataValueField = this.DataValueField;
				this.Items.Clear();
				ICollection collection = data as ICollection;
				if (collection != null)
				{
					this.Items.Capacity = collection.Count;
				}
				if (dataTextField.Length != 0 || dataValueField.Length != 0)
				{
					flag = true;
				}
				foreach (object obj in data)
				{
					ListItem listItem = new ListItem();
					if (flag)
					{
						if (dataTextField.Length > 0)
						{
							listItem.Text = DataBinder.GetPropertyValue(obj, dataTextField, null);
						}
						if (dataValueField.Length > 0)
						{
							listItem.Value = DataBinder.GetPropertyValue(obj, dataValueField, null);
						}
					}
					else
					{
						listItem.Text = (listItem.Value = obj.ToString());
					}
					this.Items.Add(listItem);
				}
			}
			if (this.cachedSelectedIndex != -1)
			{
				this.SelectedIndex = this.cachedSelectedIndex;
				this.cachedSelectedIndex = -1;
			}
			this.ViewState["_!DataBound"] = true;
			this.RequiresDataBinding = false;
		}

		// Token: 0x06002791 RID: 10129 RVA: 0x00080980 File Offset: 0x0007EB80
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

		// Token: 0x06002792 RID: 10130 RVA: 0x000809BF File Offset: 0x0007EBBF
		protected virtual void OnDataSourceViewChanged(object sender, EventArgs e)
		{
			this.RequiresDataBinding = true;
		}

		// Token: 0x06002793 RID: 10131 RVA: 0x000809C8 File Offset: 0x0007EBC8
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

		// Token: 0x06002794 RID: 10132 RVA: 0x00080A18 File Offset: 0x0007EC18
		protected internal override void OnLoad(EventArgs e)
		{
			this._inited = true;
			this.ConnectToDataSourceView();
			if (this.Page != null && !this._pagePreLoadFired && this.ViewState["_!DataBound"] == null)
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

		// Token: 0x06002795 RID: 10133 RVA: 0x00080A84 File Offset: 0x0007EC84
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
				if (this.Page.IsPostBack && base.IsViewStateEnabled && this.ViewState["_!DataBound"] == null)
				{
					this.RequiresDataBinding = true;
				}
			}
			this._pagePreLoadFired = true;
		}

		// Token: 0x06002796 RID: 10134 RVA: 0x00080B00 File Offset: 0x0007ED00
		protected internal override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			if (this.Page != null && !base.Disabled)
			{
				if (this.Size > 1)
				{
					this.Page.RegisterRequiresPostBack(this);
				}
				this.Page.RegisterEnabledControl(this);
			}
			this.EnsureDataBound();
		}

		// Token: 0x06002797 RID: 10135 RVA: 0x00080B40 File Offset: 0x0007ED40
		protected virtual void OnServerChange(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[HtmlSelect.EventServerChange];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x06002798 RID: 10136 RVA: 0x00080B70 File Offset: 0x0007ED70
		protected override void RenderAttributes(HtmlTextWriter writer)
		{
			if (this.Page != null)
			{
				this.Page.ClientScript.RegisterForEventValidation(this.RenderedNameAttribute);
			}
			writer.WriteAttribute("name", this.RenderedNameAttribute);
			base.Attributes.Remove("name");
			base.Attributes.Remove("DataValueField");
			base.Attributes.Remove("DataTextField");
			base.Attributes.Remove("DataMember");
			base.Attributes.Remove("DataSourceID");
			base.RenderAttributes(writer);
		}

		// Token: 0x06002799 RID: 10137 RVA: 0x00080C04 File Offset: 0x0007EE04
		protected internal override void RenderChildren(HtmlTextWriter writer)
		{
			bool flag = false;
			bool flag2 = !this.Multiple;
			writer.WriteLine();
			int indent = writer.Indent;
			writer.Indent = indent + 1;
			ListItemCollection listItemCollection = this.Items;
			int count = listItemCollection.Count;
			if (count > 0)
			{
				for (int i = 0; i < count; i++)
				{
					ListItem listItem = listItemCollection[i];
					writer.WriteBeginTag("option");
					if (listItem.Selected)
					{
						if (flag2)
						{
							if (flag)
							{
								throw new HttpException(SR.GetString("HtmlSelect_Cant_Multiselect_In_Single_Mode"));
							}
							flag = true;
						}
						writer.WriteAttribute("selected", "selected");
					}
					writer.WriteAttribute("value", listItem.Value, true);
					listItem.Attributes.Remove("text");
					listItem.Attributes.Remove("value");
					listItem.Attributes.Remove("selected");
					listItem.Attributes.Render(writer);
					writer.Write('>');
					HttpUtility.HtmlEncode(listItem.Text, writer);
					writer.WriteEndTag("option");
					writer.WriteLine();
				}
			}
			indent = writer.Indent;
			writer.Indent = indent - 1;
		}

		// Token: 0x0600279A RID: 10138 RVA: 0x00080D34 File Offset: 0x0007EF34
		protected override object SaveViewState()
		{
			object obj = base.SaveViewState();
			object obj2 = ((IStateManager)this.Items).SaveViewState();
			object obj3 = null;
			if (base.Events[HtmlSelect.EventServerChange] != null || base.Disabled || !this.Visible)
			{
				obj3 = this.SelectedIndices;
			}
			if (obj3 != null || obj2 != null || obj != null)
			{
				return new Triplet(obj, obj2, obj3);
			}
			return null;
		}

		// Token: 0x0600279B RID: 10139 RVA: 0x00080D94 File Offset: 0x0007EF94
		protected virtual void Select(int[] selectedIndices)
		{
			this.ClearSelection();
			foreach (int num in selectedIndices)
			{
				if (num >= 0 && num < this.Items.Count)
				{
					this.Items[num].Selected = true;
				}
			}
		}

		// Token: 0x0600279C RID: 10140 RVA: 0x00080DDD File Offset: 0x0007EFDD
		protected override void TrackViewState()
		{
			base.TrackViewState();
			((IStateManager)this.Items).TrackViewState();
		}

		// Token: 0x0600279D RID: 10141 RVA: 0x00080DF0 File Offset: 0x0007EFF0
		bool IPostBackDataHandler.LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			return this.LoadPostData(postDataKey, postCollection);
		}

		// Token: 0x0600279E RID: 10142 RVA: 0x00080DFC File Offset: 0x0007EFFC
		protected virtual bool LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			string[] values = postCollection.GetValues(postDataKey);
			bool flag = false;
			if (values != null)
			{
				if (!this.Multiple)
				{
					int num = this.Items.FindByValueInternal(values[0], false);
					if (this.SelectedIndex != num)
					{
						this.SelectedIndex = num;
						flag = true;
					}
				}
				else
				{
					int num2 = values.Length;
					int[] selectedIndices = this.SelectedIndices;
					int[] array = new int[num2];
					for (int i = 0; i < num2; i++)
					{
						array[i] = this.Items.FindByValueInternal(values[i], false);
					}
					if (selectedIndices.Length == num2)
					{
						for (int j = 0; j < num2; j++)
						{
							if (array[j] != selectedIndices[j])
							{
								flag = true;
								break;
							}
						}
					}
					else
					{
						flag = true;
					}
					if (flag)
					{
						this.Select(array);
					}
				}
			}
			else if (this.SelectedIndex != -1)
			{
				this.SelectedIndex = -1;
				flag = true;
			}
			if (flag)
			{
				base.ValidateEvent(postDataKey);
			}
			return flag;
		}

		// Token: 0x0600279F RID: 10143 RVA: 0x00080ED9 File Offset: 0x0007F0D9
		void IPostBackDataHandler.RaisePostDataChangedEvent()
		{
			this.RaisePostDataChangedEvent();
		}

		// Token: 0x060027A0 RID: 10144 RVA: 0x00080EE1 File Offset: 0x0007F0E1
		protected virtual void RaisePostDataChangedEvent()
		{
			this.OnServerChange(EventArgs.Empty);
		}

		// Token: 0x04001DD1 RID: 7633
		private static readonly object EventServerChange = new object();

		// Token: 0x04001DD2 RID: 7634
		internal const string DataBoundViewStateKey = "_!DataBound";

		// Token: 0x04001DD3 RID: 7635
		private object dataSource;

		// Token: 0x04001DD4 RID: 7636
		private ListItemCollection items;

		// Token: 0x04001DD5 RID: 7637
		private int cachedSelectedIndex;

		// Token: 0x04001DD6 RID: 7638
		private bool _requiresDataBinding;

		// Token: 0x04001DD7 RID: 7639
		private bool _inited;

		// Token: 0x04001DD8 RID: 7640
		private bool _throwOnDataPropertyChange;

		// Token: 0x04001DD9 RID: 7641
		private DataSourceView _currentView;

		// Token: 0x04001DDA RID: 7642
		private bool _currentViewIsFromDataSourceID;

		// Token: 0x04001DDB RID: 7643
		private bool _currentViewValid;

		// Token: 0x04001DDC RID: 7644
		private bool _pagePreLoadFired;
	}
}
