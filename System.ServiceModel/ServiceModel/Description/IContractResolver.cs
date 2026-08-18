using System;

namespace System.ServiceModel.Description
{
	// Token: 0x020003E1 RID: 993
	internal interface IContractResolver
	{
		// Token: 0x06002569 RID: 9577
		ContractDescription ResolveContract(string contractName);
	}
}
