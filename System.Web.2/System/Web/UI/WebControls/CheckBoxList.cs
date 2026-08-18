using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;
using System.Web.Util;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000393 RID: 915
	public class CheckBoxList : ListControl, IRepeatInfoUser, INamingContainer, IPostBackDataHandler
	{
		// Token: 0x06002B8F RID: 11151 RVA: 0x0008E66C File Offset: 0x0008C86C
		public CheckBoxList()
		{
			this._controlToRepeat = new CheckBox();
			this._controlToRepeat.EnableViewState = false;
			this._controlToRepeat.ID = "0";
			this.Controls.Add(this._controlToRepeat);
		}

		// Token: 0x17000C47 RID: 3143
		// (get) Token: 0x06002B90 RID: 11152 RVA: 0x0008566C File Offset: 0x0008386C
		// (set) Token: 0x06002B91 RID: 11153 RVA: 0x00085688 File Offset: 0x00083888
		[WebCategory("Layout")]
		[DefaultValue(-1)]
		[WebSysDescription("CheckBoxList_CellPadding")]
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

		// Token: 0x17000C48 RID: 3144
		// (get) Token: 0x06002B92 RID: 11154 RVA: 0x0008E6AC File Offset: 0x0008C8AC
		// (set) Token: 0x06002B93 RID: 11155 RVA: 0x000856B7 File Offset: 0x000838B7
		[WebCategory("Layout")]
		[DefaultValue(-1)]
		[WebSysDescription("CheckBoxList_CellSpacing")]
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

		// Token: 0x17000C49 RID: 3145
		// (get) Token: 0x06002B94 RID: 11156 RVA: 0x000097B7 File Offset: 0x000079B7
		internal override bool IsMultiSelectInternal
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000C4A RID: 3146
		// (get) Token: 0x06002B95 RID: 11157 RVA: 0x0008E6C8 File Offset: 0x0008C8C8
		// (set) Token: 0x06002B96 RID: 11158 RVA: 0x00087751 File Offset: 0x00085951
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

		// Token: 0x17000C4B RID: 3147
		// (get) Token: 0x06002B97 RID: 11159 RVA: 0x0008E6F4 File Offset: 0x0008C8F4
		// (set) Token: 0x06002B98 RID: 11160 RVA: 0x0008E71D File Offset: 0x0008C91D
		[WebCategory("Layout")]
		[DefaultValue(0)]
		[WebSysDescription("CheckBoxList_RepeatColumns")]
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

		// Token: 0x17000C4C RID: 3148
		// (get) Token: 0x06002B99 RID: 11161 RVA: 0x0008E744 File Offset: 0x0008C944
		// (set) Token: 0x06002B9A RID: 11162 RVA: 0x0008E76D File Offset: 0x0008C96D
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

		// Token: 0x17000C4D RID: 3149
		// (get) Token: 0x06002B9B RID: 11163 RVA: 0x0008E798 File Offset: 0x0008C998
		// (set) Token: 0x06002B9C RID: 11164 RVA: 0x0008E7C1 File Offset: 0x0008C9C1
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

		// Token: 0x17000C4E RID: 3150
		// (get) Token: 0x06002B9D RID: 11165 RVA: 0x0008E7E0 File Offset: 0x0008C9E0
		// (set) Token: 0x06002B9E RID: 11166 RVA: 0x0008DA6D File Offset: 0x0008BC6D
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

		// Token: 0x06002B9F RID: 11167 RVA: 0x0008E809 File Offset: 0x0008CA09
		protected override Style CreateControlStyle()
		{
			return new TableStyle(this.ViewState);
		}

		// Token: 0x06002BA0 RID: 11168 RVA: 0x00004335 File Offset: 0x00002535
		protected override Control FindControl(string id, int pathOffset)
		{
			return this;
		}

		// Token: 0x06002BA1 RID: 11169 RVA: 0x0008E816 File Offset: 0x0008CA16
		protected internal override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			if (!base.DesignMode && !string.IsNullOrEmpty(this.ItemType))
			{
				DataBoundControlHelper.EnableDynamicData(this, this.ItemType);
			}
		}

		// Token: 0x06002BA2 RID: 11170 RVA: 0x0008E840 File Offset: 0x0008CA40
		protected internal override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			this._controlToRepeat.AutoPostBack = this.AutoPostBack;
			this._controlToRepeat.CausesValidation = this.CausesValidation;
			this._controlToRepeat.ValidationGroup = this.ValidationGroup;
			if (this.Page != null)
			{
				for (int i = 0; i < this.Items.Count; i++)
				{
					ListControl.SetControlToRepeatID(this, this._controlToRepeat, i);
					this.Page.RegisterRequiresPostBack(this._controlToRepeat);
				}
			}
		}

		// Token: 0x06002BA3 RID: 11171 RVA: 0x0008E8C4 File Offset: 0x0008CAC4
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
			this._controlToRepeat.TextAlign = this.TextAlign;
			this._controlToRepeat.TabIndex = tabIndex;
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
			this._oldAccessKey = this.AccessKey;
			this.AccessKey = string.Empty;
			repeatInfo.RenderRepeater(writer, this, controlStyle, this);
			this.AccessKey = this._oldAccessKey;
			if (tabIndex != 0)
			{
				this.TabIndex = tabIndex;
			}
			if (flag)
			{
				this.ViewState.SetItemDirty("TabIndex", false);
			}
		}

		// Token: 0x06002BA4 RID: 11172 RVA: 0x0008EA26 File Offset: 0x0008CC26
		bool IPostBackDataHandler.LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			return this.LoadPostData(postDataKey, postCollection);
		}

		// Token: 0x06002BA5 RID: 11173 RVA: 0x0008EA30 File Offset: 0x0008CC30
		protected virtual bool LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			if (!base.IsEnabled)
			{
				return false;
			}
			string text = postDataKey.Substring(this.UniqueID.Length + 1);
			int num = text.LastIndexOf('_');
			if (num != -1)
			{
				text = text.Substring(num + 1);
			}
			int num2 = int.Parse(text, CultureInfo.InvariantCulture);
			base.EnsureDataBoundInLoadPostData();
			if (num2 >= 0 && num2 < this.Items.Count)
			{
				ListItem listItem = this.Items[num2];
				if (!listItem.Enabled)
				{
					return false;
				}
				bool flag = postCollection[postDataKey] != null;
				if (listItem.Selected != flag)
				{
					listItem.Selected = flag;
					if (!this._hasNotifiedOfChange)
					{
						this._hasNotifiedOfChange = true;
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06002BA6 RID: 11174 RVA: 0x0008EADD File Offset: 0x0008CCDD
		void IPostBackDataHandler.RaisePostDataChangedEvent()
		{
			this.RaisePostDataChangedEvent();
		}

		// Token: 0x06002BA7 RID: 11175 RVA: 0x0008EAE8 File Offset: 0x0008CCE8
		protected virtual void RaisePostDataChangedEvent()
		{
			if (this.AutoPostBack && !this.Page.IsPostBackEventControlRegistered)
			{
				this.Page.AutoPostBackControl = this;
				if (this.CausesValidation)
				{
					this.Page.Validate(this.ValidationGroup);
				}
			}
			this.OnSelectedIndexChanged(EventArgs.Empty);
		}

		// Token: 0x17000C4F RID: 3151
		// (get) Token: 0x06002BA8 RID: 11176 RVA: 0x0008EB3A File Offset: 0x0008CD3A
		bool IRepeatInfoUser.HasFooter
		{
			get
			{
				return this.HasFooter;
			}
		}

		// Token: 0x17000C50 RID: 3152
		// (get) Token: 0x06002BA9 RID: 11177 RVA: 0x00007722 File Offset: 0x00005922
		protected virtual bool HasFooter
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000C51 RID: 3153
		// (get) Token: 0x06002BAA RID: 11178 RVA: 0x0008EB42 File Offset: 0x0008CD42
		bool IRepeatInfoUser.HasHeader
		{
			get
			{
				return this.HasHeader;
			}
		}

		// Token: 0x17000C52 RID: 3154
		// (get) Token: 0x06002BAB RID: 11179 RVA: 0x00007722 File Offset: 0x00005922
		protected virtual bool HasHeader
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000C53 RID: 3155
		// (get) Token: 0x06002BAC RID: 11180 RVA: 0x0008EB4A File Offset: 0x0008CD4A
		bool IRepeatInfoUser.HasSeparators
		{
			get
			{
				return this.HasSeparators;
			}
		}

		// Token: 0x17000C54 RID: 3156
		// (get) Token: 0x06002BAD RID: 11181 RVA: 0x00007722 File Offset: 0x00005922
		protected virtual bool HasSeparators
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000C55 RID: 3157
		// (get) Token: 0x06002BAE RID: 11182 RVA: 0x0008EB52 File Offset: 0x0008CD52
		int IRepeatInfoUser.RepeatedItemCount
		{
			get
			{
				return this.RepeatedItemCount;
			}
		}

		// Token: 0x17000C56 RID: 3158
		// (get) Token: 0x06002BAF RID: 11183 RVA: 0x0008EB5A File Offset: 0x0008CD5A
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

		// Token: 0x06002BB0 RID: 11184 RVA: 0x0008EB71 File Offset: 0x0008CD71
		Style IRepeatInfoUser.GetItemStyle(ListItemType itemType, int repeatIndex)
		{
			return this.GetItemStyle(itemType, repeatIndex);
		}

		// Token: 0x06002BB1 RID: 11185 RVA: 0x0000298D File Offset: 0x00000B8D
		protected virtual Style GetItemStyle(ListItemType itemType, int repeatIndex)
		{
			return null;
		}

		// Token: 0x06002BB2 RID: 11186 RVA: 0x0008EB7B File Offset: 0x0008CD7B
		void IRepeatInfoUser.RenderItem(ListItemType itemType, int repeatIndex, RepeatInfo repeatInfo, HtmlTextWriter writer)
		{
			this.RenderItem(itemType, repeatIndex, repeatInfo, writer);
		}

		// Token: 0x06002BB3 RID: 11187 RVA: 0x0008EB88 File Offset: 0x0008CD88
		protected virtual void RenderItem(ListItemType itemType, int repeatIndex, RepeatInfo repeatInfo, HtmlTextWriter writer)
		{
			if (repeatIndex == 0)
			{
				this._cachedIsEnabled = base.IsEnabled;
				this._cachedRegisterEnabled = (this.Page != null && base.IsEnabled && !base.SaveSelectedIndicesViewState);
			}
			ListItem listItem = this.Items[repeatIndex];
			this._controlToRepeat.Attributes.Clear();
			if (listItem.HasAttributes)
			{
				foreach (object obj in listItem.Attributes.Keys)
				{
					string key = (string)obj;
					this._controlToRepeat.Attributes[key] = listItem.Attributes[key];
				}
			}
			if (!string.IsNullOrEmpty(this._controlToRepeat.CssClass))
			{
				this._controlToRepeat.CssClass = "";
			}
			if (this.RenderingCompatibility >= VersionUtil.Framework40)
			{
				this._controlToRepeat.InputAttributes.Add("value", listItem.Value);
			}
			ListControl.SetControlToRepeatID(this, this._controlToRepeat, repeatIndex);
			this._controlToRepeat.Text = listItem.Text;
			this._controlToRepeat.Checked = listItem.Selected;
			this._controlToRepeat.Enabled = (this._cachedIsEnabled && listItem.Enabled);
			this._controlToRepeat.AccessKey = this._oldAccessKey;
			if (this._cachedRegisterEnabled && this._controlToRepeat.Enabled)
			{
				this.Page.RegisterEnabledControl(this._controlToRepeat);
			}
			this._controlToRepeat.RenderControl(writer);
		}

		// Token: 0x04001F19 RID: 7961
		private CheckBox _controlToRepeat;

		// Token: 0x04001F1A RID: 7962
		private string _oldAccessKey;

		// Token: 0x04001F1B RID: 7963
		private bool _hasNotifiedOfChange;

		// Token: 0x04001F1C RID: 7964
		private bool _cachedRegisterEnabled;

		// Token: 0x04001F1D RID: 7965
		private bool _cachedIsEnabled;
	}
}
