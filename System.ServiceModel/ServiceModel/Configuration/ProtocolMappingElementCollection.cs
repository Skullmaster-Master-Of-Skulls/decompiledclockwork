using System;
using System.Configuration;

namespace System.ServiceModel.Configuration
{
	// Token: 0x020006C1 RID: 1729
	[ConfigurationCollection(typeof(ProtocolMappingElement), AddItemName = "add")]
	public sealed class ProtocolMappingElementCollection : ServiceModelEnhancedConfigurationElementCollection<ProtocolMappingElement>
	{
		// Token: 0x06004317 RID: 17175 RVA: 0x000FD520 File Offset: 0x000FB720
		public ProtocolMappingElementCollection() : base("add")
		{
		}

		// Token: 0x06004318 RID: 17176 RVA: 0x000FD530 File Offset: 0x000FB730
		protected override object GetElementKey(ConfigurationElement element)
		{
			if (element == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("element");
			}
			ProtocolMappingElement protocolMappingElement = (ProtocolMappingElement)element;
			return protocolMappingElement.Scheme;
		}
	}
}
