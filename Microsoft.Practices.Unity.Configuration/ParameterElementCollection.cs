using System;
using System.Configuration;
using Microsoft.Practices.Unity.Configuration.ConfigurationHelpers;

namespace Microsoft.Practices.Unity.Configuration
{
	// Token: 0x02000031 RID: 49
	[ConfigurationCollection(typeof(ParameterElement), AddItemName = "param")]
	public class ParameterElementCollection : DeserializableConfigurationElementCollection<ParameterElement>
	{
		// Token: 0x0600017B RID: 379 RVA: 0x00005D38 File Offset: 0x00003F38
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((ParameterElement)element).Name;
		}
	}
}
