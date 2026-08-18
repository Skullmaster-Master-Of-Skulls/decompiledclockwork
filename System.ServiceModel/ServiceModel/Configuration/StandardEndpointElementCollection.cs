using System;
using System.Configuration;

namespace System.ServiceModel.Configuration
{
	// Token: 0x020006D5 RID: 1749
	public sealed class StandardEndpointElementCollection<TEndpointConfiguration> : ServiceModelEnhancedConfigurationElementCollection<TEndpointConfiguration> where TEndpointConfiguration : StandardEndpointElement, new()
	{
		// Token: 0x060043B7 RID: 17335 RVA: 0x000FFE62 File Offset: 0x000FE062
		public StandardEndpointElementCollection() : base("standardEndpoint")
		{
		}

		// Token: 0x060043B8 RID: 17336 RVA: 0x000FFE70 File Offset: 0x000FE070
		protected override object GetElementKey(ConfigurationElement element)
		{
			if (element == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("element");
			}
			TEndpointConfiguration tendpointConfiguration = (TEndpointConfiguration)((object)element);
			return tendpointConfiguration.Name;
		}
	}
}
