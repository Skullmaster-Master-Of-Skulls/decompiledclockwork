using System;
using System.CodeDom;
using System.Web.UI;

namespace System.Web.Mvc
{
	// Token: 0x02000187 RID: 391
	internal sealed class ViewMasterPageControlBuilder : FileLevelMasterPageControlBuilder, IMvcControlBuilder
	{
		// Token: 0x17000288 RID: 648
		// (get) Token: 0x06000AF4 RID: 2804 RVA: 0x0001D9EE File Offset: 0x0001BBEE
		// (set) Token: 0x06000AF5 RID: 2805 RVA: 0x0001D9F6 File Offset: 0x0001BBF6
		public string Inherits { get; set; }

		// Token: 0x06000AF6 RID: 2806 RVA: 0x0001D9FF File Offset: 0x0001BBFF
		public override void ProcessGeneratedCode(CodeCompileUnit codeCompileUnit, CodeTypeDeclaration baseType, CodeTypeDeclaration derivedType, CodeMemberMethod buildMethod, CodeMemberMethod dataBindingMethod)
		{
			if (!string.IsNullOrWhiteSpace(this.Inherits))
			{
				derivedType.BaseTypes[0] = new CodeTypeReference(this.Inherits);
			}
		}
	}
}
