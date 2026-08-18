using System;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x02001ABC RID: 6844
	public class RadSiteMapNodeBinding : NavigationItemBinding
	{
		// Token: 0x17005080 RID: 20608
		// (get) Token: 0x060108ED RID: 67821 RVA: 0x003B1CE2 File Offset: 0x003AFEE2
		// (set) Token: 0x060108EE RID: 67822 RVA: 0x003B1D02 File Offset: 0x003AFF02
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

		// Token: 0x17005081 RID: 20609
		// (get) Token: 0x060108EF RID: 67823 RVA: 0x003B1D15 File Offset: 0x003AFF15
		// (set) Token: 0x060108F0 RID: 67824 RVA: 0x003B1D35 File Offset: 0x003AFF35
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

		// Token: 0x17005082 RID: 20610
		// (get) Token: 0x060108F1 RID: 67825 RVA: 0x003B1D48 File Offset: 0x003AFF48
		// (set) Token: 0x060108F2 RID: 67826 RVA: 0x003B1D68 File Offset: 0x003AFF68
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

		// Token: 0x17005083 RID: 20611
		// (get) Token: 0x060108F3 RID: 67827 RVA: 0x003B1D7B File Offset: 0x003AFF7B
		// (set) Token: 0x060108F4 RID: 67828 RVA: 0x003B1D9B File Offset: 0x003AFF9B
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

		// Token: 0x17005084 RID: 20612
		// (get) Token: 0x060108F5 RID: 67829 RVA: 0x003B1DAE File Offset: 0x003AFFAE
		// (set) Token: 0x060108F6 RID: 67830 RVA: 0x003B1DCE File Offset: 0x003AFFCE
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

		// Token: 0x17005085 RID: 20613
		// (get) Token: 0x060108F7 RID: 67831 RVA: 0x003B1DE1 File Offset: 0x003AFFE1
		// (set) Token: 0x060108F8 RID: 67832 RVA: 0x003B1E01 File Offset: 0x003B0001
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

		// Token: 0x060108F9 RID: 67833 RVA: 0x003B1E14 File Offset: 0x003B0014
		internal override void ApplyTo(NavigationItem navigationItem, object dataItem, PropertyDescriptorCache propertyDescriptorCache)
		{
			if (!string.IsNullOrEmpty(this.DisabledCssClass) || !string.IsNullOrEmpty(this.DisabledCssClassField))
			{
				base.ApplyStringProperty(propertyDescriptorCache, navigationItem, dataItem, "DisabledCssClass");
			}
			if (!string.IsNullOrEmpty(this.DisabledImageUrl) || !string.IsNullOrEmpty(this.DisabledImageUrlField))
			{
				base.ApplyStringProperty(propertyDescriptorCache, navigationItem, dataItem, "DisabledImageUrl");
			}
			base.ApplyTo(navigationItem, dataItem, propertyDescriptorCache);
		}
	}
}
