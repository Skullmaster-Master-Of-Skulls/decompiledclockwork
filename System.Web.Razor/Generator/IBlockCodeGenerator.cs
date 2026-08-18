using System;
using System.Web.Razor.Parser.SyntaxTree;

namespace System.Web.Razor.Generator
{
	// Token: 0x0200000D RID: 13
	public interface IBlockCodeGenerator
	{
		// Token: 0x06000069 RID: 105
		void GenerateStartBlockCode(Block target, CodeGeneratorContext context);

		// Token: 0x0600006A RID: 106
		void GenerateEndBlockCode(Block target, CodeGeneratorContext context);
	}
}
