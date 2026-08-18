using System;
using System.CodeDom;
using System.Collections.Specialized;
using System.Xml.Schema;

namespace System.Xml.Serialization.Advanced
{
	// Token: 0x020001D6 RID: 470
	internal class MappedTypeDesc
	{
		// Token: 0x06001F92 RID: 8082 RVA: 0x000AAC48 File Offset: 0x000A8E48
		internal MappedTypeDesc(string clrType, string name, string ns, XmlSchemaType xsdType, XmlSchemaObject context, SchemaImporterExtension extension, CodeNamespace code, StringCollection references)
		{
			this.clrType = clrType.Replace('+', '.');
			this.name = name;
			this.ns = ns;
			this.xsdType = xsdType;
			this.context = context;
			this.code = code;
			this.references = references;
			this.extension = extension;
		}

		// Token: 0x17000684 RID: 1668
		// (get) Token: 0x06001F93 RID: 8083 RVA: 0x000AACA1 File Offset: 0x000A8EA1
		internal SchemaImporterExtension Extension
		{
			get
			{
				return this.extension;
			}
		}

		// Token: 0x17000685 RID: 1669
		// (get) Token: 0x06001F94 RID: 8084 RVA: 0x000AACA9 File Offset: 0x000A8EA9
		internal string Name
		{
			get
			{
				return this.clrType;
			}
		}

		// Token: 0x17000686 RID: 1670
		// (get) Token: 0x06001F95 RID: 8085 RVA: 0x000AACB1 File Offset: 0x000A8EB1
		internal StringCollection ReferencedAssemblies
		{
			get
			{
				if (this.references == null)
				{
					this.references = new StringCollection();
				}
				return this.references;
			}
		}

		// Token: 0x06001F96 RID: 8086 RVA: 0x000AACCC File Offset: 0x000A8ECC
		internal CodeTypeDeclaration ExportTypeDefinition(CodeNamespace codeNamespace, CodeCompileUnit codeCompileUnit)
		{
			if (this.exported)
			{
				return null;
			}
			this.exported = true;
			foreach (object obj in this.code.Imports)
			{
				CodeNamespaceImport value = (CodeNamespaceImport)obj;
				codeNamespace.Imports.Add(value);
			}
			CodeTypeDeclaration codeTypeDeclaration = null;
			string @string = Res.GetString("XmlExtensionComment", new object[]
			{
				this.extension.GetType().FullName
			});
			foreach (object obj2 in this.code.Types)
			{
				CodeTypeDeclaration codeTypeDeclaration2 = (CodeTypeDeclaration)obj2;
				if (this.clrType == codeTypeDeclaration2.Name)
				{
					if (codeTypeDeclaration != null)
					{
						throw new InvalidOperationException(Res.GetString("XmlExtensionDuplicateDefinition", new object[]
						{
							this.extension.GetType().FullName,
							this.clrType
						}));
					}
					codeTypeDeclaration = codeTypeDeclaration2;
				}
				codeTypeDeclaration2.Comments.Add(new CodeCommentStatement(@string, false));
				codeNamespace.Types.Add(codeTypeDeclaration2);
			}
			if (codeCompileUnit != null)
			{
				foreach (string value2 in this.ReferencedAssemblies)
				{
					if (!codeCompileUnit.ReferencedAssemblies.Contains(value2))
					{
						codeCompileUnit.ReferencedAssemblies.Add(value2);
					}
				}
			}
			return codeTypeDeclaration;
		}

		// Token: 0x04000D46 RID: 3398
		private string name;

		// Token: 0x04000D47 RID: 3399
		private string ns;

		// Token: 0x04000D48 RID: 3400
		private XmlSchemaType xsdType;

		// Token: 0x04000D49 RID: 3401
		private XmlSchemaObject context;

		// Token: 0x04000D4A RID: 3402
		private string clrType;

		// Token: 0x04000D4B RID: 3403
		private SchemaImporterExtension extension;

		// Token: 0x04000D4C RID: 3404
		private CodeNamespace code;

		// Token: 0x04000D4D RID: 3405
		private bool exported;

		// Token: 0x04000D4E RID: 3406
		private StringCollection references;
	}
}
