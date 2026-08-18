using System;
using System.Security.Permissions;

namespace System.Management.Instrumentation
{
	// Token: 0x0200028C RID: 652
	[AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = false)]
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class ManagementBindAttribute : ManagementNewInstanceAttribute
	{
		// Token: 0x17000434 RID: 1076
		// (get) Token: 0x0600180F RID: 6159 RVA: 0x000572E0 File Offset: 0x000554E0
		// (set) Token: 0x06001810 RID: 6160 RVA: 0x000572E8 File Offset: 0x000554E8
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

		// Token: 0x04000B89 RID: 2953
		private Type _schema;
	}
}
