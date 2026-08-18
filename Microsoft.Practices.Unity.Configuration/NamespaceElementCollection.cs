using System;
using System.Configuration;
using Microsoft.Practices.Unity.Configuration.ConfigurationHelpers;

namespace Microsoft.Practices.Unity.Configuration
{
	// Token: 0x0200002E RID: 46
	[ConfigurationCollection(typeof(NamespaceElement))]
	public class NamespaceElementCollection : DeserializableConfigurationElementCollection<NamespaceElement>
	{
		// Token: 0x06000162 RID: 354 RVA: 0x00005A54 File Offset: 0x00003C54
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((NamespaceElement)element).Name;
		}
	}
}
