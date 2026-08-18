using System;
using System.Design;
using System.Security.Permissions;

namespace System.Web.UI.Design
{
	// Token: 0x0200008A RID: 138
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public class XmlDataFileEditor : UrlEditor
	{
		// Token: 0x17000114 RID: 276
		// (get) Token: 0x06000443 RID: 1091 RVA: 0x00013B02 File Offset: 0x00011D02
		protected override string Caption
		{
			get
			{
				return SR.GetString("XmlDataFileEditor_Caption");
			}
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x06000444 RID: 1092 RVA: 0x00013B0E File Offset: 0x00011D0E
		protected override string Filter
		{
			get
			{
				return SR.GetString("XmlDataFileEditor_Filter");
			}
		}
	}
}
