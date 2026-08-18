using System;
using System.Globalization;
using System.Linq;
using System.Web.Mvc.Properties;
using System.Web.Razor.Generator;
using System.Web.Razor.Parser;
using System.Web.Razor.Parser.SyntaxTree;
using System.Web.Razor.Text;
using System.Web.Razor.Tokenizer.Symbols;

namespace System.Web.Mvc.Razor
{
	// Token: 0x020000CF RID: 207
	public class MvcVBRazorCodeParser : VBCodeParser
	{
		// Token: 0x0600055C RID: 1372 RVA: 0x0000EEE5 File Offset: 0x0000D0E5
		public MvcVBRazorCodeParser()
		{
			base.MapDirective("ModelType", new Func<bool>(this.ModelTypeDirective));
		}

		// Token: 0x0600055D RID: 1373 RVA: 0x0000EF08 File Offset: 0x0000D108
		protected override bool InheritsStatement()
		{
			VBSymbol currentSymbol = base.CurrentSymbol;
			base.NextToken();
			this._endInheritsLocation = new SourceLocation?(base.CurrentLocation);
			base.PutCurrentBack();
			base.PutBack(currentSymbol);
			base.EnsureCurrent();
			bool result = base.InheritsStatement();
			this.CheckForInheritsAndModelStatements();
			return result;
		}

		// Token: 0x0600055E RID: 1374 RVA: 0x0000EF58 File Offset: 0x0000D158
		private void CheckForInheritsAndModelStatements()
		{
			if (this._modelStatementFound && this._endInheritsLocation != null)
			{
				this.Context.OnError(this._endInheritsLocation.Value, string.Format(CultureInfo.CurrentCulture, MvcResources.MvcRazorCodeParser_CannotHaveModelAndInheritsKeyword, new object[]
				{
					"ModelType"
				}));
			}
		}

		// Token: 0x0600055F RID: 1375 RVA: 0x0000EFB8 File Offset: 0x0000D1B8
		protected virtual bool ModelTypeDirective()
		{
			base.Span.CodeGenerator = SpanCodeGenerator.Null;
			this.Context.CurrentBlock.Type = new BlockType?(BlockType.Directive);
			base.AcceptAndMoveNext();
			SourceLocation currentLocation = base.CurrentLocation;
			if (base.At(VBSymbolType.WhiteSpace))
			{
				base.Span.EditHandler.AcceptedCharacters = AcceptedCharacters.None;
			}
			base.AcceptWhile(VBSymbolType.WhiteSpace);
			base.Output(SpanKind.MetaCode);
			if (this._modelStatementFound)
			{
				this.Context.OnError(currentLocation, string.Format(CultureInfo.CurrentCulture, MvcResources.MvcRazorCodeParser_OnlyOneModelStatementIsAllowed, new object[]
				{
					"ModelType"
				}));
			}
			this._modelStatementFound = true;
			if (base.EndOfFile || base.At(VBSymbolType.WhiteSpace) || base.At(VBSymbolType.NewLine))
			{
				this.Context.OnError(currentLocation, MvcResources.MvcRazorCodeParser_ModelKeywordMustBeFollowedByTypeName, new object[]
				{
					"ModelType"
				});
			}
			base.AcceptUntil(VBSymbolType.NewLine);
			if (!this.Context.DesignTimeMode)
			{
				base.Optional(VBSymbolType.NewLine);
			}
			string modelType = string.Concat(from s in base.Span.Symbols
			select s.Content).Trim();
			base.Span.CodeGenerator = new SetModelTypeCodeGenerator(modelType, "{0}(Of {1})");
			this.CheckForInheritsAndModelStatements();
			base.Output(SpanKind.Code);
			return false;
		}

		// Token: 0x0400017A RID: 378
		internal const string ModelTypeKeyword = "ModelType";

		// Token: 0x0400017B RID: 379
		private const string GenericTypeFormatString = "{0}(Of {1})";

		// Token: 0x0400017C RID: 380
		private SourceLocation? _endInheritsLocation;

		// Token: 0x0400017D RID: 381
		private bool _modelStatementFound;
	}
}
