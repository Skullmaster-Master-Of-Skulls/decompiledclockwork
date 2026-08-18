using System;
using System.Globalization;
using System.Web.Mvc.Properties;
using System.Web.Razor.Generator;
using System.Web.Razor.Parser;
using System.Web.Razor.Text;

namespace System.Web.Mvc.Razor
{
	// Token: 0x020000CD RID: 205
	public class MvcCSharpRazorCodeParser : CSharpCodeParser
	{
		// Token: 0x06000551 RID: 1361 RVA: 0x0000ED58 File Offset: 0x0000CF58
		public MvcCSharpRazorCodeParser()
		{
			base.MapDirectives(new Action(this.ModelDirective), new string[]
			{
				"model"
			});
		}

		// Token: 0x06000552 RID: 1362 RVA: 0x0000ED8E File Offset: 0x0000CF8E
		protected override void InheritsDirective()
		{
			base.AcceptAndMoveNext();
			this._endInheritsLocation = new SourceLocation?(base.CurrentLocation);
			base.InheritsDirectiveCore();
			this.CheckForInheritsAndModelStatements();
		}

		// Token: 0x06000553 RID: 1363 RVA: 0x0000EDB4 File Offset: 0x0000CFB4
		private void CheckForInheritsAndModelStatements()
		{
			if (this._modelStatementFound && this._endInheritsLocation != null)
			{
				this.Context.OnError(this._endInheritsLocation.Value, string.Format(CultureInfo.CurrentCulture, MvcResources.MvcRazorCodeParser_CannotHaveModelAndInheritsKeyword, new object[]
				{
					"model"
				}));
			}
		}

		// Token: 0x06000554 RID: 1364 RVA: 0x0000EE0C File Offset: 0x0000D00C
		protected virtual void ModelDirective()
		{
			base.AcceptAndMoveNext();
			SourceLocation currentLocation = base.CurrentLocation;
			base.BaseTypeDirective(string.Format(CultureInfo.CurrentCulture, MvcResources.MvcRazorCodeParser_ModelKeywordMustBeFollowedByTypeName, new object[]
			{
				"model"
			}), new Func<string, SpanCodeGenerator>(this.CreateModelCodeGenerator));
			if (this._modelStatementFound)
			{
				this.Context.OnError(currentLocation, string.Format(CultureInfo.CurrentCulture, MvcResources.MvcRazorCodeParser_OnlyOneModelStatementIsAllowed, new object[]
				{
					"model"
				}));
			}
			this._modelStatementFound = true;
			this.CheckForInheritsAndModelStatements();
		}

		// Token: 0x06000555 RID: 1365 RVA: 0x0000EE98 File Offset: 0x0000D098
		private SpanCodeGenerator CreateModelCodeGenerator(string model)
		{
			return new SetModelTypeCodeGenerator(model, "{0}<{1}>");
		}

		// Token: 0x04000174 RID: 372
		private const string ModelKeyword = "model";

		// Token: 0x04000175 RID: 373
		private const string GenericTypeFormatString = "{0}<{1}>";

		// Token: 0x04000176 RID: 374
		private SourceLocation? _endInheritsLocation;

		// Token: 0x04000177 RID: 375
		private bool _modelStatementFound;
	}
}
