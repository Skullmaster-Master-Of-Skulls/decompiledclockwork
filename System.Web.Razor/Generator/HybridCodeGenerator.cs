using System;
using System.Web.Razor.Parser.SyntaxTree;

namespace System.Web.Razor.Generator
{
	// Token: 0x02000027 RID: 39
	public abstract class HybridCodeGenerator : ISpanCodeGenerator, IBlockCodeGenerator
	{
		// Token: 0x06000170 RID: 368 RVA: 0x00005938 File Offset: 0x00003B38
		public virtual void GenerateStartBlockCode(Block target, CodeGeneratorContext context)
		{
		}

		// Token: 0x06000171 RID: 369 RVA: 0x0000593A File Offset: 0x00003B3A
		public virtual void GenerateEndBlockCode(Block target, CodeGeneratorContext context)
		{
		}

		// Token: 0x06000172 RID: 370 RVA: 0x0000593C File Offset: 0x00003B3C
		public virtual void GenerateCode(Span target, CodeGeneratorContext context)
		{
		}
	}
}
