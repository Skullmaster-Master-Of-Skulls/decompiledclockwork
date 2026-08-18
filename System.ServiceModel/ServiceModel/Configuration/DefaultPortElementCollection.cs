using System;
using System.Configuration;

namespace System.ServiceModel.Configuration
{
	// Token: 0x02000614 RID: 1556
	[ConfigurationCollection(typeof(DefaultPortElement), AddItemName = "add")]
	public sealed class DefaultPortElementCollection : ServiceModelEnhancedConfigurationElementCollection<DefaultPortElement>
	{
		// Token: 0x06003BE8 RID: 15336 RVA: 0x000E52EF File Offset: 0x000E34EF
		public DefaultPortElementCollection() : base("add")
		{
		}

		// Token: 0x06003BE9 RID: 15337 RVA: 0x000E52FC File Offset: 0x000E34FC
		protected override object GetElementKey(ConfigurationElement element)
		{
			if (element == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("element");
			}
			DefaultPortElement defaultPortElement = (DefaultPortElement)element;
			return defaultPortElement.Scheme;
		}
	}
}
