using System;
using System.Security.Permissions;

namespace System.Management.Instrumentation
{
	// Token: 0x0200028F RID: 655
	[AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = false)]
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class ManagementEnumeratorAttribute : ManagementNewInstanceAttribute
	{
		// Token: 0x17000436 RID: 1078
		// (get) Token: 0x06001815 RID: 6165 RVA: 0x00057312 File Offset: 0x00055512
		// (set) Token: 0x06001816 RID: 6166 RVA: 0x0005731A File Offset: 0x0005551A
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

		// Token: 0x04000B8B RID: 2955
		private Type _schema;
	}
}
