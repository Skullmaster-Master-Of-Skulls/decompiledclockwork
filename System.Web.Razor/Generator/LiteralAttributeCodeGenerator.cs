using System;
using System.Globalization;
using System.Web.Razor.Parser.SyntaxTree;
using System.Web.Razor.Text;
using Microsoft.Internal.Web.Utils;

namespace System.Web.Razor.Generator
{
	// Token: 0x02000017 RID: 23
	public class LiteralAttributeCodeGenerator : SpanCodeGenerator
	{
		// Token: 0x0600009B RID: 155 RVA: 0x00003842 File Offset: 0x00001A42
		public LiteralAttributeCodeGenerator(LocationTagged<string> prefix, LocationTagged<SpanCodeGenerator> valueGenerator)
		{
			this.Prefix = prefix;
			this.ValueGenerator = valueGenerator;
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00003858 File Offset: 0x00001A58
		public LiteralAttributeCodeGenerator(LocationTagged<string> prefix, LocationTagged<string> value)
		{
			this.Prefix = prefix;
			this.Value = value;
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600009D RID: 157 RVA: 0x0000386E File Offset: 0x00001A6E
		// (set) Token: 0x0600009E RID: 158 RVA: 0x00003876 File Offset: 0x00001A76
		public LocationTagged<string> Prefix { get; private set; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600009F RID: 159 RVA: 0x0000387F File Offset: 0x00001A7F
		// (set) Token: 0x060000A0 RID: 160 RVA: 0x00003887 File Offset: 0x00001A87
		public LocationTagged<string> Value { get; private set; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000A1 RID: 161 RVA: 0x00003890 File Offset: 0x00001A90
		// (set) Token: 0x060000A2 RID: 162 RVA: 0x00003898 File Offset: 0x00001A98
		public LocationTagged<SpanCodeGenerator> ValueGenerator { get; private set; }

		// Token: 0x060000A3 RID: 163 RVA: 0x000039A8 File Offset: 0x00001BA8
		public override void GenerateCode(Span target, CodeGeneratorContext context)
		{
			if (context.Host.DesignTimeMode)
			{
				return;
			}
			ExpressionRenderingMode expressionRenderingMode = context.ExpressionRenderingMode;
			context.BufferStatementFragment(context.BuildCodeString(delegate(CodeWriter cw)
			{
				cw.WriteParameterSeparator();
				cw.WriteStartMethodInvoke("Tuple.Create");
				cw.WriteLocationTaggedString(this.Prefix);
				cw.WriteParameterSeparator();
				if (this.ValueGenerator != null)
				{
					cw.WriteStartMethodInvoke("Tuple.Create", new string[]
					{
						"System.Object",
						"System.Int32"
					});
					context.ExpressionRenderingMode = ExpressionRenderingMode.InjectCode;
					return;
				}
				cw.WriteLocationTaggedString(this.Value);
				cw.WriteParameterSeparator();
				cw.WriteBooleanLiteral(true);
				cw.WriteEndMethodInvoke();
				cw.WriteLineContinuation();
			}));
			if (this.ValueGenerator != null)
			{
				this.ValueGenerator.Value.GenerateCode(target, context);
				context.FlushBufferedStatement();
				context.ExpressionRenderingMode = expressionRenderingMode;
				context.AddStatement(context.BuildCodeString(delegate(CodeWriter cw)
				{
					cw.WriteParameterSeparator();
					cw.WriteSnippet(this.ValueGenerator.Location.AbsoluteIndex.ToString(CultureInfo.CurrentCulture));
					cw.WriteEndMethodInvoke();
					cw.WriteParameterSeparator();
					cw.WriteBooleanLiteral(false);
					cw.WriteEndMethodInvoke();
					cw.WriteLineContinuation();
				}));
				return;
			}
			context.FlushBufferedStatement();
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00003A7C File Offset: 0x00001C7C
		public override string ToString()
		{
			if (this.ValueGenerator == null)
			{
				return string.Format(CultureInfo.CurrentCulture, "LitAttr:{0:F},{1:F}", new object[]
				{
					this.Prefix,
					this.Value
				});
			}
			return string.Format(CultureInfo.CurrentCulture, "LitAttr:{0:F},<Sub:{1:F}>", new object[]
			{
				this.Prefix,
				this.ValueGenerator
			});
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00003AEC File Offset: 0x00001CEC
		public override bool Equals(object obj)
		{
			LiteralAttributeCodeGenerator literalAttributeCodeGenerator = obj as LiteralAttributeCodeGenerator;
			return literalAttributeCodeGenerator != null && object.Equals(literalAttributeCodeGenerator.Prefix, this.Prefix) && object.Equals(literalAttributeCodeGenerator.Value, this.Value) && object.Equals(literalAttributeCodeGenerator.ValueGenerator, this.ValueGenerator);
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00003B3C File Offset: 0x00001D3C
		public override int GetHashCode()
		{
			return HashCodeCombiner.Start().Add(this.Prefix).Add(this.Value).Add(this.ValueGenerator).CombinedHash;
		}
	}
}
