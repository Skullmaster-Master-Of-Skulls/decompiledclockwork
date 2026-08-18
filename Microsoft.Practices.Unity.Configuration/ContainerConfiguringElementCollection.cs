using System;
using System.Configuration;
using Microsoft.Practices.Unity.Configuration.ConfigurationHelpers;
using Microsoft.Practices.Unity.Configuration.Properties;

namespace Microsoft.Practices.Unity.Configuration
{
	// Token: 0x0200001D RID: 29
	[ConfigurationCollection(typeof(ContainerConfiguringElement))]
	public class ContainerConfiguringElementCollection : DeserializableConfigurationElementCollectionBase<ContainerConfiguringElement>
	{
		// Token: 0x060000E5 RID: 229 RVA: 0x00004B48 File Offset: 0x00002D48
		protected override ConfigurationElement CreateNewElement()
		{
			throw new InvalidOperationException(Resources.CannotCreateContainerConfiguringElement);
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00004B54 File Offset: 0x00002D54
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((ContainerConfiguringElement)element).Key;
		}
	}
}
