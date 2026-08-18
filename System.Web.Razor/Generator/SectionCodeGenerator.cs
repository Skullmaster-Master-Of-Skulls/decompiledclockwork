using System;
using System.Web.Razor.Parser.SyntaxTree;
using Microsoft.Internal.Web.Utils;

namespace System.Web.Razor.Generator
{
	// Token: 0x0200002E RID: 46
	public class SectionCodeGenerator : BlockCodeGenerator
	{
		// Token: 0x060001C2 RID: 450 RVA: 0x00006A38 File Offset: 0x00004C38
		public SectionCodeGenerator(string sectionName)
		{
			this.SectionName = sectionName;
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060001C3 RID: 451 RVA: 0x00006A47 File Offset: 0x00004C47
		// (set) Token: 0x060001C4 RID: 452 RVA: 0x00006A4F File Offset: 0x00004C4F
		public string SectionName { get; private set; }

		// Token: 0x060001C5 RID: 453 RVA: 0x00006AB0 File Offset: 0x00004CB0
		public override void GenerateStartBlockCode(Block target, CodeGeneratorContext context)
		{
			string generatedCode = context.BuildCodeString(delegate(CodeWriter cw)
			{
				cw.WriteStartMethodInvoke(context.Host.GeneratedClassContext.DefineSectionMethodName);
				cw.WriteStringLiteral(this.SectionName);
				cw.WriteParameterSeparator();
				cw.WriteStartLambdaDelegate(new string[0]);
			});
			context.AddStatement(generatedCode);
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x00006B0C File Offset: 0x00004D0C
		public override void GenerateEndBlockCode(Block target, CodeGeneratorContext context)
		{
			string generatedCode = context.BuildCodeString(delegate(CodeWriter cw)
			{
				cw.WriteEndLambdaDelegate();
				cw.WriteEndMethodInvoke();
				cw.WriteEndStatement();
			});
			context.AddStatement(generatedCode);
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x00006B44 File Offset: 0x00004D44
		public override bool Equals(object obj)
		{
			SectionCodeGenerator sectionCodeGenerator = obj as SectionCodeGenerator;
			return sectionCodeGenerator != null && base.Equals(sectionCodeGenerator) && string.Equals(this.SectionName, sectionCodeGenerator.SectionName, StringComparison.Ordinal);
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x00006B78 File Offset: 0x00004D78
		public override int GetHashCode()
		{
			return HashCodeCombiner.Start().Add(base.GetHashCode()).Add(this.SectionName).CombinedHash;
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x00006B9A File Offset: 0x00004D9A
		public override string ToString()
		{
			return "Section:" + this.SectionName;
		}
	}
}
