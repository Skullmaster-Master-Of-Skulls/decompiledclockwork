using System;
using System.Configuration;
using Microsoft.Practices.Unity.Configuration.ConfigurationHelpers;
using Microsoft.Practices.Unity.Configuration.Properties;

namespace Microsoft.Practices.Unity.Configuration
{
	// Token: 0x02000024 RID: 36
	[ConfigurationCollection(typeof(ExtensionConfigurationElement))]
	public class ExtensionConfigurationElementCollection : DeserializableConfigurationElementCollectionBase<ExtensionConfigurationElement>
	{
		// Token: 0x06000114 RID: 276 RVA: 0x000050DF File Offset: 0x000032DF
		protected override ConfigurationElement CreateNewElement()
		{
			throw new InvalidOperationException(Resources.CannotCreateExtensionConfigurationElement);
		}

		// Token: 0x06000115 RID: 277 RVA: 0x000050EB File Offset: 0x000032EB
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((ExtensionConfigurationElement)element).Key;
		}
	}
}
