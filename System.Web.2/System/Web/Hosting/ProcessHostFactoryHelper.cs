using System;
using System.Security.Permissions;
using System.Web.Util;

namespace System.Web.Hosting
{
	// Token: 0x020007E5 RID: 2021
	public sealed class ProcessHostFactoryHelper : MarshalByRefObject, IProcessHostFactoryHelper
	{
		// Token: 0x06006083 RID: 24707 RVA: 0x0000298D File Offset: 0x00000B8D
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)]
		public override object InitializeLifetimeService()
		{
			return null;
		}

		// Token: 0x06006084 RID: 24708 RVA: 0x0014DAA8 File Offset: 0x0014BCA8
		public object GetProcessHost(IProcessHostSupportFunctions functions)
		{
			object processHost;
			try
			{
				processHost = ProcessHost.GetProcessHost(functions);
			}
			catch (Exception e)
			{
				Misc.ReportUnhandledException(e, new string[]
				{
					SR.GetString("Cant_Create_Process_Host")
				});
				throw;
			}
			return processHost;
		}
	}
}
