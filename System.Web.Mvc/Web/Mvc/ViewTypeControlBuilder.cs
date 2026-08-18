using System;
using System.CodeDom;
using System.Collections;
using System.Web.UI;

namespace System.Web.Mvc
{
	// Token: 0x02000191 RID: 401
	internal sealed class ViewTypeControlBuilder : ControlBuilder
	{
		// Token: 0x06000B67 RID: 2919 RVA: 0x0001E31A File Offset: 0x0001C51A
		public override void Init(TemplateParser parser, ControlBuilder parentBuilder, Type type, string tagName, string id, IDictionary attribs)
		{
			base.Init(parser, parentBuilder, type, tagName, id, attribs);
			this._typeName = (string)attribs["typename"];
		}

		// Token: 0x06000B68 RID: 2920 RVA: 0x0001E342 File Offset: 0x0001C542
		public override void ProcessGeneratedCode(CodeCompileUnit codeCompileUnit, CodeTypeDeclaration baseType, CodeTypeDeclaration derivedType, CodeMemberMethod buildMethod, CodeMemberMethod dataBindingMethod)
		{
			derivedType.BaseTypes[0] = new CodeTypeReference(this._typeName);
		}

		// Token: 0x04000308 RID: 776
		private string _typeName;
	}
}
