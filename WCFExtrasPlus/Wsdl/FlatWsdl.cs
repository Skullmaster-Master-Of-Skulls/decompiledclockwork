using System;
using System.Collections;
using System.Collections.Generic;
using System.ServiceModel.Description;
using System.Web.Services.Description;
using System.Xml.Schema;

namespace WCFExtrasPlus.Wsdl
{
	// Token: 0x0200001E RID: 30
	public class FlatWsdl
	{
		// Token: 0x060000B0 RID: 176 RVA: 0x00004BE8 File Offset: 0x00002DE8
		internal static void ExportEndpoint(WsdlExporter exporter)
		{
			XmlSchemaSet generatedXmlSchemas = exporter.GeneratedXmlSchemas;
			foreach (object obj in exporter.GeneratedWsdlDocuments)
			{
				System.Web.Services.Description.ServiceDescription serviceDescription = (System.Web.Services.Description.ServiceDescription)obj;
				List<XmlSchema> list = new List<XmlSchema>();
				foreach (object obj2 in serviceDescription.Types.Schemas)
				{
					XmlSchema schema = (XmlSchema)obj2;
					FlatWsdl.AddImportedSchemas(schema, generatedXmlSchemas, list);
				}
				serviceDescription.Types.Schemas.Clear();
				foreach (XmlSchema schema2 in list)
				{
					FlatWsdl.RemoveXsdImports(schema2);
					serviceDescription.Types.Schemas.Add(schema2);
				}
			}
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00004D0C File Offset: 0x00002F0C
		private static void AddImportedSchemas(XmlSchema schema, XmlSchemaSet schemaSet, List<XmlSchema> importsList)
		{
			foreach (XmlSchemaObject xmlSchemaObject in schema.Includes)
			{
				XmlSchemaImport xmlSchemaImport = (XmlSchemaImport)xmlSchemaObject;
				ICollection collection = schemaSet.Schemas(xmlSchemaImport.Namespace);
				foreach (object obj in collection)
				{
					XmlSchema xmlSchema = (XmlSchema)obj;
					if (!importsList.Contains(xmlSchema))
					{
						importsList.Add(xmlSchema);
						FlatWsdl.AddImportedSchemas(xmlSchema, schemaSet, importsList);
					}
				}
			}
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00004DCC File Offset: 0x00002FCC
		private static void RemoveXsdImports(XmlSchema schema)
		{
			for (int i = 0; i < schema.Includes.Count; i++)
			{
				if (schema.Includes[i] is XmlSchemaImport)
				{
					schema.Includes.RemoveAt(i--);
				}
			}
		}
	}
}
