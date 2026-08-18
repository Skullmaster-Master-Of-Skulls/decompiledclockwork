using System;
using System.Configuration;

namespace System.ServiceModel.Configuration
{
	// Token: 0x020006D4 RID: 1748
	public sealed class StandardBindingElementCollection<TBindingConfiguration> : ServiceModelEnhancedConfigurationElementCollection<TBindingConfiguration> where TBindingConfiguration : StandardBindingElement, new()
	{
		// Token: 0x060043B5 RID: 17333 RVA: 0x000FFE23 File Offset: 0x000FE023
		public StandardBindingElementCollection() : base("binding")
		{
		}

		// Token: 0x060043B6 RID: 17334 RVA: 0x000FFE30 File Offset: 0x000FE030
		protected override object GetElementKey(ConfigurationElement element)
		{
			if (element == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("element");
			}
			TBindingConfiguration tbindingConfiguration = (TBindingConfiguration)((object)element);
			return tbindingConfiguration.Name;
		}
	}
}
