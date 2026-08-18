using System;
using System.ComponentModel;
using System.Security.Permissions;
using System.Web;

namespace Telerik.Web.UI
{
	// Token: 0x020008F4 RID: 2292
	[DefaultProperty("DataTextField")]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class IconTileBinding : StateManager
	{
		// Token: 0x17001CA4 RID: 7332
		// (get) Token: 0x06005697 RID: 22167 RVA: 0x001090FD File Offset: 0x001072FD
		// (set) Token: 0x06005698 RID: 22168 RVA: 0x0010911D File Offset: 0x0010731D
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
	}
}
