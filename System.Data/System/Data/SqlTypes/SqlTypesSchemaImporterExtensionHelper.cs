using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using System.Xml.Serialization.Advanced;

namespace System.Data.SqlTypes
{
	// Token: 0x02000358 RID: 856
	public class SqlTypesSchemaImporterExtensionHelper : SchemaImporterExtension
	{
		// Token: 0x06002F09 RID: 12041 RVA: 0x002D3448 File Offset: 0x002D2848
		public SqlTypesSchemaImporterExtensionHelper(string name, string targetNamespace, string[] references, CodeNamespaceImport[] namespaceImports, string destinationType, bool direct)
		{
			this.Init(name, targetNamespace, references, namespaceImports, destinationType, direct);
		}

		// Token: 0x06002F0A RID: 12042 RVA: 0x002D3478 File Offset: 0x002D2878
		public SqlTypesSchemaImporterExtensionHelper(string name, string destinationType)
		{
			this.Init(name, SqlTypesSchemaImporterExtensionHelper.SqlTypesNamespace, null, null, destinationType, true);
		}

		// Token: 0x06002F0B RID: 12043 RVA: 0x002D34A8 File Offset: 0x002D28A8
		public SqlTypesSchemaImporterExtensionHelper(string name, string destinationType, bool direct)
		{
			this.Init(name, SqlTypesSchemaImporterExtensionHelper.SqlTypesNamespace, null, null, destinationType, direct);
		}

		// Token: 0x06002F0C RID: 12044 RVA: 0x002D34D8 File Offset: 0x002D28D8
		private void Init(string name, string targetNamespace, string[] references, CodeNamespaceImport[] namespaceImports, string destinationType, bool direct)
		{
			this.m_name = name;
			this.m_targetNamespace = targetNamespace;
			if (references == null)
			{
				this.m_references = new string[1];
				this.m_references[0] = "System.Data.dll";
			}
			else
			{
				this.m_references = references;
			}
			if (namespaceImports == null)
			{
				this.m_namespaceImports = new CodeNamespaceImport[2];
				this.m_namespaceImports[0] = new CodeNamespaceImport("System.Data");
				this.m_namespaceImports[1] = new CodeNamespaceImport("System.Data.SqlTypes");
			}
			else
			{
				this.m_namespaceImports = namespaceImports;
			}
			this.m_destinationType = destinationType;
			this.m_direct = direct;
		}

		// Token: 0x06002F0D RID: 12045 RVA: 0x002D3568 File Offset: 0x002D2968
		public override string ImportSchemaType(string name, string xmlNamespace, XmlSchemaObject context, XmlSchemas schemas, XmlSchemaImporter importer, CodeCompileUnit compileUnit, CodeNamespace mainNamespace, CodeGenerationOptions options, CodeDomProvider codeProvider)
		{
			if (this.m_direct && context is XmlSchemaElement && string.CompareOrdinal(this.m_name, name) == 0 && string.CompareOrdinal(this.m_targetNamespace, xmlNamespace) == 0)
			{
				compileUnit.ReferencedAssemblies.AddRange(this.m_references);
				mainNamespace.Imports.AddRange(this.m_namespaceImports);
				return this.m_destinationType;
			}
			return null;
		}

		// Token: 0x06002F0E RID: 12046 RVA: 0x002D35D8 File Offset: 0x002D29D8
		public override string ImportSchemaType(XmlSchemaType type, XmlSchemaObject context, XmlSchemas schemas, XmlSchemaImporter importer, CodeCompileUnit compileUnit, CodeNamespace mainNamespace, CodeGenerationOptions options, CodeDomProvider codeProvider)
		{
			if (!this.m_direct && type is XmlSchemaSimpleType && context is XmlSchemaElement)
			{
				XmlSchemaType baseXmlSchemaType = ((XmlSchemaSimpleType)type).BaseXmlSchemaType;
				XmlQualifiedName qualifiedName = baseXmlSchemaType.QualifiedName;
				if (string.CompareOrdinal(this.m_name, qualifiedName.Name) == 0 && string.CompareOrdinal(this.m_targetNamespace, qualifiedName.Namespace) == 0)
				{
					compileUnit.ReferencedAssemblies.AddRange(this.m_references);
					mainNamespace.Imports.AddRange(this.m_namespaceImports);
					return this.m_destinationType;
				}
			}
			return null;
		}

		// Token: 0x04001D55 RID: 7509
		private string m_name;

		// Token: 0x04001D56 RID: 7510
		private string m_targetNamespace;

		// Token: 0x04001D57 RID: 7511
		private string[] m_references;

		// Token: 0x04001D58 RID: 7512
		private CodeNamespaceImport[] m_namespaceImports;

		// Token: 0x04001D59 RID: 7513
		private string m_destinationType;

		// Token: 0x04001D5A RID: 7514
		private bool m_direct;

		// Token: 0x04001D5B RID: 7515
		protected static readonly string SqlTypesNamespace = "http://schemas.microsoft.com/sqlserver/2004/sqltypes";
	}
}
