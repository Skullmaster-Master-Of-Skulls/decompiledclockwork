using System;
using System.Collections.Generic;

namespace System.Data.EntityModel.SchemaObjectModel
{
	// Token: 0x02000326 RID: 806
	internal struct XmlSchemaResource
	{
		// Token: 0x06002F73 RID: 12147 RVA: 0x000B3842 File Offset: 0x000B1A42
		public XmlSchemaResource(string namespaceUri, string resourceName, XmlSchemaResource[] importedSchemas)
		{
			this.NamespaceUri = namespaceUri;
			this.ResourceName = resourceName;
			this.ImportedSchemas = importedSchemas;
		}

		// Token: 0x06002F74 RID: 12148 RVA: 0x000B3859 File Offset: 0x000B1A59
		public XmlSchemaResource(string namespaceUri, string resourceName)
		{
			this.NamespaceUri = namespaceUri;
			this.ResourceName = resourceName;
			this.ImportedSchemas = XmlSchemaResource.EmptyImportList;
		}

		// Token: 0x06002F75 RID: 12149 RVA: 0x000B3874 File Offset: 0x000B1A74
		internal static Dictionary<string, XmlSchemaResource> GetMetadataSchemaResourceMap(double schemaVersion)
		{
			Dictionary<string, XmlSchemaResource> dictionary = new Dictionary<string, XmlSchemaResource>(StringComparer.Ordinal);
			XmlSchemaResource.AddEdmSchemaResourceMapEntries(dictionary, schemaVersion);
			XmlSchemaResource.AddStoreSchemaResourceMapEntries(dictionary, schemaVersion);
			return dictionary;
		}

		// Token: 0x06002F76 RID: 12150 RVA: 0x000B389C File Offset: 0x000B1A9C
		internal static void AddStoreSchemaResourceMapEntries(Dictionary<string, XmlSchemaResource> schemaResourceMap, double schemaVersion)
		{
			XmlSchemaResource[] importedSchemas = new XmlSchemaResource[]
			{
				new XmlSchemaResource("http://schemas.microsoft.com/ado/2007/12/edm/EntityStoreSchemaGenerator", "System.Data.Resources.EntityStoreSchemaGenerator.xsd")
			};
			XmlSchemaResource xmlSchemaResource = new XmlSchemaResource("http://schemas.microsoft.com/ado/2006/04/edm/ssdl", "System.Data.Resources.SSDLSchema.xsd", importedSchemas);
			schemaResourceMap.Add(xmlSchemaResource.NamespaceUri, xmlSchemaResource);
			if (schemaVersion >= 2.0)
			{
				XmlSchemaResource xmlSchemaResource2 = new XmlSchemaResource("http://schemas.microsoft.com/ado/2009/02/edm/ssdl", "System.Data.Resources.SSDLSchema_2.xsd", importedSchemas);
				schemaResourceMap.Add(xmlSchemaResource2.NamespaceUri, xmlSchemaResource2);
			}
			if (schemaVersion >= 3.0)
			{
				XmlSchemaResource xmlSchemaResource3 = new XmlSchemaResource("http://schemas.microsoft.com/ado/2009/11/edm/ssdl", "System.Data.Resources.SSDLSchema_3.xsd", importedSchemas);
				schemaResourceMap.Add(xmlSchemaResource3.NamespaceUri, xmlSchemaResource3);
			}
			XmlSchemaResource xmlSchemaResource4 = new XmlSchemaResource("http://schemas.microsoft.com/ado/2006/04/edm/providermanifest", "System.Data.Resources.ProviderServices.ProviderManifest.xsd");
			schemaResourceMap.Add(xmlSchemaResource4.NamespaceUri, xmlSchemaResource4);
		}

		// Token: 0x06002F77 RID: 12151 RVA: 0x000B395C File Offset: 0x000B1B5C
		internal static void AddMappingSchemaResourceMapEntries(Dictionary<string, XmlSchemaResource> schemaResourceMap, double schemaVersion)
		{
			XmlSchemaResource xmlSchemaResource = new XmlSchemaResource("urn:schemas-microsoft-com:windows:storage:mapping:CS", "System.Data.Resources.CSMSL_1.xsd");
			schemaResourceMap.Add(xmlSchemaResource.NamespaceUri, xmlSchemaResource);
			if (schemaVersion >= 2.0)
			{
				XmlSchemaResource xmlSchemaResource2 = new XmlSchemaResource("http://schemas.microsoft.com/ado/2008/09/mapping/cs", "System.Data.Resources.CSMSL_2.xsd");
				schemaResourceMap.Add(xmlSchemaResource2.NamespaceUri, xmlSchemaResource2);
			}
			if (schemaVersion >= 3.0)
			{
				XmlSchemaResource xmlSchemaResource3 = new XmlSchemaResource("http://schemas.microsoft.com/ado/2009/11/mapping/cs", "System.Data.Resources.CSMSL_3.xsd");
				schemaResourceMap.Add(xmlSchemaResource3.NamespaceUri, xmlSchemaResource3);
			}
		}

		// Token: 0x06002F78 RID: 12152 RVA: 0x000B39DC File Offset: 0x000B1BDC
		internal static void AddEdmSchemaResourceMapEntries(Dictionary<string, XmlSchemaResource> schemaResourceMap, double schemaVersion)
		{
			XmlSchemaResource[] importedSchemas = new XmlSchemaResource[]
			{
				new XmlSchemaResource("http://schemas.microsoft.com/ado/2006/04/codegeneration", "System.Data.Resources.CodeGenerationSchema.xsd")
			};
			XmlSchemaResource[] importedSchemas2 = new XmlSchemaResource[]
			{
				new XmlSchemaResource("http://schemas.microsoft.com/ado/2006/04/codegeneration", "System.Data.Resources.CodeGenerationSchema.xsd"),
				new XmlSchemaResource("http://schemas.microsoft.com/ado/2009/02/edm/annotation", "System.Data.Resources.AnnotationSchema.xsd")
			};
			XmlSchemaResource[] importedSchemas3 = new XmlSchemaResource[]
			{
				new XmlSchemaResource("http://schemas.microsoft.com/ado/2006/04/codegeneration", "System.Data.Resources.CodeGenerationSchema.xsd"),
				new XmlSchemaResource("http://schemas.microsoft.com/ado/2009/02/edm/annotation", "System.Data.Resources.AnnotationSchema.xsd")
			};
			XmlSchemaResource xmlSchemaResource = new XmlSchemaResource("http://schemas.microsoft.com/ado/2006/04/edm", "System.Data.Resources.CSDLSchema_1.xsd", importedSchemas);
			schemaResourceMap.Add(xmlSchemaResource.NamespaceUri, xmlSchemaResource);
			XmlSchemaResource xmlSchemaResource2 = new XmlSchemaResource("http://schemas.microsoft.com/ado/2007/05/edm", "System.Data.Resources.CSDLSchema_1_1.xsd", importedSchemas);
			schemaResourceMap.Add(xmlSchemaResource2.NamespaceUri, xmlSchemaResource2);
			if (schemaVersion >= 2.0)
			{
				XmlSchemaResource xmlSchemaResource3 = new XmlSchemaResource("http://schemas.microsoft.com/ado/2008/09/edm", "System.Data.Resources.CSDLSchema_2.xsd", importedSchemas2);
				schemaResourceMap.Add(xmlSchemaResource3.NamespaceUri, xmlSchemaResource3);
			}
			if (schemaVersion >= 3.0)
			{
				XmlSchemaResource xmlSchemaResource4 = new XmlSchemaResource("http://schemas.microsoft.com/ado/2009/11/edm", "System.Data.Resources.CSDLSchema_3.xsd", importedSchemas3);
				schemaResourceMap.Add(xmlSchemaResource4.NamespaceUri, xmlSchemaResource4);
			}
		}

		// Token: 0x04001461 RID: 5217
		private static XmlSchemaResource[] EmptyImportList = new XmlSchemaResource[0];

		// Token: 0x04001462 RID: 5218
		internal string NamespaceUri;

		// Token: 0x04001463 RID: 5219
		internal string ResourceName;

		// Token: 0x04001464 RID: 5220
		internal XmlSchemaResource[] ImportedSchemas;
	}
}
