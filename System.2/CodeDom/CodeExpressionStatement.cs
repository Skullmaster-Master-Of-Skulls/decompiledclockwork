using System;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	// Token: 0x02000636 RID: 1590
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Serializable]
	public class CodeExpressionStatement : CodeStatement
	{
		// Token: 0x060039E6 RID: 14822 RVA: 0x000F369C File Offset: 0x000F189C
		public CodeExpressionStatement()
		{
		}

		// Token: 0x060039E7 RID: 14823 RVA: 0x000F36A4 File Offset: 0x000F18A4
		public CodeExpressionStatement(CodeExpression expression)
		{
			this.expression = expression;
		}

		// Token: 0x17000DDE RID: 3550
		// (get) Token: 0x060039E8 RID: 14824 RVA: 0x000F36B3 File Offset: 0x000F18B3
		// (set) Token: 0x060039E9 RID: 14825 RVA: 0x000F36BB File Offset: 0x000F18BB
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

		// Token: 0x04002BCD RID: 11213
		private CodeExpression expression;
	}
}
