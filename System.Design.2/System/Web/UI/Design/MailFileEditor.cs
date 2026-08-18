using System;
using System.Design;
using System.Security.Permissions;

namespace System.Web.UI.Design
{
	// Token: 0x02000060 RID: 96
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public class MailFileEditor : UrlEditor
	{
		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x060002E9 RID: 745 RVA: 0x0000F8AE File Offset: 0x0000DAAE
		protected override string Caption
		{
			get
			{
				return SR.GetString("MailFilePicker_Caption");
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x060002EA RID: 746 RVA: 0x0000F8BA File Offset: 0x0000DABA
		protected override string Filter
		{
			get
			{
				return SR.GetString("MailFilePicker_Filter");
			}
		}
	}
}
