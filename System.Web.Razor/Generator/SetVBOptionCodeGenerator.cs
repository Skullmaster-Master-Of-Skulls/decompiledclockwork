using System;
using System.Web.Razor.Parser.SyntaxTree;

namespace System.Web.Razor.Generator
{
	// Token: 0x0200002F RID: 47
	public class SetVBOptionCodeGenerator : SpanCodeGenerator
	{
		// Token: 0x060001CB RID: 459 RVA: 0x00006BAC File Offset: 0x00004DAC
		public SetVBOptionCodeGenerator(string optionName, bool value)
		{
			this.OptionName = optionName;
			this.Value = value;
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060001CC RID: 460 RVA: 0x00006BC2 File Offset: 0x00004DC2
		// (set) Token: 0x060001CD RID: 461 RVA: 0x00006BCA File Offset: 0x00004DCA
		public string OptionName { get; private set; }

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060001CE RID: 462 RVA: 0x00006BD3 File Offset: 0x00004DD3
		// (set) Token: 0x060001CF RID: 463 RVA: 0x00006BDB File Offset: 0x00004DDB
		public bool Value { get; private set; }

		// Token: 0x060001D0 RID: 464 RVA: 0x00006BE4 File Offset: 0x00004DE4
		public static SetVBOptionCodeGenerator Strict(bool onOffValue)
		{
			return new SetVBOptionCodeGenerator(SetVBOptionCodeGenerator.StrictCodeDomOptionName, !onOffValue);
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x00006BF4 File Offset: 0x00004DF4
		public static SetVBOptionCodeGenerator Explicit(bool onOffValue)
		{
			return new SetVBOptionCodeGenerator(SetVBOptionCodeGenerator.ExplicitCodeDomOptionName, onOffValue);
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x00006C01 File Offset: 0x00004E01
		public override void GenerateCode(Span target, CodeGeneratorContext context)
		{
			context.CompileUnit.UserData[this.OptionName] = this.Value;
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x00006C24 File Offset: 0x00004E24
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				"Option:",
				this.OptionName,
				"=",
				this.Value
			});
		}

		// Token: 0x04000081 RID: 129
		public static readonly string StrictCodeDomOptionName = "AllowLateBound";

		// Token: 0x04000082 RID: 130
		public static readonly string ExplicitCodeDomOptionName = "RequireVariableDeclaration";
	}
}
