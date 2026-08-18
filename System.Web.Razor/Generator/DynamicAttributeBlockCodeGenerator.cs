using System;
using System.Globalization;
using System.Linq;
using System.Web.Razor.Parser.SyntaxTree;
using System.Web.Razor.Text;
using Microsoft.Internal.Web.Utils;

namespace System.Web.Razor.Generator
{
	// Token: 0x02000012 RID: 18
	public class DynamicAttributeBlockCodeGenerator : BlockCodeGenerator
	{
		// Token: 0x06000083 RID: 131 RVA: 0x00003411 File Offset: 0x00001611
		public DynamicAttributeBlockCodeGenerator(LocationTagged<string> prefix, int offset, int line, int col) : this(prefix, new SourceLocation(offset, line, col))
		{
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00003423 File Offset: 0x00001623
		public DynamicAttributeBlockCodeGenerator(LocationTagged<string> prefix, SourceLocation valueStart)
		{
			this.Prefix = prefix;
			this.ValueStart = valueStart;
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000085 RID: 133 RVA: 0x00003439 File Offset: 0x00001639
		// (set) Token: 0x06000086 RID: 134 RVA: 0x00003441 File Offset: 0x00001641
		public LocationTagged<string> Prefix { get; private set; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000087 RID: 135 RVA: 0x0000344A File Offset: 0x0000164A
		// (set) Token: 0x06000088 RID: 136 RVA: 0x00003452 File Offset: 0x00001652
		public SourceLocation ValueStart { get; private set; }

		// Token: 0x06000089 RID: 137 RVA: 0x0000354C File Offset: 0x0000174C
		public override void GenerateStartBlockCode(Block target, CodeGeneratorContext context)
		{
			if (context.Host.DesignTimeMode)
			{
				return;
			}
			Block block = (from n in target.Children
			where n.IsBlock
			select n).Cast<Block>().FirstOrDefault<Block>();
			string fragment;
			if (block != null && block.Type == BlockType.Expression)
			{
				this._isExpression = true;
				fragment = context.BuildCodeString(delegate(CodeWriter cw)
				{
					cw.WriteParameterSeparator();
					cw.WriteStartMethodInvoke("Tuple.Create");
					cw.WriteLocationTaggedString(this.Prefix);
					cw.WriteParameterSeparator();
					cw.WriteStartMethodInvoke("Tuple.Create", new string[]
					{
						"System.Object",
						"System.Int32"
					});
				});
				this._oldRenderingMode = context.ExpressionRenderingMode;
				context.ExpressionRenderingMode = ExpressionRenderingMode.InjectCode;
			}
			else
			{
				fragment = context.BuildCodeString(delegate(CodeWriter cw)
				{
					cw.WriteParameterSeparator();
					cw.WriteStartMethodInvoke("Tuple.Create");
					cw.WriteLocationTaggedString(this.Prefix);
					cw.WriteParameterSeparator();
					cw.WriteStartMethodInvoke("Tuple.Create", new string[]
					{
						"System.Object",
						"System.Int32"
					});
					cw.WriteStartConstructor(context.Host.GeneratedClassContext.TemplateTypeName);
					cw.WriteStartLambdaDelegate(new string[]
					{
						"__razor_attribute_value_writer"
					});
				});
			}
			context.MarkEndOfGeneratedCode();
			context.BufferStatementFragment(fragment);
			this._oldTargetWriter = context.TargetWriterName;
			context.TargetWriterName = "__razor_attribute_value_writer";
		}

		// Token: 0x0600008A RID: 138 RVA: 0x0000371C File Offset: 0x0000191C
		public override void GenerateEndBlockCode(Block target, CodeGeneratorContext context)
		{
			if (context.Host.DesignTimeMode)
			{
				return;
			}
			string generatedCode;
			if (this._isExpression)
			{
				generatedCode = context.BuildCodeString(delegate(CodeWriter cw)
				{
					cw.WriteParameterSeparator();
					cw.WriteSnippet(this.ValueStart.AbsoluteIndex.ToString(CultureInfo.CurrentCulture));
					cw.WriteEndMethodInvoke();
					cw.WriteParameterSeparator();
					cw.WriteBooleanLiteral(false);
					cw.WriteEndMethodInvoke();
					cw.WriteLineContinuation();
				});
				context.ExpressionRenderingMode = this._oldRenderingMode;
			}
			else
			{
				generatedCode = context.BuildCodeString(delegate(CodeWriter cw)
				{
					cw.WriteEndLambdaDelegate();
					cw.WriteEndConstructor();
					cw.WriteParameterSeparator();
					cw.WriteSnippet(this.ValueStart.AbsoluteIndex.ToString(CultureInfo.CurrentCulture));
					cw.WriteEndMethodInvoke();
					cw.WriteParameterSeparator();
					cw.WriteBooleanLiteral(false);
					cw.WriteEndMethodInvoke();
					cw.WriteLineContinuation();
				});
			}
			context.AddStatement(generatedCode);
			context.TargetWriterName = this._oldTargetWriter;
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00003794 File Offset: 0x00001994
		public override string ToString()
		{
			return string.Format(CultureInfo.CurrentCulture, "DynAttr:{0:F}", new object[]
			{
				this.Prefix
			});
		}

		// Token: 0x0600008C RID: 140 RVA: 0x000037C4 File Offset: 0x000019C4
		public override bool Equals(object obj)
		{
			DynamicAttributeBlockCodeGenerator dynamicAttributeBlockCodeGenerator = obj as DynamicAttributeBlockCodeGenerator;
			return dynamicAttributeBlockCodeGenerator != null && object.Equals(dynamicAttributeBlockCodeGenerator.Prefix, this.Prefix);
		}

		// Token: 0x0600008D RID: 141 RVA: 0x000037EE File Offset: 0x000019EE
		public override int GetHashCode()
		{
			return HashCodeCombiner.Start().Add(this.Prefix).CombinedHash;
		}

		// Token: 0x04000027 RID: 39
		private const string ValueWriterName = "__razor_attribute_value_writer";

		// Token: 0x04000028 RID: 40
		private string _oldTargetWriter;

		// Token: 0x04000029 RID: 41
		private bool _isExpression;

		// Token: 0x0400002A RID: 42
		private ExpressionRenderingMode _oldRenderingMode;
	}
}
