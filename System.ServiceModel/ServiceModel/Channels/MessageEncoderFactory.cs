using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x020009E2 RID: 2530
	[__DynamicallyInvokable]
	public abstract class MessageEncoderFactory
	{
		// Token: 0x060063E3 RID: 25571 RVA: 0x00175042 File Offset: 0x00173242
		[__DynamicallyInvokable]
		protected MessageEncoderFactory()
		{
		}

		// Token: 0x17001818 RID: 6168
		// (get) Token: 0x060063E4 RID: 25572
		[__DynamicallyInvokable]
		public abstract MessageEncoder Encoder { [__DynamicallyInvokable] get; }

		// Token: 0x17001819 RID: 6169
		// (get) Token: 0x060063E5 RID: 25573
		[__DynamicallyInvokable]
		public abstract MessageVersion MessageVersion { [__DynamicallyInvokable] get; }

		// Token: 0x060063E6 RID: 25574 RVA: 0x0017504A File Offset: 0x0017324A
		[__DynamicallyInvokable]
		public virtual MessageEncoder CreateSessionEncoder()
		{
			return this.Encoder;
		}
	}
}
