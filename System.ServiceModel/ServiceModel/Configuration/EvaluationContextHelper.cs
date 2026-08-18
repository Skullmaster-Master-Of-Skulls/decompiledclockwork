using System;
using System.Configuration;
using System.Security;

namespace System.ServiceModel.Configuration
{
	// Token: 0x0200060C RID: 1548
	[SecurityCritical(SecurityCriticalScope.Everything)]
	internal struct EvaluationContextHelper
	{
		// Token: 0x06003B94 RID: 15252 RVA: 0x000E40A8 File Offset: 0x000E22A8
		internal void OnReset(ConfigurationElement parent)
		{
			this.reset = true;
			this.inheritedContext = ConfigurationHelpers.GetOriginalEvaluationContext(parent as IConfigurationContextProviderInternal);
		}

		// Token: 0x06003B95 RID: 15253 RVA: 0x000E40C2 File Offset: 0x000E22C2
		internal ContextInformation GetOriginalContext(IConfigurationContextProviderInternal owner)
		{
			if (this.reset)
			{
				return this.inheritedContext;
			}
			return ConfigurationHelpers.GetEvaluationContext(owner);
		}

		// Token: 0x04002A87 RID: 10887
		private bool reset;

		// Token: 0x04002A88 RID: 10888
		private ContextInformation inheritedContext;
	}
}
