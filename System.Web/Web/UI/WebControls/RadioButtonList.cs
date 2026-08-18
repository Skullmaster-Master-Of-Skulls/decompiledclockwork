using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000622 RID: 1570
	[ValidationProperty("SelectedItem")]
	[SupportsEventValidation]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class RadioButtonList : ListControl, IRepeatInfoUser, INamingContainer, IPostBackDataHandler
	{
		// Token: 0x06004DDE RID: 19934 RVA: 0x0013BD88 File Offset: 0x0013AD88
		public RadioButtonList()
		{
			this._offset = 0;
		}

		// Token: 0x170013A4 RID: 5028
		// (get) Token: 0x06004DDF RID: 19935 RVA: 0x0013BD97 File Offset: 0x0013AD97
		// (set) Token: 0x06004DE0 RID: 19936 RVA: 0x0013BDB3 File Offset: 0x0013ADB3
		[WebCategory("Layout")]
		[WebSysDescription("RadioButtonList_CellPadding")]
		[DefaultValue(-1)]
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

		// Token: 0x170013A5 RID: 5029
		// (get) Token: 0x06004DE1 RID: 19937 RVA: 0x0013BDC6 File Offset: 0x0013ADC6
		// (set) Token: 0x06004DE2 RID: 19938 RVA: 0x0013BDE2 File Offset: 0x0013ADE2
		[WebSysDescription("RadioButtonList_CellSpacing")]
		[WebCategory("Layout")]
		[DefaultValue(-1)]
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

		// Token: 0x170013A6 RID: 5030
		// (get) Token: 0x06004DE3 RID: 19939 RVA: 0x0013BDF8 File Offset: 0x0013ADF8
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

		// Token: 0x170013A7 RID: 5031
		// (get) Token: 0x06004DE4 RID: 19940 RVA: 0x0013BE78 File Offset: 0x0013AE78
		// (set) Token: 0x06004DE5 RID: 19941 RVA: 0x0013BEA1 File Offset: 0x0013AEA1
		[WebCategory("Layout")]
		[WebSysDescription("RadioButtonList_RepeatColumns")]
		[DefaultValue(0)]
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

		// Token: 0x170013A8 RID: 5032
		// (get) Token: 0x06004DE6 RID: 19942 RVA: 0x0013BEC8 File Offset: 0x0013AEC8
		// (set) Token: 0x06004DE7 RID: 19943 RVA: 0x0013BEF1 File Offset: 0x0013AEF1
		[WebSysDescription("Item_RepeatDirection")]
		[WebCategory("Layout")]
		[DefaultValue(RepeatDirection.Vertical)]
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

		// Token: 0x170013A9 RID: 5033
		// (get) Token: 0x06004DE8 RID: 19944 RVA: 0x0013BF1C File Offset: 0x0013AF1C
		// (set) Token: 0x06004DE9 RID: 19945 RVA: 0x0013BF45 File Offset: 0x0013AF45
		[DefaultValue(RepeatLayout.Table)]
		[WebCategory("Layout")]
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
				if (value < RepeatLayout.Table || value > RepeatLayout.Flow)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.ViewState["RepeatLayout"] = value;
			}
		}

		// Token: 0x170013AA RID: 5034
		// (get) Token: 0x06004DEA RID: 19946 RVA: 0x0013BF70 File Offset: 0x0013AF70
		// (set) Token: 0x06004DEB RID: 19947 RVA: 0x0013BF99 File Offset: 0x0013AF99
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

		// Token: 0x06004DEC RID: 19948 RVA: 0x0013BFC4 File Offset: 0x0013AFC4
		protected override Style CreateControlStyle()
		{
			return new TableStyle(this.ViewState);
		}

		// Token: 0x06004DED RID: 19949 RVA: 0x0013BFD1 File Offset: 0x0013AFD1
		protected override Control FindControl(string id, int pathOffset)
		{
			return this;
		}

		// Token: 0x06004DEE RID: 19950 RVA: 0x0013BFD4 File Offset: 0x0013AFD4
		bool IPostBackDataHandler.LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			return this.LoadPostData(postDataKey, postCollection);
		}

		// Token: 0x06004DEF RID: 19951 RVA: 0x0013BFE0 File Offset: 0x0013AFE0
		protected virtual bool LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			string text = postCollection[postDataKey];
			int selectedIndex = this.SelectedIndex;
			this.EnsureDataBound();
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

		// Token: 0x06004DF0 RID: 19952 RVA: 0x0013C05E File Offset: 0x0013B05E
		void IPostBackDataHandler.RaisePostDataChangedEvent()
		{
			this.RaisePostDataChangedEvent();
		}

		// Token: 0x06004DF1 RID: 19953 RVA: 0x0013C068 File Offset: 0x0013B068
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

		// Token: 0x06004DF2 RID: 19954 RVA: 0x0013C0C4 File Offset: 0x0013B0C4
		protected internal override void Render(HtmlTextWriter writer)
		{
			if (this.Items.Count == 0 && !base.EnableLegacyRendering)
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

		// Token: 0x170013AB RID: 5035
		// (get) Token: 0x06004DF3 RID: 19955 RVA: 0x0013C1D9 File Offset: 0x0013B1D9
		bool IRepeatInfoUser.HasFooter
		{
			get
			{
				return this.HasFooter;
			}
		}

		// Token: 0x170013AC RID: 5036
		// (get) Token: 0x06004DF4 RID: 19956 RVA: 0x0013C1E1 File Offset: 0x0013B1E1
		protected virtual bool HasFooter
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170013AD RID: 5037
		// (get) Token: 0x06004DF5 RID: 19957 RVA: 0x0013C1E4 File Offset: 0x0013B1E4
		bool IRepeatInfoUser.HasHeader
		{
			get
			{
				return this.HasHeader;
			}
		}

		// Token: 0x170013AE RID: 5038
		// (get) Token: 0x06004DF6 RID: 19958 RVA: 0x0013C1EC File Offset: 0x0013B1EC
		protected virtual bool HasHeader
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170013AF RID: 5039
		// (get) Token: 0x06004DF7 RID: 19959 RVA: 0x0013C1EF File Offset: 0x0013B1EF
		bool IRepeatInfoUser.HasSeparators
		{
			get
			{
				return this.HasSeparators;
			}
		}

		// Token: 0x170013B0 RID: 5040
		// (get) Token: 0x06004DF8 RID: 19960 RVA: 0x0013C1F7 File Offset: 0x0013B1F7
		protected virtual bool HasSeparators
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170013B1 RID: 5041
		// (get) Token: 0x06004DF9 RID: 19961 RVA: 0x0013C1FA File Offset: 0x0013B1FA
		int IRepeatInfoUser.RepeatedItemCount
		{
			get
			{
				return this.RepeatedItemCount;
			}
		}

		// Token: 0x170013B2 RID: 5042
		// (get) Token: 0x06004DFA RID: 19962 RVA: 0x0013C202 File Offset: 0x0013B202
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

		// Token: 0x06004DFB RID: 19963 RVA: 0x0013C219 File Offset: 0x0013B219
		Style IRepeatInfoUser.GetItemStyle(ListItemType itemType, int repeatIndex)
		{
			return this.GetItemStyle(itemType, repeatIndex);
		}

		// Token: 0x06004DFC RID: 19964 RVA: 0x0013C223 File Offset: 0x0013B223
		protected virtual Style GetItemStyle(ListItemType itemType, int repeatIndex)
		{
			return null;
		}

		// Token: 0x06004DFD RID: 19965 RVA: 0x0013C226 File Offset: 0x0013B226
		void IRepeatInfoUser.RenderItem(ListItemType itemType, int repeatIndex, RepeatInfo repeatInfo, HtmlTextWriter writer)
		{
			this.RenderItem(itemType, repeatIndex, repeatInfo, writer);
		}

		// Token: 0x06004DFE RID: 19966 RVA: 0x0013C234 File Offset: 0x0013B234
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
			controlToRepeat.ID = index.ToString(NumberFormatInfo.InvariantInfo);
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

		// Token: 0x04002C72 RID: 11378
		private RadioButton _controlToRepeat;

		// Token: 0x04002C73 RID: 11379
		private bool _cachedIsEnabled;

		// Token: 0x04002C74 RID: 11380
		private bool _cachedRegisterEnabled;

		// Token: 0x04002C75 RID: 11381
		private int _offset;
	}
}
