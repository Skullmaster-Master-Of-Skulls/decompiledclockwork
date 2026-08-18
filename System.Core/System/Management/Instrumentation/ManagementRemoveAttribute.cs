using System;
using System.Security.Permissions;

namespace System.Management.Instrumentation
{
	// Token: 0x0200028E RID: 654
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class ManagementRemoveAttribute : ManagementMemberAttribute
	{
		// Token: 0x17000435 RID: 1077
		// (get) Token: 0x06001812 RID: 6162 RVA: 0x000572F9 File Offset: 0x000554F9
		// (set) Token: 0x06001813 RID: 6163 RVA: 0x00057301 File Offset: 0x00055501
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

		// Token: 0x04000B8A RID: 2954
		private Type _schema;
	}
}
