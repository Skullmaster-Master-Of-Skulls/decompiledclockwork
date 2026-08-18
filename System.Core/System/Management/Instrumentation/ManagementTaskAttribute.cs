using System;
using System.Security.Permissions;

namespace System.Management.Instrumentation
{
	// Token: 0x02000291 RID: 657
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class ManagementTaskAttribute : ManagementMemberAttribute
	{
		// Token: 0x17000438 RID: 1080
		// (get) Token: 0x0600181C RID: 6172 RVA: 0x0005734C File Offset: 0x0005554C
		// (set) Token: 0x0600181D RID: 6173 RVA: 0x00057354 File Offset: 0x00055554
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

		// Token: 0x04000B8D RID: 2957
		private Type _schema;
	}
}
