using System;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x02001B41 RID: 6977
	public class RadMenuItemBinding : NavigationItemBinding
	{
		// Token: 0x17005235 RID: 21045
		// (get) Token: 0x06010DD1 RID: 69073 RVA: 0x003BDC76 File Offset: 0x003BBE76
		// (set) Token: 0x06010DD2 RID: 69074 RVA: 0x003BDC96 File Offset: 0x003BBE96
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

		// Token: 0x17005236 RID: 21046
		// (get) Token: 0x06010DD3 RID: 69075 RVA: 0x003BDCA9 File Offset: 0x003BBEA9
		// (set) Token: 0x06010DD4 RID: 69076 RVA: 0x003BDCC9 File Offset: 0x003BBEC9
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

		// Token: 0x17005237 RID: 21047
		// (get) Token: 0x06010DD5 RID: 69077 RVA: 0x003BDCDC File Offset: 0x003BBEDC
		// (set) Token: 0x06010DD6 RID: 69078 RVA: 0x003BDCFC File Offset: 0x003BBEFC
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

		// Token: 0x17005238 RID: 21048
		// (get) Token: 0x06010DD7 RID: 69079 RVA: 0x003BDD0F File Offset: 0x003BBF0F
		// (set) Token: 0x06010DD8 RID: 69080 RVA: 0x003BDD2F File Offset: 0x003BBF2F
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

		// Token: 0x17005239 RID: 21049
		// (get) Token: 0x06010DD9 RID: 69081 RVA: 0x003BDD42 File Offset: 0x003BBF42
		// (set) Token: 0x06010DDA RID: 69082 RVA: 0x003BDD62 File Offset: 0x003BBF62
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

		// Token: 0x1700523A RID: 21050
		// (get) Token: 0x06010DDB RID: 69083 RVA: 0x003BDD75 File Offset: 0x003BBF75
		// (set) Token: 0x06010DDC RID: 69084 RVA: 0x003BDD95 File Offset: 0x003BBF95
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[DefaultValue("")]
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

		// Token: 0x1700523B RID: 21051
		// (get) Token: 0x06010DDD RID: 69085 RVA: 0x003BDDA8 File Offset: 0x003BBFA8
		// (set) Token: 0x06010DDE RID: 69086 RVA: 0x003BDDC8 File Offset: 0x003BBFC8
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

		// Token: 0x1700523C RID: 21052
		// (get) Token: 0x06010DDF RID: 69087 RVA: 0x003BDDDB File Offset: 0x003BBFDB
		// (set) Token: 0x06010DE0 RID: 69088 RVA: 0x003BDDFB File Offset: 0x003BBFFB
		[DefaultValue("")]
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
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

		// Token: 0x1700523D RID: 21053
		// (get) Token: 0x06010DE1 RID: 69089 RVA: 0x003BDE0E File Offset: 0x003BC00E
		// (set) Token: 0x06010DE2 RID: 69090 RVA: 0x003BDE2E File Offset: 0x003BC02E
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

		// Token: 0x1700523E RID: 21054
		// (get) Token: 0x06010DE3 RID: 69091 RVA: 0x003BDE41 File Offset: 0x003BC041
		// (set) Token: 0x06010DE4 RID: 69092 RVA: 0x003BDE61 File Offset: 0x003BC061
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

		// Token: 0x1700523F RID: 21055
		// (get) Token: 0x06010DE5 RID: 69093 RVA: 0x003BDE74 File Offset: 0x003BC074
		// (set) Token: 0x06010DE6 RID: 69094 RVA: 0x003BDE95 File Offset: 0x003BC095
		[DefaultValue(MenuItemExpandMode.ClientSide)]
		public MenuItemExpandMode ExpandMode
		{
			get
			{
				return (MenuItemExpandMode)(base.ViewState["MenuItemExpandMode"] ?? MenuItemExpandMode.ClientSide);
			}
			set
			{
				base.ViewState["MenuItemExpandMode"] = value;
			}
		}

		// Token: 0x17005240 RID: 21056
		// (get) Token: 0x06010DE7 RID: 69095 RVA: 0x003BDEAD File Offset: 0x003BC0AD
		// (set) Token: 0x06010DE8 RID: 69096 RVA: 0x003BDECD File Offset: 0x003BC0CD
		[DefaultValue("")]
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public string ExpandModeField
		{
			get
			{
				return (string)(base.ViewState["ExpandModeField"] ?? string.Empty);
			}
			set
			{
				base.ViewState["ExpandModeField"] = value;
			}
		}

		// Token: 0x17005241 RID: 21057
		// (get) Token: 0x06010DE9 RID: 69097 RVA: 0x003BDEE0 File Offset: 0x003BC0E0
		// (set) Token: 0x06010DEA RID: 69098 RVA: 0x003BDF00 File Offset: 0x003BC100
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

		// Token: 0x17005242 RID: 21058
		// (get) Token: 0x06010DEB RID: 69099 RVA: 0x003BDF13 File Offset: 0x003BC113
		// (set) Token: 0x06010DEC RID: 69100 RVA: 0x003BDF33 File Offset: 0x003BC133
		[DefaultValue("")]
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
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

		// Token: 0x17005243 RID: 21059
		// (get) Token: 0x06010DED RID: 69101 RVA: 0x003BDF46 File Offset: 0x003BC146
		// (set) Token: 0x06010DEE RID: 69102 RVA: 0x003BDF67 File Offset: 0x003BC167
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

		// Token: 0x17005244 RID: 21060
		// (get) Token: 0x06010DEF RID: 69103 RVA: 0x003BDF7F File Offset: 0x003BC17F
		// (set) Token: 0x06010DF0 RID: 69104 RVA: 0x003BDF9F File Offset: 0x003BC19F
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

		// Token: 0x06010DF1 RID: 69105 RVA: 0x003BDFB4 File Offset: 0x003BC1B4
		internal override void ApplyTo(NavigationItem navigationItem, object dataItem, PropertyDescriptorCache propertyDescriptorCache)
		{
			base.ApplyStringProperty(propertyDescriptorCache, navigationItem, dataItem, "ClickedCssClass");
			base.ApplyStringProperty(propertyDescriptorCache, navigationItem, dataItem, "DisabledCssClass");
			base.ApplyStringProperty(propertyDescriptorCache, navigationItem, dataItem, "DisabledImageUrl");
			base.ApplyStringProperty(propertyDescriptorCache, navigationItem, dataItem, "ExpandedImageUrl");
			base.ApplyStringProperty(propertyDescriptorCache, navigationItem, dataItem, "ExpandedCssClass");
			base.ApplyStringProperty(propertyDescriptorCache, navigationItem, dataItem, "FocusedCssClass");
			base.ApplyBoolProperty(propertyDescriptorCache, navigationItem, dataItem, "IsSeparator");
			bool flag = false;
			RadMenuItem radMenuItem = navigationItem as RadMenuItem;
			if (!string.IsNullOrEmpty(this.ExpandModeField))
			{
				radMenuItem.ExpandMode = (MenuItemExpandMode)Enum.Parse(typeof(MenuItemExpandMode), propertyDescriptorCache.GetPropertyValue(dataItem, this.ExpandModeField).ToString());
				flag = true;
			}
			if (!flag)
			{
				radMenuItem.ExpandMode = this.ExpandMode;
			}
			base.ApplyTo(navigationItem, dataItem, propertyDescriptorCache);
		}
	}
}
