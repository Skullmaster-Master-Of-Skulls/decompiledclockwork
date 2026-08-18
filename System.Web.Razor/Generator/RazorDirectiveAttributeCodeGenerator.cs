using System;
using System.CodeDom;
using System.Web.Razor.Parser.SyntaxTree;
using Microsoft.Internal.Web.Utils;

namespace System.Web.Razor.Generator
{
	// Token: 0x02000025 RID: 37
	public class RazorDirectiveAttributeCodeGenerator : SpanCodeGenerator
	{
		// Token: 0x06000153 RID: 339 RVA: 0x00005254 File Offset: 0x00003454
		public RazorDirectiveAttributeCodeGenerator(string name, string value)
		{
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentException(CommonResources.Argument_Cannot_Be_Null_Or_Empty, "name");
			}
			this.Name = name;
			this.Value = (value ?? string.Empty);
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000154 RID: 340 RVA: 0x0000528B File Offset: 0x0000348B
		// (set) Token: 0x06000155 RID: 341 RVA: 0x00005293 File Offset: 0x00003493
		public string Name { get; private set; }

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000156 RID: 342 RVA: 0x0000529C File Offset: 0x0000349C
		// (set) Token: 0x06000157 RID: 343 RVA: 0x000052A4 File Offset: 0x000034A4
		public string Value { get; private set; }

		// Token: 0x06000158 RID: 344 RVA: 0x000052B0 File Offset: 0x000034B0
		public override void GenerateCode(Span target, CodeGeneratorContext context)
		{
			CodeTypeReference attributeType = new CodeTypeReference(typeof(RazorDirectiveAttribute));
			CodeAttributeDeclaration value = new CodeAttributeDeclaration(attributeType, new CodeAttributeArgument[]
			{
				new CodeAttributeArgument(new CodePrimitiveExpression(this.Name)),
				new CodeAttributeArgument(new CodePrimitiveExpression(this.Value))
			});
			context.GeneratedClass.CustomAttributes.Add(value);
		}

		// Token: 0x06000159 RID: 345 RVA: 0x00005314 File Offset: 0x00003514
		public override string ToString()
		{
			return "Directive: " + this.Name + ", Value: " + this.Value;
		}

		// Token: 0x0600015A RID: 346 RVA: 0x00005334 File Offset: 0x00003534
		public override bool Equals(object obj)
		{
			RazorDirectiveAttributeCodeGenerator razorDirectiveAttributeCodeGenerator = obj as RazorDirectiveAttributeCodeGenerator;
			return razorDirectiveAttributeCodeGenerator != null && this.Name.Equals(razorDirectiveAttributeCodeGenerator.Name, StringComparison.OrdinalIgnoreCase) && this.Value.Equals(razorDirectiveAttributeCodeGenerator.Value, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x0600015B RID: 347 RVA: 0x00005373 File Offset: 0x00003573
		public override int GetHashCode()
		{
			return Tuple.Create<string, string>(this.Name.ToUpperInvariant(), this.Value.ToUpperInvariant()).GetHashCode();
		}
	}
}
