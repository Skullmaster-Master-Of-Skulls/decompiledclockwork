using System;
using System.ComponentModel;
using System.Globalization;
using System.Security.Permissions;
using System.Web;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02001ABB RID: 6843
	[DefaultProperty("TextField")]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public abstract class NavigationItemBinding : StateManager, IDataSourceViewSchemaAccessor
	{
		// Token: 0x17005065 RID: 20581
		// (get) Token: 0x060108B2 RID: 67762 RVA: 0x003B1494 File Offset: 0x003AF694
		// (set) Token: 0x060108B3 RID: 67763 RVA: 0x003B14B4 File Offset: 0x003AF6B4
		[DefaultValue("")]
		public string Target
		{
			get
			{
				return (string)(base.ViewState["Target"] ?? string.Empty);
			}
			set
			{
				base.ViewState["Target"] = value;
			}
		}

		// Token: 0x17005066 RID: 20582
		// (get) Token: 0x060108B4 RID: 67764 RVA: 0x003B14C7 File Offset: 0x003AF6C7
		// (set) Token: 0x060108B5 RID: 67765 RVA: 0x003B14E7 File Offset: 0x003AF6E7
		[DefaultValue("")]
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public string TargetField
		{
			get
			{
				return (string)(base.ViewState["TargetField"] ?? string.Empty);
			}
			set
			{
				base.ViewState["TargetField"] = value;
			}
		}

		// Token: 0x17005067 RID: 20583
		// (get) Token: 0x060108B6 RID: 67766 RVA: 0x003B14FA File Offset: 0x003AF6FA
		// (set) Token: 0x060108B7 RID: 67767 RVA: 0x003B151A File Offset: 0x003AF71A
		[DefaultValue("")]
		public string ToolTip
		{
			get
			{
				return (string)(base.ViewState["ToolTip"] ?? string.Empty);
			}
			set
			{
				base.ViewState["ToolTip"] = value;
			}
		}

		// Token: 0x17005068 RID: 20584
		// (get) Token: 0x060108B8 RID: 67768 RVA: 0x003B152D File Offset: 0x003AF72D
		// (set) Token: 0x060108B9 RID: 67769 RVA: 0x003B154D File Offset: 0x003AF74D
		[DefaultValue("")]
		public string FormatString
		{
			get
			{
				return (string)(base.ViewState["FormatString"] ?? string.Empty);
			}
			set
			{
				base.ViewState["FormatString"] = value;
			}
		}

		// Token: 0x17005069 RID: 20585
		// (get) Token: 0x060108BA RID: 67770 RVA: 0x003B1560 File Offset: 0x003AF760
		// (set) Token: 0x060108BB RID: 67771 RVA: 0x003B1580 File Offset: 0x003AF780
		[DefaultValue("")]
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public string ToolTipField
		{
			get
			{
				return (string)(base.ViewState["ToolTipField"] ?? string.Empty);
			}
			set
			{
				base.ViewState["ToolTipField"] = value;
			}
		}

		// Token: 0x1700506A RID: 20586
		// (get) Token: 0x060108BC RID: 67772 RVA: 0x003B1593 File Offset: 0x003AF793
		// (set) Token: 0x060108BD RID: 67773 RVA: 0x003B15B3 File Offset: 0x003AF7B3
		[DefaultValue("")]
		public string Text
		{
			get
			{
				return (string)(base.ViewState["Text"] ?? string.Empty);
			}
			set
			{
				base.ViewState["Text"] = value;
			}
		}

		// Token: 0x1700506B RID: 20587
		// (get) Token: 0x060108BE RID: 67774 RVA: 0x003B15C6 File Offset: 0x003AF7C6
		// (set) Token: 0x060108BF RID: 67775 RVA: 0x003B15E6 File Offset: 0x003AF7E6
		[DefaultValue("")]
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public string TextField
		{
			get
			{
				return (string)(base.ViewState["TextField"] ?? string.Empty);
			}
			set
			{
				base.ViewState["TextField"] = value;
			}
		}

		// Token: 0x1700506C RID: 20588
		// (get) Token: 0x060108C0 RID: 67776 RVA: 0x003B15F9 File Offset: 0x003AF7F9
		// (set) Token: 0x060108C1 RID: 67777 RVA: 0x003B1619 File Offset: 0x003AF819
		[DefaultValue("")]
		public string NavigateUrl
		{
			get
			{
				return (string)(base.ViewState["NavigateUrl"] ?? string.Empty);
			}
			set
			{
				base.ViewState["NavigateUrl"] = value;
			}
		}

		// Token: 0x1700506D RID: 20589
		// (get) Token: 0x060108C2 RID: 67778 RVA: 0x003B162C File Offset: 0x003AF82C
		// (set) Token: 0x060108C3 RID: 67779 RVA: 0x003B164C File Offset: 0x003AF84C
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[DefaultValue("")]
		public string NavigateUrlField
		{
			get
			{
				return (string)(base.ViewState["NavigateUrlField"] ?? string.Empty);
			}
			set
			{
				base.ViewState["NavigateUrlField"] = value;
			}
		}

		// Token: 0x1700506E RID: 20590
		// (get) Token: 0x060108C4 RID: 67780 RVA: 0x003B165F File Offset: 0x003AF85F
		// (set) Token: 0x060108C5 RID: 67781 RVA: 0x003B167F File Offset: 0x003AF87F
		[DefaultValue("")]
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public string ValueField
		{
			get
			{
				return (string)(base.ViewState["ValueField"] ?? string.Empty);
			}
			set
			{
				base.ViewState["ValueField"] = value;
			}
		}

		// Token: 0x1700506F RID: 20591
		// (get) Token: 0x060108C6 RID: 67782 RVA: 0x003B1692 File Offset: 0x003AF892
		// (set) Token: 0x060108C7 RID: 67783 RVA: 0x003B16B2 File Offset: 0x003AF8B2
		[DefaultValue("")]
		public string Value
		{
			get
			{
				return (string)(base.ViewState["Value"] ?? string.Empty);
			}
			set
			{
				base.ViewState["Value"] = value;
			}
		}

		// Token: 0x17005070 RID: 20592
		// (get) Token: 0x060108C8 RID: 67784 RVA: 0x003B16C5 File Offset: 0x003AF8C5
		// (set) Token: 0x060108C9 RID: 67785 RVA: 0x003B16E5 File Offset: 0x003AF8E5
		[DefaultValue("")]
		public string DataMember
		{
			get
			{
				return (string)(base.ViewState["DataMember"] ?? string.Empty);
			}
			set
			{
				base.ViewState["DataMember"] = value;
			}
		}

		// Token: 0x17005071 RID: 20593
		// (get) Token: 0x060108CA RID: 67786 RVA: 0x003B16F8 File Offset: 0x003AF8F8
		// (set) Token: 0x060108CB RID: 67787 RVA: 0x003B1718 File Offset: 0x003AF918
		[DefaultValue("")]
		public string ModelID
		{
			get
			{
				return (string)(base.ViewState["ModelID"] ?? "");
			}
			set
			{
				base.ViewState["ModelID"] = value;
			}
		}

		// Token: 0x17005072 RID: 20594
		// (get) Token: 0x060108CC RID: 67788 RVA: 0x003B172B File Offset: 0x003AF92B
		// (set) Token: 0x060108CD RID: 67789 RVA: 0x003B174B File Offset: 0x003AF94B
		[DefaultValue("")]
		public string FieldID
		{
			get
			{
				return (string)(base.ViewState["FieldID"] ?? "");
			}
			set
			{
				base.ViewState["FieldID"] = value;
			}
		}

		// Token: 0x17005073 RID: 20595
		// (get) Token: 0x060108CE RID: 67790 RVA: 0x003B175E File Offset: 0x003AF95E
		// (set) Token: 0x060108CF RID: 67791 RVA: 0x003B177E File Offset: 0x003AF97E
		[DefaultValue("")]
		public string FieldParentID
		{
			get
			{
				return (string)(base.ViewState["ParentKeyField"] ?? "");
			}
			set
			{
				base.ViewState["ParentKeyField"] = value;
			}
		}

		// Token: 0x17005074 RID: 20596
		// (get) Token: 0x060108D0 RID: 67792 RVA: 0x003B1791 File Offset: 0x003AF991
		// (set) Token: 0x060108D1 RID: 67793 RVA: 0x003B17B2 File Offset: 0x003AF9B2
		[DefaultValue(-1)]
		[TypeConverter("System.Web.UI.Design.WebControls.TreeNodeBindingDepthConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public int Depth
		{
			get
			{
				return (int)(base.ViewState["Depth"] ?? -1);
			}
			set
			{
				base.ViewState["Depth"] = value;
			}
		}

		// Token: 0x17005075 RID: 20597
		// (get) Token: 0x060108D2 RID: 67794 RVA: 0x003B17CA File Offset: 0x003AF9CA
		// (set) Token: 0x060108D3 RID: 67795 RVA: 0x003B17EB File Offset: 0x003AF9EB
		[DefaultValue(true)]
		public bool Enabled
		{
			get
			{
				return (bool)(base.ViewState["Enabled"] ?? true);
			}
			set
			{
				base.ViewState["Enabled"] = value;
			}
		}

		// Token: 0x17005076 RID: 20598
		// (get) Token: 0x060108D4 RID: 67796 RVA: 0x003B1803 File Offset: 0x003AFA03
		// (set) Token: 0x060108D5 RID: 67797 RVA: 0x003B1823 File Offset: 0x003AFA23
		[DefaultValue("")]
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public string EnabledField
		{
			get
			{
				return (string)(base.ViewState["EnabledField"] ?? string.Empty);
			}
			set
			{
				base.ViewState["EnabledField"] = value;
			}
		}

		// Token: 0x17005077 RID: 20599
		// (get) Token: 0x060108D6 RID: 67798 RVA: 0x003B1836 File Offset: 0x003AFA36
		// (set) Token: 0x060108D7 RID: 67799 RVA: 0x003B1856 File Offset: 0x003AFA56
		[DefaultValue("")]
		public string ImageUrl
		{
			get
			{
				return (string)(base.ViewState["ImageUrl"] ?? string.Empty);
			}
			set
			{
				base.ViewState["ImageUrl"] = value;
			}
		}

		// Token: 0x17005078 RID: 20600
		// (get) Token: 0x060108D8 RID: 67800 RVA: 0x003B1869 File Offset: 0x003AFA69
		// (set) Token: 0x060108D9 RID: 67801 RVA: 0x003B1889 File Offset: 0x003AFA89
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[DefaultValue("")]
		public string ImageUrlField
		{
			get
			{
				return (string)(base.ViewState["ImageUrlField"] ?? string.Empty);
			}
			set
			{
				base.ViewState["ImageUrlField"] = value;
			}
		}

		// Token: 0x17005079 RID: 20601
		// (get) Token: 0x060108DA RID: 67802 RVA: 0x003B189C File Offset: 0x003AFA9C
		// (set) Token: 0x060108DB RID: 67803 RVA: 0x003B18BC File Offset: 0x003AFABC
		[DefaultValue("")]
		public string HoveredImageUrl
		{
			get
			{
				return (string)(base.ViewState["HoveredImageUrl"] ?? string.Empty);
			}
			set
			{
				base.ViewState["HoveredImageUrl"] = value;
			}
		}

		// Token: 0x1700507A RID: 20602
		// (get) Token: 0x060108DC RID: 67804 RVA: 0x003B18CF File Offset: 0x003AFACF
		// (set) Token: 0x060108DD RID: 67805 RVA: 0x003B18EF File Offset: 0x003AFAEF
		[DefaultValue("")]
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public string HoveredImageUrlField
		{
			get
			{
				return (string)(base.ViewState["HoveredImageUrlField"] ?? string.Empty);
			}
			set
			{
				base.ViewState["HoveredImageUrlField"] = value;
			}
		}

		// Token: 0x1700507B RID: 20603
		// (get) Token: 0x060108DE RID: 67806 RVA: 0x003B1902 File Offset: 0x003AFB02
		// (set) Token: 0x060108DF RID: 67807 RVA: 0x003B1923 File Offset: 0x003AFB23
		[DefaultValue(true)]
		public bool PostBack
		{
			get
			{
				return (bool)(base.ViewState["PostBack"] ?? true);
			}
			set
			{
				base.ViewState["PostBack"] = value;
			}
		}

		// Token: 0x1700507C RID: 20604
		// (get) Token: 0x060108E0 RID: 67808 RVA: 0x003B193B File Offset: 0x003AFB3B
		// (set) Token: 0x060108E1 RID: 67809 RVA: 0x003B195B File Offset: 0x003AFB5B
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[DefaultValue("")]
		public string PostBackField
		{
			get
			{
				return (string)(base.ViewState["PostBackField"] ?? string.Empty);
			}
			set
			{
				base.ViewState["PostBackField"] = value;
			}
		}

		// Token: 0x1700507D RID: 20605
		// (get) Token: 0x060108E2 RID: 67810 RVA: 0x003B196E File Offset: 0x003AFB6E
		// (set) Token: 0x060108E3 RID: 67811 RVA: 0x003B198E File Offset: 0x003AFB8E
		[DefaultValue("")]
		public string CssClass
		{
			get
			{
				return (string)(base.ViewState["CssClass"] ?? string.Empty);
			}
			set
			{
				base.ViewState["CssClass"] = value;
			}
		}

		// Token: 0x1700507E RID: 20606
		// (get) Token: 0x060108E4 RID: 67812 RVA: 0x003B19A1 File Offset: 0x003AFBA1
		// (set) Token: 0x060108E5 RID: 67813 RVA: 0x003B19C1 File Offset: 0x003AFBC1
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[DefaultValue("")]
		public string CssClassField
		{
			get
			{
				return (string)(base.ViewState["CssClassField"] ?? string.Empty);
			}
			set
			{
				base.ViewState["CssClassField"] = value;
			}
		}

		// Token: 0x060108E6 RID: 67814 RVA: 0x003B19D4 File Offset: 0x003AFBD4
		internal void ApplyStringProperty(PropertyDescriptorCache propertyDescriptorCache, NavigationItem item, object dataItem, string propertyName)
		{
			PropertyDescriptor propertyDescriptor = propertyDescriptorCache.GetPropertyDescriptor(item, propertyName);
			string value = propertyDescriptorCache.GetPropertyValue(this, propertyName).ToString();
			if (!string.IsNullOrEmpty(value))
			{
				propertyDescriptor.SetValue(item, value);
				return;
			}
			string text = propertyDescriptorCache.GetPropertyValue(this, propertyName + "Field").ToString();
			if (string.IsNullOrEmpty(text))
			{
				return;
			}
			object propertyValue = propertyDescriptorCache.GetPropertyValue(dataItem, text);
			if (propertyValue != null)
			{
				propertyDescriptor.SetValue(item, propertyValue.ToString());
			}
		}

		// Token: 0x060108E7 RID: 67815 RVA: 0x003B1A48 File Offset: 0x003AFC48
		internal void ApplyBoolProperty(PropertyDescriptorCache propertyDescriptorCache, NavigationItem item, object dataItem, string propertyName)
		{
			bool flag = false;
			PropertyDescriptor propertyDescriptor = propertyDescriptorCache.GetPropertyDescriptor(item, propertyName);
			string text = propertyDescriptorCache.GetPropertyDescriptor(this, propertyName + "Field").GetValue(this).ToString();
			if (!string.IsNullOrEmpty(text))
			{
				propertyDescriptor.SetValue(item, this.GetBooleanProperty(text, dataItem, propertyDescriptorCache));
				flag = true;
			}
			if (!flag)
			{
				bool flag2 = Convert.ToBoolean(propertyDescriptorCache.GetPropertyValue(this, propertyName));
				propertyDescriptor.SetValue(item, flag2);
			}
		}

		// Token: 0x060108E8 RID: 67816 RVA: 0x003B1AC0 File Offset: 0x003AFCC0
		internal virtual void ApplyTo(NavigationItem navigationItem, object dataItem, PropertyDescriptorCache propertyDescriptorCache)
		{
			if (!string.IsNullOrEmpty(this.Text) || !string.IsNullOrEmpty(this.TextField))
			{
				this.ApplyStringProperty(propertyDescriptorCache, navigationItem, dataItem, "Text");
				if (!string.IsNullOrEmpty(this.FormatString))
				{
					navigationItem.Text = string.Format(CultureInfo.CurrentCulture, this.FormatString, new object[]
					{
						navigationItem.Text
					});
				}
			}
			if (!string.IsNullOrEmpty(this.Value) || !string.IsNullOrEmpty(this.ValueField))
			{
				this.ApplyStringProperty(propertyDescriptorCache, navigationItem, dataItem, "Value");
			}
			if (!string.IsNullOrEmpty(this.NavigateUrl) || !string.IsNullOrEmpty(this.NavigateUrlField))
			{
				this.ApplyStringProperty(propertyDescriptorCache, navigationItem, dataItem, "NavigateUrl");
			}
			if (!string.IsNullOrEmpty(this.ToolTip) || !string.IsNullOrEmpty(this.ToolTipField))
			{
				this.ApplyStringProperty(propertyDescriptorCache, navigationItem, dataItem, "ToolTip");
			}
			if (!string.IsNullOrEmpty(this.ImageUrl) || !string.IsNullOrEmpty(this.ImageUrlField))
			{
				this.ApplyStringProperty(propertyDescriptorCache, navigationItem, dataItem, "ImageUrl");
			}
			if (!string.IsNullOrEmpty(this.HoveredImageUrl) || !string.IsNullOrEmpty(this.HoveredImageUrlField))
			{
				this.ApplyStringProperty(propertyDescriptorCache, navigationItem, dataItem, "HoveredImageUrl");
			}
			if (!string.IsNullOrEmpty(this.CssClass) || !string.IsNullOrEmpty(this.CssClassField))
			{
				this.ApplyStringProperty(propertyDescriptorCache, navigationItem, dataItem, "CssClass");
			}
			if (!string.IsNullOrEmpty(this.Target) || !string.IsNullOrEmpty(this.TargetField))
			{
				this.ApplyStringProperty(propertyDescriptorCache, navigationItem, dataItem, "Target");
			}
			if (!this.Enabled || !string.IsNullOrEmpty(this.EnabledField))
			{
				this.ApplyBoolProperty(propertyDescriptorCache, navigationItem, dataItem, "Enabled");
			}
			if (!this.PostBack || !string.IsNullOrEmpty(this.PostBackField))
			{
				this.ApplyBoolProperty(propertyDescriptorCache, navigationItem, dataItem, "PostBack");
			}
		}

		// Token: 0x060108E9 RID: 67817 RVA: 0x003B1C88 File Offset: 0x003AFE88
		private bool GetBooleanProperty(string propertyName, object dataItem, PropertyDescriptorCache propertyDescriptorCache)
		{
			object propertyValue = propertyDescriptorCache.GetPropertyValue(dataItem, propertyName);
			bool flag;
			return propertyValue != null && bool.TryParse(propertyValue.ToString(), out flag) && flag;
		}

		// Token: 0x1700507F RID: 20607
		// (get) Token: 0x060108EA RID: 67818 RVA: 0x003B1CB5 File Offset: 0x003AFEB5
		// (set) Token: 0x060108EB RID: 67819 RVA: 0x003B1CC7 File Offset: 0x003AFEC7
		object IDataSourceViewSchemaAccessor.DataSourceViewSchema
		{
			get
			{
				return base.ViewState["IDataSourceViewSchemaAccessor.DataSourceViewSchema"];
			}
			set
			{
				base.ViewState["IDataSourceViewSchemaAccessor.DataSourceViewSchema"] = value;
			}
		}
	}
}
