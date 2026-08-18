using System;
using System.Design;
using System.Security.Permissions;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x020000E3 RID: 227
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public class MailDefinitionBodyFileNameEditor : UrlEditor
	{
		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x060007AF RID: 1967 RVA: 0x0002A145 File Offset: 0x00028345
		protected override string Caption
		{
			get
			{
				return SR.GetString("MailDefinitionBodyFileNameEditor_DefaultCaption");
			}
		}

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x060007B0 RID: 1968 RVA: 0x0002A151 File Offset: 0x00028351
		protected override string Filter
		{
			get
			{
				return SR.GetString("MailDefinitionBodyFileNameEditor_DefaultFilter");
			}
		}
	}
}
