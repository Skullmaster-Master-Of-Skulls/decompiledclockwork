using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using System.Xml.Serialization.Advanced;

namespace System.Data.SqlTypes
{
	// Token: 0x02000169 RID: 361
	public class SqlTypesSchemaImporterExtensionHelper : SchemaImporterExtension
	{
		// Token: 0x0600175F RID: 5983 RVA: 0x000A80C0 File Offset: 0x000A74C0
		public SqlTypesSchemaImporterExtensionHelper(string name, string targetNamespace, string[] references, CodeNamespaceImport[] namespaceImports, string destinationType, bool direct)
		{
			this.Init(name, targetNamespace, references, namespaceImports, destinationType, direct);
		}

		// Token: 0x06001760 RID: 5984 RVA: 0x000A80E4 File Offset: 0x000A74E4
		public SqlTypesSchemaImporterExtensionHelper(string name, string destinationType)
		{
			this.Init(name, SqlTypesSchemaImporterExtensionHelper.SqlTypesNamespace, null, null, destinationType, true);
		}

		// Token: 0x06001761 RID: 5985 RVA: 0x000A8108 File Offset: 0x000A7508
		public SqlTypesSchemaImporterExtensionHelper(string name, string destinationType, bool direct)
		{
			this.Init(name, SqlTypesSchemaImporterExtensionHelper.SqlTypesNamespace, null, null, destinationType, direct);
		}

		// Token: 0x06001762 RID: 5986 RVA: 0x000A812C File Offset: 0x000A752C
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

		// Token: 0x06001763 RID: 5987 RVA: 0x000A81BC File Offset: 0x000A75BC
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

		// Token: 0x06001764 RID: 5988 RVA: 0x000A8224 File Offset: 0x000A7624
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

		// Token: 0x04000E3E RID: 3646
		private string m_name;

		// Token: 0x04000E3F RID: 3647
		private string m_targetNamespace;

		// Token: 0x04000E40 RID: 3648
		private string[] m_references;

		// Token: 0x04000E41 RID: 3649
		private CodeNamespaceImport[] m_namespaceImports;

		// Token: 0x04000E42 RID: 3650
		private string m_destinationType;

		// Token: 0x04000E43 RID: 3651
		private bool m_direct;

		// Token: 0x04000E44 RID: 3652
		protected static readonly string SqlTypesNamespace = "http://schemas.microsoft.com/sqlserver/2004/sqltypes";
	}
}
