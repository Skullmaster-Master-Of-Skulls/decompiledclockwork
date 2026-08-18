using System;
using System.CodeDom;
using System.Globalization;
using System.Web.Razor.Parser.SyntaxTree;
using System.Web.Razor.Text;
using Microsoft.Internal.Web.Utils;

namespace System.Web.Razor.Generator
{
	// Token: 0x0200002D RID: 45
	public class HelperCodeGenerator : BlockCodeGenerator
	{
		// Token: 0x060001B5 RID: 437 RVA: 0x00006680 File Offset: 0x00004880
		public HelperCodeGenerator(LocationTagged<string> signature, bool headerComplete)
		{
			this.Signature = signature;
			this.HeaderComplete = headerComplete;
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060001B6 RID: 438 RVA: 0x00006696 File Offset: 0x00004896
		// (set) Token: 0x060001B7 RID: 439 RVA: 0x0000669E File Offset: 0x0000489E
		public LocationTagged<string> Signature { get; private set; }

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060001B8 RID: 440 RVA: 0x000066A7 File Offset: 0x000048A7
		// (set) Token: 0x060001B9 RID: 441 RVA: 0x000066AF File Offset: 0x000048AF
		public LocationTagged<string> Footer { get; set; }

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060001BA RID: 442 RVA: 0x000066B8 File Offset: 0x000048B8
		// (set) Token: 0x060001BB RID: 443 RVA: 0x000066C0 File Offset: 0x000048C0
		public bool HeaderComplete { get; private set; }

		// Token: 0x060001BC RID: 444 RVA: 0x00006710 File Offset: 0x00004910
		public override void GenerateStartBlockCode(Block target, CodeGeneratorContext context)
		{
			this._writer = context.CreateCodeWriter();
			string text = context.BuildCodeString(delegate(CodeWriter cw)
			{
				cw.WriteHelperHeaderPrefix(context.Host.GeneratedClassContext.TemplateTypeName, context.Host.StaticHelpers);
			});
			this._writer.WriteLinePragma(context.GenerateLinePragma(this.Signature.Location, text.Length, this.Signature.Value.Length));
			this._writer.WriteSnippet(text);
			this._writer.WriteSnippet(this.Signature);
			if (this.HeaderComplete)
			{
				this._writer.WriteHelperHeaderSuffix(context.Host.GeneratedClassContext.TemplateTypeName);
			}
			this._writer.WriteLinePragma(null);
			if (this.HeaderComplete)
			{
				this._writer.WriteReturn();
				this._writer.WriteStartConstructor(context.Host.GeneratedClassContext.TemplateTypeName);
				this._writer.WriteStartLambdaDelegate(new string[]
				{
					"__razor_helper_writer"
				});
			}
			this._statementCollectorToken = context.ChangeStatementCollector(new Action<string, CodeLinePragma>(this.AddStatementToHelper));
			this._oldWriter = context.TargetWriterName;
			context.TargetWriterName = "__razor_helper_writer";
		}

		// Token: 0x060001BD RID: 445 RVA: 0x00006878 File Offset: 0x00004A78
		public override void GenerateEndBlockCode(Block target, CodeGeneratorContext context)
		{
			this._statementCollectorToken.Dispose();
			if (this.HeaderComplete)
			{
				this._writer.WriteEndLambdaDelegate();
				this._writer.WriteEndConstructor();
				this._writer.WriteEndStatement();
			}
			if (this.Footer != null && !string.IsNullOrEmpty(this.Footer.Value))
			{
				this._writer.WriteLinePragma(context.GenerateLinePragma(this.Footer.Location, 0, this.Footer.Value.Length));
				this._writer.WriteSnippet(this.Footer);
				this._writer.WriteLinePragma();
			}
			this._writer.WriteHelperTrailer();
			context.GeneratedClass.Members.Add(new CodeSnippetTypeMember(this._writer.Content));
			context.TargetWriterName = this._oldWriter;
		}

		// Token: 0x060001BE RID: 446 RVA: 0x00006960 File Offset: 0x00004B60
		public override bool Equals(object obj)
		{
			HelperCodeGenerator helperCodeGenerator = obj as HelperCodeGenerator;
			return helperCodeGenerator != null && base.Equals(helperCodeGenerator) && this.HeaderComplete == helperCodeGenerator.HeaderComplete && object.Equals(this.Signature, helperCodeGenerator.Signature);
		}

		// Token: 0x060001BF RID: 447 RVA: 0x000069A1 File Offset: 0x00004BA1
		public override int GetHashCode()
		{
			return HashCodeCombiner.Start().Add(base.GetHashCode()).Add(this.Signature).CombinedHash;
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x000069C3 File Offset: 0x00004BC3
		public override string ToString()
		{
			return "Helper:" + this.Signature.ToString("F", CultureInfo.CurrentCulture) + ";" + (this.HeaderComplete ? "C" : "I");
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x000069FD File Offset: 0x00004BFD
		private void AddStatementToHelper(string statement, CodeLinePragma pragma)
		{
			if (pragma != null)
			{
				this._writer.WriteLinePragma(pragma);
			}
			this._writer.WriteSnippet(statement);
			this._writer.InnerWriter.WriteLine();
			if (pragma != null)
			{
				this._writer.WriteLinePragma();
			}
		}

		// Token: 0x04000078 RID: 120
		private const string HelperWriterName = "__razor_helper_writer";

		// Token: 0x04000079 RID: 121
		private CodeWriter _writer;

		// Token: 0x0400007A RID: 122
		private string _oldWriter;

		// Token: 0x0400007B RID: 123
		private IDisposable _statementCollectorToken;
	}
}
