using System;
using System.Configuration;
using Microsoft.Practices.Unity.Configuration.ConfigurationHelpers;

namespace Microsoft.Practices.Unity.Configuration
{
	// Token: 0x0200003B RID: 59
	[ConfigurationCollection(typeof(SectionExtensionElement))]
	public class SectionExtensionElementCollection : DeserializableConfigurationElementCollection<SectionExtensionElement>
	{
		// Token: 0x060001DA RID: 474 RVA: 0x000066B4 File Offset: 0x000048B4
		protected override object GetElementKey(ConfigurationElement element)
		{
			SectionExtensionElement sectionExtensionElement = (SectionExtensionElement)element;
			string text = sectionExtensionElement.Prefix;
			if (!string.IsNullOrEmpty(text))
			{
				text += ".";
			}
			return text + sectionExtensionElement.TypeName;
		}
	}
}
