using System;
using System.Configuration;
using Microsoft.Practices.Unity.Configuration.ConfigurationHelpers;

namespace Microsoft.Practices.Unity.Configuration
{
	// Token: 0x0200000B RID: 11
	[ConfigurationCollection(typeof(AssemblyElement))]
	public class AssemblyElementCollection : DeserializableConfigurationElementCollection<AssemblyElement>
	{
		// Token: 0x06000030 RID: 48 RVA: 0x00002692 File Offset: 0x00000892
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((AssemblyElement)element).Name;
		}
	}
}
