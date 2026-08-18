using System;

namespace System.ServiceModel.Administration
{
	// Token: 0x0200045A RID: 1114
	internal interface IWmiInstance
	{
		// Token: 0x06002B26 RID: 11046
		IWmiInstance NewInstance(string className);

		// Token: 0x06002B27 RID: 11047
		object GetProperty(string name);

		// Token: 0x06002B28 RID: 11048
		void SetProperty(string name, object value);
	}
}
