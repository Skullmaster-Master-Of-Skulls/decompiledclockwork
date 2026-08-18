using System;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	// Token: 0x02000643 RID: 1603
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Serializable]
	public class CodeMethodReturnStatement : CodeStatement
	{
		// Token: 0x06003A48 RID: 14920 RVA: 0x000F3E8A File Offset: 0x000F208A
		public CodeMethodReturnStatement()
		{
		}

		// Token: 0x06003A49 RID: 14921 RVA: 0x000F3E92 File Offset: 0x000F2092
		public CodeMethodReturnStatement(CodeExpression expression)
		{
			this.Expression = expression;
		}

		// Token: 0x17000E05 RID: 3589
		// (get) Token: 0x06003A4A RID: 14922 RVA: 0x000F3EA1 File Offset: 0x000F20A1
		// (set) Token: 0x06003A4B RID: 14923 RVA: 0x000F3EA9 File Offset: 0x000F20A9
		public CodeExpression Expression
		{
			get
			{
				return this.expression;
			}
			set
			{
				this.expression = value;
			}
		}

		// Token: 0x04002BFB RID: 11259
		private CodeExpression expression;
	}
}
