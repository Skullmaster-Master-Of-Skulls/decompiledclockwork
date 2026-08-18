using System;
using System.CodeDom;
using System.Collections;
using System.Globalization;
using System.Web.UI;

namespace System.Web.Compilation
{
	// Token: 0x0200084B RID: 2123
	internal class MasterPageCodeDomTreeGenerator : TemplateControlCodeDomTreeGenerator
	{
		// Token: 0x17001C5F RID: 7263
		// (get) Token: 0x060064BE RID: 25790 RVA: 0x00160D4E File Offset: 0x0015EF4E
		private MasterPageParser Parser
		{
			get
			{
				return this._masterPageParser;
			}
		}

		// Token: 0x060064BF RID: 25791 RVA: 0x00160D56 File Offset: 0x0015EF56
		internal MasterPageCodeDomTreeGenerator(MasterPageParser parser) : base(parser)
		{
			this._masterPageParser = parser;
		}

		// Token: 0x060064C0 RID: 25792 RVA: 0x00160D68 File Offset: 0x0015EF68
		protected override void BuildDefaultConstructor()
		{
			base.BuildDefaultConstructor();
			foreach (object obj in ((IEnumerable)this.Parser.PlaceHolderList))
			{
				string placeHolderID = (string)obj;
				this.BuildAddContentPlaceHolderNames(base.InitMethod, placeHolderID);
			}
		}

		// Token: 0x060064C1 RID: 25793 RVA: 0x00160DD4 File Offset: 0x0015EFD4
		private void BuildAddContentPlaceHolderNames(CodeMemberMethod method, string placeHolderID)
		{
			CodePropertyReferenceExpression targetObject = new CodePropertyReferenceExpression(new CodeThisReferenceExpression(), "ContentPlaceHolders");
			CodeExpressionStatement codeExpressionStatement = new CodeExpressionStatement();
			codeExpressionStatement.Expression = new CodeMethodInvokeExpression(targetObject, "Add", new CodeExpression[]
			{
				new CodePrimitiveExpression(placeHolderID.ToLower(CultureInfo.InvariantCulture))
			});
			method.Statements.Add(codeExpressionStatement);
		}

		// Token: 0x060064C2 RID: 25794 RVA: 0x00160E2E File Offset: 0x0015F02E
		protected override void BuildMiscClassMembers()
		{
			base.BuildMiscClassMembers();
			if (this.Parser.MasterPageType != null)
			{
				base.BuildStronglyTypedProperty("Master", this.Parser.MasterPageType);
			}
		}

		// Token: 0x040033F8 RID: 13304
		private const string _masterPropertyName = "Master";

		// Token: 0x040033F9 RID: 13305
		protected MasterPageParser _masterPageParser;
	}
}
