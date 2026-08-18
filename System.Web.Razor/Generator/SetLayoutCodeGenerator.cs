using System;
using System.CodeDom;
using System.Web.Razor.Parser.SyntaxTree;

namespace System.Web.Razor.Generator
{
	// Token: 0x02000019 RID: 25
	public class SetLayoutCodeGenerator : SpanCodeGenerator
	{
		// Token: 0x060000AB RID: 171 RVA: 0x00003BCB File Offset: 0x00001DCB
		public SetLayoutCodeGenerator(string layoutPath)
		{
			this.LayoutPath = layoutPath;
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000AC RID: 172 RVA: 0x00003BDA File Offset: 0x00001DDA
		// (set) Token: 0x060000AD RID: 173 RVA: 0x00003BE2 File Offset: 0x00001DE2
		public string LayoutPath { get; set; }

		// Token: 0x060000AE RID: 174 RVA: 0x00003BEC File Offset: 0x00001DEC
		public override void GenerateCode(Span target, CodeGeneratorContext context)
		{
			if (!context.Host.DesignTimeMode && !string.IsNullOrEmpty(context.Host.GeneratedClassContext.LayoutPropertyName))
			{
				context.TargetMethod.Statements.Add(new CodeAssignStatement(new CodePropertyReferenceExpression(null, context.Host.GeneratedClassContext.LayoutPropertyName), new CodePrimitiveExpression(this.LayoutPath)));
			}
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00003C5A File Offset: 0x00001E5A
		public override string ToString()
		{
			return "Layout: " + this.LayoutPath;
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00003C6C File Offset: 0x00001E6C
		public override bool Equals(object obj)
		{
			SetLayoutCodeGenerator setLayoutCodeGenerator = obj as SetLayoutCodeGenerator;
			return setLayoutCodeGenerator != null && string.Equals(setLayoutCodeGenerator.LayoutPath, this.LayoutPath, StringComparison.Ordinal);
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00003C97 File Offset: 0x00001E97
		public override int GetHashCode()
		{
			return this.LayoutPath.GetHashCode();
		}
	}
}
