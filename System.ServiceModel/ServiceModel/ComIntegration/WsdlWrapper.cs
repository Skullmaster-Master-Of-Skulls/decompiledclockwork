using System;
using System.IO;
using System.Web.Services.Description;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System.ServiceModel.ComIntegration
{
	// Token: 0x020001F4 RID: 500
	internal class WsdlWrapper : IXmlSerializable
	{
		// Token: 0x06000FCA RID: 4042 RVA: 0x00038CC4 File Offset: 0x00036EC4
		public WsdlWrapper(ServiceDescription wsdl)
		{
			this.wsdl = wsdl;
		}

		// Token: 0x06000FCB RID: 4043 RVA: 0x00038CD4 File Offset: 0x00036ED4
		public void WriteXml(XmlWriter xmlWriter)
		{
			if (this.wsdl != null)
			{
				using (MemoryStream memoryStream = new MemoryStream())
				{
					this.wsdl.Write(memoryStream);
					XmlDictionaryReaderQuotas xmlDictionaryReaderQuotas = new XmlDictionaryReaderQuotas();
					xmlDictionaryReaderQuotas.MaxDepth = 32;
					xmlDictionaryReaderQuotas.MaxStringContentLength = 8192;
					xmlDictionaryReaderQuotas.MaxArrayLength = 16384;
					xmlDictionaryReaderQuotas.MaxBytesPerRead = 4096;
					xmlDictionaryReaderQuotas.MaxNameTableCharCount = 16384;
					memoryStream.Seek(0L, SeekOrigin.Begin);
					XmlDictionaryReader xmlDictionaryReader = XmlDictionaryReader.CreateTextReader(memoryStream, null, xmlDictionaryReaderQuotas, null);
					if (xmlDictionaryReader.MoveToContent() == XmlNodeType.Element && xmlDictionaryReader.Name == "wsdl:definitions")
					{
						xmlWriter.WriteNode(xmlDictionaryReader, false);
					}
					xmlDictionaryReader.Close();
				}
			}
		}

		// Token: 0x06000FCC RID: 4044 RVA: 0x00038D94 File Offset: 0x00036F94
		public void ReadXml(XmlReader xmlReader)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotImplementedException());
		}

		// Token: 0x06000FCD RID: 4045 RVA: 0x00038DA5 File Offset: 0x00036FA5
		public XmlSchema GetSchema()
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotImplementedException());
		}

		// Token: 0x040017E0 RID: 6112
		private ServiceDescription wsdl;
	}
}
