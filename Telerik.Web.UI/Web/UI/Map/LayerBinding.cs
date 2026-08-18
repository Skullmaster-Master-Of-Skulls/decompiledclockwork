using System;
using System.ComponentModel;

namespace Telerik.Web.UI.Map
{
	// Token: 0x02000441 RID: 1089
	public class LayerBinding : StateManager
	{
		// Token: 0x17000C8A RID: 3210
		// (get) Token: 0x060026F9 RID: 9977 RVA: 0x0007EE06 File Offset: 0x0007D006
		// (set) Token: 0x060026FA RID: 9978 RVA: 0x0007EE26 File Offset: 0x0007D026
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[DefaultValue("")]
		public string DataTypeField
		{
			get
			{
				return (string)(base.ViewState["DataTypeField"] ?? string.Empty);
			}
			set
			{
				base.ViewState["DataTypeField"] = value;
			}
		}

		// Token: 0x17000C8B RID: 3211
		// (get) Token: 0x060026FB RID: 9979 RVA: 0x0007EE39 File Offset: 0x0007D039
		// (set) Token: 0x060026FC RID: 9980 RVA: 0x0007EE59 File Offset: 0x0007D059
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[DefaultValue("")]
		public string DataUrlTemplateField
		{
			get
			{
				return (string)(base.ViewState["DataUrlTemplateField"] ?? string.Empty);
			}
			set
			{
				base.ViewState["DataUrlTemplateField"] = value;
			}
		}

		// Token: 0x17000C8C RID: 3212
		// (get) Token: 0x060026FD RID: 9981 RVA: 0x0007EE6C File Offset: 0x0007D06C
		// (set) Token: 0x060026FE RID: 9982 RVA: 0x0007EE8C File Offset: 0x0007D08C
		[DefaultValue("")]
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public string DataMinZoomField
		{
			get
			{
				return (string)(base.ViewState["DataMinZoomField"] ?? string.Empty);
			}
			set
			{
				base.ViewState["DataMinZoomField"] = value;
			}
		}

		// Token: 0x17000C8D RID: 3213
		// (get) Token: 0x060026FF RID: 9983 RVA: 0x0007EE9F File Offset: 0x0007D09F
		// (set) Token: 0x06002700 RID: 9984 RVA: 0x0007EEBF File Offset: 0x0007D0BF
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[DefaultValue("")]
		public string DataMaxZoomField
		{
			get
			{
				return (string)(base.ViewState["DataMaxZoomField"] ?? string.Empty);
			}
			set
			{
				base.ViewState["DataMaxZoomField"] = value;
			}
		}

		// Token: 0x17000C8E RID: 3214
		// (get) Token: 0x06002701 RID: 9985 RVA: 0x0007EED2 File Offset: 0x0007D0D2
		// (set) Token: 0x06002702 RID: 9986 RVA: 0x0007EEF2 File Offset: 0x0007D0F2
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[DefaultValue("")]
		public string DataOpacityField
		{
			get
			{
				return (string)(base.ViewState["DataOpacityField"] ?? string.Empty);
			}
			set
			{
				base.ViewState["DataOpacityField"] = value;
			}
		}

		// Token: 0x17000C8F RID: 3215
		// (get) Token: 0x06002703 RID: 9987 RVA: 0x0007EF05 File Offset: 0x0007D105
		// (set) Token: 0x06002704 RID: 9988 RVA: 0x0007EF25 File Offset: 0x0007D125
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[DefaultValue("")]
		public string DataAttributionField
		{
			get
			{
				return (string)(base.ViewState["DataAttributionField"] ?? string.Empty);
			}
			set
			{
				base.ViewState["DataAttributionField"] = value;
			}
		}

		// Token: 0x17000C90 RID: 3216
		// (get) Token: 0x06002705 RID: 9989 RVA: 0x0007EF38 File Offset: 0x0007D138
		// (set) Token: 0x06002706 RID: 9990 RVA: 0x0007EF58 File Offset: 0x0007D158
		[DefaultValue("")]
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public string DataKeyField
		{
			get
			{
				return (string)(base.ViewState["DataKeyField"] ?? string.Empty);
			}
			set
			{
				base.ViewState["DataKeyField"] = value;
			}
		}

		// Token: 0x17000C91 RID: 3217
		// (get) Token: 0x06002707 RID: 9991 RVA: 0x0007EF6B File Offset: 0x0007D16B
		// (set) Token: 0x06002708 RID: 9992 RVA: 0x0007EF8B File Offset: 0x0007D18B
		[DefaultValue("")]
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public string DataSubdomainsField
		{
			get
			{
				return (string)(base.ViewState["DataSubdomainsField"] ?? string.Empty);
			}
			set
			{
				base.ViewState["DataSubdomainsField"] = value;
			}
		}
	}
}
