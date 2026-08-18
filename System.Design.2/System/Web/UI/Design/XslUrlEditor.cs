using System;
using System.Design;
using System.Security.Permissions;

namespace System.Web.UI.Design
{
	// Token: 0x02000092 RID: 146
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public class XslUrlEditor : UrlEditor
	{
		// Token: 0x17000128 RID: 296
		// (get) Token: 0x06000467 RID: 1127 RVA: 0x00014031 File Offset: 0x00012231
		protected override string Caption
		{
			get
			{
				return SR.GetString("UrlPicker_XslCaption");
			}
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x06000468 RID: 1128 RVA: 0x0001403D File Offset: 0x0001223D
		protected override string Filter
		{
			get
			{
				return SR.GetString("UrlPicker_XslFilter");
			}
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x06000469 RID: 1129 RVA: 0x00003B0F File Offset: 0x00001D0F
		protected override UrlBuilderOptions Options
		{
			get
			{
				return UrlBuilderOptions.NoAbsolute;
			}
		}
	}
}
