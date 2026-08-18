using System;
using System.CodeDom;

namespace System.ComponentModel.Design.Serialization
{
	// Token: 0x020001E3 RID: 483
	public interface ICodeDomDesignerReload
	{
		// Token: 0x06001226 RID: 4646
		bool ShouldReloadDesigner(CodeCompileUnit newTree);
	}
}
