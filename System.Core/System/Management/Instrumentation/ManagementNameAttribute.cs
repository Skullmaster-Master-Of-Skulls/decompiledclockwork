using System;
using System.Security.Permissions;

namespace System.Management.Instrumentation
{
	// Token: 0x02000297 RID: 663
	[AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false)]
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class ManagementNameAttribute : Attribute
	{
		// Token: 0x06001828 RID: 6184 RVA: 0x000573B7 File Offset: 0x000555B7
		public ManagementNameAttribute(string name)
		{
			this._Name = name;
		}

		// Token: 0x1700043C RID: 1084
		// (get) Token: 0x06001829 RID: 6185 RVA: 0x000573C6 File Offset: 0x000555C6
		public string Name
		{
			get
			{
				return this._Name;
			}
		}

		// Token: 0x04000B94 RID: 2964
		private string _Name;
	}
}
