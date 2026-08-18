using System;
using System.Configuration;
using Microsoft.Practices.Unity.Configuration.ConfigurationHelpers;

namespace Microsoft.Practices.Unity.Configuration
{
	// Token: 0x02000029 RID: 41
	[ConfigurationCollection(typeof(InstanceElement))]
	public class InstanceElementCollection : DeserializableConfigurationElementCollection<InstanceElement>
	{
		// Token: 0x06000143 RID: 323 RVA: 0x00005605 File Offset: 0x00003805
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((InstanceElement)element).Key;
		}
	}
}
