using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x020008B2 RID: 2226
	public abstract class CorrelationDataDescription
	{
		// Token: 0x170014D8 RID: 5336
		// (get) Token: 0x060054DC RID: 21724
		public abstract bool IsOptional { get; }

		// Token: 0x170014D9 RID: 5337
		// (get) Token: 0x060054DD RID: 21725
		public abstract bool IsDefault { get; }

		// Token: 0x170014DA RID: 5338
		// (get) Token: 0x060054DE RID: 21726
		public abstract bool KnownBeforeSend { get; }

		// Token: 0x170014DB RID: 5339
		// (get) Token: 0x060054DF RID: 21727
		public abstract string Name { get; }

		// Token: 0x170014DC RID: 5340
		// (get) Token: 0x060054E0 RID: 21728
		public abstract bool ReceiveValue { get; }

		// Token: 0x170014DD RID: 5341
		// (get) Token: 0x060054E1 RID: 21729
		public abstract bool SendValue { get; }
	}
}
