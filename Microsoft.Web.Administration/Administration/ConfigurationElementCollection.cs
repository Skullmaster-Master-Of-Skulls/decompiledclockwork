using System;

namespace Microsoft.Web.Administration
{
	// Token: 0x02000023 RID: 35
	public class ConfigurationElementCollection : ConfigurationElementCollectionBase<ConfigurationElement>
	{
		// Token: 0x06000193 RID: 403 RVA: 0x00005F9F File Offset: 0x00004F9F
		protected override ConfigurationElement CreateNewElement(string elementTagName)
		{
			return new ConfigurationElement();
		}
	}
}
