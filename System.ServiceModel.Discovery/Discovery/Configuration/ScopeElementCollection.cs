using System;
using System.Configuration;
using System.ServiceModel.Configuration;

namespace System.ServiceModel.Discovery.Configuration
{
	// Token: 0x020000BB RID: 187
	[ConfigurationCollection(typeof(ScopeElement))]
	public sealed class ScopeElementCollection : ServiceModelConfigurationElementCollection<ScopeElement>
	{
		// Token: 0x06000781 RID: 1921 RVA: 0x00013868 File Offset: 0x00011A68
		protected override object GetElementKey(ConfigurationElement element)
		{
			if (element == null)
			{
				throw FxTrace.Exception.ArgumentNull("element");
			}
			return ((ScopeElement)element).Scope;
		}
	}
}
