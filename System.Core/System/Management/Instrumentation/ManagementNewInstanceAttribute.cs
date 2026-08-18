using System;
using System.Security.Permissions;

namespace System.Management.Instrumentation
{
	// Token: 0x0200028B RID: 651
	[AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = false)]
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public abstract class ManagementNewInstanceAttribute : ManagementMemberAttribute
	{
	}
}
