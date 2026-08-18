using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x020007C7 RID: 1991
	internal interface IChannelBindingProvider
	{
		// Token: 0x06004B0F RID: 19215
		void EnableChannelBindingSupport();

		// Token: 0x170012DD RID: 4829
		// (get) Token: 0x06004B10 RID: 19216
		bool IsChannelBindingSupportEnabled { get; }
	}
}
