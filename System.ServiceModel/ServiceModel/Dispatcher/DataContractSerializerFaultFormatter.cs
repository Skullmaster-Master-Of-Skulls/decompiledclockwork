using System;
using System.Collections.Generic;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x0200055F RID: 1375
	internal class DataContractSerializerFaultFormatter : FaultFormatter
	{
		// Token: 0x06003597 RID: 13719 RVA: 0x000D0B4A File Offset: 0x000CED4A
		internal DataContractSerializerFaultFormatter(Type[] detailTypes) : base(detailTypes)
		{
		}

		// Token: 0x06003598 RID: 13720 RVA: 0x000D0B53 File Offset: 0x000CED53
		internal DataContractSerializerFaultFormatter(SynchronizedCollection<FaultContractInfo> faultContractInfoCollection) : base(faultContractInfoCollection)
		{
		}
	}
}
