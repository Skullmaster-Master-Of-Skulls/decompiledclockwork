using System;
using System.Collections.Generic;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;

namespace System.ServiceModel.ComIntegration
{
	// Token: 0x02000228 RID: 552
	internal interface IProvideChannelBuilderSettings
	{
		// Token: 0x170003D4 RID: 980
		// (get) Token: 0x060010AA RID: 4266
		ServiceChannelFactory ServiceChannelFactoryReadWrite { get; }

		// Token: 0x170003D5 RID: 981
		// (get) Token: 0x060010AB RID: 4267
		ServiceChannelFactory ServiceChannelFactoryReadOnly { get; }

		// Token: 0x170003D6 RID: 982
		// (get) Token: 0x060010AC RID: 4268
		KeyedByTypeCollection<IEndpointBehavior> Behaviors { get; }

		// Token: 0x170003D7 RID: 983
		// (get) Token: 0x060010AD RID: 4269
		ServiceChannel ServiceChannel { get; }
	}
}
