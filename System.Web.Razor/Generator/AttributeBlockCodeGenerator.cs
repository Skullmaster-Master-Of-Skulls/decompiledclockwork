using System;
using System.Globalization;
using System.Web.Razor.Parser.SyntaxTree;
using System.Web.Razor.Text;
using Microsoft.Internal.Web.Utils;

namespace System.Web.Razor.Generator
{
	// Token: 0x02000010 RID: 16
	public class AttributeBlockCodeGenerator : BlockCodeGenerator
	{
		// Token: 0x06000075 RID: 117 RVA: 0x0000313C File Offset: 0x0000133C
		public AttributeBlockCodeGenerator(string name, LocationTagged<string> prefix, LocationTagged<string> suffix)
		{
			this.Name = name;
			this.Prefix = prefix;
			this.Suffix = suffix;
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000076 RID: 118 RVA: 0x00003159 File Offset: 0x00001359
		// (set) Token: 0x06000077 RID: 119 RVA: 0x00003161 File Offset: 0x00001361
		public string Name { get; private set; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000078 RID: 120 RVA: 0x0000316A File Offset: 0x0000136A
		// (set) Token: 0x06000079 RID: 121 RVA: 0x00003172 File Offset: 0x00001372
		public LocationTagged<string> Prefix { get; private set; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600007A RID: 122 RVA: 0x0000317B File Offset: 0x0000137B
		// (set) Token: 0x0600007B RID: 123 RVA: 0x00003183 File Offset: 0x00001383
		public LocationTagged<string> Suffix { get; private set; }

		// Token: 0x0600007C RID: 124 RVA: 0x00003250 File Offset: 0x00001450
		public override void GenerateStartBlockCode(Block target, CodeGeneratorContext context)
		{
			if (context.Host.DesignTimeMode)
			{
				return;
			}
			context.FlushBufferedStatement();
			context.AddStatement(context.BuildCodeString(delegate(CodeWriter cw)
			{
				if (!string.IsNullOrEmpty(context.TargetWriterName))
				{
					cw.WriteStartMethodInvoke(context.Host.GeneratedClassContext.WriteAttributeToMethodName);
					cw.WriteSnippet(context.TargetWriterName);
					cw.WriteParameterSeparator();
				}
				else
				{
					cw.WriteStartMethodInvoke(context.Host.GeneratedClassContext.WriteAttributeMethodName);
				}
				cw.WriteStringLiteral(this.Name);
				cw.WriteParameterSeparator();
				cw.WriteLocationTaggedString(this.Prefix);
				cw.WriteParameterSeparator();
				cw.WriteLocationTaggedString(this.Suffix);
				cw.WriteLineContinuation();
			}));
		}

		// Token: 0x0600007D RID: 125 RVA: 0x000032BF File Offset: 0x000014BF
		public override void GenerateEndBlockCode(Block target, CodeGeneratorContext context)
		{
			if (context.Host.DesignTimeMode)
			{
				return;
			}
			context.FlushBufferedStatement();
			context.AddStatement(context.BuildCodeString(delegate(CodeWriter cw)
			{
				cw.WriteEndMethodInvoke();
				cw.WriteEndStatement();
			}));
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00003300 File Offset: 0x00001500
		public override string ToString()
		{
			return string.Format(CultureInfo.CurrentCulture, "Attr:{0},{1:F},{2:F}", new object[]
			{
				this.Name,
				this.Prefix,
				this.Suffix
			});
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00003340 File Offset: 0x00001540
		public override bool Equals(object obj)
		{
			AttributeBlockCodeGenerator attributeBlockCodeGenerator = obj as AttributeBlockCodeGenerator;
			return attributeBlockCodeGenerator != null && string.Equals(attributeBlockCodeGenerator.Name, this.Name, StringComparison.Ordinal) && object.Equals(attributeBlockCodeGenerator.Prefix, this.Prefix) && object.Equals(attributeBlockCodeGenerator.Suffix, this.Suffix);
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00003391 File Offset: 0x00001591
		public override int GetHashCode()
		{
			return HashCodeCombiner.Start().Add(this.Name).Add(this.Prefix).Add(this.Suffix).CombinedHash;
		}
	}
}
