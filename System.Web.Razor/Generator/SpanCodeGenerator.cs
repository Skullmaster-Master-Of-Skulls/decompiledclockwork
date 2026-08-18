using System;
using System.Web.Razor.Parser.SyntaxTree;

namespace System.Web.Razor.Generator
{
	// Token: 0x02000015 RID: 21
	public abstract class SpanCodeGenerator : ISpanCodeGenerator
	{
		// Token: 0x06000093 RID: 147 RVA: 0x00003805 File Offset: 0x00001A05
		public virtual void GenerateCode(Span target, CodeGeneratorContext context)
		{
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00003807 File Offset: 0x00001A07
		public override bool Equals(object obj)
		{
			return obj is ISpanCodeGenerator;
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00003815 File Offset: 0x00001A15
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x04000031 RID: 49
		public static readonly ISpanCodeGenerator Null = new SpanCodeGenerator.NullSpanCodeGenerator();

		// Token: 0x02000016 RID: 22
		private class NullSpanCodeGenerator : ISpanCodeGenerator
		{
			// Token: 0x06000098 RID: 152 RVA: 0x00003831 File Offset: 0x00001A31
			public void GenerateCode(Span target, CodeGeneratorContext context)
			{
			}

			// Token: 0x06000099 RID: 153 RVA: 0x00003833 File Offset: 0x00001A33
			public override string ToString()
			{
				return "None";
			}
		}
	}
}
