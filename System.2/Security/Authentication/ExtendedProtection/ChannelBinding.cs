using System;
using Microsoft.Win32.SafeHandles;

namespace System.Security.Authentication.ExtendedProtection
{
	// Token: 0x02000440 RID: 1088
	[__DynamicallyInvokable]
	public abstract class ChannelBinding : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x0600288B RID: 10379 RVA: 0x000BA2EC File Offset: 0x000B84EC
		[__DynamicallyInvokable]
		protected ChannelBinding() : base(true)
		{
		}

		// Token: 0x0600288C RID: 10380 RVA: 0x000BA2F5 File Offset: 0x000B84F5
		[__DynamicallyInvokable]
		protected ChannelBinding(bool ownsHandle) : base(ownsHandle)
		{
		}

		// Token: 0x170009F6 RID: 2550
		// (get) Token: 0x0600288D RID: 10381
		[__DynamicallyInvokable]
		public abstract int Size { [__DynamicallyInvokable] get; }
	}
}
