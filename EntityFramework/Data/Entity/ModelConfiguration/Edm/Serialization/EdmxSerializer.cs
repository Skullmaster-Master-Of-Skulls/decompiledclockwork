using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Globalization;
using System.Xml;

namespace System.Data.Entity.ModelConfiguration.Edm.Serialization
{
	// Token: 0x0200081B RID: 2075
	internal sealed class EdmxSerializer
	{
		// Token: 0x06005D52 RID: 23890 RVA: 0x00192B84 File Offset: 0x00190D84
		public void Serialize(DbDatabaseMapping databaseMapping, XmlWriter xmlWriter)
		{
			this._xmlWriter = xmlWriter;
			this._databaseMapping = databaseMapping;
			this._version = databaseMapping.Model.SchemaVersion;
			this._namespace = (object.Equals(this._version, 3.0) ? "http://schemas.microsoft.com/ado/2009/11/edmx" : (object.Equals(this._version, 2.0) ? "http://schemas.microsoft.com/ado/2008/10/edmx" : "http://schemas.microsoft.com/ado/2007/06/edmx"));
			this._xmlWriter.WriteStartDocument();
			using (this.Element("Edmx", new string[]
			{
				"Version",
				string.Format(CultureInfo.InvariantCulture, "{0:F1}", new object[]
				{
					this._version
				})
			}))
			{
				this.WriteEdmxRuntime();
				this.WriteEdmxDesigner();
			}
			this._xmlWriter.WriteEndDocument();
			this._xmlWriter.Flush();
		}

		// Token: 0x06005D53 RID: 23891 RVA: 0x00192C98 File Offset: 0x00190E98
		private void WriteEdmxRuntime()
		{
			using (this.Element("Runtime", new string[0]))
			{
				using (this.Element("ConceptualModels", new string[0]))
				{
					this._databaseMapping.Model.ValidateAndSerializeCsdl(this._xmlWriter);
				}
				using (this.Element("Mappings", new string[0]))
				{
					new MslSerializer().Serialize(this._databaseMapping, this._xmlWriter);
				}
				using (this.Element("StorageModels", new string[0]))
				{
					new SsdlSerializer().Serialize(this._databaseMapping.Database, this._databaseMapping.ProviderInfo.ProviderInvariantName, this._databaseMapping.ProviderInfo.ProviderManifestToken, this._xmlWriter, true);
				}
			}
		}

		// Token: 0x06005D54 RID: 23892 RVA: 0x00192DBC File Offset: 0x00190FBC
		private void WriteEdmxDesigner()
		{
			using (this.Element("Designer", new string[0]))
			{
				this.WriteEdmxConnection();
				this.WriteEdmxOptions();
				this.WriteEdmxDiagrams();
			}
		}

		// Token: 0x06005D55 RID: 23893 RVA: 0x00192E0C File Offset: 0x0019100C
		private void WriteEdmxConnection()
		{
			using (this.Element("Connection", new string[0]))
			{
				using (this.Element("DesignerInfoPropertySet", new string[0]))
				{
					this.WriteDesignerPropertyElement("MetadataArtifactProcessing", "EmbedInOutputAssembly");
				}
			}
		}

		// Token: 0x06005D56 RID: 23894 RVA: 0x00192E84 File Offset: 0x00191084
		private void WriteEdmxOptions()
		{
			using (this.Element("Options", new string[0]))
			{
				using (this.Element("DesignerInfoPropertySet", new string[0]))
				{
					this.WriteDesignerPropertyElement("ValidateOnBuild", "False");
					this.WriteDesignerPropertyElement("CodeGenerationStrategy", "None");
					this.WriteDesignerPropertyElement("ProcessDependentTemplatesOnSave", "False");
					this.WriteDesignerPropertyElement("UseLegacyProvider", "False");
				}
			}
		}

		// Token: 0x06005D57 RID: 23895 RVA: 0x00192F2C File Offset: 0x0019112C
		private void WriteDesignerPropertyElement(string name, string value)
		{
			using (this.Element("DesignerProperty", new string[]
			{
				"Name",
				name,
				"Value",
				value
			}))
			{
			}
		}

		// Token: 0x06005D58 RID: 23896 RVA: 0x00192F84 File Offset: 0x00191184
		private void WriteEdmxDiagrams()
		{
			using (this.Element("Diagrams", new string[0]))
			{
			}
		}

		// Token: 0x06005D59 RID: 23897 RVA: 0x00192FC0 File Offset: 0x001911C0
		private IDisposable Element(string elementName, params string[] attributes)
		{
			this._xmlWriter.WriteStartElement(elementName, this._namespace);
			for (int i = 0; i < attributes.Length - 1; i += 2)
			{
				this._xmlWriter.WriteAttributeString(attributes[i], attributes[i + 1]);
			}
			return new EdmxSerializer.EndElement(this._xmlWriter);
		}

		// Token: 0x040024E2 RID: 9442
		private const string EdmXmlNamespaceV1 = "http://schemas.microsoft.com/ado/2007/06/edmx";

		// Token: 0x040024E3 RID: 9443
		private const string EdmXmlNamespaceV2 = "http://schemas.microsoft.com/ado/2008/10/edmx";

		// Token: 0x040024E4 RID: 9444
		private const string EdmXmlNamespaceV3 = "http://schemas.microsoft.com/ado/2009/11/edmx";

		// Token: 0x040024E5 RID: 9445
		private DbDatabaseMapping _databaseMapping;

		// Token: 0x040024E6 RID: 9446
		private double _version;

		// Token: 0x040024E7 RID: 9447
		private XmlWriter _xmlWriter;

		// Token: 0x040024E8 RID: 9448
		private string _namespace;

		// Token: 0x0200081C RID: 2076
		private class EndElement : IDisposable
		{
			// Token: 0x06005D5B RID: 23899 RVA: 0x00193015 File Offset: 0x00191215
			public EndElement(XmlWriter xmlWriter)
			{
				this._xmlWriter = xmlWriter;
			}

			// Token: 0x06005D5C RID: 23900 RVA: 0x00193024 File Offset: 0x00191224
			public void Dispose()
			{
				this._xmlWriter.WriteEndElement();
			}

			// Token: 0x040024E9 RID: 9449
			private readonly XmlWriter _xmlWriter;
		}
	}
}
