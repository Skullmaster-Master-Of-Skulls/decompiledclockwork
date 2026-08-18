using System;
using System.Collections;
using System.ComponentModel.Design;
using System.Security.Permissions;

namespace System.Diagnostics.Design
{
	// Token: 0x02000214 RID: 532
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public class ProcessThreadDesigner : ComponentDesigner
	{
		// Token: 0x06001394 RID: 5012 RVA: 0x0006FD42 File Offset: 0x0006DF42
		protected override void PreFilterProperties(IDictionary properties)
		{
			base.PreFilterProperties(properties);
			RuntimeComponentFilter.FilterProperties(properties, null, new string[]
			{
				"IdealProcessor",
				"ProcessorAffinity"
			});
		}
	}
}
