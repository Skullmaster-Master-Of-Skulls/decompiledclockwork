using System;
using System.Configuration;
using System.Globalization;

namespace System.ServiceModel.Configuration
{
	// Token: 0x020006C8 RID: 1736
	[ConfigurationCollection(typeof(ServiceEndpointElement), AddItemName = "endpoint")]
	public sealed class ServiceEndpointElementCollection : ServiceModelEnhancedConfigurationElementCollection<ServiceEndpointElement>
	{
		// Token: 0x06004333 RID: 17203 RVA: 0x000FDED1 File Offset: 0x000FC0D1
		public ServiceEndpointElementCollection() : base("endpoint")
		{
		}

		// Token: 0x17001162 RID: 4450
		// (get) Token: 0x06004334 RID: 17204 RVA: 0x000FDEDE File Offset: 0x000FC0DE
		protected override bool ThrowOnDuplicate
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06004335 RID: 17205 RVA: 0x000FDEE4 File Offset: 0x000FC0E4
		protected override object GetElementKey(ConfigurationElement element)
		{
			if (element == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("element");
			}
			ServiceEndpointElement serviceEndpointElement = (ServiceEndpointElement)element;
			return string.Format(CultureInfo.InvariantCulture, "address:{0};bindingConfiguration{1};bindingName:{2};bindingNamespace:{3};bindingSectionName:{4};contractType:{5};kind:{6};endpointConfiguration:{7};", new object[]
			{
				(serviceEndpointElement.Address == null) ? null : serviceEndpointElement.Address.ToString().ToUpperInvariant(),
				serviceEndpointElement.BindingConfiguration,
				serviceEndpointElement.BindingName,
				serviceEndpointElement.BindingNamespace,
				serviceEndpointElement.Binding,
				serviceEndpointElement.Contract,
				serviceEndpointElement.Kind,
				serviceEndpointElement.EndpointConfiguration
			});
		}
	}
}
