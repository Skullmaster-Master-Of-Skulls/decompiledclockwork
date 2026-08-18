using System;
using System.ComponentModel;
using System.Security.Permissions;
using System.Web;

namespace Telerik.Web.UI
{
	// Token: 0x020008F5 RID: 2293
	[DefaultProperty("DataImageUrlField")]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class ImageAndTextTileBinding : StateManager
	{
		// Token: 0x17001CA5 RID: 7333
		// (get) Token: 0x0600569A RID: 22170 RVA: 0x00109138 File Offset: 0x00107338
		// (set) Token: 0x0600569B RID: 22171 RVA: 0x00109158 File Offset: 0x00107358
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[DefaultValue("")]
		public string DataImageUrlField
		{
			get
			{
				return (string)(base.ViewState["DataImageUrlField"] ?? string.Empty);
			}
			set
			{
				base.ViewState["DataImageUrlField"] = value;
			}
		}

		// Token: 0x17001CA6 RID: 7334
		// (get) Token: 0x0600569C RID: 22172 RVA: 0x0010916B File Offset: 0x0010736B
		// (set) Token: 0x0600569D RID: 22173 RVA: 0x0010918B File Offset: 0x0010738B
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[DefaultValue("")]
		public string DataTextField
		{
			get
			{
				return (string)(base.ViewState["DataTextField"] ?? string.Empty);
			}
			set
			{
				base.ViewState["DataTextField"] = value;
			}
		}
	}
}
