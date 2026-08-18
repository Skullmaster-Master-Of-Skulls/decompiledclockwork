using System;
using System.CodeDom;
using System.Web.UI;

namespace System.Web.Mvc
{
	// Token: 0x02000192 RID: 402
	internal sealed class ViewUserControlControlBuilder : FileLevelUserControlBuilder, IMvcControlBuilder
	{
		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x06000B6A RID: 2922 RVA: 0x0001E363 File Offset: 0x0001C563
		// (set) Token: 0x06000B6B RID: 2923 RVA: 0x0001E36B File Offset: 0x0001C56B
		public string Inherits { get; set; }

		// Token: 0x06000B6C RID: 2924 RVA: 0x0001E374 File Offset: 0x0001C574
		public override void ProcessGeneratedCode(CodeCompileUnit codeCompileUnit, CodeTypeDeclaration baseType, CodeTypeDeclaration derivedType, CodeMemberMethod buildMethod, CodeMemberMethod dataBindingMethod)
		{
			if (!string.IsNullOrWhiteSpace(this.Inherits))
			{
				derivedType.BaseTypes[0] = new CodeTypeReference(this.Inherits);
			}
		}
	}
}
