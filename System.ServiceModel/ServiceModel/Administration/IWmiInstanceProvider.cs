using System;

namespace System.ServiceModel.Administration
{
	// Token: 0x0200045C RID: 1116
	internal interface IWmiInstanceProvider
	{
		// Token: 0x06002B2E RID: 11054
		string GetInstanceType();

		// Token: 0x06002B2F RID: 11055
		void FillInstance(IWmiInstance wmiInstance);
	}
}
