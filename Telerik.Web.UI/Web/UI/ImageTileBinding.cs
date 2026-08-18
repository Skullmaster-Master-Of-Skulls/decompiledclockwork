using System;
using System.ComponentModel;
using System.Security.Permissions;
using System.Web;

namespace Telerik.Web.UI
{
	// Token: 0x020008F6 RID: 2294
	[DefaultProperty("DataImageUrlField")]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class ImageTileBinding : StateManager
	{
		// Token: 0x17001CA7 RID: 7335
		// (get) Token: 0x0600569F RID: 22175 RVA: 0x001091A6 File Offset: 0x001073A6
		// (set) Token: 0x060056A0 RID: 22176 RVA: 0x001091C6 File Offset: 0x001073C6
		[DefaultValue("")]
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
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
