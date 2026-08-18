using System;
using System.ComponentModel;
using System.Drawing.Design;

namespace System.Web.UI.WebControls
{
	// Token: 0x020004FB RID: 1275
	[DefaultProperty("TextField")]
	public sealed class TreeNodeBinding : IStateManager, ICloneable, IDataSourceViewSchemaAccessor
	{
		// Token: 0x1700129F RID: 4767
		// (get) Token: 0x06003FBA RID: 16314 RVA: 0x000CE110 File Offset: 0x000CC310
		// (set) Token: 0x06003FBB RID: 16315 RVA: 0x000CE13D File Offset: 0x000CC33D
		[DefaultValue("")]
		[WebCategory("Data")]
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

		// Token: 0x170012A0 RID: 4768
		// (get) Token: 0x06003FBC RID: 16316 RVA: 0x000CE150 File Offset: 0x000CC350
		// (set) Token: 0x06003FBD RID: 16317 RVA: 0x000CE179 File Offset: 0x000CC379
		[DefaultValue(-1)]
		[TypeConverter("System.Web.UI.Design.WebControls.TreeNodeBindingDepthConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[WebCategory("Data")]
		[WebSysDescription("TreeNodeBinding_Depth")]
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

		// Token: 0x170012A1 RID: 4769
		// (get) Token: 0x06003FBE RID: 16318 RVA: 0x000CE194 File Offset: 0x000CC394
		// (set) Token: 0x06003FBF RID: 16319 RVA: 0x000CE1C1 File Offset: 0x000CC3C1
		[DefaultValue("")]
		[Localizable(true)]
		[WebCategory("Databindings")]
		[WebSysDescription("TreeNodeBinding_FormatString")]
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

		// Token: 0x170012A2 RID: 4770
		// (get) Token: 0x06003FC0 RID: 16320 RVA: 0x000CE1D4 File Offset: 0x000CC3D4
		// (set) Token: 0x06003FC1 RID: 16321 RVA: 0x000CE201 File Offset: 0x000CC401
		[DefaultValue("")]
		[Localizable(true)]
		[WebCategory("DefaultProperties")]
		[WebSysDescription("TreeNodeBinding_ImageToolTip")]
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

		// Token: 0x170012A3 RID: 4771
		// (get) Token: 0x06003FC2 RID: 16322 RVA: 0x000CE214 File Offset: 0x000CC414
		// (set) Token: 0x06003FC3 RID: 16323 RVA: 0x000CE241 File Offset: 0x000CC441
		[DefaultValue("")]
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[WebSysDescription("TreeNodeBinding_ImageToolTipField")]
		[WebCategory("Databindings")]
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

		// Token: 0x170012A4 RID: 4772
		// (get) Token: 0x06003FC4 RID: 16324 RVA: 0x000CE254 File Offset: 0x000CC454
		// (set) Token: 0x06003FC5 RID: 16325 RVA: 0x000CE281 File Offset: 0x000CC481
		[DefaultValue("")]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[UrlProperty]
		[WebCategory("DefaultProperties")]
		[WebSysDescription("TreeNodeBinding_ImageUrl")]
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

		// Token: 0x170012A5 RID: 4773
		// (get) Token: 0x06003FC6 RID: 16326 RVA: 0x000CE294 File Offset: 0x000CC494
		// (set) Token: 0x06003FC7 RID: 16327 RVA: 0x000CE2C1 File Offset: 0x000CC4C1
		[DefaultValue("")]
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[WebCategory("Databindings")]
		[WebSysDescription("TreeNodeBinding_ImageUrlField")]
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

		// Token: 0x170012A6 RID: 4774
		// (get) Token: 0x06003FC8 RID: 16328 RVA: 0x000CE2D4 File Offset: 0x000CC4D4
		// (set) Token: 0x06003FC9 RID: 16329 RVA: 0x000CE301 File Offset: 0x000CC501
		[DefaultValue("")]
		[Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[UrlProperty]
		[WebCategory("DefaultProperties")]
		[WebSysDescription("TreeNodeBinding_NavigateUrl")]
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

		// Token: 0x170012A7 RID: 4775
		// (get) Token: 0x06003FCA RID: 16330 RVA: 0x000CE314 File Offset: 0x000CC514
		// (set) Token: 0x06003FCB RID: 16331 RVA: 0x000CE341 File Offset: 0x000CC541
		[DefaultValue("")]
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[WebCategory("Databindings")]
		[WebSysDescription("TreeNodeBinding_NavigateUrlField")]
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

		// Token: 0x170012A8 RID: 4776
		// (get) Token: 0x06003FCC RID: 16332 RVA: 0x000CE354 File Offset: 0x000CC554
		// (set) Token: 0x06003FCD RID: 16333 RVA: 0x000CE37D File Offset: 0x000CC57D
		[DefaultValue(false)]
		[WebCategory("DefaultProperties")]
		[WebSysDescription("TreeNodeBinding_PopulateOnDemand")]
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

		// Token: 0x170012A9 RID: 4777
		// (get) Token: 0x06003FCE RID: 16334 RVA: 0x000CE398 File Offset: 0x000CC598
		// (set) Token: 0x06003FCF RID: 16335 RVA: 0x000CE3C1 File Offset: 0x000CC5C1
		[DefaultValue(TreeNodeSelectAction.Select)]
		[WebCategory("DefaultProperties")]
		[WebSysDescription("TreeNodeBinding_SelectAction")]
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

		// Token: 0x170012AA RID: 4778
		// (get) Token: 0x06003FD0 RID: 16336 RVA: 0x000CE3DC File Offset: 0x000CC5DC
		// (set) Token: 0x06003FD1 RID: 16337 RVA: 0x000CE40D File Offset: 0x000CC60D
		[DefaultValue(typeof(bool?), "")]
		[WebCategory("DefaultProperties")]
		[WebSysDescription("TreeNodeBinding_ShowCheckBox")]
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

		// Token: 0x170012AB RID: 4779
		// (get) Token: 0x06003FD2 RID: 16338 RVA: 0x000CE428 File Offset: 0x000CC628
		// (set) Token: 0x06003FD3 RID: 16339 RVA: 0x000CE455 File Offset: 0x000CC655
		[DefaultValue("")]
		[WebCategory("DefaultProperties")]
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

		// Token: 0x170012AC RID: 4780
		// (get) Token: 0x06003FD4 RID: 16340 RVA: 0x000CE468 File Offset: 0x000CC668
		// (set) Token: 0x06003FD5 RID: 16341 RVA: 0x000CE495 File Offset: 0x000CC695
		[DefaultValue("")]
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[WebCategory("Databindings")]
		[WebSysDescription("TreeNodeBinding_TargetField")]
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

		// Token: 0x170012AD RID: 4781
		// (get) Token: 0x06003FD6 RID: 16342 RVA: 0x000CE4A8 File Offset: 0x000CC6A8
		// (set) Token: 0x06003FD7 RID: 16343 RVA: 0x000CE4EE File Offset: 0x000CC6EE
		[DefaultValue("")]
		[Localizable(true)]
		[WebCategory("DefaultProperties")]
		[WebSysDescription("TreeNodeBinding_Text")]
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

		// Token: 0x170012AE RID: 4782
		// (get) Token: 0x06003FD8 RID: 16344 RVA: 0x000CE504 File Offset: 0x000CC704
		// (set) Token: 0x06003FD9 RID: 16345 RVA: 0x000CE531 File Offset: 0x000CC731
		[DefaultValue("")]
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[WebCategory("Databindings")]
		[WebSysDescription("TreeNodeBinding_TextField")]
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

		// Token: 0x170012AF RID: 4783
		// (get) Token: 0x06003FDA RID: 16346 RVA: 0x000CE544 File Offset: 0x000CC744
		// (set) Token: 0x06003FDB RID: 16347 RVA: 0x000CE571 File Offset: 0x000CC771
		[DefaultValue("")]
		[Localizable(true)]
		[WebCategory("DefaultProperties")]
		[WebSysDescription("TreeNodeBinding_ToolTip")]
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

		// Token: 0x170012B0 RID: 4784
		// (get) Token: 0x06003FDC RID: 16348 RVA: 0x000CE584 File Offset: 0x000CC784
		// (set) Token: 0x06003FDD RID: 16349 RVA: 0x000CE5B1 File Offset: 0x000CC7B1
		[DefaultValue("")]
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[WebCategory("Databindings")]
		[WebSysDescription("TreeNodeBinding_ToolTipField")]
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

		// Token: 0x170012B1 RID: 4785
		// (get) Token: 0x06003FDE RID: 16350 RVA: 0x000CE5C4 File Offset: 0x000CC7C4
		// (set) Token: 0x06003FDF RID: 16351 RVA: 0x000CE60A File Offset: 0x000CC80A
		[DefaultValue("")]
		[Localizable(true)]
		[WebCategory("DefaultProperties")]
		[WebSysDescription("TreeNodeBinding_Value")]
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

		// Token: 0x170012B2 RID: 4786
		// (get) Token: 0x06003FE0 RID: 16352 RVA: 0x000CE620 File Offset: 0x000CC820
		// (set) Token: 0x06003FE1 RID: 16353 RVA: 0x000CE64D File Offset: 0x000CC84D
		[DefaultValue("")]
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[WebCategory("Databindings")]
		[WebSysDescription("TreeNodeBinding_ValueField")]
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

		// Token: 0x170012B3 RID: 4787
		// (get) Token: 0x06003FE2 RID: 16354 RVA: 0x000CE660 File Offset: 0x000CC860
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

		// Token: 0x06003FE3 RID: 16355 RVA: 0x000CE68E File Offset: 0x000CC88E
		internal void SetDirty()
		{
			this.ViewState.SetDirty(true);
		}

		// Token: 0x06003FE4 RID: 16356 RVA: 0x000CE69C File Offset: 0x000CC89C
		public override string ToString()
		{
			if (!string.IsNullOrEmpty(this.DataMember))
			{
				return this.DataMember;
			}
			return SR.GetString("TreeNodeBinding_EmptyBindingText");
		}

		// Token: 0x06003FE5 RID: 16357 RVA: 0x000CE6BC File Offset: 0x000CC8BC
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

		// Token: 0x170012B4 RID: 4788
		// (get) Token: 0x06003FE6 RID: 16358 RVA: 0x000CE7C0 File Offset: 0x000CC9C0
		bool IStateManager.IsTrackingViewState
		{
			get
			{
				return this._isTrackingViewState;
			}
		}

		// Token: 0x06003FE7 RID: 16359 RVA: 0x000CE7C8 File Offset: 0x000CC9C8
		void IStateManager.LoadViewState(object state)
		{
			if (state != null)
			{
				((IStateManager)this.ViewState).LoadViewState(state);
			}
		}

		// Token: 0x06003FE8 RID: 16360 RVA: 0x000CE7D9 File Offset: 0x000CC9D9
		object IStateManager.SaveViewState()
		{
			if (this._viewState != null)
			{
				return ((IStateManager)this._viewState).SaveViewState();
			}
			return null;
		}

		// Token: 0x06003FE9 RID: 16361 RVA: 0x000CE7F0 File Offset: 0x000CC9F0
		void IStateManager.TrackViewState()
		{
			this._isTrackingViewState = true;
			if (this._viewState != null)
			{
				((IStateManager)this._viewState).TrackViewState();
			}
		}

		// Token: 0x170012B5 RID: 4789
		// (get) Token: 0x06003FEA RID: 16362 RVA: 0x000CE80C File Offset: 0x000CCA0C
		// (set) Token: 0x06003FEB RID: 16363 RVA: 0x000CE81E File Offset: 0x000CCA1E
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

		// Token: 0x04002462 RID: 9314
		private bool _isTrackingViewState;

		// Token: 0x04002463 RID: 9315
		private StateBag _viewState;
	}
}
