using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	// Token: 0x020005E4 RID: 1508
	[DefaultProperty("TextField")]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class MenuItemBinding : IStateManager, ICloneable, IDataSourceViewSchemaAccessor
	{
		// Token: 0x170012A2 RID: 4770
		// (get) Token: 0x06004A85 RID: 19077 RVA: 0x00130EC4 File Offset: 0x0012FEC4
		// (set) Token: 0x06004A86 RID: 19078 RVA: 0x00130EF1 File Offset: 0x0012FEF1
		[WebCategory("Data")]
		[WebSysDescription("Binding_DataMember")]
		[DefaultValue("")]
		public string DataMember
		{
			get
			{
				object obj = this.ViewState["DataMember"];
				if (obj == null)
				{
					return string.Empty;
				}
				return (string)obj;
			}
			set
			{
				this.ViewState["DataMember"] = value;
			}
		}

		// Token: 0x170012A3 RID: 4771
		// (get) Token: 0x06004A87 RID: 19079 RVA: 0x00130F04 File Offset: 0x0012FF04
		// (set) Token: 0x06004A88 RID: 19080 RVA: 0x00130F2D File Offset: 0x0012FF2D
		[WebCategory("Data")]
		[WebSysDescription("MenuItemBinding_Depth")]
		[TypeConverter("System.Web.UI.Design.WebControls.TreeNodeBindingDepthConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[DefaultValue(-1)]
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

		// Token: 0x170012A4 RID: 4772
		// (get) Token: 0x06004A89 RID: 19081 RVA: 0x00130F48 File Offset: 0x0012FF48
		// (set) Token: 0x06004A8A RID: 19082 RVA: 0x00130F71 File Offset: 0x0012FF71
		[WebSysDescription("MenuItemBinding_Enabled")]
		[WebCategory("DefaultProperties")]
		[DefaultValue(true)]
		public bool Enabled
		{
			get
			{
				object obj = this.ViewState["Enabled"];
				return obj == null || (bool)obj;
			}
			set
			{
				this.ViewState["Enabled"] = value;
			}
		}

		// Token: 0x170012A5 RID: 4773
		// (get) Token: 0x06004A8B RID: 19083 RVA: 0x00130F8C File Offset: 0x0012FF8C
		// (set) Token: 0x06004A8C RID: 19084 RVA: 0x00130FB9 File Offset: 0x0012FFB9
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[DefaultValue("")]
		[WebCategory("Databindings")]
		[WebSysDescription("MenuItemBinding_EnabledField")]
		public string EnabledField
		{
			get
			{
				object obj = this.ViewState["EnabledField"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["EnabledField"] = value;
			}
		}

		// Token: 0x170012A6 RID: 4774
		// (get) Token: 0x06004A8D RID: 19085 RVA: 0x00130FCC File Offset: 0x0012FFCC
		// (set) Token: 0x06004A8E RID: 19086 RVA: 0x00130FF9 File Offset: 0x0012FFF9
		[WebSysDescription("MenuItemBinding_FormatString")]
		[DefaultValue("")]
		[Localizable(true)]
		[WebCategory("Databindings")]
		public string FormatString
		{
			get
			{
				object obj = this.ViewState["FormatString"];
				if (obj == null)
				{
					return string.Empty;
				}
				return (string)obj;
			}
			set
			{
				this.ViewState["FormatString"] = value;
			}
		}

		// Token: 0x170012A7 RID: 4775
		// (get) Token: 0x06004A8F RID: 19087 RVA: 0x0013100C File Offset: 0x0013000C
		// (set) Token: 0x06004A90 RID: 19088 RVA: 0x00131039 File Offset: 0x00130039
		[WebSysDescription("MenuItemBinding_ImageUrl")]
		[UrlProperty]
		[DefaultValue("")]
		[WebCategory("DefaultProperties")]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		public string ImageUrl
		{
			get
			{
				object obj = this.ViewState["ImageUrl"];
				if (obj == null)
				{
					return string.Empty;
				}
				return (string)obj;
			}
			set
			{
				this.ViewState["ImageUrl"] = value;
			}
		}

		// Token: 0x170012A8 RID: 4776
		// (get) Token: 0x06004A91 RID: 19089 RVA: 0x0013104C File Offset: 0x0013004C
		// (set) Token: 0x06004A92 RID: 19090 RVA: 0x00131079 File Offset: 0x00130079
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[WebSysDescription("MenuItemBinding_ImageUrlField")]
		[DefaultValue("")]
		[WebCategory("Databindings")]
		public string ImageUrlField
		{
			get
			{
				object obj = this.ViewState["ImageUrlField"];
				if (obj == null)
				{
					return string.Empty;
				}
				return (string)obj;
			}
			set
			{
				this.ViewState["ImageUrlField"] = value;
			}
		}

		// Token: 0x170012A9 RID: 4777
		// (get) Token: 0x06004A93 RID: 19091 RVA: 0x0013108C File Offset: 0x0013008C
		// (set) Token: 0x06004A94 RID: 19092 RVA: 0x001310B9 File Offset: 0x001300B9
		[DefaultValue("")]
		[UrlProperty]
		[WebSysDescription("MenuItemBinding_NavigateUrl")]
		[WebCategory("DefaultProperties")]
		[Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		public string NavigateUrl
		{
			get
			{
				object obj = this.ViewState["NavigateUrl"];
				if (obj == null)
				{
					return string.Empty;
				}
				return (string)obj;
			}
			set
			{
				this.ViewState["NavigateUrl"] = value;
			}
		}

		// Token: 0x170012AA RID: 4778
		// (get) Token: 0x06004A95 RID: 19093 RVA: 0x001310CC File Offset: 0x001300CC
		// (set) Token: 0x06004A96 RID: 19094 RVA: 0x001310F9 File Offset: 0x001300F9
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[WebSysDescription("MenuItemBinding_NavigateUrlField")]
		[DefaultValue("")]
		[WebCategory("Databindings")]
		public string NavigateUrlField
		{
			get
			{
				object obj = this.ViewState["NavigateUrlField"];
				if (obj == null)
				{
					return string.Empty;
				}
				return (string)obj;
			}
			set
			{
				this.ViewState["NavigateUrlField"] = value;
			}
		}

		// Token: 0x170012AB RID: 4779
		// (get) Token: 0x06004A97 RID: 19095 RVA: 0x0013110C File Offset: 0x0013010C
		// (set) Token: 0x06004A98 RID: 19096 RVA: 0x00131139 File Offset: 0x00130139
		[WebCategory("DefaultProperties")]
		[WebSysDescription("MenuItemBinding_PopOutImageUrl")]
		[Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[UrlProperty]
		[DefaultValue("")]
		public string PopOutImageUrl
		{
			get
			{
				object obj = this.ViewState["PopOutImageUrl"];
				if (obj == null)
				{
					return string.Empty;
				}
				return (string)obj;
			}
			set
			{
				this.ViewState["PopOutImageUrl"] = value;
			}
		}

		// Token: 0x170012AC RID: 4780
		// (get) Token: 0x06004A99 RID: 19097 RVA: 0x0013114C File Offset: 0x0013014C
		// (set) Token: 0x06004A9A RID: 19098 RVA: 0x00131179 File Offset: 0x00130179
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[WebSysDescription("MenuItemBinding_PopOutImageUrlField")]
		[DefaultValue("")]
		[WebCategory("Databindings")]
		public string PopOutImageUrlField
		{
			get
			{
				object obj = this.ViewState["PopOutImageUrlField"];
				if (obj == null)
				{
					return string.Empty;
				}
				return (string)obj;
			}
			set
			{
				this.ViewState["PopOutImageUrlField"] = value;
			}
		}

		// Token: 0x170012AD RID: 4781
		// (get) Token: 0x06004A9B RID: 19099 RVA: 0x0013118C File Offset: 0x0013018C
		// (set) Token: 0x06004A9C RID: 19100 RVA: 0x001311B5 File Offset: 0x001301B5
		[DefaultValue(true)]
		[WebSysDescription("MenuItemBinding_Selectable")]
		[WebCategory("DefaultProperties")]
		public bool Selectable
		{
			get
			{
				object obj = this.ViewState["Selectable"];
				return obj == null || (bool)obj;
			}
			set
			{
				this.ViewState["Selectable"] = value;
			}
		}

		// Token: 0x170012AE RID: 4782
		// (get) Token: 0x06004A9D RID: 19101 RVA: 0x001311D0 File Offset: 0x001301D0
		// (set) Token: 0x06004A9E RID: 19102 RVA: 0x001311FD File Offset: 0x001301FD
		[DefaultValue("")]
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[WebCategory("Databindings")]
		[WebSysDescription("MenuItemBinding_SelectableField")]
		public string SelectableField
		{
			get
			{
				object obj = this.ViewState["SelectableField"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["SelectableField"] = value;
			}
		}

		// Token: 0x170012AF RID: 4783
		// (get) Token: 0x06004A9F RID: 19103 RVA: 0x00131210 File Offset: 0x00130210
		// (set) Token: 0x06004AA0 RID: 19104 RVA: 0x0013123D File Offset: 0x0013023D
		[UrlProperty]
		[WebSysDescription("MenuItemBinding_SeparatorImageUrl")]
		[WebCategory("DefaultProperties")]
		[DefaultValue("")]
		[Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		public string SeparatorImageUrl
		{
			get
			{
				object obj = this.ViewState["SeparatorImageUrl"];
				if (obj == null)
				{
					return string.Empty;
				}
				return (string)obj;
			}
			set
			{
				this.ViewState["SeparatorImageUrl"] = value;
			}
		}

		// Token: 0x170012B0 RID: 4784
		// (get) Token: 0x06004AA1 RID: 19105 RVA: 0x00131250 File Offset: 0x00130250
		// (set) Token: 0x06004AA2 RID: 19106 RVA: 0x0013127D File Offset: 0x0013027D
		[WebCategory("Databindings")]
		[DefaultValue("")]
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[WebSysDescription("MenuItemBinding_SeparatorImageUrlField")]
		public string SeparatorImageUrlField
		{
			get
			{
				object obj = this.ViewState["SeparatorImageUrlField"];
				if (obj == null)
				{
					return string.Empty;
				}
				return (string)obj;
			}
			set
			{
				this.ViewState["SeparatorImageUrlField"] = value;
			}
		}

		// Token: 0x170012B1 RID: 4785
		// (get) Token: 0x06004AA3 RID: 19107 RVA: 0x00131290 File Offset: 0x00130290
		// (set) Token: 0x06004AA4 RID: 19108 RVA: 0x001312BD File Offset: 0x001302BD
		[DefaultValue("")]
		[WebSysDescription("MenuItemBinding_Target")]
		[WebCategory("DefaultProperties")]
		public string Target
		{
			get
			{
				object obj = this.ViewState["Target"];
				if (obj == null)
				{
					return string.Empty;
				}
				return (string)obj;
			}
			set
			{
				this.ViewState["Target"] = value;
			}
		}

		// Token: 0x170012B2 RID: 4786
		// (get) Token: 0x06004AA5 RID: 19109 RVA: 0x001312D0 File Offset: 0x001302D0
		// (set) Token: 0x06004AA6 RID: 19110 RVA: 0x001312FD File Offset: 0x001302FD
		[DefaultValue("")]
		[WebCategory("Databindings")]
		[WebSysDescription("MenuItemBinding_TargetField")]
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
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

		// Token: 0x170012B3 RID: 4787
		// (get) Token: 0x06004AA7 RID: 19111 RVA: 0x00131310 File Offset: 0x00130310
		// (set) Token: 0x06004AA8 RID: 19112 RVA: 0x00131351 File Offset: 0x00130351
		[WebCategory("DefaultProperties")]
		[WebSysDescription("MenuItemBinding_Text")]
		[DefaultValue("")]
		[Localizable(true)]
		public string Text
		{
			get
			{
				object obj = this.ViewState["Text"];
				if (obj == null)
				{
					obj = this.ViewState["Value"];
					if (obj == null)
					{
						return string.Empty;
					}
				}
				return (string)obj;
			}
			set
			{
				this.ViewState["Text"] = value;
			}
		}

		// Token: 0x170012B4 RID: 4788
		// (get) Token: 0x06004AA9 RID: 19113 RVA: 0x00131364 File Offset: 0x00130364
		// (set) Token: 0x06004AAA RID: 19114 RVA: 0x00131391 File Offset: 0x00130391
		[DefaultValue("")]
		[WebSysDescription("MenuItemBinding_TextField")]
		[WebCategory("Databindings")]
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public string TextField
		{
			get
			{
				object obj = this.ViewState["TextField"];
				if (obj == null)
				{
					return string.Empty;
				}
				return (string)obj;
			}
			set
			{
				this.ViewState["TextField"] = value;
			}
		}

		// Token: 0x170012B5 RID: 4789
		// (get) Token: 0x06004AAB RID: 19115 RVA: 0x001313A4 File Offset: 0x001303A4
		// (set) Token: 0x06004AAC RID: 19116 RVA: 0x001313D1 File Offset: 0x001303D1
		[WebSysDescription("MenuItemBinding_ToolTip")]
		[DefaultValue("")]
		[Localizable(true)]
		[WebCategory("DefaultProperties")]
		public string ToolTip
		{
			get
			{
				object obj = this.ViewState["ToolTip"];
				if (obj == null)
				{
					return string.Empty;
				}
				return (string)obj;
			}
			set
			{
				this.ViewState["ToolTip"] = value;
			}
		}

		// Token: 0x170012B6 RID: 4790
		// (get) Token: 0x06004AAD RID: 19117 RVA: 0x001313E4 File Offset: 0x001303E4
		// (set) Token: 0x06004AAE RID: 19118 RVA: 0x00131411 File Offset: 0x00130411
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[WebCategory("Databindings")]
		[WebSysDescription("MenuItemBinding_ToolTipField")]
		[DefaultValue("")]
		public string ToolTipField
		{
			get
			{
				object obj = this.ViewState["ToolTipField"];
				if (obj == null)
				{
					return string.Empty;
				}
				return (string)obj;
			}
			set
			{
				this.ViewState["ToolTipField"] = value;
			}
		}

		// Token: 0x170012B7 RID: 4791
		// (get) Token: 0x06004AAF RID: 19119 RVA: 0x00131424 File Offset: 0x00130424
		// (set) Token: 0x06004AB0 RID: 19120 RVA: 0x00131465 File Offset: 0x00130465
		[WebCategory("DefaultProperties")]
		[Localizable(true)]
		[DefaultValue("")]
		[WebSysDescription("MenuItemBinding_Value")]
		public string Value
		{
			get
			{
				object obj = this.ViewState["Value"];
				if (obj == null)
				{
					obj = this.ViewState["Text"];
					if (obj == null)
					{
						return string.Empty;
					}
				}
				return (string)obj;
			}
			set
			{
				this.ViewState["Value"] = value;
			}
		}

		// Token: 0x170012B8 RID: 4792
		// (get) Token: 0x06004AB1 RID: 19121 RVA: 0x00131478 File Offset: 0x00130478
		// (set) Token: 0x06004AB2 RID: 19122 RVA: 0x001314A5 File Offset: 0x001304A5
		[DefaultValue("")]
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[WebCategory("Databindings")]
		[WebSysDescription("MenuItemBinding_ValueField")]
		public string ValueField
		{
			get
			{
				object obj = this.ViewState["ValueField"];
				if (obj == null)
				{
					return string.Empty;
				}
				return (string)obj;
			}
			set
			{
				this.ViewState["ValueField"] = value;
			}
		}

		// Token: 0x170012B9 RID: 4793
		// (get) Token: 0x06004AB3 RID: 19123 RVA: 0x001314B8 File Offset: 0x001304B8
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

		// Token: 0x06004AB4 RID: 19124 RVA: 0x001314E6 File Offset: 0x001304E6
		internal void SetDirty()
		{
			this.ViewState.SetDirty(true);
		}

		// Token: 0x06004AB5 RID: 19125 RVA: 0x001314F4 File Offset: 0x001304F4
		public override string ToString()
		{
			if (!string.IsNullOrEmpty(this.DataMember))
			{
				return this.DataMember;
			}
			return SR.GetString("TreeNodeBinding_EmptyBindingText");
		}

		// Token: 0x06004AB6 RID: 19126 RVA: 0x00131514 File Offset: 0x00130514
		object ICloneable.Clone()
		{
			return new MenuItemBinding
			{
				DataMember = this.DataMember,
				Depth = this.Depth,
				Enabled = this.Enabled,
				EnabledField = this.EnabledField,
				FormatString = this.FormatString,
				ImageUrl = this.ImageUrl,
				ImageUrlField = this.ImageUrlField,
				NavigateUrl = this.NavigateUrl,
				NavigateUrlField = this.NavigateUrlField,
				PopOutImageUrl = this.PopOutImageUrl,
				PopOutImageUrlField = this.PopOutImageUrlField,
				Selectable = this.Selectable,
				SelectableField = this.SelectableField,
				SeparatorImageUrl = this.SeparatorImageUrl,
				SeparatorImageUrlField = this.SeparatorImageUrlField,
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

		// Token: 0x170012BA RID: 4794
		// (get) Token: 0x06004AB7 RID: 19127 RVA: 0x0013163C File Offset: 0x0013063C
		bool IStateManager.IsTrackingViewState
		{
			get
			{
				return this._isTrackingViewState;
			}
		}

		// Token: 0x06004AB8 RID: 19128 RVA: 0x00131644 File Offset: 0x00130644
		void IStateManager.LoadViewState(object state)
		{
			if (state != null)
			{
				((IStateManager)this.ViewState).LoadViewState(state);
			}
		}

		// Token: 0x06004AB9 RID: 19129 RVA: 0x00131655 File Offset: 0x00130655
		object IStateManager.SaveViewState()
		{
			if (this._viewState != null)
			{
				return ((IStateManager)this._viewState).SaveViewState();
			}
			return null;
		}

		// Token: 0x06004ABA RID: 19130 RVA: 0x0013166C File Offset: 0x0013066C
		void IStateManager.TrackViewState()
		{
			this._isTrackingViewState = true;
			if (this._viewState != null)
			{
				((IStateManager)this._viewState).TrackViewState();
			}
		}

		// Token: 0x170012BB RID: 4795
		// (get) Token: 0x06004ABB RID: 19131 RVA: 0x00131688 File Offset: 0x00130688
		// (set) Token: 0x06004ABC RID: 19132 RVA: 0x0013169A File Offset: 0x0013069A
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

		// Token: 0x04002B7E RID: 11134
		private bool _isTrackingViewState;

		// Token: 0x04002B7F RID: 11135
		private StateBag _viewState;
	}
}
