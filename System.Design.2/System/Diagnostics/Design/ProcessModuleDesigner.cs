using System;
using System.Collections;
using System.ComponentModel.Design;
using System.Security.Permissions;

namespace System.Diagnostics.Design
{
	// Token: 0x02000213 RID: 531
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public class ProcessModuleDesigner : ComponentDesigner
	{
		// Token: 0x06001392 RID: 5010 RVA: 0x0006FD24 File Offset: 0x0006DF24
		protected override void PreFilterProperties(IDictionary properties)
		{
			base.PreFilterProperties(properties);
			RuntimeComponentFilter.FilterProperties(properties, null, new string[]
			{
				"FileVersionInfo"
			});
		}
	}
}
