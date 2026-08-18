using System;
using System.Configuration;

namespace System.ServiceModel.Configuration
{
	// Token: 0x0200060B RID: 1547
	internal interface IConfigurationContextProviderInternal
	{
		// Token: 0x06003B92 RID: 15250
		ContextInformation GetEvaluationContext();

		// Token: 0x06003B93 RID: 15251
		ContextInformation GetOriginalEvaluationContext();
	}
}
