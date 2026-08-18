using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000851 RID: 2129
	internal interface ISocketListenerSettings
	{
		// Token: 0x170013CB RID: 5067
		// (get) Token: 0x06004FE7 RID: 20455
		int BufferSize { get; }

		// Token: 0x170013CC RID: 5068
		// (get) Token: 0x06004FE8 RID: 20456
		bool TeredoEnabled { get; }

		// Token: 0x170013CD RID: 5069
		// (get) Token: 0x06004FE9 RID: 20457
		int ListenBacklog { get; }
	}
}
