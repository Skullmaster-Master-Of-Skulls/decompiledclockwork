using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x020007C6 RID: 1990
	internal class ChannelBindingProviderHelper : IChannelBindingProvider
	{
		// Token: 0x06004B0B RID: 19211 RVA: 0x00113077 File Offset: 0x00111277
		public void EnableChannelBindingSupport()
		{
			this.IsChannelBindingSupportEnabled = true;
		}

		// Token: 0x170012DC RID: 4828
		// (get) Token: 0x06004B0C RID: 19212 RVA: 0x00113080 File Offset: 0x00111280
		// (set) Token: 0x06004B0D RID: 19213 RVA: 0x00113088 File Offset: 0x00111288
		public bool IsChannelBindingSupportEnabled { get; private set; }
	}
}
