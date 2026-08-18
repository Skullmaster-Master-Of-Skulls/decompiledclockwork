using System;
using System.Security.Permissions;

namespace System.Management.Instrumentation
{
	// Token: 0x0200028A RID: 650
	[AttributeUsage(AttributeTargets.All)]
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public abstract class ManagementMemberAttribute : Attribute
	{
		// Token: 0x17000433 RID: 1075
		// (get) Token: 0x0600180A RID: 6154 RVA: 0x000572B7 File Offset: 0x000554B7
		// (set) Token: 0x0600180B RID: 6155 RVA: 0x000572BF File Offset: 0x000554BF
		public string Name
		{
			get
			{
				return this._Name;
			}
			set
			{
				this._Name = value;
			}
		}

		// Token: 0x04000B88 RID: 2952
		private string _Name;
	}
}
