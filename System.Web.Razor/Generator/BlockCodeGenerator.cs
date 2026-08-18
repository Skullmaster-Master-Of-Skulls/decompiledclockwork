using System;
using System.Web.Razor.Parser.SyntaxTree;

namespace System.Web.Razor.Generator
{
	// Token: 0x0200000E RID: 14
	public abstract class BlockCodeGenerator : IBlockCodeGenerator
	{
		// Token: 0x0600006B RID: 107 RVA: 0x000030FB File Offset: 0x000012FB
		public virtual void GenerateStartBlockCode(Block target, CodeGeneratorContext context)
		{
		}

		// Token: 0x0600006C RID: 108 RVA: 0x000030FD File Offset: 0x000012FD
		public virtual void GenerateEndBlockCode(Block target, CodeGeneratorContext context)
		{
		}

		// Token: 0x0600006D RID: 109 RVA: 0x000030FF File Offset: 0x000012FF
		public override bool Equals(object obj)
		{
			return obj is IBlockCodeGenerator;
		}

		// Token: 0x0600006E RID: 110 RVA: 0x0000310D File Offset: 0x0000130D
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x04000022 RID: 34
		public static readonly IBlockCodeGenerator Null = new BlockCodeGenerator.NullBlockCodeGenerator();

		// Token: 0x0200000F RID: 15
		private class NullBlockCodeGenerator : IBlockCodeGenerator
		{
			// Token: 0x06000071 RID: 113 RVA: 0x00003129 File Offset: 0x00001329
			public void GenerateStartBlockCode(Block target, CodeGeneratorContext context)
			{
			}

			// Token: 0x06000072 RID: 114 RVA: 0x0000312B File Offset: 0x0000132B
			public void GenerateEndBlockCode(Block target, CodeGeneratorContext context)
			{
			}

			// Token: 0x06000073 RID: 115 RVA: 0x0000312D File Offset: 0x0000132D
			public override string ToString()
			{
				return "None";
			}
		}
	}
}
