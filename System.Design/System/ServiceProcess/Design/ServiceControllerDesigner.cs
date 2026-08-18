using System;
using System.Collections;
using System.ComponentModel.Design;
using System.Security.Permissions;

namespace System.ServiceProcess.Design
{
	// Token: 0x0200054B RID: 1355
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public class ServiceControllerDesigner : ComponentDesigner
	{
		// Token: 0x06002F8C RID: 12172 RVA: 0x0010EA50 File Offset: 0x0010DA50
		protected override void PreFilterProperties(IDictionary properties)
		{
			base.PreFilterProperties(properties);
			RuntimeComponentFilter.FilterProperties(properties, new string[]
			{
				"ServiceName",
				"DisplayName"
			}, new string[]
			{
				"CanPauseAndContinue",
				"CanShutdown",
				"CanStop",
				"DisplayName",
				"DependentServices",
				"ServicesDependedOn",
				"Status",
				"ServiceType",
				"MachineName"
			}, new bool[]
			{
				default(bool),
				default(bool),
				default(bool),
				default(bool),
				default(bool),
				default(bool),
				default(bool),
				default(bool),
				true
			});
		}
	}
}
