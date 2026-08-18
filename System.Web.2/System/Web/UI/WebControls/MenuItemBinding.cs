using System;
using System.ComponentModel;
using System.Drawing.Design;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000470 RID: 1136
	[DefaultProperty("TextField")]
	public sealed class MenuItemBinding : IStateManager, ICloneable, IDataSourceViewSchemaAccessor
	{
		// Token: 0x17001061 RID: 4193
		// (get) Token: 0x060037E2 RID: 14306 RVA: 0x000B65A0 File Offset: 0x000B47A0
		// (set) Token: 0x060037E3 RID: 14307 RVA: 0x000B65CD File Offset: 0x000B47CD
		[DefaultValue("")]
		[WebCategory("Data")]
		[WebSysDescription("Binding_DataMember")]
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

		// Token: 0x17001062 RID: 4194
		// (get) Token: 0x060037E4 RID: 14308 RVA: 0x000B65E0 File Offset: 0x000B47E0
		// (set) Token: 0x060037E5 RID: 14309 RVA: 0x000B6609 File Offset: 0x000B4809
		[DefaultValue(-1)]
		[TypeConverter("System.Web.UI.Design.WebControls.TreeNodeBindingDepthConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[WebCategory("Data")]
		[WebSysDescription("MenuItemBinding_Depth")]
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

		// Token: 0x17001063 RID: 4195
		// (get) Token: 0x060037E6 RID: 14310 RVA: 0x000B6624 File Offset: 0x000B4824
		// (set) Token: 0x060037E7 RID: 14311 RVA: 0x000B664D File Offset: 0x000B484D
		[DefaultValue(true)]
		[WebCategory("DefaultProperties")]
		[WebSysDescription("MenuItemBinding_Enabled")]
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

		// Token: 0x17001064 RID: 4196
		// (get) Token: 0x060037E8 RID: 14312 RVA: 0x000B6668 File Offset: 0x000B4868
		// (set) Token: 0x060037E9 RID: 14313 RVA: 0x000B6695 File Offset: 0x000B4895
		[DefaultValue("")]
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
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

		// Token: 0x17001065 RID: 4197
		// (get) Token: 0x060037EA RID: 14314 RVA: 0x000B66A8 File Offset: 0x000B48A8
		// (set) Token: 0x060037EB RID: 14315 RVA: 0x000B66D5 File Offset: 0x000B48D5
		[DefaultValue("")]
		[Localizable(true)]
		[WebCategory("Databindings")]
		[WebSysDescription("MenuItemBinding_FormatString")]
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

		// Token: 0x17001066 RID: 4198
		// (get) Token: 0x060037EC RID: 14316 RVA: 0x000B66E8 File Offset: 0x000B48E8
		// (set) Token: 0x060037ED RID: 14317 RVA: 0x000B6715 File Offset: 0x000B4915
		[DefaultValue("")]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[UrlProperty]
		[WebCategory("DefaultProperties")]
		[WebSysDescription("MenuItemBinding_ImageUrl")]
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

		// Token: 0x17001067 RID: 4199
		// (get) Token: 0x060037EE RID: 14318 RVA: 0x000B6728 File Offset: 0x000B4928
		// (set) Token: 0x060037EF RID: 14319 RVA: 0x000B6755 File Offset: 0x000B4955
		[DefaultValue("")]
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[WebCategory("Databindings")]
		[WebSysDescription("MenuItemBinding_ImageUrlField")]
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

		// Token: 0x17001068 RID: 4200
		// (get) Token: 0x060037F0 RID: 14320 RVA: 0x000B6768 File Offset: 0x000B4968
		// (set) Token: 0x060037F1 RID: 14321 RVA: 0x000B6795 File Offset: 0x000B4995
		[DefaultValue("")]
		[Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[UrlProperty]
		[WebCategory("DefaultProperties")]
		[WebSysDescription("MenuItemBinding_NavigateUrl")]
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

		// Token: 0x17001069 RID: 4201
		// (get) Token: 0x060037F2 RID: 14322 RVA: 0x000B67A8 File Offset: 0x000B49A8
		// (set) Token: 0x060037F3 RID: 14323 RVA: 0x000B67D5 File Offset: 0x000B49D5
		[DefaultValue("")]
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[WebCategory("Databindings")]
		[WebSysDescription("MenuItemBinding_NavigateUrlField")]
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

		// Token: 0x1700106A RID: 4202
		// (get) Token: 0x060037F4 RID: 14324 RVA: 0x000B67E8 File Offset: 0x000B49E8
		// (set) Token: 0x060037F5 RID: 14325 RVA: 0x000B6815 File Offset: 0x000B4A15
		[DefaultValue("")]
		[Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[UrlProperty]
		[WebCategory("DefaultProperties")]
		[WebSysDescription("MenuItemBinding_PopOutImageUrl")]
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

		// Token: 0x1700106B RID: 4203
		// (get) Token: 0x060037F6 RID: 14326 RVA: 0x000B6828 File Offset: 0x000B4A28
		// (set) Token: 0x060037F7 RID: 14327 RVA: 0x000B6855 File Offset: 0x000B4A55
		[DefaultValue("")]
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[WebCategory("Databindings")]
		[WebSysDescription("MenuItemBinding_PopOutImageUrlField")]
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

		// Token: 0x1700106C RID: 4204
		// (get) Token: 0x060037F8 RID: 14328 RVA: 0x000B6868 File Offset: 0x000B4A68
		// (set) Token: 0x060037F9 RID: 14329 RVA: 0x000B6891 File Offset: 0x000B4A91
		[DefaultValue(true)]
		[WebCategory("DefaultProperties")]
		[WebSysDescription("MenuItemBinding_Selectable")]
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

		// Token: 0x1700106D RID: 4205
		// (get) Token: 0x060037FA RID: 14330 RVA: 0x000B68AC File Offset: 0x000B4AAC
		// (set) Token: 0x060037FB RID: 14331 RVA: 0x000B68D9 File Offset: 0x000B4AD9
		[DefaultValue("")]
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
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

		// Token: 0x1700106E RID: 4206
		// (get) Token: 0x060037FC RID: 14332 RVA: 0x000B68EC File Offset: 0x000B4AEC
		// (set) Token: 0x060037FD RID: 14333 RVA: 0x000B6919 File Offset: 0x000B4B19
		[DefaultValue("")]
		[Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[UrlProperty]
		[WebCategory("DefaultProperties")]
		[WebSysDescription("MenuItemBinding_SeparatorImageUrl")]
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

		// Token: 0x1700106F RID: 4207
		// (get) Token: 0x060037FE RID: 14334 RVA: 0x000B692C File Offset: 0x000B4B2C
		// (set) Token: 0x060037FF RID: 14335 RVA: 0x000B6959 File Offset: 0x000B4B59
		[DefaultValue("")]
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[WebCategory("Databindings")]
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

		// Token: 0x17001070 RID: 4208
		// (get) Token: 0x06003800 RID: 14336 RVA: 0x000B696C File Offset: 0x000B4B6C
		// (set) Token: 0x06003801 RID: 14337 RVA: 0x000B6999 File Offset: 0x000B4B99
		[DefaultValue("")]
		[WebCategory("DefaultProperties")]
		[WebSysDescription("MenuItemBinding_Target")]
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

		// Token: 0x17001071 RID: 4209
		// (get) Token: 0x06003802 RID: 14338 RVA: 0x000B69AC File Offset: 0x000B4BAC
		// (set) Token: 0x06003803 RID: 14339 RVA: 0x000B69D9 File Offset: 0x000B4BD9
		[DefaultValue("")]
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[WebCategory("Databindings")]
		[WebSysDescription("MenuItemBinding_TargetField")]
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

		// Token: 0x17001072 RID: 4210
		// (get) Token: 0x06003804 RID: 14340 RVA: 0x000B69EC File Offset: 0x000B4BEC
		// (set) Token: 0x06003805 RID: 14341 RVA: 0x000B6A2D File Offset: 0x000B4C2D
		[DefaultValue("")]
		[Localizable(true)]
		[WebCategory("DefaultProperties")]
		[WebSysDescription("MenuItemBinding_Text")]
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

		// Token: 0x17001073 RID: 4211
		// (get) Token: 0x06003806 RID: 14342 RVA: 0x000B6A40 File Offset: 0x000B4C40
		// (set) Token: 0x06003807 RID: 14343 RVA: 0x000B6A6D File Offset: 0x000B4C6D
		[DefaultValue("")]
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[WebCategory("Databindings")]
		[WebSysDescription("MenuItemBinding_TextField")]
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

		// Token: 0x17001074 RID: 4212
		// (get) Token: 0x06003808 RID: 14344 RVA: 0x000B6A80 File Offset: 0x000B4C80
		// (set) Token: 0x06003809 RID: 14345 RVA: 0x000B6AAD File Offset: 0x000B4CAD
		[DefaultValue("")]
		[Localizable(true)]
		[WebCategory("DefaultProperties")]
		[WebSysDescription("MenuItemBinding_ToolTip")]
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

		// Token: 0x17001075 RID: 4213
		// (get) Token: 0x0600380A RID: 14346 RVA: 0x000B6AC0 File Offset: 0x000B4CC0
		// (set) Token: 0x0600380B RID: 14347 RVA: 0x000B6AED File Offset: 0x000B4CED
		[DefaultValue("")]
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[WebCategory("Databindings")]
		[WebSysDescription("MenuItemBinding_ToolTipField")]
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

		// Token: 0x17001076 RID: 4214
		// (get) Token: 0x0600380C RID: 14348 RVA: 0x000B6B00 File Offset: 0x000B4D00
		// (set) Token: 0x0600380D RID: 14349 RVA: 0x000B6B41 File Offset: 0x000B4D41
		[DefaultValue("")]
		[Localizable(true)]
		[WebCategory("DefaultProperties")]
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

		// Token: 0x17001077 RID: 4215
		// (get) Token: 0x0600380E RID: 14350 RVA: 0x000B6B54 File Offset: 0x000B4D54
		// (set) Token: 0x0600380F RID: 14351 RVA: 0x000B6B81 File Offset: 0x000B4D81
		[DefaultValue("")]
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
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

		// Token: 0x17001078 RID: 4216
		// (get) Token: 0x06003810 RID: 14352 RVA: 0x000B6B94 File Offset: 0x000B4D94
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

		// Token: 0x06003811 RID: 14353 RVA: 0x000B6BC2 File Offset: 0x000B4DC2
		internal void SetDirty()
		{
			this.ViewState.SetDirty(true);
		}

		// Token: 0x06003812 RID: 14354 RVA: 0x000B6BD0 File Offset: 0x000B4DD0
		public override string ToString()
		{
			if (!string.IsNullOrEmpty(this.DataMember))
			{
				return this.DataMember;
			}
			return SR.GetString("TreeNodeBinding_EmptyBindingText");
		}

		// Token: 0x06003813 RID: 14355 RVA: 0x000B6BF0 File Offset: 0x000B4DF0
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

		// Token: 0x17001079 RID: 4217
		// (get) Token: 0x06003814 RID: 14356 RVA: 0x000B6D18 File Offset: 0x000B4F18
		bool IStateManager.IsTrackingViewState
		{
			get
			{
				return this._isTrackingViewState;
			}
		}

		// Token: 0x06003815 RID: 14357 RVA: 0x000B6D20 File Offset: 0x000B4F20
		void IStateManager.LoadViewState(object state)
		{
			if (state != null)
			{
				((IStateManager)this.ViewState).LoadViewState(state);
			}
		}

		// Token: 0x06003816 RID: 14358 RVA: 0x000B6D31 File Offset: 0x000B4F31
		object IStateManager.SaveViewState()
		{
			if (this._viewState != null)
			{
				return ((IStateManager)this._viewState).SaveViewState();
			}
			return null;
		}

		// Token: 0x06003817 RID: 14359 RVA: 0x000B6D48 File Offset: 0x000B4F48
		void IStateManager.TrackViewState()
		{
			this._isTrackingViewState = true;
			if (this._viewState != null)
			{
				((IStateManager)this._viewState).TrackViewState();
			}
		}

		// Token: 0x1700107A RID: 4218
		// (get) Token: 0x06003818 RID: 14360 RVA: 0x000B6D64 File Offset: 0x000B4F64
		// (set) Token: 0x06003819 RID: 14361 RVA: 0x000B6D76 File Offset: 0x000B4F76
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

		// Token: 0x04002271 RID: 8817
		private bool _isTrackingViewState;

		// Token: 0x04002272 RID: 8818
		private StateBag _viewState;
	}
}
