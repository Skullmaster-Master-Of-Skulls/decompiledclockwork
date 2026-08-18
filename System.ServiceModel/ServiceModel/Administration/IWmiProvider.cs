using System;

namespace System.ServiceModel.Administration
{
	// Token: 0x02000458 RID: 1112
	internal interface IWmiProvider
	{
		// Token: 0x06002B1F RID: 11039
		void EnumInstances(IWmiInstances instances);

		// Token: 0x06002B20 RID: 11040
		bool GetInstance(IWmiInstance instance);

		// Token: 0x06002B21 RID: 11041
		bool PutInstance(IWmiInstance instance);

		// Token: 0x06002B22 RID: 11042
		bool DeleteInstance(IWmiInstance instance);

		// Token: 0x06002B23 RID: 11043
		bool InvokeMethod(IWmiMethodContext method);
	}
}
