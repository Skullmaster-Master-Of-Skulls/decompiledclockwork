using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing.Design;

namespace System.Web.UI.WebControls
{
	// Token: 0x020003B3 RID: 947
	[TypeConverter(typeof(ExpandableObjectConverter))]
	[DefaultProperty("HeaderText")]
	public abstract class DataControlField : IStateManager, IDataSourceViewSchemaAccessor
	{
		// Token: 0x1400006A RID: 106
		// (add) Token: 0x06002D9C RID: 11676 RVA: 0x0009527C File Offset: 0x0009347C
		// (remove) Token: 0x06002D9D RID: 11677 RVA: 0x000952B4 File Offset: 0x000934B4
		internal event EventHandler FieldChanged;

		// Token: 0x06002D9E RID: 11678 RVA: 0x000952E9 File Offset: 0x000934E9
		protected DataControlField()
		{
			this._statebag = new StateBag();
			this._dataSourceViewSchema = null;
		}

		// Token: 0x17000CFF RID: 3327
		// (get) Token: 0x06002D9F RID: 11679 RVA: 0x00095304 File Offset: 0x00093504
		// (set) Token: 0x06002DA0 RID: 11680 RVA: 0x00095331 File Offset: 0x00093531
		[Localizable(true)]
		[WebCategory("Accessibility")]
		[DefaultValue("")]
		[WebSysDescription("DataControlField_AccessibleHeaderText")]
		public virtual string AccessibleHeaderText
		{
			get
			{
				object obj = this.ViewState["AccessibleHeaderText"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				if (!object.Equals(value, this.ViewState["AccessibleHeaderText"]))
				{
					this.ViewState["AccessibleHeaderText"] = value;
					this.OnFieldChanged();
				}
			}
		}

		// Token: 0x17000D00 RID: 3328
		// (get) Token: 0x06002DA1 RID: 11681 RVA: 0x00095362 File Offset: 0x00093562
		[WebCategory("Styles")]
		[DefaultValue(null)]
		[WebSysDescription("DataControlField_ControlStyle")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public Style ControlStyle
		{
			get
			{
				if (this._controlStyle == null)
				{
					this._controlStyle = new Style();
					if (this.IsTrackingViewState)
					{
						((IStateManager)this._controlStyle).TrackViewState();
					}
				}
				return this._controlStyle;
			}
		}

		// Token: 0x17000D01 RID: 3329
		// (get) Token: 0x06002DA2 RID: 11682 RVA: 0x00095390 File Offset: 0x00093590
		// (set) Token: 0x06002DA3 RID: 11683 RVA: 0x000953B9 File Offset: 0x000935B9
		protected internal virtual ValidateRequestMode ValidateRequestMode
		{
			get
			{
				object obj = this.ViewState["ValidateRequestMode"];
				if (obj != null)
				{
					return (ValidateRequestMode)obj;
				}
				return ValidateRequestMode.Inherit;
			}
			set
			{
				if (value < ValidateRequestMode.Inherit || value > ValidateRequestMode.Enabled)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				if (value != this.ValidateRequestMode)
				{
					this.ViewState["ValidateRequestMode"] = value;
					this.OnFieldChanged();
				}
			}
		}

		// Token: 0x17000D02 RID: 3330
		// (get) Token: 0x06002DA4 RID: 11684 RVA: 0x000953F3 File Offset: 0x000935F3
		internal Style ControlStyleInternal
		{
			get
			{
				return this._controlStyle;
			}
		}

		// Token: 0x17000D03 RID: 3331
		// (get) Token: 0x06002DA5 RID: 11685 RVA: 0x000953FB File Offset: 0x000935FB
		protected Control Control
		{
			get
			{
				return this._control;
			}
		}

		// Token: 0x17000D04 RID: 3332
		// (get) Token: 0x06002DA6 RID: 11686 RVA: 0x00095403 File Offset: 0x00093603
		protected bool DesignMode
		{
			get
			{
				return this._control != null && this._control.DesignMode;
			}
		}

		// Token: 0x17000D05 RID: 3333
		// (get) Token: 0x06002DA7 RID: 11687 RVA: 0x0009541A File Offset: 0x0009361A
		[WebCategory("Styles")]
		[DefaultValue(null)]
		[WebSysDescription("DataControlField_FooterStyle")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public TableItemStyle FooterStyle
		{
			get
			{
				if (this._footerStyle == null)
				{
					this._footerStyle = new TableItemStyle();
					if (this.IsTrackingViewState)
					{
						((IStateManager)this._footerStyle).TrackViewState();
					}
				}
				return this._footerStyle;
			}
		}

		// Token: 0x17000D06 RID: 3334
		// (get) Token: 0x06002DA8 RID: 11688 RVA: 0x00095448 File Offset: 0x00093648
		internal TableItemStyle FooterStyleInternal
		{
			get
			{
				return this._footerStyle;
			}
		}

		// Token: 0x17000D07 RID: 3335
		// (get) Token: 0x06002DA9 RID: 11689 RVA: 0x00095450 File Offset: 0x00093650
		// (set) Token: 0x06002DAA RID: 11690 RVA: 0x0009547D File Offset: 0x0009367D
		[Localizable(true)]
		[WebCategory("Appearance")]
		[DefaultValue("")]
		[WebSysDescription("DataControlField_FooterText")]
		public virtual string FooterText
		{
			get
			{
				object obj = this.ViewState["FooterText"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				if (!object.Equals(value, this.ViewState["FooterText"]))
				{
					this.ViewState["FooterText"] = value;
					this.OnFieldChanged();
				}
			}
		}

		// Token: 0x17000D08 RID: 3336
		// (get) Token: 0x06002DAB RID: 11691 RVA: 0x000954B0 File Offset: 0x000936B0
		// (set) Token: 0x06002DAC RID: 11692 RVA: 0x000954DD File Offset: 0x000936DD
		[WebCategory("Appearance")]
		[DefaultValue("")]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[UrlProperty]
		[WebSysDescription("DataControlField_HeaderImageUrl")]
		public virtual string HeaderImageUrl
		{
			get
			{
				object obj = this.ViewState["HeaderImageUrl"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				if (!object.Equals(value, this.ViewState["HeaderImageUrl"]))
				{
					this.ViewState["HeaderImageUrl"] = value;
					this.OnFieldChanged();
				}
			}
		}

		// Token: 0x17000D09 RID: 3337
		// (get) Token: 0x06002DAD RID: 11693 RVA: 0x0009550E File Offset: 0x0009370E
		[WebCategory("Styles")]
		[DefaultValue(null)]
		[WebSysDescription("DataControlField_HeaderStyle")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public TableItemStyle HeaderStyle
		{
			get
			{
				if (this._headerStyle == null)
				{
					this._headerStyle = new TableItemStyle();
					if (this.IsTrackingViewState)
					{
						((IStateManager)this._headerStyle).TrackViewState();
					}
				}
				return this._headerStyle;
			}
		}

		// Token: 0x17000D0A RID: 3338
		// (get) Token: 0x06002DAE RID: 11694 RVA: 0x0009553C File Offset: 0x0009373C
		internal TableItemStyle HeaderStyleInternal
		{
			get
			{
				return this._headerStyle;
			}
		}

		// Token: 0x17000D0B RID: 3339
		// (get) Token: 0x06002DAF RID: 11695 RVA: 0x00095544 File Offset: 0x00093744
		// (set) Token: 0x06002DB0 RID: 11696 RVA: 0x00095571 File Offset: 0x00093771
		[Localizable(true)]
		[WebCategory("Appearance")]
		[DefaultValue("")]
		[WebSysDescription("DataControlField_HeaderText")]
		public virtual string HeaderText
		{
			get
			{
				object obj = this.ViewState["HeaderText"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				if (!object.Equals(value, this.ViewState["HeaderText"]))
				{
					this.ViewState["HeaderText"] = value;
					this.OnFieldChanged();
				}
			}
		}

		// Token: 0x17000D0C RID: 3340
		// (get) Token: 0x06002DB1 RID: 11697 RVA: 0x000955A4 File Offset: 0x000937A4
		// (set) Token: 0x06002DB2 RID: 11698 RVA: 0x000955D0 File Offset: 0x000937D0
		[WebCategory("Behavior")]
		[DefaultValue(true)]
		[WebSysDescription("DataControlField_InsertVisible")]
		public virtual bool InsertVisible
		{
			get
			{
				object obj = this.ViewState["InsertVisible"];
				return obj == null || (bool)obj;
			}
			set
			{
				object obj = this.ViewState["InsertVisible"];
				if (obj == null || value != (bool)obj)
				{
					this.ViewState["InsertVisible"] = value;
					this.OnFieldChanged();
				}
			}
		}

		// Token: 0x17000D0D RID: 3341
		// (get) Token: 0x06002DB3 RID: 11699 RVA: 0x00095616 File Offset: 0x00093816
		[WebCategory("Styles")]
		[DefaultValue(null)]
		[WebSysDescription("DataControlField_ItemStyle")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public TableItemStyle ItemStyle
		{
			get
			{
				if (this._itemStyle == null)
				{
					this._itemStyle = new TableItemStyle();
					if (this.IsTrackingViewState)
					{
						((IStateManager)this._itemStyle).TrackViewState();
					}
				}
				return this._itemStyle;
			}
		}

		// Token: 0x17000D0E RID: 3342
		// (get) Token: 0x06002DB4 RID: 11700 RVA: 0x00095644 File Offset: 0x00093844
		internal TableItemStyle ItemStyleInternal
		{
			get
			{
				return this._itemStyle;
			}
		}

		// Token: 0x17000D0F RID: 3343
		// (get) Token: 0x06002DB5 RID: 11701 RVA: 0x0009564C File Offset: 0x0009384C
		// (set) Token: 0x06002DB6 RID: 11702 RVA: 0x00095678 File Offset: 0x00093878
		[WebCategory("Behavior")]
		[DefaultValue(true)]
		[WebSysDescription("DataControlField_ShowHeader")]
		public virtual bool ShowHeader
		{
			get
			{
				object obj = this.ViewState["ShowHeader"];
				return obj == null || (bool)obj;
			}
			set
			{
				object obj = this.ViewState["ShowHeader"];
				if (obj == null || (bool)obj != value)
				{
					this.ViewState["ShowHeader"] = value;
					this.OnFieldChanged();
				}
			}
		}

		// Token: 0x17000D10 RID: 3344
		// (get) Token: 0x06002DB7 RID: 11703 RVA: 0x000956C0 File Offset: 0x000938C0
		// (set) Token: 0x06002DB8 RID: 11704 RVA: 0x000956ED File Offset: 0x000938ED
		[WebCategory("Behavior")]
		[DefaultValue("")]
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[WebSysDescription("DataControlField_SortExpression")]
		public virtual string SortExpression
		{
			get
			{
				object obj = this.ViewState["SortExpression"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				if (!object.Equals(value, this.ViewState["SortExpression"]))
				{
					this.ViewState["SortExpression"] = value;
					this.OnFieldChanged();
				}
			}
		}

		// Token: 0x17000D11 RID: 3345
		// (get) Token: 0x06002DB9 RID: 11705 RVA: 0x0009571E File Offset: 0x0009391E
		protected StateBag ViewState
		{
			get
			{
				return this._statebag;
			}
		}

		// Token: 0x17000D12 RID: 3346
		// (get) Token: 0x06002DBA RID: 11706 RVA: 0x00095728 File Offset: 0x00093928
		// (set) Token: 0x06002DBB RID: 11707 RVA: 0x00095754 File Offset: 0x00093954
		[WebCategory("Behavior")]
		[DefaultValue(true)]
		[WebSysDescription("DataControlField_Visible")]
		public bool Visible
		{
			get
			{
				object obj = this.ViewState["Visible"];
				return obj == null || (bool)obj;
			}
			set
			{
				object obj = this.ViewState["Visible"];
				if (obj == null || value != (bool)obj)
				{
					this.ViewState["Visible"] = value;
					this.OnFieldChanged();
				}
			}
		}

		// Token: 0x06002DBC RID: 11708 RVA: 0x0009579C File Offset: 0x0009399C
		protected internal DataControlField CloneField()
		{
			DataControlField dataControlField = this.CreateField();
			this.CopyProperties(dataControlField);
			return dataControlField;
		}

		// Token: 0x06002DBD RID: 11709 RVA: 0x000957B8 File Offset: 0x000939B8
		protected virtual void CopyProperties(DataControlField newField)
		{
			newField.AccessibleHeaderText = this.AccessibleHeaderText;
			newField.ControlStyle.CopyFrom(this.ControlStyle);
			newField.FooterStyle.CopyFrom(this.FooterStyle);
			newField.HeaderStyle.CopyFrom(this.HeaderStyle);
			newField.ItemStyle.CopyFrom(this.ItemStyle);
			newField.FooterText = this.FooterText;
			newField.HeaderImageUrl = this.HeaderImageUrl;
			newField.HeaderText = this.HeaderText;
			newField.InsertVisible = this.InsertVisible;
			newField.ShowHeader = this.ShowHeader;
			newField.SortExpression = this.SortExpression;
			newField.Visible = this.Visible;
			newField.ValidateRequestMode = this.ValidateRequestMode;
		}

		// Token: 0x06002DBE RID: 11710
		protected abstract DataControlField CreateField();

		// Token: 0x06002DBF RID: 11711 RVA: 0x00006164 File Offset: 0x00004364
		public virtual void ExtractValuesFromCell(IOrderedDictionary dictionary, DataControlFieldCell cell, DataControlRowState rowState, bool includeReadOnly)
		{
		}

		// Token: 0x06002DC0 RID: 11712 RVA: 0x00095875 File Offset: 0x00093A75
		public virtual bool Initialize(bool sortingEnabled, Control control)
		{
			this._sortingEnabled = sortingEnabled;
			this._control = control;
			return false;
		}

		// Token: 0x06002DC1 RID: 11713 RVA: 0x00095888 File Offset: 0x00093A88
		public virtual void InitializeCell(DataControlFieldCell cell, DataControlCellType cellType, DataControlRowState rowState, int rowIndex)
		{
			if (cellType != DataControlCellType.Header)
			{
				if (cellType != DataControlCellType.Footer)
				{
					return;
				}
				string text = this.FooterText;
				if (text.Length == 0)
				{
					text = "&nbsp;";
				}
				cell.Text = text;
			}
			else
			{
				WebControl webControl = null;
				string sortExpression = this.SortExpression;
				bool flag = this._sortingEnabled && sortExpression.Length > 0;
				string headerImageUrl = this.HeaderImageUrl;
				string text2 = this.HeaderText;
				if (headerImageUrl.Length != 0)
				{
					if (flag)
					{
						IPostBackContainer postBackContainer = this._control as IPostBackContainer;
						ImageButton imageButton;
						if (postBackContainer != null)
						{
							imageButton = new DataControlImageButton(postBackContainer);
							((DataControlImageButton)imageButton).EnableCallback(null);
						}
						else
						{
							imageButton = new ImageButton();
						}
						imageButton.ImageUrl = this.HeaderImageUrl;
						imageButton.CommandName = "Sort";
						imageButton.CommandArgument = sortExpression;
						if (!(imageButton is DataControlImageButton))
						{
							imageButton.CausesValidation = false;
						}
						imageButton.AlternateText = text2;
						webControl = imageButton;
					}
					else
					{
						Image image = new Image();
						image.ImageUrl = headerImageUrl;
						webControl = image;
						image.AlternateText = text2;
					}
				}
				else if (flag)
				{
					IPostBackContainer postBackContainer2 = this._control as IPostBackContainer;
					LinkButton linkButton;
					if (postBackContainer2 != null)
					{
						linkButton = new DataControlLinkButton(postBackContainer2);
						((DataControlLinkButton)linkButton).EnableCallback(null);
					}
					else
					{
						linkButton = new LinkButton();
					}
					linkButton.Text = text2;
					linkButton.CommandName = "Sort";
					linkButton.CommandArgument = sortExpression;
					if (!(linkButton is DataControlLinkButton))
					{
						linkButton.CausesValidation = false;
					}
					webControl = linkButton;
				}
				else
				{
					if (text2.Length == 0)
					{
						text2 = "&nbsp;";
					}
					cell.Text = text2;
				}
				if (webControl != null)
				{
					cell.Controls.Add(webControl);
					return;
				}
			}
		}

		// Token: 0x17000D13 RID: 3347
		// (get) Token: 0x06002DC2 RID: 11714 RVA: 0x00095A19 File Offset: 0x00093C19
		protected bool IsTrackingViewState
		{
			get
			{
				return this._trackViewState;
			}
		}

		// Token: 0x06002DC3 RID: 11715 RVA: 0x00095A24 File Offset: 0x00093C24
		protected virtual void LoadViewState(object savedState)
		{
			if (savedState != null)
			{
				object[] array = (object[])savedState;
				if (array[0] != null)
				{
					((IStateManager)this.ViewState).LoadViewState(array[0]);
				}
				if (array[1] != null)
				{
					((IStateManager)this.ItemStyle).LoadViewState(array[1]);
				}
				if (array[2] != null)
				{
					((IStateManager)this.HeaderStyle).LoadViewState(array[2]);
				}
				if (array[3] != null)
				{
					((IStateManager)this.FooterStyle).LoadViewState(array[3]);
				}
			}
		}

		// Token: 0x06002DC4 RID: 11716 RVA: 0x00095A87 File Offset: 0x00093C87
		protected virtual void OnFieldChanged()
		{
			if (this.FieldChanged != null)
			{
				this.FieldChanged(this, EventArgs.Empty);
			}
		}

		// Token: 0x06002DC5 RID: 11717 RVA: 0x00095AA4 File Offset: 0x00093CA4
		protected virtual object SaveViewState()
		{
			object obj = ((IStateManager)this.ViewState).SaveViewState();
			object obj2 = (this._itemStyle != null) ? ((IStateManager)this._itemStyle).SaveViewState() : null;
			object obj3 = (this._headerStyle != null) ? ((IStateManager)this._headerStyle).SaveViewState() : null;
			object obj4 = (this._footerStyle != null) ? ((IStateManager)this._footerStyle).SaveViewState() : null;
			object obj5 = (this._controlStyle != null) ? ((IStateManager)this._controlStyle).SaveViewState() : null;
			if (obj != null || obj2 != null || obj3 != null || obj4 != null || obj5 != null)
			{
				return new object[]
				{
					obj,
					obj2,
					obj3,
					obj4,
					obj5
				};
			}
			return null;
		}

		// Token: 0x06002DC6 RID: 11718 RVA: 0x00095B48 File Offset: 0x00093D48
		internal void SetDirty()
		{
			this._statebag.SetDirty(true);
			if (this._itemStyle != null)
			{
				this._itemStyle.SetDirty();
			}
			if (this._headerStyle != null)
			{
				this._headerStyle.SetDirty();
			}
			if (this._footerStyle != null)
			{
				this._footerStyle.SetDirty();
			}
			if (this._controlStyle != null)
			{
				this._controlStyle.SetDirty();
			}
		}

		// Token: 0x06002DC7 RID: 11719 RVA: 0x00095BB0 File Offset: 0x00093DB0
		public override string ToString()
		{
			string text = this.HeaderText.Trim();
			if (text.Length <= 0)
			{
				return base.GetType().Name;
			}
			return text;
		}

		// Token: 0x06002DC8 RID: 11720 RVA: 0x00095BE0 File Offset: 0x00093DE0
		protected virtual void TrackViewState()
		{
			this._trackViewState = true;
			((IStateManager)this.ViewState).TrackViewState();
			if (this._itemStyle != null)
			{
				((IStateManager)this._itemStyle).TrackViewState();
			}
			if (this._headerStyle != null)
			{
				((IStateManager)this._headerStyle).TrackViewState();
			}
			if (this._footerStyle != null)
			{
				((IStateManager)this._footerStyle).TrackViewState();
			}
			if (this._controlStyle != null)
			{
				((IStateManager)this._controlStyle).TrackViewState();
			}
		}

		// Token: 0x06002DC9 RID: 11721 RVA: 0x00095C4B File Offset: 0x00093E4B
		public virtual void ValidateSupportsCallback()
		{
			throw new NotSupportedException(SR.GetString("DataControlField_CallbacksNotSupported", new object[]
			{
				this.Control.ID
			}));
		}

		// Token: 0x17000D14 RID: 3348
		// (get) Token: 0x06002DCA RID: 11722 RVA: 0x00095C70 File Offset: 0x00093E70
		bool IStateManager.IsTrackingViewState
		{
			get
			{
				return this.IsTrackingViewState;
			}
		}

		// Token: 0x06002DCB RID: 11723 RVA: 0x00095C78 File Offset: 0x00093E78
		void IStateManager.LoadViewState(object state)
		{
			this.LoadViewState(state);
		}

		// Token: 0x06002DCC RID: 11724 RVA: 0x00095C81 File Offset: 0x00093E81
		void IStateManager.TrackViewState()
		{
			this.TrackViewState();
		}

		// Token: 0x06002DCD RID: 11725 RVA: 0x00095C89 File Offset: 0x00093E89
		object IStateManager.SaveViewState()
		{
			return this.SaveViewState();
		}

		// Token: 0x17000D15 RID: 3349
		// (get) Token: 0x06002DCE RID: 11726 RVA: 0x00095C91 File Offset: 0x00093E91
		// (set) Token: 0x06002DCF RID: 11727 RVA: 0x00095C99 File Offset: 0x00093E99
		object IDataSourceViewSchemaAccessor.DataSourceViewSchema
		{
			get
			{
				return this._dataSourceViewSchema;
			}
			set
			{
				this._dataSourceViewSchema = value;
			}
		}

		// Token: 0x04001FA8 RID: 8104
		private TableItemStyle _itemStyle;

		// Token: 0x04001FA9 RID: 8105
		private TableItemStyle _headerStyle;

		// Token: 0x04001FAA RID: 8106
		private TableItemStyle _footerStyle;

		// Token: 0x04001FAB RID: 8107
		private Style _controlStyle;

		// Token: 0x04001FAC RID: 8108
		private StateBag _statebag;

		// Token: 0x04001FAD RID: 8109
		private bool _trackViewState;

		// Token: 0x04001FAE RID: 8110
		private bool _sortingEnabled;

		// Token: 0x04001FAF RID: 8111
		private Control _control;

		// Token: 0x04001FB0 RID: 8112
		private object _dataSourceViewSchema;
	}
}
