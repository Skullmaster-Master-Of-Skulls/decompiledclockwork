using System;
using System.ServiceModel.Description;
using System.Web.Services.Description;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace WCFExtrasPlus.Wsdl
{
	// Token: 0x02000020 RID: 32
	internal class SingleFileExporter
	{
		// Token: 0x060000BA RID: 186 RVA: 0x000052B8 File Offset: 0x000034B8
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
				XmlSchema schema = (XmlSchema)obj;
				xmlSchemas.Add(schema);
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
