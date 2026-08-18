using System;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	// Token: 0x0200065B RID: 1627
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Serializable]
	public class CodeTypeConstructor : CodeMemberMethod
	{
		// Token: 0x06003AF0 RID: 15088 RVA: 0x000F4C6C File Offset: 0x000F2E6C
		public CodeTypeConstructor()
		{
			base.Name = ".cctor";
		}
	}
}
