using System;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x02001AD9 RID: 6873
	public class RadTabBinding : NavigationItemBinding
	{
		// Token: 0x170050DB RID: 20699
		// (get) Token: 0x06010A1C RID: 68124 RVA: 0x003B5C21 File Offset: 0x003B3E21
		// (set) Token: 0x06010A1D RID: 68125 RVA: 0x003B5C41 File Offset: 0x003B3E41
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

		// Token: 0x170050DC RID: 20700
		// (get) Token: 0x06010A1E RID: 68126 RVA: 0x003B5C54 File Offset: 0x003B3E54
		// (set) Token: 0x06010A1F RID: 68127 RVA: 0x003B5C74 File Offset: 0x003B3E74
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[DefaultValue("")]
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

		// Token: 0x170050DD RID: 20701
		// (get) Token: 0x06010A20 RID: 68128 RVA: 0x003B5C87 File Offset: 0x003B3E87
		// (set) Token: 0x06010A21 RID: 68129 RVA: 0x003B5CA7 File Offset: 0x003B3EA7
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

		// Token: 0x170050DE RID: 20702
		// (get) Token: 0x06010A22 RID: 68130 RVA: 0x003B5CBA File Offset: 0x003B3EBA
		// (set) Token: 0x06010A23 RID: 68131 RVA: 0x003B5CDA File Offset: 0x003B3EDA
		[DefaultValue("")]
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
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

		// Token: 0x170050DF RID: 20703
		// (get) Token: 0x06010A24 RID: 68132 RVA: 0x003B5CED File Offset: 0x003B3EED
		// (set) Token: 0x06010A25 RID: 68133 RVA: 0x003B5D0D File Offset: 0x003B3F0D
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

		// Token: 0x170050E0 RID: 20704
		// (get) Token: 0x06010A26 RID: 68134 RVA: 0x003B5D20 File Offset: 0x003B3F20
		// (set) Token: 0x06010A27 RID: 68135 RVA: 0x003B5D40 File Offset: 0x003B3F40
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

		// Token: 0x170050E1 RID: 20705
		// (get) Token: 0x06010A28 RID: 68136 RVA: 0x003B5D53 File Offset: 0x003B3F53
		// (set) Token: 0x06010A29 RID: 68137 RVA: 0x003B5D73 File Offset: 0x003B3F73
		[DefaultValue("")]
		public string HoveredCssClass
		{
			get
			{
				return (string)(base.ViewState["HoveredCssClass"] ?? string.Empty);
			}
			set
			{
				base.ViewState["HoveredCssClass"] = value;
			}
		}

		// Token: 0x170050E2 RID: 20706
		// (get) Token: 0x06010A2A RID: 68138 RVA: 0x003B5D86 File Offset: 0x003B3F86
		// (set) Token: 0x06010A2B RID: 68139 RVA: 0x003B5DA6 File Offset: 0x003B3FA6
		[DefaultValue("")]
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public string HoveredCssClassField
		{
			get
			{
				return (string)(base.ViewState["HoveredCssClassField"] ?? string.Empty);
			}
			set
			{
				base.ViewState["HoveredCssClassField"] = value;
			}
		}

		// Token: 0x170050E3 RID: 20707
		// (get) Token: 0x06010A2C RID: 68140 RVA: 0x003B5DB9 File Offset: 0x003B3FB9
		// (set) Token: 0x06010A2D RID: 68141 RVA: 0x003B5DD9 File Offset: 0x003B3FD9
		[DefaultValue("")]
		public string OuterCssClass
		{
			get
			{
				return (string)(base.ViewState["OuterCssClass"] ?? string.Empty);
			}
			set
			{
				base.ViewState["OuterCssClass"] = value;
			}
		}

		// Token: 0x170050E4 RID: 20708
		// (get) Token: 0x06010A2E RID: 68142 RVA: 0x003B5DEC File Offset: 0x003B3FEC
		// (set) Token: 0x06010A2F RID: 68143 RVA: 0x003B5E0C File Offset: 0x003B400C
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[DefaultValue("")]
		public string OuterCssClassField
		{
			get
			{
				return (string)(base.ViewState["OuterCssClassField"] ?? string.Empty);
			}
			set
			{
				base.ViewState["OuterCssClassField"] = value;
			}
		}

		// Token: 0x170050E5 RID: 20709
		// (get) Token: 0x06010A30 RID: 68144 RVA: 0x003B5E1F File Offset: 0x003B401F
		// (set) Token: 0x06010A31 RID: 68145 RVA: 0x003B5E40 File Offset: 0x003B4040
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

		// Token: 0x170050E6 RID: 20710
		// (get) Token: 0x06010A32 RID: 68146 RVA: 0x003B5E58 File Offset: 0x003B4058
		// (set) Token: 0x06010A33 RID: 68147 RVA: 0x003B5E78 File Offset: 0x003B4078
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[DefaultValue("")]
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

		// Token: 0x170050E7 RID: 20711
		// (get) Token: 0x06010A34 RID: 68148 RVA: 0x003B5E8B File Offset: 0x003B408B
		// (set) Token: 0x06010A35 RID: 68149 RVA: 0x003B5EAC File Offset: 0x003B40AC
		[DefaultValue(false)]
		public bool IsBreak
		{
			get
			{
				return (bool)(base.ViewState["IsBreak"] ?? false);
			}
			set
			{
				base.ViewState["IsBreak"] = value;
			}
		}

		// Token: 0x170050E8 RID: 20712
		// (get) Token: 0x06010A36 RID: 68150 RVA: 0x003B5EC4 File Offset: 0x003B40C4
		// (set) Token: 0x06010A37 RID: 68151 RVA: 0x003B5EE4 File Offset: 0x003B40E4
		[DefaultValue("")]
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public string IsBreakField
		{
			get
			{
				return (string)(base.ViewState["IsBreakField"] ?? string.Empty);
			}
			set
			{
				base.ViewState["IsBreakField"] = value;
			}
		}

		// Token: 0x170050E9 RID: 20713
		// (get) Token: 0x06010A38 RID: 68152 RVA: 0x003B5EF7 File Offset: 0x003B40F7
		// (set) Token: 0x06010A39 RID: 68153 RVA: 0x003B5F18 File Offset: 0x003B4118
		[DefaultValue(false)]
		public bool PerTabScrolling
		{
			get
			{
				return (bool)(base.ViewState["PerTabScrolling"] ?? false);
			}
			set
			{
				base.ViewState["PerTabScrolling"] = value;
			}
		}

		// Token: 0x170050EA RID: 20714
		// (get) Token: 0x06010A3A RID: 68154 RVA: 0x003B5F30 File Offset: 0x003B4130
		// (set) Token: 0x06010A3B RID: 68155 RVA: 0x003B5F50 File Offset: 0x003B4150
		[DefaultValue("")]
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public string PerTabScrollingField
		{
			get
			{
				return (string)(base.ViewState["PerTabScrollingField"] ?? string.Empty);
			}
			set
			{
				base.ViewState["PerTabScrollingField"] = value;
			}
		}

		// Token: 0x170050EB RID: 20715
		// (get) Token: 0x06010A3C RID: 68156 RVA: 0x003B5F63 File Offset: 0x003B4163
		// (set) Token: 0x06010A3D RID: 68157 RVA: 0x003B5F84 File Offset: 0x003B4184
		[DefaultValue(false)]
		public bool ScrollChildren
		{
			get
			{
				return (bool)(base.ViewState["ScrollChildren"] ?? false);
			}
			set
			{
				base.ViewState["ScrollChildren"] = value;
			}
		}

		// Token: 0x170050EC RID: 20716
		// (get) Token: 0x06010A3E RID: 68158 RVA: 0x003B5F9C File Offset: 0x003B419C
		// (set) Token: 0x06010A3F RID: 68159 RVA: 0x003B5FBC File Offset: 0x003B41BC
		[DefaultValue("")]
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public string ScrollChildrenField
		{
			get
			{
				return (string)(base.ViewState["ScrollChildrenField"] ?? string.Empty);
			}
			set
			{
				base.ViewState["ScrollChildrenField"] = value;
			}
		}

		// Token: 0x170050ED RID: 20717
		// (get) Token: 0x06010A40 RID: 68160 RVA: 0x003B5FCF File Offset: 0x003B41CF
		// (set) Token: 0x06010A41 RID: 68161 RVA: 0x003B5FF0 File Offset: 0x003B41F0
		[DefaultValue(0)]
		public int ScrollPosition
		{
			get
			{
				return (int)(base.ViewState["ScrollPosition"] ?? 0);
			}
			set
			{
				base.ViewState["ScrollPosition"] = value;
			}
		}

		// Token: 0x170050EE RID: 20718
		// (get) Token: 0x06010A42 RID: 68162 RVA: 0x003B6008 File Offset: 0x003B4208
		// (set) Token: 0x06010A43 RID: 68163 RVA: 0x003B6028 File Offset: 0x003B4228
		[DefaultValue("")]
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public string ScrollPositionField
		{
			get
			{
				return (string)(base.ViewState["ScrollPositionField"] ?? string.Empty);
			}
			set
			{
				base.ViewState["ScrollPositionField"] = value;
			}
		}

		// Token: 0x170050EF RID: 20719
		// (get) Token: 0x06010A44 RID: 68164 RVA: 0x003B603B File Offset: 0x003B423B
		// (set) Token: 0x06010A45 RID: 68165 RVA: 0x003B605B File Offset: 0x003B425B
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

		// Token: 0x170050F0 RID: 20720
		// (get) Token: 0x06010A46 RID: 68166 RVA: 0x003B606E File Offset: 0x003B426E
		// (set) Token: 0x06010A47 RID: 68167 RVA: 0x003B608E File Offset: 0x003B428E
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[DefaultValue("")]
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

		// Token: 0x170050F1 RID: 20721
		// (get) Token: 0x06010A48 RID: 68168 RVA: 0x003B60A1 File Offset: 0x003B42A1
		// (set) Token: 0x06010A49 RID: 68169 RVA: 0x003B60C1 File Offset: 0x003B42C1
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

		// Token: 0x170050F2 RID: 20722
		// (get) Token: 0x06010A4A RID: 68170 RVA: 0x003B60D4 File Offset: 0x003B42D4
		// (set) Token: 0x06010A4B RID: 68171 RVA: 0x003B60F4 File Offset: 0x003B42F4
		[DefaultValue("")]
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
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

		// Token: 0x170050F3 RID: 20723
		// (get) Token: 0x06010A4C RID: 68172 RVA: 0x003B6107 File Offset: 0x003B4307
		// (set) Token: 0x06010A4D RID: 68173 RVA: 0x003B6127 File Offset: 0x003B4327
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[DefaultValue(0)]
		public string SelectedIndexField
		{
			get
			{
				return (string)(base.ViewState["SelectedIndexField"] ?? string.Empty);
			}
			set
			{
				base.ViewState["SelectedIndexField"] = value;
			}
		}

		// Token: 0x170050F4 RID: 20724
		// (get) Token: 0x06010A4E RID: 68174 RVA: 0x003B613A File Offset: 0x003B433A
		// (set) Token: 0x06010A4F RID: 68175 RVA: 0x003B615B File Offset: 0x003B435B
		[DefaultValue(-1)]
		public int SelectedIndex
		{
			get
			{
				return (int)(base.ViewState["SelectedIndex"] ?? -1);
			}
			set
			{
				base.ViewState["SelectedIndex"] = value;
			}
		}

		// Token: 0x170050F5 RID: 20725
		// (get) Token: 0x06010A50 RID: 68176 RVA: 0x003B6173 File Offset: 0x003B4373
		// (set) Token: 0x06010A51 RID: 68177 RVA: 0x003B6193 File Offset: 0x003B4393
		[DefaultValue("")]
		public string PageViewID
		{
			get
			{
				return (string)(base.ViewState["PageViewID"] ?? string.Empty);
			}
			set
			{
				base.ViewState["PageViewID"] = value;
			}
		}

		// Token: 0x170050F6 RID: 20726
		// (get) Token: 0x06010A52 RID: 68178 RVA: 0x003B61A6 File Offset: 0x003B43A6
		// (set) Token: 0x06010A53 RID: 68179 RVA: 0x003B61C6 File Offset: 0x003B43C6
		[DefaultValue("")]
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public string PageViewIDField
		{
			get
			{
				return (string)(base.ViewState["PageViewIDField"] ?? string.Empty);
			}
			set
			{
				base.ViewState["PageViewIDField"] = value;
			}
		}

		// Token: 0x170050F7 RID: 20727
		// (get) Token: 0x06010A54 RID: 68180 RVA: 0x003B61D9 File Offset: 0x003B43D9
		// (set) Token: 0x06010A55 RID: 68181 RVA: 0x003B61FA File Offset: 0x003B43FA
		[DefaultValue(TabStripScrollButtonsPosition.Right)]
		public TabStripScrollButtonsPosition ScrollButtonsPosition
		{
			get
			{
				return (TabStripScrollButtonsPosition)(base.ViewState["ScrollButtonsPosition"] ?? TabStripScrollButtonsPosition.Right);
			}
			set
			{
				base.ViewState["ScrollButtonsPosition"] = value;
			}
		}

		// Token: 0x170050F8 RID: 20728
		// (get) Token: 0x06010A56 RID: 68182 RVA: 0x003B6212 File Offset: 0x003B4412
		// (set) Token: 0x06010A57 RID: 68183 RVA: 0x003B6232 File Offset: 0x003B4432
		[DefaultValue("")]
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public string ScrollButtonsPositionField
		{
			get
			{
				return (string)(base.ViewState["ScrollButtonsPositionField"] ?? string.Empty);
			}
			set
			{
				base.ViewState["ScrollButtonsPositionField"] = value;
			}
		}

		// Token: 0x06010A58 RID: 68184 RVA: 0x003B6248 File Offset: 0x003B4448
		internal override void ApplyTo(NavigationItem navigationItem, object dataItem, PropertyDescriptorCache dataItemProperties)
		{
			base.ApplyStringProperty(dataItemProperties, navigationItem, dataItem, "ChildGroupCssClass");
			base.ApplyStringProperty(dataItemProperties, navigationItem, dataItem, "DisabledCssClass");
			base.ApplyStringProperty(dataItemProperties, navigationItem, dataItem, "DisabledImageUrl");
			base.ApplyStringProperty(dataItemProperties, navigationItem, dataItem, "HoveredCssClass");
			base.ApplyStringProperty(dataItemProperties, navigationItem, dataItem, "OuterCssClass");
			base.ApplyBoolProperty(dataItemProperties, navigationItem, dataItem, "IsSeparator");
			base.ApplyBoolProperty(dataItemProperties, navigationItem, dataItem, "IsBreak");
			base.ApplyBoolProperty(dataItemProperties, navigationItem, dataItem, "PerTabScrolling");
			base.ApplyBoolProperty(dataItemProperties, navigationItem, dataItem, "ScrollChildren");
			base.ApplyStringProperty(dataItemProperties, navigationItem, dataItem, "SelectedCssClass");
			base.ApplyStringProperty(dataItemProperties, navigationItem, dataItem, "SelectedImageUrl");
			base.ApplyStringProperty(dataItemProperties, navigationItem, dataItem, "PageViewID");
			this.ApplyIntProperty(dataItemProperties, navigationItem, dataItem, "SelectedIndex", -1);
			this.ApplyIntProperty(dataItemProperties, navigationItem, dataItem, "ScrollPosition", 0);
			bool flag = false;
			RadTab radTab = navigationItem as RadTab;
			if (!string.IsNullOrEmpty(this.ScrollButtonsPositionField))
			{
				radTab.ScrollButtonsPosition = (TabStripScrollButtonsPosition)Enum.Parse(typeof(TabStripScrollButtonsPosition), dataItemProperties.GetPropertyValue(dataItem, this.ScrollButtonsPositionField).ToString());
				flag = true;
			}
			if (!flag)
			{
				radTab.ScrollButtonsPosition = this.ScrollButtonsPosition;
			}
			base.ApplyTo(navigationItem, dataItem, dataItemProperties);
		}

		// Token: 0x06010A59 RID: 68185 RVA: 0x003B6378 File Offset: 0x003B4578
		internal void ApplyIntProperty(PropertyDescriptorCache cache, NavigationItem item, object dataItem, string propertyName, int defaultValue)
		{
			int num = (int)cache.GetPropertyValue(this, propertyName);
			PropertyDescriptor propertyDescriptor = cache.GetPropertyDescriptor(item, propertyName);
			if (num != defaultValue)
			{
				propertyDescriptor.SetValue(item, num);
				return;
			}
			string text = cache.GetPropertyValue(this, propertyName + "Field").ToString();
			if (string.IsNullOrEmpty(text))
			{
				return;
			}
			PropertyDescriptor propertyDescriptor2 = cache.GetPropertyDescriptor(dataItem, text);
			object value = propertyDescriptor2.GetValue(dataItem);
			if (value != null)
			{
				propertyDescriptor.SetValue(item, (int)value);
			}
		}
	}
}
