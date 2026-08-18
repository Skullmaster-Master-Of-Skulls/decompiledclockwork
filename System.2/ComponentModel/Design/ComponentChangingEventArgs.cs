using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.ComponentModel.Design
{
	// Token: 0x020005CE RID: 1486
	[ComVisible(true)]
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	public sealed class ComponentChangingEventArgs : EventArgs
	{
		// Token: 0x17000D59 RID: 3417
		// (get) Token: 0x06003774 RID: 14196 RVA: 0x000F0807 File Offset: 0x000EEA07
		public object Component
		{
			get
			{
				return this.component;
			}
		}

		// Token: 0x17000D5A RID: 3418
		// (get) Token: 0x06003775 RID: 14197 RVA: 0x000F080F File Offset: 0x000EEA0F
		public MemberDescriptor Member
		{
			get
			{
				return this.member;
			}
		}

		// Token: 0x06003776 RID: 14198 RVA: 0x000F0817 File Offset: 0x000EEA17
		public ComponentChangingEventArgs(object component, MemberDescriptor member)
		{
			this.component = component;
			this.member = member;
		}

		// Token: 0x04002AF6 RID: 10998
		private object component;

		// Token: 0x04002AF7 RID: 10999
		private MemberDescriptor member;
	}
}
