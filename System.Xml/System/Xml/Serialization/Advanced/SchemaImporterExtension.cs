using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Xml.Schema;

namespace System.Xml.Serialization.Advanced
{
	// Token: 0x02000348 RID: 840
	public abstract class SchemaImporterExtension
	{
		// Token: 0x060028D4 RID: 10452 RVA: 0x000D1F48 File Offset: 0x000D0F48
		public virtual string ImportSchemaType(string name, string ns, XmlSchemaObject context, XmlSchemas schemas, XmlSchemaImporter importer, CodeCompileUnit compileUnit, CodeNamespace mainNamespace, CodeGenerationOptions options, CodeDomProvider codeProvider)
		{
			return null;
		}

		// Token: 0x060028D5 RID: 10453 RVA: 0x000D1F4B File Offset: 0x000D0F4B
		public virtual string ImportSchemaType(XmlSchemaType type, XmlSchemaObject context, XmlSchemas schemas, XmlSchemaImporter importer, CodeCompileUnit compileUnit, CodeNamespace mainNamespace, CodeGenerationOptions options, CodeDomProvider codeProvider)
		{
			return null;
		}

		// Token: 0x060028D6 RID: 10454 RVA: 0x000D1F4E File Offset: 0x000D0F4E
		public virtual string ImportAnyElement(XmlSchemaAny any, bool mixed, XmlSchemas schemas, XmlSchemaImporter importer, CodeCompileUnit compileUnit, CodeNamespace mainNamespace, CodeGenerationOptions options, CodeDomProvider codeProvider)
		{
			return null;
		}

		// Token: 0x060028D7 RID: 10455 RVA: 0x000D1F51 File Offset: 0x000D0F51
		public virtual CodeExpression ImportDefaultValue(string value, string type)
		{
			return null;
		}
	}
}
