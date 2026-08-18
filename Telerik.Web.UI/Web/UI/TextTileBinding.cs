using System;
using System.ComponentModel;
using System.Security.Permissions;
using System.Web;

namespace Telerik.Web.UI
{
	// Token: 0x020008F7 RID: 2295
	[DefaultProperty("DataTextField")]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class TextTileBinding : StateManager
	{
		// Token: 0x17001CA8 RID: 7336
		// (get) Token: 0x060056A2 RID: 22178 RVA: 0x001091E1 File Offset: 0x001073E1
		// (set) Token: 0x060056A3 RID: 22179 RVA: 0x00109201 File Offset: 0x00107401
		[DefaultValue("")]
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
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
