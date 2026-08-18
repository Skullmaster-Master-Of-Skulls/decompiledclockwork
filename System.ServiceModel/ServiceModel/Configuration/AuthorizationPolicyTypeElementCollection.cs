using System;
using System.Configuration;

namespace System.ServiceModel.Configuration
{
	// Token: 0x020005E5 RID: 1509
	[ConfigurationCollection(typeof(AuthorizationPolicyTypeElement))]
	public sealed class AuthorizationPolicyTypeElementCollection : ServiceModelConfigurationElementCollection<AuthorizationPolicyTypeElement>
	{
		// Token: 0x06003A66 RID: 14950 RVA: 0x000E0BF8 File Offset: 0x000DEDF8
		protected override object GetElementKey(ConfigurationElement element)
		{
			if (element == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("element");
			}
			AuthorizationPolicyTypeElement authorizationPolicyTypeElement = (AuthorizationPolicyTypeElement)element;
			return authorizationPolicyTypeElement.PolicyType;
		}
	}
}
