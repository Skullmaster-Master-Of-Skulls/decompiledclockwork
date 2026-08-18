using System;

namespace System.ServiceModel.Administration
{
	// Token: 0x0200045B RID: 1115
	internal interface IWmiMethodContext
	{
		// Token: 0x17000A7F RID: 2687
		// (get) Token: 0x06002B29 RID: 11049
		string MethodName { get; }

		// Token: 0x17000A80 RID: 2688
		// (get) Token: 0x06002B2A RID: 11050
		IWmiInstance Instance { get; }

		// Token: 0x17000A81 RID: 2689
		// (set) Token: 0x06002B2B RID: 11051
		object ReturnParameter { set; }

		// Token: 0x06002B2C RID: 11052
		object GetParameter(string name);

		// Token: 0x06002B2D RID: 11053
		void SetParameter(string name, object value);
	}
}
