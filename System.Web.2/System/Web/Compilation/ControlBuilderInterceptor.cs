using System;
using System.CodeDom;
using System.Collections;
using System.Web.UI;

namespace System.Web.Compilation
{
	// Token: 0x020007F1 RID: 2033
	public abstract class ControlBuilderInterceptor
	{
		// Token: 0x060060F2 RID: 24818 RVA: 0x00006164 File Offset: 0x00004364
		public virtual void PreControlBuilderInit(ControlBuilder controlBuilder, TemplateParser parser, ControlBuilder parentBuilder, Type type, string tagName, string id, IDictionary attributes, IDictionary additionalState)
		{
		}

		// Token: 0x060060F3 RID: 24819 RVA: 0x00006164 File Offset: 0x00004364
		public virtual void OnProcessGeneratedCode(ControlBuilder controlBuilder, CodeCompileUnit codeCompileUnit, CodeTypeDeclaration baseType, CodeTypeDeclaration derivedType, CodeMemberMethod buildMethod, CodeMemberMethod dataBindingMethod, IDictionary additionalState)
		{
		}
	}
}
