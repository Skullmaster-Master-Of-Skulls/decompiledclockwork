using System;
using System.Collections.Generic;

namespace System.ServiceModel.Channels
{
	// Token: 0x020008B7 RID: 2231
	public interface ICorrelationDataSource
	{
		// Token: 0x170014EC RID: 5356
		// (get) Token: 0x06005511 RID: 21777
		ICollection<CorrelationDataDescription> DataSources { get; }
	}
}
