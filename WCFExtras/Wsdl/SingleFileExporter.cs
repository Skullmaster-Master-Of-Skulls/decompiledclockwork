using System;
using System.ServiceModel.Description;
using System.Web.Services.Description;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace WCFExtras.Wsdl
{
	// Token: 0x0200000C RID: 12
	internal class SingleFileExporter
	{
		// Token: 0x06000038 RID: 56 RVA: 0x00003064 File Offset: 0x00001264
		internal static void ExportEndpoint(WsdlExporter wsdlExporter)
		{
			if (wsdlExporter.GeneratedWsdlDocuments.Count > 1)
			{
				throw new ApplicationException("Single file option is not supported in multiple wsdl files");
			}
			System.Web.Services.Description.ServiceDescription serviceDescription = wsdlExporter.GeneratedWsdlDocuments[0];
			XmlSchemas xmlSchemas = new XmlSchemas();
			foreach (object obj in wsdlExporter.GeneratedXmlSchemas.Schemas())
			{
				XmlSchema xmlSchema = (XmlSchema)obj;
				xmlSchemas.Add(xmlSchema);
			}
			foreach (object obj2 in xmlSchemas)
			{
				XmlSchema xmlSchema = (XmlSchema)obj2;
				xmlSchema.Includes.Clear();
			}
			serviceDescription.Types.Schemas.Clear();
			serviceDescription.Types.Schemas.Add(xmlSchemas);
		}
	}
}
