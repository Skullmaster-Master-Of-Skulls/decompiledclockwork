using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Xml.Schema;

namespace System.Xml.Serialization.Advanced
{
	// Token: 0x020001D4 RID: 468
	public abstract class SchemaImporterExtension
	{
		// Token: 0x06001F7E RID: 8062 RVA: 0x000AAA1D File Offset: 0x000A8C1D
		public virtual string ImportSchemaType(string name, string ns, XmlSchemaObject context, XmlSchemas schemas, XmlSchemaImporter importer, CodeCompileUnit compileUnit, CodeNamespace mainNamespace, CodeGenerationOptions options, CodeDomProvider codeProvider)
		{
			return null;
		}

		// Token: 0x06001F7F RID: 8063 RVA: 0x000AAA20 File Offset: 0x000A8C20
		public virtual string ImportSchemaType(XmlSchemaType type, XmlSchemaObject context, XmlSchemas schemas, XmlSchemaImporter importer, CodeCompileUnit compileUnit, CodeNamespace mainNamespace, CodeGenerationOptions options, CodeDomProvider codeProvider)
		{
			return null;
		}

		// Token: 0x06001F80 RID: 8064 RVA: 0x000AAA23 File Offset: 0x000A8C23
		public virtual string ImportAnyElement(XmlSchemaAny any, bool mixed, XmlSchemas schemas, XmlSchemaImporter importer, CodeCompileUnit compileUnit, CodeNamespace mainNamespace, CodeGenerationOptions options, CodeDomProvider codeProvider)
		{
			return null;
		}

		// Token: 0x06001F81 RID: 8065 RVA: 0x000AAA26 File Offset: 0x000A8C26
		public virtual CodeExpression ImportDefaultValue(string value, string type)
		{
			return null;
		}
	}
}
