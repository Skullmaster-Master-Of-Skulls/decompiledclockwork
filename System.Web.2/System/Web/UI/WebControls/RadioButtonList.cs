using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Web.Util;

namespace System.Web.UI.WebControls
{
	// Token: 0x020004A9 RID: 1193
	[ValidationProperty("SelectedItem")]
	[SupportsEventValidation]
	public class RadioButtonList : ListControl, IRepeatInfoUser, INamingContainer, IPostBackDataHandler
	{
		// Token: 0x06003BAF RID: 15279 RVA: 0x000C1FD4 File Offset: 0x000C01D4
		public RadioButtonList()
		{
			this._offset = 0;
		}

		// Token: 0x1700116C RID: 4460
		// (get) Token: 0x06003BB0 RID: 15280 RVA: 0x0008566C File Offset: 0x0008386C
		// (set) Token: 0x06003BB1 RID: 15281 RVA: 0x00085688 File Offset: 0x00083888
		[WebCategory("Layout")]
		[DefaultValue(-1)]
		[WebSysDescription("RadioButtonList_CellPadding")]
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

		// Token: 0x1700116D RID: 4461
		// (get) Token: 0x06003BB2 RID: 15282 RVA: 0x0008E6AC File Offset: 0x0008C8AC
		// (set) Token: 0x06003BB3 RID: 15283 RVA: 0x000856B7 File Offset: 0x000838B7
		[WebCategory("Layout")]
		[DefaultValue(-1)]
		[WebSysDescription("RadioButtonList_CellSpacing")]
		public virtual int CellSpacing
		{
			get
			{
				if (!base.ControlStyleCreated)
				{
					return -1;
				}
				return ((TableStyle)base.ControlStyle).CellSpacing;
			}
			set
			{
				((TableStyle)base.ControlStyle).CellSpacing = value;
			}
		}

		// Token: 0x1700116E RID: 4462
		// (get) Token: 0x06003BB4 RID: 15284 RVA: 0x000C1FE4 File Offset: 0x000C01E4
		private RadioButton ControlToRepeat
		{
			get
			{
				if (this._controlToRepeat != null)
				{
					return this._controlToRepeat;
				}
				this._controlToRepeat = new RadioButton();
				this._controlToRepeat.EnableViewState = false;
				this.Controls.Add(this._controlToRepeat);
				this._controlToRepeat.AutoPostBack = this.AutoPostBack;
				this._controlToRepeat.CausesValidation = this.CausesValidation;
				this._controlToRepeat.ValidationGroup = this.ValidationGroup;
				return this._controlToRepeat;
			}
		}

		// Token: 0x1700116F RID: 4463
		// (get) Token: 0x06003BB5 RID: 15285 RVA: 0x000C2064 File Offset: 0x000C0264
		// (set) Token: 0x06003BB6 RID: 15286 RVA: 0x00087751 File Offset: 0x00085951
		[DefaultValue(false)]
		[Themeable(true)]
		[WebCategory("Behavior")]
		[WebSysDescription("ListControl_RenderWhenDataEmpty")]
		public virtual bool RenderWhenDataEmpty
		{
			get
			{
				object obj = this.ViewState["RenderWhenDataEmpty"];
				return obj != null && (bool)obj;
			}
			set
			{
				this.ViewState["RenderWhenDataEmpty"] = value;
			}
		}

		// Token: 0x17001170 RID: 4464
		// (get) Token: 0x06003BB7 RID: 15287 RVA: 0x000C2090 File Offset: 0x000C0290
		// (set) Token: 0x06003BB8 RID: 15288 RVA: 0x0008E71D File Offset: 0x0008C91D
		[WebCategory("Layout")]
		[DefaultValue(0)]
		[WebSysDescription("RadioButtonList_RepeatColumns")]
		public virtual int RepeatColumns
		{
			get
			{
				object obj = this.ViewState["RepeatColumns"];
				if (obj != null)
				{
					return (int)obj;
				}
				return 0;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.ViewState["RepeatColumns"] = value;
			}
		}

		// Token: 0x17001171 RID: 4465
		// (get) Token: 0x06003BB9 RID: 15289 RVA: 0x000C20BC File Offset: 0x000C02BC
		// (set) Token: 0x06003BBA RID: 15290 RVA: 0x0008E76D File Offset: 0x0008C96D
		[WebCategory("Layout")]
		[DefaultValue(RepeatDirection.Vertical)]
		[WebSysDescription("Item_RepeatDirection")]
		public virtual RepeatDirection RepeatDirection
		{
			get
			{
				object obj = this.ViewState["RepeatDirection"];
				if (obj != null)
				{
					return (RepeatDirection)obj;
				}
				return RepeatDirection.Vertical;
			}
			set
			{
				if (value < RepeatDirection.Horizontal || value > RepeatDirection.Vertical)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.ViewState["RepeatDirection"] = value;
			}
		}

		// Token: 0x17001172 RID: 4466
		// (get) Token: 0x06003BBB RID: 15291 RVA: 0x000C20E8 File Offset: 0x000C02E8
		// (set) Token: 0x06003BBC RID: 15292 RVA: 0x0008E7C1 File Offset: 0x0008C9C1
		[WebCategory("Layout")]
		[DefaultValue(RepeatLayout.Table)]
		[WebSysDescription("WebControl_RepeatLayout")]
		public virtual RepeatLayout RepeatLayout
		{
			get
			{
				object obj = this.ViewState["RepeatLayout"];
				if (obj != null)
				{
					return (RepeatLayout)obj;
				}
				return RepeatLayout.Table;
			}
			set
			{
				EnumerationRangeValidationUtil.ValidateRepeatLayout(value);
				this.ViewState["RepeatLayout"] = value;
			}
		}

		// Token: 0x17001173 RID: 4467
		// (get) Token: 0x06003BBD RID: 15293 RVA: 0x000C2114 File Offset: 0x000C0314
		// (set) Token: 0x06003BBE RID: 15294 RVA: 0x0008DA6D File Offset: 0x0008BC6D
		[WebCategory("Appearance")]
		[DefaultValue(TextAlign.Right)]
		[WebSysDescription("WebControl_TextAlign")]
		public virtual TextAlign TextAlign
		{
			get
			{
				object obj = this.ViewState["TextAlign"];
				if (obj != null)
				{
					return (TextAlign)obj;
				}
				return TextAlign.Right;
			}
			set
			{
				if (value < TextAlign.Left || value > TextAlign.Right)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.ViewState["TextAlign"] = value;
			}
		}

		// Token: 0x06003BBF RID: 15295 RVA: 0x0008E809 File Offset: 0x0008CA09
		protected override Style CreateControlStyle()
		{
			return new TableStyle(this.ViewState);
		}

		// Token: 0x06003BC0 RID: 15296 RVA: 0x00004335 File Offset: 0x00002535
		protected override Control FindControl(string id, int pathOffset)
		{
			return this;
		}

		// Token: 0x06003BC1 RID: 15297 RVA: 0x000C213D File Offset: 0x000C033D
		bool IPostBackDataHandler.LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			return this.LoadPostData(postDataKey, postCollection);
		}

		// Token: 0x06003BC2 RID: 15298 RVA: 0x000C2148 File Offset: 0x000C0348
		protected virtual bool LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			string text = postCollection[postDataKey];
			int selectedIndex = this.SelectedIndex;
			base.EnsureDataBoundInLoadPostData();
			int count = this.Items.Count;
			int i = 0;
			while (i < count)
			{
				if (text == this.Items[i].Value && this.Items[i].Enabled)
				{
					base.ValidateEvent(postDataKey, text);
					if (i != selectedIndex)
					{
						base.SetPostDataSelection(i);
						return true;
					}
					return false;
				}
				else
				{
					i++;
				}
			}
			return false;
		}

		// Token: 0x06003BC3 RID: 15299 RVA: 0x000C21C6 File Offset: 0x000C03C6
		void IPostBackDataHandler.RaisePostDataChangedEvent()
		{
			this.RaisePostDataChangedEvent();
		}

		// Token: 0x06003BC4 RID: 15300 RVA: 0x000C21D0 File Offset: 0x000C03D0
		protected virtual void RaisePostDataChangedEvent()
		{
			if (this.AutoPostBack && this.Page != null && !this.Page.IsPostBackEventControlRegistered)
			{
				this.Page.AutoPostBackControl = this;
				if (this.CausesValidation)
				{
					this.Page.Validate(this.ValidationGroup);
				}
			}
			this.OnSelectedIndexChanged(EventArgs.Empty);
		}

		// Token: 0x06003BC5 RID: 15301 RVA: 0x0008E816 File Offset: 0x0008CA16
		protected internal override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			if (!base.DesignMode && !string.IsNullOrEmpty(this.ItemType))
			{
				DataBoundControlHelper.EnableDynamicData(this, this.ItemType);
			}
		}

		// Token: 0x06003BC6 RID: 15302 RVA: 0x000C222C File Offset: 0x000C042C
		protected internal override void Render(HtmlTextWriter writer)
		{
			if (this.RepeatLayout == RepeatLayout.Table && this.RenderWhenDataEmpty)
			{
				throw new InvalidOperationException(SR.GetString("ListControl_RenderWhenDataEmptyNotSupportedWithTableLayout", new object[]
				{
					this.ID
				}));
			}
			if (this.Items.Count == 0 && !base.EnableLegacyRendering && !this.RenderWhenDataEmpty)
			{
				return;
			}
			RepeatInfo repeatInfo = new RepeatInfo();
			Style controlStyle = base.ControlStyleCreated ? base.ControlStyle : null;
			short tabIndex = this.TabIndex;
			bool flag = false;
			this.ControlToRepeat.TabIndex = tabIndex;
			if (tabIndex != 0)
			{
				if (!this.ViewState.IsItemDirty("TabIndex"))
				{
					flag = true;
				}
				this.TabIndex = 0;
			}
			repeatInfo.RepeatColumns = this.RepeatColumns;
			repeatInfo.RepeatDirection = this.RepeatDirection;
			if (!base.DesignMode && !this.Context.Request.Browser.Tables)
			{
				repeatInfo.RepeatLayout = RepeatLayout.Flow;
			}
			else
			{
				repeatInfo.RepeatLayout = this.RepeatLayout;
			}
			if (repeatInfo.RepeatLayout == RepeatLayout.Flow)
			{
				repeatInfo.EnableLegacyRendering = base.EnableLegacyRendering;
			}
			repeatInfo.RenderRepeater(writer, this, controlStyle, this);
			if (this.Page != null)
			{
				this.Page.ClientScript.RegisterForEventValidation(this.UniqueID);
			}
			if (tabIndex != 0)
			{
				this.TabIndex = tabIndex;
			}
			if (flag)
			{
				this.ViewState.SetItemDirty("TabIndex", false);
			}
		}

		// Token: 0x17001174 RID: 4468
		// (get) Token: 0x06003BC7 RID: 15303 RVA: 0x000C2378 File Offset: 0x000C0578
		bool IRepeatInfoUser.HasFooter
		{
			get
			{
				return this.HasFooter;
			}
		}

		// Token: 0x17001175 RID: 4469
		// (get) Token: 0x06003BC8 RID: 15304 RVA: 0x00007722 File Offset: 0x00005922
		protected virtual bool HasFooter
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001176 RID: 4470
		// (get) Token: 0x06003BC9 RID: 15305 RVA: 0x000C2380 File Offset: 0x000C0580
		bool IRepeatInfoUser.HasHeader
		{
			get
			{
				return this.HasHeader;
			}
		}

		// Token: 0x17001177 RID: 4471
		// (get) Token: 0x06003BCA RID: 15306 RVA: 0x00007722 File Offset: 0x00005922
		protected virtual bool HasHeader
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001178 RID: 4472
		// (get) Token: 0x06003BCB RID: 15307 RVA: 0x000C2388 File Offset: 0x000C0588
		bool IRepeatInfoUser.HasSeparators
		{
			get
			{
				return this.HasSeparators;
			}
		}

		// Token: 0x17001179 RID: 4473
		// (get) Token: 0x06003BCC RID: 15308 RVA: 0x00007722 File Offset: 0x00005922
		protected virtual bool HasSeparators
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700117A RID: 4474
		// (get) Token: 0x06003BCD RID: 15309 RVA: 0x000C2390 File Offset: 0x000C0590
		int IRepeatInfoUser.RepeatedItemCount
		{
			get
			{
				return this.RepeatedItemCount;
			}
		}

		// Token: 0x1700117B RID: 4475
		// (get) Token: 0x06003BCE RID: 15310 RVA: 0x0008EB5A File Offset: 0x0008CD5A
		protected virtual int RepeatedItemCount
		{
			get
			{
				if (this.Items == null)
				{
					return 0;
				}
				return this.Items.Count;
			}
		}

		// Token: 0x06003BCF RID: 15311 RVA: 0x000C2398 File Offset: 0x000C0598
		Style IRepeatInfoUser.GetItemStyle(ListItemType itemType, int repeatIndex)
		{
			return this.GetItemStyle(itemType, repeatIndex);
		}

		// Token: 0x06003BD0 RID: 15312 RVA: 0x0000298D File Offset: 0x00000B8D
		protected virtual Style GetItemStyle(ListItemType itemType, int repeatIndex)
		{
			return null;
		}

		// Token: 0x06003BD1 RID: 15313 RVA: 0x000C23A2 File Offset: 0x000C05A2
		void IRepeatInfoUser.RenderItem(ListItemType itemType, int repeatIndex, RepeatInfo repeatInfo, HtmlTextWriter writer)
		{
			this.RenderItem(itemType, repeatIndex, repeatInfo, writer);
		}

		// Token: 0x06003BD2 RID: 15314 RVA: 0x000C23B0 File Offset: 0x000C05B0
		protected virtual void RenderItem(ListItemType itemType, int repeatIndex, RepeatInfo repeatInfo, HtmlTextWriter writer)
		{
			if (repeatIndex == 0)
			{
				this._cachedIsEnabled = base.IsEnabled;
				this._cachedRegisterEnabled = (this.Page != null && !base.SaveSelectedIndicesViewState);
			}
			RadioButton controlToRepeat = this.ControlToRepeat;
			int index = repeatIndex + this._offset;
			ListItem listItem = this.Items[index];
			controlToRepeat.Attributes.Clear();
			if (listItem.HasAttributes)
			{
				foreach (object obj in listItem.Attributes.Keys)
				{
					string key = (string)obj;
					controlToRepeat.Attributes[key] = listItem.Attributes[key];
				}
			}
			if (!string.IsNullOrEmpty(controlToRepeat.CssClass))
			{
				controlToRepeat.CssClass = "";
			}
			ListControl.SetControlToRepeatID(this, controlToRepeat, index);
			controlToRepeat.Text = listItem.Text;
			controlToRepeat.Attributes["value"] = listItem.Value;
			controlToRepeat.Checked = listItem.Selected;
			controlToRepeat.Enabled = (this._cachedIsEnabled && listItem.Enabled);
			controlToRepeat.TextAlign = this.TextAlign;
			controlToRepeat.RenderControl(writer);
			if (controlToRepeat.Enabled && this._cachedRegisterEnabled && this.Page != null)
			{
				this.Page.RegisterEnabledControl(controlToRepeat);
			}
		}

		// Token: 0x0400234B RID: 9035
		private RadioButton _controlToRepeat;

		// Token: 0x0400234C RID: 9036
		private bool _cachedIsEnabled;

		// Token: 0x0400234D RID: 9037
		private bool _cachedRegisterEnabled;

		// Token: 0x0400234E RID: 9038
		private int _offset;
	}
}
