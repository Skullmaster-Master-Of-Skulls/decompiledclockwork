using System;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x02000562 RID: 1378
	internal interface IDispatchFaultFormatterWrapper
	{
		// Token: 0x17000CD1 RID: 3281
		// (get) Token: 0x0600359B RID: 13723
		// (set) Token: 0x0600359C RID: 13724
		IDispatchFaultFormatter InnerFaultFormatter { get; set; }
	}
}
