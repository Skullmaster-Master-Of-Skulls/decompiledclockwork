using System;
using System.Configuration;

namespace System.ServiceModel.Configuration
{
	// Token: 0x020006DF RID: 1759
	[ConfigurationCollection(typeof(TransportConfigurationTypeElement))]
	public sealed class TransportConfigurationTypeElementCollection : ServiceModelConfigurationElementCollection<TransportConfigurationTypeElement>
	{
		// Token: 0x060043E9 RID: 17385 RVA: 0x001007C1 File Offset: 0x000FE9C1
		public TransportConfigurationTypeElementCollection() : base(ConfigurationElementCollectionType.AddRemoveClearMap, null)
		{
		}

		// Token: 0x060043EA RID: 17386 RVA: 0x001007CC File Offset: 0x000FE9CC
		protected override object GetElementKey(ConfigurationElement element)
		{
			if (element == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("element");
			}
			TransportConfigurationTypeElement transportConfigurationTypeElement = (TransportConfigurationTypeElement)element;
			return transportConfigurationTypeElement.Name;
		}
	}
}
