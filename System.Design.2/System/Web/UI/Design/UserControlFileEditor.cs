using System;
using System.Design;
using System.Security.Permissions;

namespace System.Web.UI.Design
{
	// Token: 0x02000080 RID: 128
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public class UserControlFileEditor : UrlEditor
	{
		// Token: 0x17000102 RID: 258
		// (get) Token: 0x060003E9 RID: 1001 RVA: 0x000131B9 File Offset: 0x000113B9
		protected override string Caption
		{
			get
			{
				return SR.GetString("UserControlFileEditor_Caption");
			}
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x060003EA RID: 1002 RVA: 0x000131C5 File Offset: 0x000113C5
		protected override string Filter
		{
			get
			{
				return SR.GetString("UserControlFileEditor_Filter");
			}
		}
	}
}
