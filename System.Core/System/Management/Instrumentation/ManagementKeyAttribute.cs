using System;
using System.Security.Permissions;

namespace System.Management.Instrumentation
{
	// Token: 0x02000292 RID: 658
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class ManagementKeyAttribute : ManagementMemberAttribute
	{
	}
}
