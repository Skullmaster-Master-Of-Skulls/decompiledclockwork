using System;
using System.Configuration;
using Microsoft.Practices.Unity.Configuration.ConfigurationHelpers;

namespace Microsoft.Practices.Unity.Configuration
{
	// Token: 0x02000020 RID: 32
	[ConfigurationCollection(typeof(ContainerExtensionElement))]
	public class ContainerExtensionElementCollection : DeserializableConfigurationElementCollection<ContainerExtensionElement>
	{
		// Token: 0x06000101 RID: 257 RVA: 0x00004EDE File Offset: 0x000030DE
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((ContainerExtensionElement)element).TypeName;
		}
	}
}
