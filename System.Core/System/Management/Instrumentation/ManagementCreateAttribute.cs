using System;
using System.Security.Permissions;

namespace System.Management.Instrumentation
{
	// Token: 0x0200028D RID: 653
	[AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = false)]
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class ManagementCreateAttribute : ManagementNewInstanceAttribute
	{
	}
}
