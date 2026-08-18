using System;
using System.Security.Permissions;

namespace System.Management.Instrumentation
{
	// Token: 0x02000296 RID: 662
	[AttributeUsage(AttributeTargets.Method)]
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class ManagementCommitAttribute : ManagementMemberAttribute
	{
	}
}
