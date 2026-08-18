using System;
using System.CodeDom;
using System.Web.Razor.Parser.SyntaxTree;

namespace System.Web.Razor.Generator
{
	// Token: 0x0200002C RID: 44
	public class SetBaseTypeCodeGenerator : SpanCodeGenerator
	{
		// Token: 0x060001AD RID: 429 RVA: 0x00006500 File Offset: 0x00004700
		public SetBaseTypeCodeGenerator(string baseType)
		{
			this.BaseType = baseType;
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060001AE RID: 430 RVA: 0x0000650F File Offset: 0x0000470F
		// (set) Token: 0x060001AF RID: 431 RVA: 0x00006517 File Offset: 0x00004717
		public string BaseType { get; private set; }

		// Token: 0x060001B0 RID: 432 RVA: 0x0000655C File Offset: 0x0000475C
		public override void GenerateCode(Span target, CodeGeneratorContext context)
		{
			context.GeneratedClass.BaseTypes.Clear();
			context.GeneratedClass.BaseTypes.Add(new CodeTypeReference(this.ResolveType(context, this.BaseType.Trim())));
			if (context.Host.DesignTimeMode)
			{
				int generatedCodeStart = 0;
				string code = context.BuildCodeString(delegate(CodeWriter cw)
				{
					generatedCodeStart = cw.WriteVariableDeclaration(target.Content, "__inheritsHelper", null);
					cw.WriteEndStatement();
				});
				int num;
				CodeSnippetStatement statement = new CodeSnippetStatement(CodeGeneratorPaddingHelper.Pad(context.Host, code, target, generatedCodeStart, out num))
				{
					LinePragma = context.GenerateLinePragma(target, generatedCodeStart + num)
				};
				context.AddDesignTimeHelperStatement(statement);
			}
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x00006630 File Offset: 0x00004830
		protected virtual string ResolveType(CodeGeneratorContext context, string baseType)
		{
			return baseType;
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x00006633 File Offset: 0x00004833
		public override string ToString()
		{
			return "Base:" + this.BaseType;
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x00006648 File Offset: 0x00004848
		public override bool Equals(object obj)
		{
			SetBaseTypeCodeGenerator setBaseTypeCodeGenerator = obj as SetBaseTypeCodeGenerator;
			return setBaseTypeCodeGenerator != null && string.Equals(this.BaseType, setBaseTypeCodeGenerator.BaseType, StringComparison.Ordinal);
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x00006673 File Offset: 0x00004873
		public override int GetHashCode()
		{
			return this.BaseType.GetHashCode();
		}
	}
}
