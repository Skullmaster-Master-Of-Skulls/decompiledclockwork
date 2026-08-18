using System;
using System.Security.Permissions;

namespace System.ComponentModel.Design.Serialization
{
	// Token: 0x02000612 RID: 1554
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	public class ResolveNameEventArgs : EventArgs
	{
		// Token: 0x060038EC RID: 14572 RVA: 0x000F250A File Offset: 0x000F070A
		public ResolveNameEventArgs(string name)
		{
			this.name = name;
			this.value = null;
		}

		// Token: 0x17000D9F RID: 3487
		// (get) Token: 0x060038ED RID: 14573 RVA: 0x000F2520 File Offset: 0x000F0720
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000DA0 RID: 3488
		// (get) Token: 0x060038EE RID: 14574 RVA: 0x000F2528 File Offset: 0x000F0728
		// (set) Token: 0x060038EF RID: 14575 RVA: 0x000F2530 File Offset: 0x000F0730
		public object Value
		{
			get
			{
				return this.value;
			}
			set
			{
				this.value = value;
			}
		}

		// Token: 0x04002B83 RID: 11139
		private string name;

		// Token: 0x04002B84 RID: 11140
		private object value;
	}
}
