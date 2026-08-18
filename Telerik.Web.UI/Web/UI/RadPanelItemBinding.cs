using System;
using System.ComponentModel;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02001B4B RID: 6987
	public class RadPanelItemBinding : NavigationItemBinding
	{
		// Token: 0x17005286 RID: 21126
		// (get) Token: 0x06010E84 RID: 69252 RVA: 0x003BF377 File Offset: 0x003BD577
		// (set) Token: 0x06010E85 RID: 69253 RVA: 0x003BF397 File Offset: 0x003BD597
		[DefaultValue("")]
		public string ChildGroupCssClass
		{
			get
			{
				return (string)(base.ViewState["ChildGroupCssClass"] ?? string.Empty);
			}
			set
			{
				base.ViewState["ChildGroupCssClass"] = value;
			}
		}

		// Token: 0x17005287 RID: 21127
		// (get) Token: 0x06010E86 RID: 69254 RVA: 0x003BF3AA File Offset: 0x003BD5AA
		// (set) Token: 0x06010E87 RID: 69255 RVA: 0x003BF3CA File Offset: 0x003BD5CA
		[DefaultValue("")]
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public string ChildGroupCssClassField
		{
			get
			{
				return (string)(base.ViewState["ChildGroupCssClassField"] ?? string.Empty);
			}
			set
			{
				base.ViewState["ChildGroupCssClassField"] = value;
			}
		}

		// Token: 0x17005288 RID: 21128
		// (get) Token: 0x06010E88 RID: 69256 RVA: 0x003BF3DD File Offset: 0x003BD5DD
		// (set) Token: 0x06010E89 RID: 69257 RVA: 0x003BF40C File Offset: 0x003BD60C
		[DefaultValue(typeof(Unit), "")]
		public Unit ChildGroupHeight
		{
			get
			{
				if (base.ViewState["ChildGroupHeight"] == null)
				{
					return Unit.Empty;
				}
				return (Unit)base.ViewState["ChildGroupHeight"];
			}
			set
			{
				base.ViewState["ChildGroupHeight"] = value;
			}
		}

		// Token: 0x17005289 RID: 21129
		// (get) Token: 0x06010E8A RID: 69258 RVA: 0x003BF424 File Offset: 0x003BD624
		// (set) Token: 0x06010E8B RID: 69259 RVA: 0x003BF444 File Offset: 0x003BD644
		[DefaultValue("")]
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public string ChildGroupHeightField
		{
			get
			{
				return (string)(base.ViewState["ChildGroupHeightField"] ?? string.Empty);
			}
			set
			{
				base.ViewState["ChildGroupHeightField"] = value;
			}
		}

		// Token: 0x1700528A RID: 21130
		// (get) Token: 0x06010E8C RID: 69260 RVA: 0x003BF457 File Offset: 0x003BD657
		// (set) Token: 0x06010E8D RID: 69261 RVA: 0x003BF477 File Offset: 0x003BD677
		[DefaultValue("")]
		public string ClickedCssClass
		{
			get
			{
				return (string)(base.ViewState["ClickedCssClass"] ?? string.Empty);
			}
			set
			{
				base.ViewState["ClickedCssClass"] = value;
			}
		}

		// Token: 0x1700528B RID: 21131
		// (get) Token: 0x06010E8E RID: 69262 RVA: 0x003BF48A File Offset: 0x003BD68A
		// (set) Token: 0x06010E8F RID: 69263 RVA: 0x003BF4AA File Offset: 0x003BD6AA
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[DefaultValue("")]
		public string ClickedCssClassField
		{
			get
			{
				return (string)(base.ViewState["ClickedCssClassField"] ?? string.Empty);
			}
			set
			{
				base.ViewState["ClickedCssClassField"] = value;
			}
		}

		// Token: 0x1700528C RID: 21132
		// (get) Token: 0x06010E90 RID: 69264 RVA: 0x003BF4BD File Offset: 0x003BD6BD
		// (set) Token: 0x06010E91 RID: 69265 RVA: 0x003BF4DD File Offset: 0x003BD6DD
		[DefaultValue("")]
		public string DisabledCssClass
		{
			get
			{
				return (string)(base.ViewState["DisabledCssClass"] ?? string.Empty);
			}
			set
			{
				base.ViewState["DisabledCssClass"] = value;
			}
		}

		// Token: 0x1700528D RID: 21133
		// (get) Token: 0x06010E92 RID: 69266 RVA: 0x003BF4F0 File Offset: 0x003BD6F0
		// (set) Token: 0x06010E93 RID: 69267 RVA: 0x003BF510 File Offset: 0x003BD710
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[DefaultValue("")]
		public string DisabledCssClassField
		{
			get
			{
				return (string)(base.ViewState["DisabledCssClassField"] ?? string.Empty);
			}
			set
			{
				base.ViewState["DisabledCssClassField"] = value;
			}
		}

		// Token: 0x1700528E RID: 21134
		// (get) Token: 0x06010E94 RID: 69268 RVA: 0x003BF523 File Offset: 0x003BD723
		// (set) Token: 0x06010E95 RID: 69269 RVA: 0x003BF543 File Offset: 0x003BD743
		[DefaultValue("")]
		public string DisabledImageUrl
		{
			get
			{
				return (string)(base.ViewState["DisabledImageUrl"] ?? string.Empty);
			}
			set
			{
				base.ViewState["DisabledImageUrl"] = value;
			}
		}

		// Token: 0x1700528F RID: 21135
		// (get) Token: 0x06010E96 RID: 69270 RVA: 0x003BF556 File Offset: 0x003BD756
		// (set) Token: 0x06010E97 RID: 69271 RVA: 0x003BF576 File Offset: 0x003BD776
		[DefaultValue("")]
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public string DisabledImageUrlField
		{
			get
			{
				return (string)(base.ViewState["DisabledImageUrlField"] ?? string.Empty);
			}
			set
			{
				base.ViewState["DisabledImageUrlField"] = value;
			}
		}

		// Token: 0x17005290 RID: 21136
		// (get) Token: 0x06010E98 RID: 69272 RVA: 0x003BF589 File Offset: 0x003BD789
		// (set) Token: 0x06010E99 RID: 69273 RVA: 0x003BF5AA File Offset: 0x003BD7AA
		[DefaultValue(false)]
		public bool Expanded
		{
			get
			{
				return (bool)(base.ViewState["Expanded"] ?? false);
			}
			set
			{
				base.ViewState["Expanded"] = value;
			}
		}

		// Token: 0x17005291 RID: 21137
		// (get) Token: 0x06010E9A RID: 69274 RVA: 0x003BF5C2 File Offset: 0x003BD7C2
		// (set) Token: 0x06010E9B RID: 69275 RVA: 0x003BF5E2 File Offset: 0x003BD7E2
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[DefaultValue("")]
		public string ExpandedField
		{
			get
			{
				return (string)(base.ViewState["ExpandedField"] ?? string.Empty);
			}
			set
			{
				base.ViewState["ExpandedField"] = value;
			}
		}

		// Token: 0x17005292 RID: 21138
		// (get) Token: 0x06010E9C RID: 69276 RVA: 0x003BF5F5 File Offset: 0x003BD7F5
		// (set) Token: 0x06010E9D RID: 69277 RVA: 0x003BF615 File Offset: 0x003BD815
		[DefaultValue("")]
		public string ExpandedImageUrl
		{
			get
			{
				return (string)(base.ViewState["ExpandedImageUrl"] ?? string.Empty);
			}
			set
			{
				base.ViewState["ExpandedImageUrl"] = value;
			}
		}

		// Token: 0x17005293 RID: 21139
		// (get) Token: 0x06010E9E RID: 69278 RVA: 0x003BF628 File Offset: 0x003BD828
		// (set) Token: 0x06010E9F RID: 69279 RVA: 0x003BF648 File Offset: 0x003BD848
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[DefaultValue("")]
		public string ExpandedImageUrlField
		{
			get
			{
				return (string)(base.ViewState["ExpandedImageUrlField"] ?? string.Empty);
			}
			set
			{
				base.ViewState["ExpandedImageUrlField"] = value;
			}
		}

		// Token: 0x17005294 RID: 21140
		// (get) Token: 0x06010EA0 RID: 69280 RVA: 0x003BF65B File Offset: 0x003BD85B
		// (set) Token: 0x06010EA1 RID: 69281 RVA: 0x003BF67B File Offset: 0x003BD87B
		[DefaultValue("")]
		public string ExpandedCssClass
		{
			get
			{
				return (string)(base.ViewState["ExpandedCssClass"] ?? string.Empty);
			}
			set
			{
				base.ViewState["ExpandedCssClass"] = value;
			}
		}

		// Token: 0x17005295 RID: 21141
		// (get) Token: 0x06010EA2 RID: 69282 RVA: 0x003BF68E File Offset: 0x003BD88E
		// (set) Token: 0x06010EA3 RID: 69283 RVA: 0x003BF6AE File Offset: 0x003BD8AE
		[DefaultValue("")]
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public string ExpandedCssClassField
		{
			get
			{
				return (string)(base.ViewState["ExpandedCssClassField"] ?? string.Empty);
			}
			set
			{
				base.ViewState["ExpandedCssClassField"] = value;
			}
		}

		// Token: 0x17005296 RID: 21142
		// (get) Token: 0x06010EA4 RID: 69284 RVA: 0x003BF6C1 File Offset: 0x003BD8C1
		// (set) Token: 0x06010EA5 RID: 69285 RVA: 0x003BF6E1 File Offset: 0x003BD8E1
		[DefaultValue("")]
		public string FocusedCssClass
		{
			get
			{
				return (string)(base.ViewState["FocusedCssClass"] ?? string.Empty);
			}
			set
			{
				base.ViewState["FocusedCssClass"] = value;
			}
		}

		// Token: 0x17005297 RID: 21143
		// (get) Token: 0x06010EA6 RID: 69286 RVA: 0x003BF6F4 File Offset: 0x003BD8F4
		// (set) Token: 0x06010EA7 RID: 69287 RVA: 0x003BF714 File Offset: 0x003BD914
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[DefaultValue("")]
		public string FocusedCssClassField
		{
			get
			{
				return (string)(base.ViewState["FocusedCssClassField"] ?? string.Empty);
			}
			set
			{
				base.ViewState["FocusedCssClassField"] = value;
			}
		}

		// Token: 0x17005298 RID: 21144
		// (get) Token: 0x06010EA8 RID: 69288 RVA: 0x003BF727 File Offset: 0x003BD927
		// (set) Token: 0x06010EA9 RID: 69289 RVA: 0x003BF748 File Offset: 0x003BD948
		[DefaultValue(RadPanelItemImagePosition.Left)]
		public RadPanelItemImagePosition ImagePosition
		{
			get
			{
				return (RadPanelItemImagePosition)(base.ViewState["ImagePosition"] ?? RadPanelItemImagePosition.Left);
			}
			set
			{
				base.ViewState["ImagePosition"] = value;
			}
		}

		// Token: 0x17005299 RID: 21145
		// (get) Token: 0x06010EAA RID: 69290 RVA: 0x003BF760 File Offset: 0x003BD960
		// (set) Token: 0x06010EAB RID: 69291 RVA: 0x003BF780 File Offset: 0x003BD980
		[DefaultValue("")]
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public string ImagePositionField
		{
			get
			{
				return (string)(base.ViewState["ImagePositionField"] ?? string.Empty);
			}
			set
			{
				base.ViewState["ImagePositionField"] = value;
			}
		}

		// Token: 0x1700529A RID: 21146
		// (get) Token: 0x06010EAC RID: 69292 RVA: 0x003BF793 File Offset: 0x003BD993
		// (set) Token: 0x06010EAD RID: 69293 RVA: 0x003BF7B4 File Offset: 0x003BD9B4
		[DefaultValue(false)]
		public bool IsSeparator
		{
			get
			{
				return (bool)(base.ViewState["IsSeparator"] ?? false);
			}
			set
			{
				base.ViewState["IsSeparator"] = value;
			}
		}

		// Token: 0x1700529B RID: 21147
		// (get) Token: 0x06010EAE RID: 69294 RVA: 0x003BF7CC File Offset: 0x003BD9CC
		// (set) Token: 0x06010EAF RID: 69295 RVA: 0x003BF7EC File Offset: 0x003BD9EC
		[DefaultValue("")]
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public string IsSeparatorField
		{
			get
			{
				return (string)(base.ViewState["IsSeparatorField"] ?? string.Empty);
			}
			set
			{
				base.ViewState["IsSeparatorField"] = value;
			}
		}

		// Token: 0x1700529C RID: 21148
		// (get) Token: 0x06010EB0 RID: 69296 RVA: 0x003BF7FF File Offset: 0x003BD9FF
		// (set) Token: 0x06010EB1 RID: 69297 RVA: 0x003BF820 File Offset: 0x003BDA20
		[DefaultValue(false)]
		public bool PreventCollapse
		{
			get
			{
				return (bool)(base.ViewState["PreventCollapse"] ?? false);
			}
			set
			{
				base.ViewState["PreventCollapse"] = value;
			}
		}

		// Token: 0x1700529D RID: 21149
		// (get) Token: 0x06010EB2 RID: 69298 RVA: 0x003BF838 File Offset: 0x003BDA38
		// (set) Token: 0x06010EB3 RID: 69299 RVA: 0x003BF858 File Offset: 0x003BDA58
		[DefaultValue("")]
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public string PreventCollapseField
		{
			get
			{
				return (string)(base.ViewState["PreventCollapseField"] ?? string.Empty);
			}
			set
			{
				base.ViewState["PreventCollapseField"] = value;
			}
		}

		// Token: 0x1700529E RID: 21150
		// (get) Token: 0x06010EB4 RID: 69300 RVA: 0x003BF86B File Offset: 0x003BDA6B
		// (set) Token: 0x06010EB5 RID: 69301 RVA: 0x003BF88B File Offset: 0x003BDA8B
		[DefaultValue("")]
		public string SelectedCssClass
		{
			get
			{
				return (string)(base.ViewState["SelectedCssClass"] ?? string.Empty);
			}
			set
			{
				base.ViewState["SelectedCssClass"] = value;
			}
		}

		// Token: 0x1700529F RID: 21151
		// (get) Token: 0x06010EB6 RID: 69302 RVA: 0x003BF89E File Offset: 0x003BDA9E
		// (set) Token: 0x06010EB7 RID: 69303 RVA: 0x003BF8BE File Offset: 0x003BDABE
		[DefaultValue("")]
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public string SelectedCssClassField
		{
			get
			{
				return (string)(base.ViewState["SelectedCssClassField"] ?? string.Empty);
			}
			set
			{
				base.ViewState["SelectedCssClassField"] = value;
			}
		}

		// Token: 0x170052A0 RID: 21152
		// (get) Token: 0x06010EB8 RID: 69304 RVA: 0x003BF8D1 File Offset: 0x003BDAD1
		// (set) Token: 0x06010EB9 RID: 69305 RVA: 0x003BF8F1 File Offset: 0x003BDAF1
		[DefaultValue("")]
		public string SelectedImageUrl
		{
			get
			{
				return (string)(base.ViewState["SelectedImageUrl"] ?? string.Empty);
			}
			set
			{
				base.ViewState["SelectedImageUrl"] = value;
			}
		}

		// Token: 0x170052A1 RID: 21153
		// (get) Token: 0x06010EBA RID: 69306 RVA: 0x003BF904 File Offset: 0x003BDB04
		// (set) Token: 0x06010EBB RID: 69307 RVA: 0x003BF924 File Offset: 0x003BDB24
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[DefaultValue("")]
		public string SelectedImageUrlField
		{
			get
			{
				return (string)(base.ViewState["SelectedImageUrlField"] ?? string.Empty);
			}
			set
			{
				base.ViewState["SelectedImageUrlField"] = value;
			}
		}

		// Token: 0x06010EBC RID: 69308 RVA: 0x003BF938 File Offset: 0x003BDB38
		internal override void ApplyTo(NavigationItem navigationItem, object dataItem, PropertyDescriptorCache dataItemProperties)
		{
			base.ApplyStringProperty(dataItemProperties, navigationItem, dataItem, "ChildGroupCssClass");
			base.ApplyStringProperty(dataItemProperties, navigationItem, dataItem, "ClickedCssClass");
			base.ApplyStringProperty(dataItemProperties, navigationItem, dataItem, "DisabledCssClass");
			base.ApplyStringProperty(dataItemProperties, navigationItem, dataItem, "DisabledImageUrl");
			base.ApplyBoolProperty(dataItemProperties, navigationItem, dataItem, "Expanded");
			base.ApplyStringProperty(dataItemProperties, navigationItem, dataItem, "ExpandedImageUrl");
			base.ApplyStringProperty(dataItemProperties, navigationItem, dataItem, "ExpandedCssClass");
			base.ApplyStringProperty(dataItemProperties, navigationItem, dataItem, "FocusedCssClass");
			base.ApplyBoolProperty(dataItemProperties, navigationItem, dataItem, "IsSeparator");
			base.ApplyBoolProperty(dataItemProperties, navigationItem, dataItem, "PreventCollapse");
			base.ApplyStringProperty(dataItemProperties, navigationItem, dataItem, "SelectedCssClass");
			base.ApplyStringProperty(dataItemProperties, navigationItem, dataItem, "SelectedImageUrl");
			bool flag = false;
			RadPanelItem radPanelItem = navigationItem as RadPanelItem;
			if (!string.IsNullOrEmpty(this.ImagePositionField))
			{
				radPanelItem.ImagePosition = (RadPanelItemImagePosition)Enum.Parse(typeof(RadPanelItemImagePosition), dataItemProperties.GetPropertyValue(dataItem, this.ImagePositionField).ToString());
				flag = true;
			}
			if (!flag)
			{
				radPanelItem.ImagePosition = this.ImagePosition;
			}
			bool flag2 = false;
			if (!string.IsNullOrEmpty(this.ChildGroupHeightField))
			{
				radPanelItem.ChildGroupHeight = Unit.Parse(dataItemProperties.GetPropertyValue(dataItem, this.ChildGroupHeightField).ToString());
				flag2 = true;
			}
			if (!flag2)
			{
				radPanelItem.ChildGroupHeight = this.ChildGroupHeight;
			}
			base.ApplyTo(navigationItem, dataItem, dataItemProperties);
		}
	}
}
