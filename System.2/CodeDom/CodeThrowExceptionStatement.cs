using System;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	// Token: 0x02000659 RID: 1625
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Serializable]
	public class CodeThrowExceptionStatement : CodeStatement
	{
		// Token: 0x06003AE6 RID: 15078 RVA: 0x000F4B5C File Offset: 0x000F2D5C
		public CodeThrowExceptionStatement()
		{
		}

		// Token: 0x06003AE7 RID: 15079 RVA: 0x000F4B64 File Offset: 0x000F2D64
		public CodeThrowExceptionStatement(CodeExpression toThrow)
		{
			this.ToThrow = toThrow;
		}

		// Token: 0x17000E2D RID: 3629
		// (get) Token: 0x06003AE8 RID: 15080 RVA: 0x000F4B73 File Offset: 0x000F2D73
		// (set) Token: 0x06003AE9 RID: 15081 RVA: 0x000F4B7B File Offset: 0x000F2D7B
		public CodeExpression ToThrow
		{
			get
			{
				return this.toThrow;
			}
			set
			{
				this.toThrow = value;
			}
		}

		// Token: 0x04002C27 RID: 11303
		private CodeExpression toThrow;
	}
}
