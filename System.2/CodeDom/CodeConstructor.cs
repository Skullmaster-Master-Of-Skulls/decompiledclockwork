using System;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	// Token: 0x0200062B RID: 1579
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Serializable]
	public class CodeConstructor : CodeMemberMethod
	{
		// Token: 0x060039A8 RID: 14760 RVA: 0x000F3274 File Offset: 0x000F1474
		public CodeConstructor()
		{
			base.Name = ".ctor";
		}

		// Token: 0x17000DD0 RID: 3536
		// (get) Token: 0x060039A9 RID: 14761 RVA: 0x000F329D File Offset: 0x000F149D
		public CodeExpressionCollection BaseConstructorArgs
		{
			get
			{
				return this.baseConstructorArgs;
			}
		}

		// Token: 0x17000DD1 RID: 3537
		// (get) Token: 0x060039AA RID: 14762 RVA: 0x000F32A5 File Offset: 0x000F14A5
		public CodeExpressionCollection ChainedConstructorArgs
		{
			get
			{
				return this.chainedConstructorArgs;
			}
		}

		// Token: 0x04002BC1 RID: 11201
		private CodeExpressionCollection baseConstructorArgs = new CodeExpressionCollection();

		// Token: 0x04002BC2 RID: 11202
		private CodeExpressionCollection chainedConstructorArgs = new CodeExpressionCollection();
	}
}
