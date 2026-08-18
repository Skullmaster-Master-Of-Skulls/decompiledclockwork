using System;
using System.Collections;
using System.ComponentModel.Design;
using System.Security.Permissions;

namespace System.ServiceProcess.Design
{
	// Token: 0x0200000D RID: 13
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public class ServiceControllerDesigner : ComponentDesigner
	{
		// Token: 0x06000022 RID: 34 RVA: 0x000033D4 File Offset: 0x000015D4
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
