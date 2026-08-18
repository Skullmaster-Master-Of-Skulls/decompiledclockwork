using System;
using System.Security.Permissions;

namespace System.Management.Instrumentation
{
	// Token: 0x02000293 RID: 659
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class ManagementReferenceAttribute : Attribute
	{
		// Token: 0x17000439 RID: 1081
		// (get) Token: 0x06001820 RID: 6176 RVA: 0x0005736D File Offset: 0x0005556D
		// (set) Token: 0x06001821 RID: 6177 RVA: 0x00057375 File Offset: 0x00055575
		public string Type
		{
			get
			{
				return this._Type;
			}
			set
			{
				this._Type = value;
			}
		}

		// Token: 0x04000B8E RID: 2958
		private string _Type;
	}
}
