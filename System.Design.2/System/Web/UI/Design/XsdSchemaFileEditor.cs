using System;
using System.Design;
using System.Security.Permissions;

namespace System.Web.UI.Design
{
	// Token: 0x02000090 RID: 144
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public class XsdSchemaFileEditor : UrlEditor
	{
		// Token: 0x17000124 RID: 292
		// (get) Token: 0x06000461 RID: 1121 RVA: 0x00014001 File Offset: 0x00012201
		protected override string Caption
		{
			get
			{
				return SR.GetString("XsdSchemaFileEditor_Caption");
			}
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x06000462 RID: 1122 RVA: 0x0001400D File Offset: 0x0001220D
		protected override string Filter
		{
			get
			{
				return SR.GetString("XsdSchemaFileEditor_Filter");
			}
		}
	}
}
