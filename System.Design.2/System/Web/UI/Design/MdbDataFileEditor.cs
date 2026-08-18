using System;
using System.Design;
using System.Security.Permissions;

namespace System.Web.UI.Design
{
	// Token: 0x02000061 RID: 97
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public class MdbDataFileEditor : UrlEditor
	{
		// Token: 0x170000BB RID: 187
		// (get) Token: 0x060002EC RID: 748 RVA: 0x0000F8C6 File Offset: 0x0000DAC6
		protected override string Caption
		{
			get
			{
				return SR.GetString("MdbDataFileEditor_Caption");
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x060002ED RID: 749 RVA: 0x0000F8D2 File Offset: 0x0000DAD2
		protected override string Filter
		{
			get
			{
				return SR.GetString("MdbDataFileEditor_Filter");
			}
		}
	}
}
