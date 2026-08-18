using System;
using System.CodeDom;
using System.Web.UI;

namespace System.Web.Mvc
{
	// Token: 0x02000193 RID: 403
	internal sealed class ViewPageControlBuilder : FileLevelPageControlBuilder, IMvcControlBuilder
	{
		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x06000B6E RID: 2926 RVA: 0x0001E3A2 File Offset: 0x0001C5A2
		// (set) Token: 0x06000B6F RID: 2927 RVA: 0x0001E3AA File Offset: 0x0001C5AA
		public string Inherits { get; set; }

		// Token: 0x06000B70 RID: 2928 RVA: 0x0001E3B3 File Offset: 0x0001C5B3
		public override void ProcessGeneratedCode(CodeCompileUnit codeCompileUnit, CodeTypeDeclaration baseType, CodeTypeDeclaration derivedType, CodeMemberMethod buildMethod, CodeMemberMethod dataBindingMethod)
		{
			if (!string.IsNullOrWhiteSpace(this.Inherits))
			{
				derivedType.BaseTypes[0] = new CodeTypeReference(this.Inherits);
			}
		}
	}
}
