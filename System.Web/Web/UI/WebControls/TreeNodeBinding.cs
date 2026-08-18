using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000669 RID: 1641
	[DefaultProperty("TextField")]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class TreeNodeBinding : IStateManager, ICloneable, IDataSourceViewSchemaAccessor
	{
		// Token: 0x17001463 RID: 5219
		// (get) Token: 0x0600506A RID: 20586 RVA: 0x00143968 File Offset: 0x00142968
		// (set) Token: 0x0600506B RID: 20587 RVA: 0x00143995 File Offset: 0x00142995
		[WebCategory("Data")]
		[DefaultValue("")]
		[WebSysDescription("Binding_DataMember")]
		public string DataMember
		{
			get
			{
				string text = (string)this.ViewState["DataMember"];
				if (text == null)
				{
					return string.Empty;
				}
				return text;
			}
			set
			{
				this.ViewState["DataMember"] = value;
			}
		}

		// Token: 0x17001464 RID: 5220
		// (get) Token: 0x0600506C RID: 20588 RVA: 0x001439A8 File Offset: 0x001429A8
		// (set) Token: 0x0600506D RID: 20589 RVA: 0x001439D1 File Offset: 0x001429D1
		[DefaultValue(-1)]
		[WebSysDescription("TreeNodeBinding_Depth")]
		[TypeConverter("System.Web.UI.Design.WebControls.TreeNodeBindingDepthConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[WebCategory("Data")]
		public int Depth
		{
			get
			{
				object obj = this.ViewState["Depth"];
				if (obj == null)
				{
					return -1;
				}
				return (int)obj;
			}
			set
			{
				this.ViewState["Depth"] = value;
			}
		}

		// Token: 0x17001465 RID: 5221
		// (get) Token: 0x0600506E RID: 20590 RVA: 0x001439EC File Offset: 0x001429EC
		// (set) Token: 0x0600506F RID: 20591 RVA: 0x00143A19 File Offset: 0x00142A19
		[Localizable(true)]
		[WebSysDescription("TreeNodeBinding_FormatString")]
		[DefaultValue("")]
		[WebCategory("Databindings")]
		public string FormatString
		{
			get
			{
				string text = (string)this.ViewState["FormatString"];
				if (text == null)
				{
					return string.Empty;
				}
				return text;
			}
			set
			{
				this.ViewState["FormatString"] = value;
			}
		}

		// Token: 0x17001466 RID: 5222
		// (get) Token: 0x06005070 RID: 20592 RVA: 0x00143A2C File Offset: 0x00142A2C
		// (set) Token: 0x06005071 RID: 20593 RVA: 0x00143A59 File Offset: 0x00142A59
		[WebCategory("DefaultProperties")]
		[WebSysDescription("TreeNodeBinding_ImageToolTip")]
		[Localizable(true)]
		[DefaultValue("")]
		public string ImageToolTip
		{
			get
			{
				string text = (string)this.ViewState["ImageToolTip"];
				if (text == null)
				{
					return string.Empty;
				}
				return text;
			}
			set
			{
				this.ViewState["ImageToolTip"] = value;
			}
		}

		// Token: 0x17001467 RID: 5223
		// (get) Token: 0x06005072 RID: 20594 RVA: 0x00143A6C File Offset: 0x00142A6C
		// (set) Token: 0x06005073 RID: 20595 RVA: 0x00143A99 File Offset: 0x00142A99
		[DefaultValue("")]
		[WebCategory("Databindings")]
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[WebSysDescription("TreeNodeBinding_ImageToolTipField")]
		public string ImageToolTipField
		{
			get
			{
				string text = (string)this.ViewState["ImageToolTipField"];
				if (text == null)
				{
					return string.Empty;
				}
				return text;
			}
			set
			{
				this.ViewState["ImageToolTipField"] = value;
			}
		}

		// Token: 0x17001468 RID: 5224
		// (get) Token: 0x06005074 RID: 20596 RVA: 0x00143AAC File Offset: 0x00142AAC
		// (set) Token: 0x06005075 RID: 20597 RVA: 0x00143AD9 File Offset: 0x00142AD9
		[WebCategory("DefaultProperties")]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[WebSysDescription("TreeNodeBinding_ImageUrl")]
		[UrlProperty]
		[DefaultValue("")]
		public string ImageUrl
		{
			get
			{
				string text = (string)this.ViewState["ImageUrl"];
				if (text == null)
				{
					return string.Empty;
				}
				return text;
			}
			set
			{
				this.ViewState["ImageUrl"] = value;
			}
		}

		// Token: 0x17001469 RID: 5225
		// (get) Token: 0x06005076 RID: 20598 RVA: 0x00143AEC File Offset: 0x00142AEC
		// (set) Token: 0x06005077 RID: 20599 RVA: 0x00143B19 File Offset: 0x00142B19
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[WebSysDescription("TreeNodeBinding_ImageUrlField")]
		[DefaultValue("")]
		[WebCategory("Databindings")]
		public string ImageUrlField
		{
			get
			{
				string text = (string)this.ViewState["ImageUrlField"];
				if (text == null)
				{
					return string.Empty;
				}
				return text;
			}
			set
			{
				this.ViewState["ImageUrlField"] = value;
			}
		}

		// Token: 0x1700146A RID: 5226
		// (get) Token: 0x06005078 RID: 20600 RVA: 0x00143B2C File Offset: 0x00142B2C
		// (set) Token: 0x06005079 RID: 20601 RVA: 0x00143B59 File Offset: 0x00142B59
		[DefaultValue("")]
		[WebCategory("DefaultProperties")]
		[WebSysDescription("TreeNodeBinding_NavigateUrl")]
		[Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[UrlProperty]
		public string NavigateUrl
		{
			get
			{
				string text = (string)this.ViewState["NavigateUrl"];
				if (text == null)
				{
					return string.Empty;
				}
				return text;
			}
			set
			{
				this.ViewState["NavigateUrl"] = value;
			}
		}

		// Token: 0x1700146B RID: 5227
		// (get) Token: 0x0600507A RID: 20602 RVA: 0x00143B6C File Offset: 0x00142B6C
		// (set) Token: 0x0600507B RID: 20603 RVA: 0x00143B99 File Offset: 0x00142B99
		[WebCategory("Databindings")]
		[WebSysDescription("TreeNodeBinding_NavigateUrlField")]
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[DefaultValue("")]
		public string NavigateUrlField
		{
			get
			{
				string text = (string)this.ViewState["NavigateUrlField"];
				if (text == null)
				{
					return string.Empty;
				}
				return text;
			}
			set
			{
				this.ViewState["NavigateUrlField"] = value;
			}
		}

		// Token: 0x1700146C RID: 5228
		// (get) Token: 0x0600507C RID: 20604 RVA: 0x00143BAC File Offset: 0x00142BAC
		// (set) Token: 0x0600507D RID: 20605 RVA: 0x00143BD5 File Offset: 0x00142BD5
		[WebCategory("DefaultProperties")]
		[WebSysDescription("TreeNodeBinding_PopulateOnDemand")]
		[DefaultValue(false)]
		public bool PopulateOnDemand
		{
			get
			{
				object obj = this.ViewState["PopulateOnDemand"];
				return obj != null && (bool)obj;
			}
			set
			{
				this.ViewState["PopulateOnDemand"] = value;
			}
		}

		// Token: 0x1700146D RID: 5229
		// (get) Token: 0x0600507E RID: 20606 RVA: 0x00143BF0 File Offset: 0x00142BF0
		// (set) Token: 0x0600507F RID: 20607 RVA: 0x00143C19 File Offset: 0x00142C19
		[WebSysDescription("TreeNodeBinding_SelectAction")]
		[DefaultValue(TreeNodeSelectAction.Select)]
		[WebCategory("DefaultProperties")]
		public TreeNodeSelectAction SelectAction
		{
			get
			{
				object obj = this.ViewState["SelectAction"];
				if (obj == null)
				{
					return TreeNodeSelectAction.Select;
				}
				return (TreeNodeSelectAction)obj;
			}
			set
			{
				this.ViewState["SelectAction"] = value;
			}
		}

		// Token: 0x1700146E RID: 5230
		// (get) Token: 0x06005080 RID: 20608 RVA: 0x00143C34 File Offset: 0x00142C34
		// (set) Token: 0x06005081 RID: 20609 RVA: 0x00143C65 File Offset: 0x00142C65
		[DefaultValue(typeof(bool?), "")]
		[WebSysDescription("TreeNodeBinding_ShowCheckBox")]
		[WebCategory("DefaultProperties")]
		public bool? ShowCheckBox
		{
			get
			{
				object obj = this.ViewState["ShowCheckBox"];
				if (obj == null)
				{
					return null;
				}
				return (bool?)obj;
			}
			set
			{
				this.ViewState["ShowCheckBox"] = value;
			}
		}

		// Token: 0x1700146F RID: 5231
		// (get) Token: 0x06005082 RID: 20610 RVA: 0x00143C80 File Offset: 0x00142C80
		// (set) Token: 0x06005083 RID: 20611 RVA: 0x00143CAD File Offset: 0x00142CAD
		[WebCategory("DefaultProperties")]
		[DefaultValue("")]
		[WebSysDescription("TreeNodeBinding_Target")]
		public string Target
		{
			get
			{
				string text = (string)this.ViewState["Target"];
				if (text == null)
				{
					return string.Empty;
				}
				return text;
			}
			set
			{
				this.ViewState["Target"] = value;
			}
		}

		// Token: 0x17001470 RID: 5232
		// (get) Token: 0x06005084 RID: 20612 RVA: 0x00143CC0 File Offset: 0x00142CC0
		// (set) Token: 0x06005085 RID: 20613 RVA: 0x00143CED File Offset: 0x00142CED
		[WebSysDescription("TreeNodeBinding_TargetField")]
		[DefaultValue("")]
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[WebCategory("Databindings")]
		public string TargetField
		{
			get
			{
				string text = (string)this.ViewState["TargetField"];
				if (text == null)
				{
					return string.Empty;
				}
				return text;
			}
			set
			{
				this.ViewState["TargetField"] = value;
			}
		}

		// Token: 0x17001471 RID: 5233
		// (get) Token: 0x06005086 RID: 20614 RVA: 0x00143D00 File Offset: 0x00142D00
		// (set) Token: 0x06005087 RID: 20615 RVA: 0x00143D46 File Offset: 0x00142D46
		[WebCategory("DefaultProperties")]
		[WebSysDescription("TreeNodeBinding_Text")]
		[Localizable(true)]
		[DefaultValue("")]
		public string Text
		{
			get
			{
				string text = (string)this.ViewState["Text"];
				if (text == null)
				{
					text = (string)this.ViewState["Value"];
					if (text == null)
					{
						return string.Empty;
					}
				}
				return text;
			}
			set
			{
				this.ViewState["Text"] = value;
			}
		}

		// Token: 0x17001472 RID: 5234
		// (get) Token: 0x06005088 RID: 20616 RVA: 0x00143D5C File Offset: 0x00142D5C
		// (set) Token: 0x06005089 RID: 20617 RVA: 0x00143D89 File Offset: 0x00142D89
		[WebCategory("Databindings")]
		[WebSysDescription("TreeNodeBinding_TextField")]
		[DefaultValue("")]
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public string TextField
		{
			get
			{
				string text = (string)this.ViewState["TextField"];
				if (text == null)
				{
					return string.Empty;
				}
				return text;
			}
			set
			{
				this.ViewState["TextField"] = value;
			}
		}

		// Token: 0x17001473 RID: 5235
		// (get) Token: 0x0600508A RID: 20618 RVA: 0x00143D9C File Offset: 0x00142D9C
		// (set) Token: 0x0600508B RID: 20619 RVA: 0x00143DC9 File Offset: 0x00142DC9
		[DefaultValue("")]
		[WebCategory("DefaultProperties")]
		[WebSysDescription("TreeNodeBinding_ToolTip")]
		[Localizable(true)]
		public string ToolTip
		{
			get
			{
				string text = (string)this.ViewState["ToolTip"];
				if (text == null)
				{
					return string.Empty;
				}
				return text;
			}
			set
			{
				this.ViewState["ToolTip"] = value;
			}
		}

		// Token: 0x17001474 RID: 5236
		// (get) Token: 0x0600508C RID: 20620 RVA: 0x00143DDC File Offset: 0x00142DDC
		// (set) Token: 0x0600508D RID: 20621 RVA: 0x00143E09 File Offset: 0x00142E09
		[DefaultValue("")]
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[WebSysDescription("TreeNodeBinding_ToolTipField")]
		[WebCategory("Databindings")]
		public string ToolTipField
		{
			get
			{
				string text = (string)this.ViewState["ToolTipField"];
				if (text == null)
				{
					return string.Empty;
				}
				return text;
			}
			set
			{
				this.ViewState["ToolTipField"] = value;
			}
		}

		// Token: 0x17001475 RID: 5237
		// (get) Token: 0x0600508E RID: 20622 RVA: 0x00143E1C File Offset: 0x00142E1C
		// (set) Token: 0x0600508F RID: 20623 RVA: 0x00143E62 File Offset: 0x00142E62
		[Localizable(true)]
		[WebSysDescription("TreeNodeBinding_Value")]
		[WebCategory("DefaultProperties")]
		[DefaultValue("")]
		public string Value
		{
			get
			{
				string text = (string)this.ViewState["Value"];
				if (text == null)
				{
					text = (string)this.ViewState["Text"];
					if (text == null)
					{
						return string.Empty;
					}
				}
				return text;
			}
			set
			{
				this.ViewState["Value"] = value;
			}
		}

		// Token: 0x17001476 RID: 5238
		// (get) Token: 0x06005090 RID: 20624 RVA: 0x00143E78 File Offset: 0x00142E78
		// (set) Token: 0x06005091 RID: 20625 RVA: 0x00143EA5 File Offset: 0x00142EA5
		[WebSysDescription("TreeNodeBinding_ValueField")]
		[DefaultValue("")]
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[WebCategory("Databindings")]
		public string ValueField
		{
			get
			{
				string text = (string)this.ViewState["ValueField"];
				if (text == null)
				{
					return string.Empty;
				}
				return text;
			}
			set
			{
				this.ViewState["ValueField"] = value;
			}
		}

		// Token: 0x17001477 RID: 5239
		// (get) Token: 0x06005092 RID: 20626 RVA: 0x00143EB8 File Offset: 0x00142EB8
		private StateBag ViewState
		{
			get
			{
				if (this._viewState == null)
				{
					this._viewState = new StateBag();
					if (this._isTrackingViewState)
					{
						((IStateManager)this._viewState).TrackViewState();
					}
				}
				return this._viewState;
			}
		}

		// Token: 0x06005093 RID: 20627 RVA: 0x00143EE6 File Offset: 0x00142EE6
		internal void SetDirty()
		{
			this.ViewState.SetDirty(true);
		}

		// Token: 0x06005094 RID: 20628 RVA: 0x00143EF4 File Offset: 0x00142EF4
		public override string ToString()
		{
			if (!string.IsNullOrEmpty(this.DataMember))
			{
				return this.DataMember;
			}
			return SR.GetString("TreeNodeBinding_EmptyBindingText");
		}

		// Token: 0x06005095 RID: 20629 RVA: 0x00143F14 File Offset: 0x00142F14
		object ICloneable.Clone()
		{
			return new TreeNodeBinding
			{
				DataMember = this.DataMember,
				Depth = this.Depth,
				FormatString = this.FormatString,
				ImageToolTip = this.ImageToolTip,
				ImageToolTipField = this.ImageToolTipField,
				ImageUrl = this.ImageUrl,
				ImageUrlField = this.ImageUrlField,
				NavigateUrl = this.NavigateUrl,
				NavigateUrlField = this.NavigateUrlField,
				PopulateOnDemand = this.PopulateOnDemand,
				SelectAction = this.SelectAction,
				ShowCheckBox = this.ShowCheckBox,
				Target = this.Target,
				TargetField = this.TargetField,
				Text = this.Text,
				TextField = this.TextField,
				ToolTip = this.ToolTip,
				ToolTipField = this.ToolTipField,
				Value = this.Value,
				ValueField = this.ValueField
			};
		}

		// Token: 0x17001478 RID: 5240
		// (get) Token: 0x06005096 RID: 20630 RVA: 0x00144018 File Offset: 0x00143018
		bool IStateManager.IsTrackingViewState
		{
			get
			{
				return this._isTrackingViewState;
			}
		}

		// Token: 0x06005097 RID: 20631 RVA: 0x00144020 File Offset: 0x00143020
		void IStateManager.LoadViewState(object state)
		{
			if (state != null)
			{
				((IStateManager)this.ViewState).LoadViewState(state);
			}
		}

		// Token: 0x06005098 RID: 20632 RVA: 0x00144031 File Offset: 0x00143031
		object IStateManager.SaveViewState()
		{
			if (this._viewState != null)
			{
				return ((IStateManager)this._viewState).SaveViewState();
			}
			return null;
		}

		// Token: 0x06005099 RID: 20633 RVA: 0x00144048 File Offset: 0x00143048
		void IStateManager.TrackViewState()
		{
			this._isTrackingViewState = true;
			if (this._viewState != null)
			{
				((IStateManager)this._viewState).TrackViewState();
			}
		}

		// Token: 0x17001479 RID: 5241
		// (get) Token: 0x0600509A RID: 20634 RVA: 0x00144064 File Offset: 0x00143064
		// (set) Token: 0x0600509B RID: 20635 RVA: 0x00144076 File Offset: 0x00143076
		object IDataSourceViewSchemaAccessor.DataSourceViewSchema
		{
			get
			{
				return this.ViewState["IDataSourceViewSchemaAccessor.DataSourceViewSchema"];
			}
			set
			{
				this.ViewState["IDataSourceViewSchemaAccessor.DataSourceViewSchema"] = value;
			}
		}

		// Token: 0x04002D22 RID: 11554
		private bool _isTrackingViewState;

		// Token: 0x04002D23 RID: 11555
		private StateBag _viewState;
	}
}
