using System;

namespace System.ServiceModel.Discovery
{
	// Token: 0x02000020 RID: 32
	internal class DiscoveryMessageProperty
	{
		// Token: 0x06000180 RID: 384 RVA: 0x00006351 File Offset: 0x00004551
		public DiscoveryMessageProperty()
		{
		}

		// Token: 0x06000181 RID: 385 RVA: 0x000063CE File Offset: 0x000045CE
		public DiscoveryMessageProperty(object correlationState)
		{
			this.CorrelationState = correlationState;
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000182 RID: 386 RVA: 0x000063DD File Offset: 0x000045DD
		// (set) Token: 0x06000183 RID: 387 RVA: 0x000063E5 File Offset: 0x000045E5
		public object CorrelationState { get; set; }

		// Token: 0x04000063 RID: 99
		public const string Name = "System.ServiceModel.Discovery.DiscoveryMessageProperty";
	}
}
