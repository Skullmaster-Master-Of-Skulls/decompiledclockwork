using System;
using System.Security.Permissions;

namespace System.Management.Instrumentation
{
	// Token: 0x02000290 RID: 656
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class ManagementProbeAttribute : ManagementMemberAttribute
	{
		// Token: 0x17000437 RID: 1079
		// (get) Token: 0x06001818 RID: 6168 RVA: 0x0005732B File Offset: 0x0005552B
		// (set) Token: 0x06001819 RID: 6169 RVA: 0x00057333 File Offset: 0x00055533
		public Type Schema
		{
			get
			{
				return this._schema;
			}
			set
			{
				this._schema = value;
			}
		}

		// Token: 0x04000B8C RID: 2956
		private Type _schema;
	}
}
