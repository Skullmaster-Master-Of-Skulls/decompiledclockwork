using System;
using System.ComponentModel;

namespace Telerik.Web.UI.Map
{
	// Token: 0x02000442 RID: 1090
	public class MarkerBinding : StateManager
	{
		// Token: 0x17000C92 RID: 3218
		// (get) Token: 0x0600270A RID: 9994 RVA: 0x0007EFA6 File Offset: 0x0007D1A6
		// (set) Token: 0x0600270B RID: 9995 RVA: 0x0007EFC6 File Offset: 0x0007D1C6
		[DefaultValue("")]
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public string DataLocationLatitudeField
		{
			get
			{
				return (string)(base.ViewState["DataLocationLatitudeField"] ?? string.Empty);
			}
			set
			{
				base.ViewState["DataLocationLatitudeField"] = value;
			}
		}

		// Token: 0x17000C93 RID: 3219
		// (get) Token: 0x0600270C RID: 9996 RVA: 0x0007EFD9 File Offset: 0x0007D1D9
		// (set) Token: 0x0600270D RID: 9997 RVA: 0x0007EFF9 File Offset: 0x0007D1F9
		[DefaultValue("")]
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public string DataLocationLongitudeField
		{
			get
			{
				return (string)(base.ViewState["DataLocationLongitudeField"] ?? string.Empty);
			}
			set
			{
				base.ViewState["DataLocationLongitudeField"] = value;
			}
		}

		// Token: 0x17000C94 RID: 3220
		// (get) Token: 0x0600270E RID: 9998 RVA: 0x0007F00C File Offset: 0x0007D20C
		// (set) Token: 0x0600270F RID: 9999 RVA: 0x0007F02C File Offset: 0x0007D22C
		[DefaultValue("")]
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public string DataShapeField
		{
			get
			{
				return (string)(base.ViewState["DataShapeField"] ?? string.Empty);
			}
			set
			{
				base.ViewState["DataShapeField"] = value;
			}
		}

		// Token: 0x17000C95 RID: 3221
		// (get) Token: 0x06002710 RID: 10000 RVA: 0x0007F03F File Offset: 0x0007D23F
		// (set) Token: 0x06002711 RID: 10001 RVA: 0x0007F05F File Offset: 0x0007D25F
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[DefaultValue("")]
		public string DataTitleField
		{
			get
			{
				return (string)(base.ViewState["DataTitleField"] ?? string.Empty);
			}
			set
			{
				base.ViewState["DataTitleField"] = value;
			}
		}

		// Token: 0x17000C96 RID: 3222
		// (get) Token: 0x06002712 RID: 10002 RVA: 0x0007F072 File Offset: 0x0007D272
		// (set) Token: 0x06002713 RID: 10003 RVA: 0x0007F092 File Offset: 0x0007D292
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[DefaultValue("")]
		public string DataTooltipTemplateField
		{
			get
			{
				return (string)(base.ViewState["DataTooltipTemplateField"] ?? string.Empty);
			}
			set
			{
				base.ViewState["DataTooltipTemplateField"] = value;
			}
		}

		// Token: 0x17000C97 RID: 3223
		// (get) Token: 0x06002714 RID: 10004 RVA: 0x0007F0A5 File Offset: 0x0007D2A5
		// (set) Token: 0x06002715 RID: 10005 RVA: 0x0007F0C5 File Offset: 0x0007D2C5
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[DefaultValue("")]
		public string DataTooltipContentField
		{
			get
			{
				return (string)(base.ViewState["DataTooltipContentField"] ?? string.Empty);
			}
			set
			{
				base.ViewState["DataTooltipContentField"] = value;
			}
		}
	}
}
