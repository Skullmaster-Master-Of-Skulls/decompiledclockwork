using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing.Design;
using System.Globalization;
using System.Web.Util;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000454 RID: 1108
	[ControlValueProperty("SelectedValue")]
	[DataBindingHandler("System.Web.UI.Design.WebControls.ListControlDataBindingHandler, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[DefaultEvent("SelectedIndexChanged")]
	[ParseChildren(true, "Items")]
	[Designer("System.Web.UI.Design.WebControls.ListControlDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public abstract class ListControl : DataBoundControl, IEditableTextControl, ITextControl
	{
		// Token: 0x06003575 RID: 13685 RVA: 0x000AD292 File Offset: 0x000AB492
		public ListControl()
		{
			this.cachedSelectedIndex = -1;
		}

		// Token: 0x17000F8A RID: 3978
		// (get) Token: 0x06003576 RID: 13686 RVA: 0x000AD2A4 File Offset: 0x000AB4A4
		// (set) Token: 0x06003577 RID: 13687 RVA: 0x000AD2CD File Offset: 0x000AB4CD
		[DefaultValue(false)]
		[Themeable(false)]
		[WebCategory("Behavior")]
		[WebSysDescription("ListControl_AppendDataBoundItems")]
		public virtual bool AppendDataBoundItems
		{
			get
			{
				object obj = this.ViewState["AppendDataBoundItems"];
				return obj != null && (bool)obj;
			}
			set
			{
				this.ViewState["AppendDataBoundItems"] = value;
				if (base.Initialized)
				{
					base.RequiresDataBinding = true;
				}
			}
		}

		// Token: 0x17000F8B RID: 3979
		// (get) Token: 0x06003578 RID: 13688 RVA: 0x000AD2F4 File Offset: 0x000AB4F4
		// (set) Token: 0x06003579 RID: 13689 RVA: 0x0008D869 File Offset: 0x0008BA69
		[DefaultValue(false)]
		[WebCategory("Behavior")]
		[WebSysDescription("ListControl_AutoPostBack")]
		[Themeable(false)]
		public virtual bool AutoPostBack
		{
			get
			{
				object obj = this.ViewState["AutoPostBack"];
				return obj != null && (bool)obj;
			}
			set
			{
				this.ViewState["AutoPostBack"] = value;
			}
		}

		// Token: 0x17000F8C RID: 3980
		// (get) Token: 0x0600357A RID: 13690 RVA: 0x000AD320 File Offset: 0x000AB520
		// (set) Token: 0x0600357B RID: 13691 RVA: 0x0007E239 File Offset: 0x0007C439
		[DefaultValue(false)]
		[Themeable(false)]
		[WebCategory("Behavior")]
		[WebSysDescription("AutoPostBackControl_CausesValidation")]
		public virtual bool CausesValidation
		{
			get
			{
				object obj = this.ViewState["CausesValidation"];
				return obj != null && (bool)obj;
			}
			set
			{
				this.ViewState["CausesValidation"] = value;
			}
		}

		// Token: 0x17000F8D RID: 3981
		// (get) Token: 0x0600357C RID: 13692 RVA: 0x000AD34C File Offset: 0x000AB54C
		// (set) Token: 0x0600357D RID: 13693 RVA: 0x000AD379 File Offset: 0x000AB579
		[DefaultValue("")]
		[Themeable(false)]
		[WebCategory("Data")]
		[WebSysDescription("ListControl_DataTextField")]
		public virtual string DataTextField
		{
			get
			{
				object obj = this.ViewState["DataTextField"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["DataTextField"] = value;
				if (base.Initialized)
				{
					base.RequiresDataBinding = true;
				}
			}
		}

		// Token: 0x17000F8E RID: 3982
		// (get) Token: 0x0600357E RID: 13694 RVA: 0x000AD39C File Offset: 0x000AB59C
		// (set) Token: 0x0600357F RID: 13695 RVA: 0x000AD3C9 File Offset: 0x000AB5C9
		[DefaultValue("")]
		[Themeable(false)]
		[WebCategory("Data")]
		[WebSysDescription("ListControl_DataTextFormatString")]
		public virtual string DataTextFormatString
		{
			get
			{
				object obj = this.ViewState["DataTextFormatString"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["DataTextFormatString"] = value;
				if (base.Initialized)
				{
					base.RequiresDataBinding = true;
				}
			}
		}

		// Token: 0x17000F8F RID: 3983
		// (get) Token: 0x06003580 RID: 13696 RVA: 0x000AD3EC File Offset: 0x000AB5EC
		// (set) Token: 0x06003581 RID: 13697 RVA: 0x000AD419 File Offset: 0x000AB619
		[DefaultValue("")]
		[Themeable(false)]
		[WebCategory("Data")]
		[WebSysDescription("ListControl_DataValueField")]
		public virtual string DataValueField
		{
			get
			{
				object obj = this.ViewState["DataValueField"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["DataValueField"] = value;
				if (base.Initialized)
				{
					base.RequiresDataBinding = true;
				}
			}
		}

		// Token: 0x17000F90 RID: 3984
		// (get) Token: 0x06003582 RID: 13698 RVA: 0x00007722 File Offset: 0x00005922
		internal virtual bool IsMultiSelectInternal
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000F91 RID: 3985
		// (get) Token: 0x06003583 RID: 13699 RVA: 0x000AD43B File Offset: 0x000AB63B
		[WebCategory("Default")]
		[DefaultValue(null)]
		[Editor("System.Web.UI.Design.WebControls.ListItemsCollectionEditor,System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[MergableProperty(false)]
		[WebSysDescription("ListControl_Items")]
		[PersistenceMode(PersistenceMode.InnerDefaultProperty)]
		public virtual ListItemCollection Items
		{
			get
			{
				if (this.items == null)
				{
					this.items = new ListItemCollection();
					if (base.IsTrackingViewState)
					{
						this.items.TrackViewState();
					}
				}
				return this.items;
			}
		}

		// Token: 0x17000F92 RID: 3986
		// (get) Token: 0x06003584 RID: 13700 RVA: 0x000AD46C File Offset: 0x000AB66C
		internal bool SaveSelectedIndicesViewState
		{
			get
			{
				if (base.Events[ListControl.EventSelectedIndexChanged] != null || base.Events[ListControl.EventTextChanged] != null || !base.IsEnabled || !this.Visible || (this.AutoPostBack && this.Page != null && !this.Page.ClientSupportsJavaScript))
				{
					return true;
				}
				foreach (object obj in this.Items)
				{
					ListItem listItem = (ListItem)obj;
					if (!listItem.Enabled)
					{
						return true;
					}
				}
				Type type = base.GetType();
				return !(type == typeof(DropDownList)) && !(type == typeof(ListBox)) && !(type == typeof(CheckBoxList)) && !(type == typeof(RadioButtonList));
			}
		}

		// Token: 0x17000F93 RID: 3987
		// (get) Token: 0x06003585 RID: 13701 RVA: 0x000AD574 File Offset: 0x000AB774
		// (set) Token: 0x06003586 RID: 13702 RVA: 0x000AD5B0 File Offset: 0x000AB7B0
		[Bindable(true)]
		[Browsable(false)]
		[DefaultValue(0)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Themeable(false)]
		[WebCategory("Behavior")]
		[WebSysDescription("WebControl_SelectedIndex")]
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
				return -1;
			}
			set
			{
				if (value < -1)
				{
					if (this.Items.Count != 0)
					{
						throw new ArgumentOutOfRangeException("value", SR.GetString("ListControl_SelectionOutOfRange", new object[]
						{
							this.ID,
							"SelectedIndex"
						}));
					}
					value = -1;
				}
				if ((this.Items.Count != 0 && value < this.Items.Count) || value == -1)
				{
					this.ClearSelection();
					if (value >= 0)
					{
						this.Items[value].Selected = true;
					}
				}
				else if (this._stateLoaded)
				{
					throw new ArgumentOutOfRangeException("value", SR.GetString("ListControl_SelectionOutOfRange", new object[]
					{
						this.ID,
						"SelectedIndex"
					}));
				}
				this.cachedSelectedIndex = value;
			}
		}

		// Token: 0x17000F94 RID: 3988
		// (get) Token: 0x06003587 RID: 13703 RVA: 0x000AD678 File Offset: 0x000AB878
		internal virtual ArrayList SelectedIndicesInternal
		{
			get
			{
				this.cachedSelectedIndices = new ArrayList(3);
				for (int i = 0; i < this.Items.Count; i++)
				{
					if (this.Items[i].Selected)
					{
						this.cachedSelectedIndices.Add(i);
					}
				}
				return this.cachedSelectedIndices;
			}
		}

		// Token: 0x17000F95 RID: 3989
		// (get) Token: 0x06003588 RID: 13704 RVA: 0x000AD6D4 File Offset: 0x000AB8D4
		[WebCategory("Behavior")]
		[Browsable(false)]
		[DefaultValue(null)]
		[WebSysDescription("ListControl_SelectedItem")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual ListItem SelectedItem
		{
			get
			{
				int selectedIndex = this.SelectedIndex;
				if (selectedIndex >= 0)
				{
					return this.Items[selectedIndex];
				}
				return null;
			}
		}

		// Token: 0x17000F96 RID: 3990
		// (get) Token: 0x06003589 RID: 13705 RVA: 0x000AD6FC File Offset: 0x000AB8FC
		// (set) Token: 0x0600358A RID: 13706 RVA: 0x000AD72C File Offset: 0x000AB92C
		[Bindable(true, BindingDirection.TwoWay)]
		[Browsable(false)]
		[DefaultValue("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Themeable(false)]
		[WebSysDescription("ListControl_SelectedValue")]
		[WebCategory("Behavior")]
		public virtual string SelectedValue
		{
			get
			{
				int selectedIndex = this.SelectedIndex;
				if (selectedIndex >= 0)
				{
					return this.Items[selectedIndex].Value;
				}
				return string.Empty;
			}
			set
			{
				if (this.Items.Count != 0)
				{
					if (value == null || (base.DesignMode && value.Length == 0))
					{
						this.ClearSelection();
						return;
					}
					ListItem listItem = this.Items.FindByValue(value);
					bool flag = this.Page != null && this.Page.IsPostBack && this._stateLoaded;
					if (flag && listItem == null)
					{
						throw new ArgumentOutOfRangeException("value", SR.GetString("ListControl_SelectionOutOfRange", new object[]
						{
							this.ID,
							"SelectedValue"
						}));
					}
					if (listItem != null)
					{
						this.ClearSelection();
						listItem.Selected = true;
					}
				}
				this.cachedSelectedValue = value;
			}
		}

		// Token: 0x17000F97 RID: 3991
		// (get) Token: 0x0600358B RID: 13707 RVA: 0x000AD7D8 File Offset: 0x000AB9D8
		// (set) Token: 0x0600358C RID: 13708 RVA: 0x000AD7E0 File Offset: 0x000AB9E0
		[Browsable(false)]
		[Themeable(false)]
		[DefaultValue("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[WebSysDescription("ListControl_Text")]
		[WebCategory("Behavior")]
		public virtual string Text
		{
			get
			{
				return this.SelectedValue;
			}
			set
			{
				this.SelectedValue = value;
			}
		}

		// Token: 0x17000F98 RID: 3992
		// (get) Token: 0x0600358D RID: 13709 RVA: 0x000AD7E9 File Offset: 0x000AB9E9
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Select;
			}
		}

		// Token: 0x17000F99 RID: 3993
		// (get) Token: 0x0600358E RID: 13710 RVA: 0x000AD7F0 File Offset: 0x000AB9F0
		// (set) Token: 0x0600358F RID: 13711 RVA: 0x0007E369 File Offset: 0x0007C569
		[WebCategory("Behavior")]
		[Themeable(false)]
		[DefaultValue("")]
		[WebSysDescription("PostBackControl_ValidationGroup")]
		public virtual string ValidationGroup
		{
			get
			{
				string text = (string)this.ViewState["ValidationGroup"];
				if (text != null)
				{
					return text;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["ValidationGroup"] = value;
			}
		}

		// Token: 0x140000B0 RID: 176
		// (add) Token: 0x06003590 RID: 13712 RVA: 0x000AD81D File Offset: 0x000ABA1D
		// (remove) Token: 0x06003591 RID: 13713 RVA: 0x000AD830 File Offset: 0x000ABA30
		[WebCategory("Action")]
		[WebSysDescription("ListControl_OnSelectedIndexChanged")]
		public event EventHandler SelectedIndexChanged
		{
			add
			{
				base.Events.AddHandler(ListControl.EventSelectedIndexChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(ListControl.EventSelectedIndexChanged, value);
			}
		}

		// Token: 0x140000B1 RID: 177
		// (add) Token: 0x06003592 RID: 13714 RVA: 0x000AD843 File Offset: 0x000ABA43
		// (remove) Token: 0x06003593 RID: 13715 RVA: 0x000AD856 File Offset: 0x000ABA56
		[WebCategory("Action")]
		[WebSysDescription("ListControl_TextChanged")]
		public event EventHandler TextChanged
		{
			add
			{
				base.Events.AddHandler(ListControl.EventTextChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(ListControl.EventTextChanged, value);
			}
		}

		// Token: 0x06003594 RID: 13716 RVA: 0x000AD86C File Offset: 0x000ABA6C
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			if (this.Page != null)
			{
				this.Page.VerifyRenderingInServerForm(this);
			}
			if (this.IsMultiSelectInternal)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Multiple, "multiple");
			}
			if (this.AutoPostBack && this.Page != null && this.Page.ClientSupportsJavaScript)
			{
				string text = null;
				if (base.HasAttributes)
				{
					text = base.Attributes["onchange"];
					if (text != null)
					{
						text = Util.EnsureEndWithSemiColon(text);
						base.Attributes.Remove("onchange");
					}
				}
				PostBackOptions postBackOptions = new PostBackOptions(this, string.Empty);
				if (this.CausesValidation)
				{
					postBackOptions.PerformValidation = true;
					postBackOptions.ValidationGroup = this.ValidationGroup;
				}
				if (this.Page.Form != null)
				{
					postBackOptions.AutoPostBack = true;
				}
				text = Util.MergeScript(text, this.Page.ClientScript.GetPostBackEventReference(postBackOptions, true));
				writer.AddAttribute(HtmlTextWriterAttribute.Onchange, text);
				if (base.EnableLegacyRendering)
				{
					writer.AddAttribute("language", "javascript", false);
				}
			}
			if (this.Enabled && (!base.IsEnabled & this.SupportsDisabledAttribute))
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Disabled, "disabled");
			}
			base.AddAttributesToRender(writer);
		}

		// Token: 0x06003595 RID: 13717 RVA: 0x000AD9A4 File Offset: 0x000ABBA4
		public virtual void ClearSelection()
		{
			for (int i = 0; i < this.Items.Count; i++)
			{
				this.Items[i].Selected = false;
			}
		}

		// Token: 0x06003596 RID: 13718 RVA: 0x000AD9DC File Offset: 0x000ABBDC
		protected override void LoadViewState(object savedState)
		{
			if (savedState != null)
			{
				Triplet triplet = (Triplet)savedState;
				base.LoadViewState(triplet.First);
				this.Items.LoadViewState(triplet.Second);
				ArrayList arrayList = triplet.Third as ArrayList;
				if (arrayList != null)
				{
					this.SelectInternal(arrayList);
				}
			}
			else
			{
				base.LoadViewState(null);
			}
			this._stateLoaded = true;
		}

		// Token: 0x06003597 RID: 13719 RVA: 0x000ADA36 File Offset: 0x000ABC36
		private void OnDataSourceViewSelectCallback(IEnumerable data)
		{
			this._asyncSelectPending = false;
			this.PerformDataBinding(data);
			this.PostPerformDataBindingAction();
		}

		// Token: 0x06003598 RID: 13720 RVA: 0x000ADA4C File Offset: 0x000ABC4C
		protected override void OnDataBinding(EventArgs e)
		{
			base.OnDataBinding(e);
			DataSourceView data = this.GetData();
			if (data == null)
			{
				throw new InvalidOperationException(SR.GetString("DataControl_ViewNotFound", new object[]
				{
					this.ID
				}));
			}
			bool flag = false;
			if (AppSettings.EnableAsyncModelBinding)
			{
				ModelDataSourceView modelDataSourceView = data as ModelDataSourceView;
				flag = (modelDataSourceView != null && modelDataSourceView.IsSelectMethodAsync);
			}
			if (flag)
			{
				this._asyncSelectPending = true;
				data.Select(base.SelectArguments, new DataSourceViewSelectCallback(this.OnDataSourceViewSelectCallback));
				return;
			}
			IEnumerable data2 = data.ExecuteSelect(DataSourceSelectArguments.Empty);
			this.PerformDataBinding(data2);
		}

		// Token: 0x06003599 RID: 13721 RVA: 0x000ADADC File Offset: 0x000ABCDC
		internal void EnsureDataBoundInLoadPostData()
		{
			if (!this.SkipEnsureDataBoundInLoadPostData)
			{
				this.EnsureDataBound();
			}
		}

		// Token: 0x17000F9A RID: 3994
		// (get) Token: 0x0600359A RID: 13722 RVA: 0x000ADAEC File Offset: 0x000ABCEC
		// (set) Token: 0x0600359B RID: 13723 RVA: 0x000ADAF4 File Offset: 0x000ABCF4
		internal bool SkipEnsureDataBoundInLoadPostData { get; set; }

		// Token: 0x0600359C RID: 13724 RVA: 0x000ADB00 File Offset: 0x000ABD00
		protected internal override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			if (this.Page != null && base.IsEnabled)
			{
				if (this.AutoPostBack)
				{
					this.Page.RegisterPostBackScript();
					this.Page.RegisterFocusScript();
					if (this.CausesValidation && this.Page.GetValidators(this.ValidationGroup).Count > 0)
					{
						this.Page.RegisterWebFormsScript();
					}
				}
				if (!this.SaveSelectedIndicesViewState)
				{
					this.Page.RegisterEnabledControl(this);
				}
			}
		}

		// Token: 0x0600359D RID: 13725 RVA: 0x000ADB84 File Offset: 0x000ABD84
		protected virtual void OnSelectedIndexChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[ListControl.EventSelectedIndexChanged];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
			this.OnTextChanged(e);
		}

		// Token: 0x0600359E RID: 13726 RVA: 0x000ADBBC File Offset: 0x000ABDBC
		protected virtual void OnTextChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[ListControl.EventTextChanged];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x0600359F RID: 13727 RVA: 0x000ADBEC File Offset: 0x000ABDEC
		protected internal override void PerformDataBinding(IEnumerable dataSource)
		{
			base.PerformDataBinding(dataSource);
			if (dataSource != null)
			{
				bool flag = false;
				bool flag2 = false;
				string dataTextField = this.DataTextField;
				string dataValueField = this.DataValueField;
				string dataTextFormatString = this.DataTextFormatString;
				if (!this.AppendDataBoundItems)
				{
					this.Items.Clear();
				}
				ICollection collection = dataSource as ICollection;
				if (collection != null)
				{
					this.Items.Capacity = collection.Count + this.Items.Count;
				}
				if (dataTextField.Length != 0 || dataValueField.Length != 0)
				{
					flag = true;
				}
				if (dataTextFormatString.Length != 0)
				{
					flag2 = true;
				}
				foreach (object obj in dataSource)
				{
					ListItem listItem = new ListItem();
					if (flag)
					{
						if (dataTextField.Length > 0)
						{
							listItem.Text = DataBinder.GetPropertyValue(obj, dataTextField, dataTextFormatString);
						}
						if (dataValueField.Length > 0)
						{
							listItem.Value = DataBinder.GetPropertyValue(obj, dataValueField, null);
						}
					}
					else
					{
						if (flag2)
						{
							listItem.Text = string.Format(CultureInfo.CurrentCulture, dataTextFormatString, new object[]
							{
								obj
							});
						}
						else
						{
							listItem.Text = obj.ToString();
						}
						listItem.Value = obj.ToString();
					}
					this.Items.Add(listItem);
				}
			}
			if (this.cachedSelectedValue == null)
			{
				if (this.cachedSelectedIndex != -1)
				{
					this.SelectedIndex = this.cachedSelectedIndex;
					this.cachedSelectedIndex = -1;
				}
				return;
			}
			int num = this.Items.FindByValueInternal(this.cachedSelectedValue, true);
			if (-1 == num)
			{
				throw new ArgumentOutOfRangeException("value", SR.GetString("ListControl_SelectionOutOfRange", new object[]
				{
					this.ID,
					"SelectedValue"
				}));
			}
			if (this.cachedSelectedIndex != -1 && this.cachedSelectedIndex != num)
			{
				throw new ArgumentException(SR.GetString("Attributes_mutually_exclusive", new object[]
				{
					"SelectedIndex",
					"SelectedValue"
				}));
			}
			this.SelectedIndex = num;
			this.cachedSelectedValue = null;
			this.cachedSelectedIndex = -1;
		}

		// Token: 0x060035A0 RID: 13728 RVA: 0x000ADE10 File Offset: 0x000AC010
		protected override void PerformSelect()
		{
			this.OnDataBinding(EventArgs.Empty);
			this.PostPerformDataBindingAction();
		}

		// Token: 0x060035A1 RID: 13729 RVA: 0x000ADE23 File Offset: 0x000AC023
		private void PostPerformDataBindingAction()
		{
			if (this._asyncSelectPending)
			{
				return;
			}
			base.RequiresDataBinding = false;
			base.MarkAsDataBound();
			this.OnDataBound(EventArgs.Empty);
		}

		// Token: 0x060035A2 RID: 13730 RVA: 0x000ADE48 File Offset: 0x000AC048
		protected internal override void RenderContents(HtmlTextWriter writer)
		{
			ListItemCollection listItemCollection = this.Items;
			int count = listItemCollection.Count;
			if (count > 0)
			{
				bool flag = false;
				for (int i = 0; i < count; i++)
				{
					ListItem listItem = listItemCollection[i];
					if (listItem.Enabled)
					{
						writer.WriteBeginTag("option");
						if (listItem.Selected)
						{
							if (flag)
							{
								this.VerifyMultiSelect();
							}
							flag = true;
							writer.WriteAttribute("selected", "selected");
						}
						writer.WriteAttribute("value", listItem.Value, true);
						if (listItem.HasAttributes)
						{
							listItem.Attributes.Render(writer);
						}
						if (this.Page != null)
						{
							this.Page.ClientScript.RegisterForEventValidation(this.UniqueID, listItem.Value);
						}
						writer.Write('>');
						HttpUtility.HtmlEncode(listItem.Text, writer);
						writer.WriteEndTag("option");
						writer.WriteLine();
					}
				}
			}
		}

		// Token: 0x060035A3 RID: 13731 RVA: 0x000ADF38 File Offset: 0x000AC138
		protected override object SaveViewState()
		{
			object obj = base.SaveViewState();
			object obj2 = this.Items.SaveViewState();
			object obj3 = null;
			if (this.SaveSelectedIndicesViewState)
			{
				obj3 = this.SelectedIndicesInternal;
			}
			if (obj3 != null || obj2 != null || obj != null)
			{
				return new Triplet(obj, obj2, obj3);
			}
			return null;
		}

		// Token: 0x060035A4 RID: 13732 RVA: 0x000ADF7C File Offset: 0x000AC17C
		internal void SelectInternal(ArrayList selectedIndices)
		{
			this.ClearSelection();
			for (int i = 0; i < selectedIndices.Count; i++)
			{
				int num = (int)selectedIndices[i];
				if (num >= 0 && num < this.Items.Count)
				{
					this.Items[num].Selected = true;
				}
			}
			this.cachedSelectedIndices = selectedIndices;
		}

		// Token: 0x060035A5 RID: 13733 RVA: 0x000ADFD8 File Offset: 0x000AC1D8
		internal static void SetControlToRepeatID(Control owner, Control controlToRepeat, int index)
		{
			string text = index.ToString(NumberFormatInfo.InvariantInfo);
			if (owner.EffectiveClientIDMode != ClientIDMode.Static)
			{
				controlToRepeat.ID = text;
				controlToRepeat.ClientIDMode = ClientIDMode.Inherit;
				return;
			}
			if (string.IsNullOrEmpty(owner.ID))
			{
				controlToRepeat.ID = text;
				controlToRepeat.ClientIDMode = ClientIDMode.AutoID;
				return;
			}
			controlToRepeat.ID = owner.ID + "_" + text;
			controlToRepeat.ClientIDMode = ClientIDMode.Inherit;
		}

		// Token: 0x060035A6 RID: 13734 RVA: 0x000AE044 File Offset: 0x000AC244
		protected void SetPostDataSelection(int selectedIndex)
		{
			if (this.Items.Count != 0 && selectedIndex < this.Items.Count)
			{
				this.ClearSelection();
				if (selectedIndex >= 0)
				{
					this.Items[selectedIndex].Selected = true;
				}
			}
		}

		// Token: 0x060035A7 RID: 13735 RVA: 0x000AE07D File Offset: 0x000AC27D
		protected override void TrackViewState()
		{
			base.TrackViewState();
			this.Items.TrackViewState();
		}

		// Token: 0x060035A8 RID: 13736 RVA: 0x000AE090 File Offset: 0x000AC290
		protected internal virtual void VerifyMultiSelect()
		{
			if (!this.IsMultiSelectInternal)
			{
				throw new HttpException(SR.GetString("Cant_Multiselect_In_Single_Mode"));
			}
		}

		// Token: 0x040021C0 RID: 8640
		private static readonly object EventSelectedIndexChanged = new object();

		// Token: 0x040021C1 RID: 8641
		private static readonly object EventTextChanged = new object();

		// Token: 0x040021C2 RID: 8642
		private ListItemCollection items;

		// Token: 0x040021C3 RID: 8643
		private int cachedSelectedIndex;

		// Token: 0x040021C4 RID: 8644
		private string cachedSelectedValue;

		// Token: 0x040021C5 RID: 8645
		private ArrayList cachedSelectedIndices;

		// Token: 0x040021C6 RID: 8646
		private bool _stateLoaded;

		// Token: 0x040021C7 RID: 8647
		private bool _asyncSelectPending;
	}
}
