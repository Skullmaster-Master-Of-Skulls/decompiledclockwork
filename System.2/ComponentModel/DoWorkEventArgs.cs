using System;
using System.Security.Permissions;

namespace System.ComponentModel
{
	// Token: 0x02000549 RID: 1353
	[__DynamicallyInvokable]
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public class DoWorkEventArgs : CancelEventArgs
	{
		// Token: 0x060032E8 RID: 13032 RVA: 0x000E2D4D File Offset: 0x000E0F4D
		[__DynamicallyInvokable]
		public DoWorkEventArgs(object argument)
		{
			this.argument = argument;
		}

		// Token: 0x17000C73 RID: 3187
		// (get) Token: 0x060032E9 RID: 13033 RVA: 0x000E2D5C File Offset: 0x000E0F5C
		[SRDescription("BackgroundWorker_DoWorkEventArgs_Argument")]
		[__DynamicallyInvokable]
		public object Argument
		{
			[__DynamicallyInvokable]
			get
			{
				return this.argument;
			}
		}

		// Token: 0x17000C74 RID: 3188
		// (get) Token: 0x060032EA RID: 13034 RVA: 0x000E2D64 File Offset: 0x000E0F64
		// (set) Token: 0x060032EB RID: 13035 RVA: 0x000E2D6C File Offset: 0x000E0F6C
		[SRDescription("BackgroundWorker_DoWorkEventArgs_Result")]
		[__DynamicallyInvokable]
		public object Result
		{
			[__DynamicallyInvokable]
			get
			{
				return this.result;
			}
			[__DynamicallyInvokable]
			set
			{
				this.result = value;
			}
		}

		// Token: 0x040029A5 RID: 10661
		private object result;

		// Token: 0x040029A6 RID: 10662
		private object argument;
	}
}
