using System;

namespace System.ServiceModel.Administration
{
	// Token: 0x02000459 RID: 1113
	internal interface IWmiInstances
	{
		// Token: 0x06002B24 RID: 11044
		IWmiInstance NewInstance(string className);

		// Token: 0x06002B25 RID: 11045
		void AddInstance(IWmiInstance inst);
	}
}
