using System;
using System.Configuration;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;

namespace System.ServiceModel.Configuration
{
	// Token: 0x020006E2 RID: 1762
	[ConfigurationCollection(typeof(WsdlImporterElement), AddItemName = "extension")]
	public sealed class WsdlImporterElementCollection : ServiceModelEnhancedConfigurationElementCollection<WsdlImporterElement>
	{
		// Token: 0x060043F8 RID: 17400 RVA: 0x00100D57 File Offset: 0x000FEF57
		public WsdlImporterElementCollection() : base("extension")
		{
		}

		// Token: 0x060043F9 RID: 17401 RVA: 0x00100D64 File Offset: 0x000FEF64
		protected override object GetElementKey(ConfigurationElement element)
		{
			if (element == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("element");
			}
			WsdlImporterElement wsdlImporterElement = (WsdlImporterElement)element;
			return wsdlImporterElement.Type;
		}

		// Token: 0x060043FA RID: 17402 RVA: 0x00100D94 File Offset: 0x000FEF94
		internal void SetDefaults()
		{
			base.Add(new WsdlImporterElement(typeof(DataContractSerializerMessageContractImporter)));
			base.Add(new WsdlImporterElement(typeof(XmlSerializerMessageContractImporter)));
			base.Add(new WsdlImporterElement(typeof(MessageEncodingBindingElementImporter)));
			base.Add(new WsdlImporterElement(typeof(TransportBindingElementImporter)));
			base.Add(new WsdlImporterElement(typeof(StandardBindingImporter)));
			base.Add(new WsdlImporterElement("System.ServiceModel.Channels.UdpTransportImporter, System.ServiceModel.Channels, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"));
		}
	}
}
