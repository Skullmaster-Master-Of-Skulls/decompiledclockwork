using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	// Token: 0x020004F2 RID: 1266
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class CheckBoxList : ListControl, IRepeatInfoUser, INamingContainer, IPostBackDataHandler
	{
		// Token: 0x06003DAC RID: 15788 RVA: 0x001025AA File Offset: 0x001015AA
		public CheckBoxList()
		{
			this._controlToRepeat = new CheckBox();
			this._controlToRepeat.EnableViewState = false;
			this._controlToRepeat.ID = "0";
			this.Controls.Add(this._controlToRepeat);
		}

		// Token: 0x17000E78 RID: 3704
		// (get) Token: 0x06003DAD RID: 15789 RVA: 0x001025EA File Offset: 0x001015EA
		// (set) Token: 0x06003DAE RID: 15790 RVA: 0x00102606 File Offset: 0x00101606
		[DefaultValue(-1)]
		[WebCategory("Layout")]
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

		// Token: 0x17000E79 RID: 3705
		// (get) Token: 0x06003DAF RID: 15791 RVA: 0x00102619 File Offset: 0x00101619
		// (set) Token: 0x06003DB0 RID: 15792 RVA: 0x00102635 File Offset: 0x00101635
		[DefaultValue(-1)]
		[WebCategory("Layout")]
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

		// Token: 0x17000E7A RID: 3706
		// (get) Token: 0x06003DB1 RID: 15793 RVA: 0x00102648 File Offset: 0x00101648
		internal override bool IsMultiSelectInternal
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000E7B RID: 3707
		// (get) Token: 0x06003DB2 RID: 15794 RVA: 0x0010264C File Offset: 0x0010164C
		// (set) Token: 0x06003DB3 RID: 15795 RVA: 0x00102675 File Offset: 0x00101675
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

		// Token: 0x17000E7C RID: 3708
		// (get) Token: 0x06003DB4 RID: 15796 RVA: 0x0010269C File Offset: 0x0010169C
		// (set) Token: 0x06003DB5 RID: 15797 RVA: 0x001026C5 File Offset: 0x001016C5
		[WebSysDescription("Item_RepeatDirection")]
		[DefaultValue(RepeatDirection.Vertical)]
		[WebCategory("Layout")]
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

		// Token: 0x17000E7D RID: 3709
		// (get) Token: 0x06003DB6 RID: 15798 RVA: 0x001026F0 File Offset: 0x001016F0
		// (set) Token: 0x06003DB7 RID: 15799 RVA: 0x00102719 File Offset: 0x00101719
		[WebSysDescription("WebControl_RepeatLayout")]
		[WebCategory("Layout")]
		[DefaultValue(RepeatLayout.Table)]
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

		// Token: 0x17000E7E RID: 3710
		// (get) Token: 0x06003DB8 RID: 15800 RVA: 0x00102744 File Offset: 0x00101744
		// (set) Token: 0x06003DB9 RID: 15801 RVA: 0x0010276D File Offset: 0x0010176D
		[DefaultValue(TextAlign.Right)]
		[WebSysDescription("WebControl_TextAlign")]
		[WebCategory("Appearance")]
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

		// Token: 0x06003DBA RID: 15802 RVA: 0x00102798 File Offset: 0x00101798
		protected override Style CreateControlStyle()
		{
			return new TableStyle(this.ViewState);
		}

		// Token: 0x06003DBB RID: 15803 RVA: 0x001027A5 File Offset: 0x001017A5
		protected override Control FindControl(string id, int pathOffset)
		{
			return this;
		}

		// Token: 0x06003DBC RID: 15804 RVA: 0x001027A8 File Offset: 0x001017A8
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
					this._controlToRepeat.ID = i.ToString(NumberFormatInfo.InvariantInfo);
					this.Page.RegisterRequiresPostBack(this._controlToRepeat);
				}
			}
		}

		// Token: 0x06003DBD RID: 15805 RVA: 0x00102838 File Offset: 0x00101838
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

		// Token: 0x06003DBE RID: 15806 RVA: 0x00102963 File Offset: 0x00101963
		bool IPostBackDataHandler.LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			return this.LoadPostData(postDataKey, postCollection);
		}

		// Token: 0x06003DBF RID: 15807 RVA: 0x00102970 File Offset: 0x00101970
		protected virtual bool LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			if (!base.IsEnabled)
			{
				return false;
			}
			string s = postDataKey.Substring(this.UniqueID.Length + 1);
			int num = int.Parse(s, CultureInfo.InvariantCulture);
			this.EnsureDataBound();
			if (num >= 0 && num < this.Items.Count)
			{
				ListItem listItem = this.Items[num];
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

		// Token: 0x06003DC0 RID: 15808 RVA: 0x00102A06 File Offset: 0x00101A06
		void IPostBackDataHandler.RaisePostDataChangedEvent()
		{
			this.RaisePostDataChangedEvent();
		}

		// Token: 0x06003DC1 RID: 15809 RVA: 0x00102A10 File Offset: 0x00101A10
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

		// Token: 0x17000E7F RID: 3711
		// (get) Token: 0x06003DC2 RID: 15810 RVA: 0x00102A62 File Offset: 0x00101A62
		bool IRepeatInfoUser.HasFooter
		{
			get
			{
				return this.HasFooter;
			}
		}

		// Token: 0x17000E80 RID: 3712
		// (get) Token: 0x06003DC3 RID: 15811 RVA: 0x00102A6A File Offset: 0x00101A6A
		protected virtual bool HasFooter
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000E81 RID: 3713
		// (get) Token: 0x06003DC4 RID: 15812 RVA: 0x00102A6D File Offset: 0x00101A6D
		bool IRepeatInfoUser.HasHeader
		{
			get
			{
				return this.HasHeader;
			}
		}

		// Token: 0x17000E82 RID: 3714
		// (get) Token: 0x06003DC5 RID: 15813 RVA: 0x00102A75 File Offset: 0x00101A75
		protected virtual bool HasHeader
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000E83 RID: 3715
		// (get) Token: 0x06003DC6 RID: 15814 RVA: 0x00102A78 File Offset: 0x00101A78
		bool IRepeatInfoUser.HasSeparators
		{
			get
			{
				return this.HasSeparators;
			}
		}

		// Token: 0x17000E84 RID: 3716
		// (get) Token: 0x06003DC7 RID: 15815 RVA: 0x00102A80 File Offset: 0x00101A80
		protected virtual bool HasSeparators
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000E85 RID: 3717
		// (get) Token: 0x06003DC8 RID: 15816 RVA: 0x00102A83 File Offset: 0x00101A83
		int IRepeatInfoUser.RepeatedItemCount
		{
			get
			{
				return this.RepeatedItemCount;
			}
		}

		// Token: 0x17000E86 RID: 3718
		// (get) Token: 0x06003DC9 RID: 15817 RVA: 0x00102A8B File Offset: 0x00101A8B
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

		// Token: 0x06003DCA RID: 15818 RVA: 0x00102AA2 File Offset: 0x00101AA2
		Style IRepeatInfoUser.GetItemStyle(ListItemType itemType, int repeatIndex)
		{
			return this.GetItemStyle(itemType, repeatIndex);
		}

		// Token: 0x06003DCB RID: 15819 RVA: 0x00102AAC File Offset: 0x00101AAC
		protected virtual Style GetItemStyle(ListItemType itemType, int repeatIndex)
		{
			return null;
		}

		// Token: 0x06003DCC RID: 15820 RVA: 0x00102AAF File Offset: 0x00101AAF
		void IRepeatInfoUser.RenderItem(ListItemType itemType, int repeatIndex, RepeatInfo repeatInfo, HtmlTextWriter writer)
		{
			this.RenderItem(itemType, repeatIndex, repeatInfo, writer);
		}

		// Token: 0x06003DCD RID: 15821 RVA: 0x00102ABC File Offset: 0x00101ABC
		protected virtual void RenderItem(ListItemType itemType, int repeatIndex, RepeatInfo repeatInfo, HtmlTextWriter writer)
		{
			if (repeatIndex == 0)
			{
				this._cachedIsEnabled = base.IsEnabled;
				this._cachedRegisterEnabled = (this.Page != null && base.IsEnabled && !base.SaveSelectedIndicesViewState);
			}
			int index = repeatIndex;
			ListItem listItem = this.Items[index];
			this._controlToRepeat.Attributes.Clear();
			if (listItem.HasAttributes)
			{
				foreach (object obj in listItem.Attributes.Keys)
				{
					string key = (string)obj;
					this._controlToRepeat.Attributes[key] = listItem.Attributes[key];
				}
			}
			this._controlToRepeat.ID = index.ToString(NumberFormatInfo.InvariantInfo);
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

		// Token: 0x04002785 RID: 10117
		private CheckBox _controlToRepeat;

		// Token: 0x04002786 RID: 10118
		private string _oldAccessKey;

		// Token: 0x04002787 RID: 10119
		private bool _hasNotifiedOfChange;

		// Token: 0x04002788 RID: 10120
		private bool _cachedRegisterEnabled;

		// Token: 0x04002789 RID: 10121
		private bool _cachedIsEnabled;
	}
}
