using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.ComponentModel.Design
{
	// Token: 0x020005CC RID: 1484
	[ComVisible(true)]
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	public sealed class ComponentChangedEventArgs : EventArgs
	{
		// Token: 0x17000D55 RID: 3413
		// (get) Token: 0x0600376B RID: 14187 RVA: 0x000F07C2 File Offset: 0x000EE9C2
		public object Component
		{
			get
			{
				return this.component;
			}
		}

		// Token: 0x17000D56 RID: 3414
		// (get) Token: 0x0600376C RID: 14188 RVA: 0x000F07CA File Offset: 0x000EE9CA
		public MemberDescriptor Member
		{
			get
			{
				return this.member;
			}
		}

		// Token: 0x17000D57 RID: 3415
		// (get) Token: 0x0600376D RID: 14189 RVA: 0x000F07D2 File Offset: 0x000EE9D2
		public object NewValue
		{
			get
			{
				return this.newValue;
			}
		}

		// Token: 0x17000D58 RID: 3416
		// (get) Token: 0x0600376E RID: 14190 RVA: 0x000F07DA File Offset: 0x000EE9DA
		public object OldValue
		{
			get
			{
				return this.oldValue;
			}
		}

		// Token: 0x0600376F RID: 14191 RVA: 0x000F07E2 File Offset: 0x000EE9E2
		public ComponentChangedEventArgs(object component, MemberDescriptor member, object oldValue, object newValue)
		{
			this.component = component;
			this.member = member;
			this.oldValue = oldValue;
			this.newValue = newValue;
		}

		// Token: 0x04002AF2 RID: 10994
		private object component;

		// Token: 0x04002AF3 RID: 10995
		private MemberDescriptor member;

		// Token: 0x04002AF4 RID: 10996
		private object oldValue;

		// Token: 0x04002AF5 RID: 10997
		private object newValue;
	}
}
