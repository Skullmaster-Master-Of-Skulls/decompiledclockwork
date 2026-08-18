using System;
using System.Design;
using System.Security.Permissions;

namespace System.Web.UI.Design
{
	// Token: 0x02000091 RID: 145
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public class XslTransformFileEditor : UrlEditor
	{
		// Token: 0x17000126 RID: 294
		// (get) Token: 0x06000464 RID: 1124 RVA: 0x00014019 File Offset: 0x00012219
		protected override string Caption
		{
			get
			{
				return SR.GetString("XslTransformFileEditor_Caption");
			}
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x06000465 RID: 1125 RVA: 0x00014025 File Offset: 0x00012225
		protected override string Filter
		{
			get
			{
				return SR.GetString("XslTransformFileEditor_Filter");
			}
		}
	}
}
