using System;
using System.Configuration;

namespace System.ServiceModel.Configuration
{
	// Token: 0x020006C7 RID: 1735
	[ConfigurationCollection(typeof(ServiceElement), AddItemName = "service")]
	public sealed class ServiceElementCollection : ServiceModelEnhancedConfigurationElementCollection<ServiceElement>
	{
		// Token: 0x06004331 RID: 17201 RVA: 0x000FDE94 File Offset: 0x000FC094
		public ServiceElementCollection() : base("service")
		{
		}

		// Token: 0x06004332 RID: 17202 RVA: 0x000FDEA4 File Offset: 0x000FC0A4
		protected override object GetElementKey(ConfigurationElement element)
		{
			if (element == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("element");
			}
			ServiceElement serviceElement = (ServiceElement)element;
			return serviceElement.Name;
		}
	}
}
