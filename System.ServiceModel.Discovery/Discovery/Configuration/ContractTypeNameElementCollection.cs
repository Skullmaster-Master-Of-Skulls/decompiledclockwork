using System;
using System.Configuration;
using System.ServiceModel.Configuration;
using System.Xml;

namespace System.ServiceModel.Discovery.Configuration
{
	// Token: 0x020000AF RID: 175
	[ConfigurationCollection(typeof(ContractTypeNameElement))]
	public sealed class ContractTypeNameElementCollection : ServiceModelConfigurationElementCollection<ContractTypeNameElement>
	{
		// Token: 0x06000733 RID: 1843 RVA: 0x0001293C File Offset: 0x00010B3C
		protected override object GetElementKey(ConfigurationElement element)
		{
			ContractTypeNameElement contractTypeNameElement = (ContractTypeNameElement)element;
			return new XmlQualifiedName(contractTypeNameElement.Name, contractTypeNameElement.Namespace);
		}
	}
}
