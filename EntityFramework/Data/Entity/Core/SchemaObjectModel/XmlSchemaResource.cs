using System;
using System.Collections.Generic;

namespace System.Data.Entity.Core.SchemaObjectModel
{
	// Token: 0x0200039C RID: 924
	internal struct XmlSchemaResource
	{
		// Token: 0x06002173 RID: 8563 RVA: 0x0009D69B File Offset: 0x0009B89B
		public XmlSchemaResource(string namespaceUri, string resourceName, XmlSchemaResource[] importedSchemas)
		{
			this.NamespaceUri = namespaceUri;
			this.ResourceName = resourceName;
			this.ImportedSchemas = importedSchemas;
		}

		// Token: 0x06002174 RID: 8564 RVA: 0x0009D6B2 File Offset: 0x0009B8B2
		public XmlSchemaResource(string namespaceUri, string resourceName)
		{
			this.NamespaceUri = namespaceUri;
			this.ResourceName = resourceName;
			this.ImportedSchemas = XmlSchemaResource._emptyImportList;
		}

		// Token: 0x06002175 RID: 8565 RVA: 0x0009D6D0 File Offset: 0x0009B8D0
		internal static Dictionary<string, XmlSchemaResource> GetMetadataSchemaResourceMap(double schemaVersion)
		{
			Dictionary<string, XmlSchemaResource> dictionary = new Dictionary<string, XmlSchemaResource>(StringComparer.Ordinal);
			XmlSchemaResource.AddEdmSchemaResourceMapEntries(dictionary, schemaVersion);
			XmlSchemaResource.AddStoreSchemaResourceMapEntries(dictionary, schemaVersion);
			return dictionary;
		}

		// Token: 0x06002176 RID: 8566 RVA: 0x0009D6F8 File Offset: 0x0009B8F8
		internal static void AddStoreSchemaResourceMapEntries(Dictionary<string, XmlSchemaResource> schemaResourceMap, double schemaVersion)
		{
			XmlSchemaResource[] importedSchemas = new XmlSchemaResource[]
			{
				new XmlSchemaResource("http://schemas.microsoft.com/ado/2007/12/edm/EntityStoreSchemaGenerator", "System.Data.Resources.EntityStoreSchemaGenerator.xsd")
			};
			XmlSchemaResource value = new XmlSchemaResource("http://schemas.microsoft.com/ado/2006/04/edm/ssdl", "System.Data.Resources.SSDLSchema.xsd", importedSchemas);
			schemaResourceMap.Add(value.NamespaceUri, value);
			if (schemaVersion >= 2.0)
			{
				XmlSchemaResource value2 = new XmlSchemaResource("http://schemas.microsoft.com/ado/2009/02/edm/ssdl", "System.Data.Resources.SSDLSchema_2.xsd", importedSchemas);
				schemaResourceMap.Add(value2.NamespaceUri, value2);
			}
			if (schemaVersion >= 3.0)
			{
				XmlSchemaResource value3 = new XmlSchemaResource("http://schemas.microsoft.com/ado/2009/11/edm/ssdl", "System.Data.Resources.SSDLSchema_3.xsd", importedSchemas);
				schemaResourceMap.Add(value3.NamespaceUri, value3);
			}
			XmlSchemaResource value4 = new XmlSchemaResource("http://schemas.microsoft.com/ado/2006/04/edm/providermanifest", "System.Data.Resources.ProviderServices.ProviderManifest.xsd");
			schemaResourceMap.Add(value4.NamespaceUri, value4);
		}

		// Token: 0x06002177 RID: 8567 RVA: 0x0009D7C4 File Offset: 0x0009B9C4
		internal static void AddMappingSchemaResourceMapEntries(Dictionary<string, XmlSchemaResource> schemaResourceMap, double schemaVersion)
		{
			XmlSchemaResource value = new XmlSchemaResource("urn:schemas-microsoft-com:windows:storage:mapping:CS", "System.Data.Resources.CSMSL_1.xsd");
			schemaResourceMap.Add(value.NamespaceUri, value);
			if (schemaVersion >= 2.0)
			{
				XmlSchemaResource value2 = new XmlSchemaResource("http://schemas.microsoft.com/ado/2008/09/mapping/cs", "System.Data.Resources.CSMSL_2.xsd");
				schemaResourceMap.Add(value2.NamespaceUri, value2);
			}
			if (schemaVersion >= 3.0)
			{
				XmlSchemaResource value3 = new XmlSchemaResource("http://schemas.microsoft.com/ado/2009/11/mapping/cs", "System.Data.Resources.CSMSL_3.xsd");
				schemaResourceMap.Add(value3.NamespaceUri, value3);
			}
		}

		// Token: 0x06002178 RID: 8568 RVA: 0x0009D848 File Offset: 0x0009BA48
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
			XmlSchemaResource value = new XmlSchemaResource("http://schemas.microsoft.com/ado/2006/04/edm", "System.Data.Resources.CSDLSchema_1.xsd", importedSchemas);
			schemaResourceMap.Add(value.NamespaceUri, value);
			XmlSchemaResource value2 = new XmlSchemaResource("http://schemas.microsoft.com/ado/2007/05/edm", "System.Data.Resources.CSDLSchema_1_1.xsd", importedSchemas);
			schemaResourceMap.Add(value2.NamespaceUri, value2);
			if (schemaVersion >= 2.0)
			{
				XmlSchemaResource value3 = new XmlSchemaResource("http://schemas.microsoft.com/ado/2008/09/edm", "System.Data.Resources.CSDLSchema_2.xsd", importedSchemas2);
				schemaResourceMap.Add(value3.NamespaceUri, value3);
			}
			if (schemaVersion >= 3.0)
			{
				XmlSchemaResource value4 = new XmlSchemaResource("http://schemas.microsoft.com/ado/2009/11/edm", "System.Data.Resources.CSDLSchema_3.xsd", importedSchemas3);
				schemaResourceMap.Add(value4.NamespaceUri, value4);
			}
		}

		// Token: 0x04000BCF RID: 3023
		private static readonly XmlSchemaResource[] _emptyImportList = new XmlSchemaResource[0];

		// Token: 0x04000BD0 RID: 3024
		internal string NamespaceUri;

		// Token: 0x04000BD1 RID: 3025
		internal string ResourceName;

		// Token: 0x04000BD2 RID: 3026
		internal XmlSchemaResource[] ImportedSchemas;
	}
}
