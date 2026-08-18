using System;
using System.Configuration;

namespace System.ServiceModel.Configuration
{
	// Token: 0x02000610 RID: 1552
	[ConfigurationCollection(typeof(CustomBindingElement), AddItemName = "binding")]
	public sealed class CustomBindingElementCollection : ServiceModelEnhancedConfigurationElementCollection<CustomBindingElement>
	{
		// Token: 0x06003BC9 RID: 15305 RVA: 0x000E4C60 File Offset: 0x000E2E60
		public CustomBindingElementCollection() : base("binding")
		{
		}

		// Token: 0x06003BCA RID: 15306 RVA: 0x000E4C70 File Offset: 0x000E2E70
		protected override object GetElementKey(ConfigurationElement element)
		{
			if (element == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("element");
			}
			CustomBindingElement customBindingElement = (CustomBindingElement)element;
			return customBindingElement.Name;
		}
	}
}
