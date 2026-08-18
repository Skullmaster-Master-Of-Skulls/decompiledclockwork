using System;
using System.Configuration;

namespace System.ServiceModel.Configuration
{
	// Token: 0x02000635 RID: 1589
	[ConfigurationCollection(typeof(IssuedTokenClientBehaviorsElement))]
	public sealed class IssuedTokenClientBehaviorsElementCollection : ServiceModelConfigurationElementCollection<IssuedTokenClientBehaviorsElement>
	{
		// Token: 0x06003CFD RID: 15613 RVA: 0x000E8B74 File Offset: 0x000E6D74
		protected override object GetElementKey(ConfigurationElement element)
		{
			if (element == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("element");
			}
			IssuedTokenClientBehaviorsElement issuedTokenClientBehaviorsElement = (IssuedTokenClientBehaviorsElement)element;
			return issuedTokenClientBehaviorsElement.IssuerAddress;
		}
	}
}
