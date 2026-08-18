using System;
using System.Design;
using System.Security.Permissions;

namespace System.Web.UI.Design
{
	// Token: 0x02000056 RID: 86
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public class ImageUrlEditor : UrlEditor
	{
		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060002BA RID: 698 RVA: 0x0000F88E File Offset: 0x0000DA8E
		protected override string Caption
		{
			get
			{
				return SR.GetString("UrlPicker_ImageCaption");
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060002BB RID: 699 RVA: 0x0000F89A File Offset: 0x0000DA9A
		protected override string Filter
		{
			get
			{
				return SR.GetString("UrlPicker_ImageFilter");
			}
		}
	}
}
