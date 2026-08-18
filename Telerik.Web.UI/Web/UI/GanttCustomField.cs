using System;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x02000322 RID: 802
	public class GanttCustomField : StateManager
	{
		// Token: 0x170008FC RID: 2300
		// (get) Token: 0x06001AC9 RID: 6857 RVA: 0x00056B9D File Offset: 0x00054D9D
		// (set) Token: 0x06001ACA RID: 6858 RVA: 0x00056BBD File Offset: 0x00054DBD
		[Description("The server name of the custom property.")]
		[DefaultValue("")]
		[Category("Behavior")]
		public string PropertyName
		{
			get
			{
				return (string)(base.ViewState["PropertyName"] ?? string.Empty);
			}
			set
			{
				base.ViewState["PropertyName"] = value;
			}
		}

		// Token: 0x170008FD RID: 2301
		// (get) Token: 0x06001ACB RID: 6859 RVA: 0x00056BD0 File Offset: 0x00054DD0
		// (set) Token: 0x06001ACC RID: 6860 RVA: 0x00056BF0 File Offset: 0x00054DF0
		[DefaultValue("")]
		[Description("The client name of the custom property.")]
		[Category("Behavior")]
		public string ClientPropertyName
		{
			get
			{
				return (string)(base.ViewState["ClientPropertyName"] ?? string.Empty);
			}
			set
			{
				base.ViewState["ClientPropertyName"] = value;
			}
		}

		// Token: 0x170008FE RID: 2302
		// (get) Token: 0x06001ACD RID: 6861 RVA: 0x00056C03 File Offset: 0x00054E03
		// (set) Token: 0x06001ACE RID: 6862 RVA: 0x00056C15 File Offset: 0x00054E15
		[DefaultValue(null)]
		[Description("The default value of the custom property.")]
		[Category("Behavior")]
		public object DefaultValue
		{
			get
			{
				return base.ViewState["DefaultValue"];
			}
			set
			{
				base.ViewState["DefaultValue"] = value;
			}
		}

		// Token: 0x170008FF RID: 2303
		// (get) Token: 0x06001ACF RID: 6863 RVA: 0x00056C28 File Offset: 0x00054E28
		// (set) Token: 0x06001AD0 RID: 6864 RVA: 0x00056C49 File Offset: 0x00054E49
		[Description("The client type ot the custom property.")]
		[DefaultValue(GanttCustomFieldType.Default)]
		[Category("Behavior")]
		public GanttCustomFieldType Type
		{
			get
			{
				return (GanttCustomFieldType)(base.ViewState["Type"] ?? GanttCustomFieldType.Default);
			}
			set
			{
				base.ViewState["Type"] = value;
			}
		}
	}
}
