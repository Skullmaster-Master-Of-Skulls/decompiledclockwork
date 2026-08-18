using System;
using System.Web.Razor.Parser.SyntaxTree;

namespace System.Web.Razor.Generator
{
	// Token: 0x02000014 RID: 20
	public interface ISpanCodeGenerator
	{
		// Token: 0x06000092 RID: 146
		void GenerateCode(Span target, CodeGeneratorContext context);
	}
}
