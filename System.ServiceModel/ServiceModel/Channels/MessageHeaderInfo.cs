using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x020009B3 RID: 2483
	[__DynamicallyInvokable]
	public abstract class MessageHeaderInfo
	{
		// Token: 0x1700177F RID: 6015
		// (get) Token: 0x06006176 RID: 24950
		[__DynamicallyInvokable]
		public abstract string Actor { [__DynamicallyInvokable] get; }

		// Token: 0x17001780 RID: 6016
		// (get) Token: 0x06006177 RID: 24951
		[__DynamicallyInvokable]
		public abstract bool IsReferenceParameter { [__DynamicallyInvokable] get; }

		// Token: 0x17001781 RID: 6017
		// (get) Token: 0x06006178 RID: 24952
		[__DynamicallyInvokable]
		public abstract string Name { [__DynamicallyInvokable] get; }

		// Token: 0x17001782 RID: 6018
		// (get) Token: 0x06006179 RID: 24953
		[__DynamicallyInvokable]
		public abstract string Namespace { [__DynamicallyInvokable] get; }

		// Token: 0x17001783 RID: 6019
		// (get) Token: 0x0600617A RID: 24954
		[__DynamicallyInvokable]
		public abstract bool MustUnderstand { [__DynamicallyInvokable] get; }

		// Token: 0x17001784 RID: 6020
		// (get) Token: 0x0600617B RID: 24955
		[__DynamicallyInvokable]
		public abstract bool Relay { [__DynamicallyInvokable] get; }

		// Token: 0x0600617C RID: 24956 RVA: 0x0016B553 File Offset: 0x00169753
		[__DynamicallyInvokable]
		protected MessageHeaderInfo()
		{
		}
	}
}
