using System;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	// Token: 0x0200062F RID: 1583
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Serializable]
	public class CodeDirectionExpression : CodeExpression
	{
		// Token: 0x060039BD RID: 14781 RVA: 0x000F33D7 File Offset: 0x000F15D7
		public CodeDirectionExpression()
		{
		}

		// Token: 0x060039BE RID: 14782 RVA: 0x000F33DF File Offset: 0x000F15DF
		public CodeDirectionExpression(FieldDirection direction, CodeExpression expression)
		{
			this.expression = expression;
			this.direction = direction;
		}

		// Token: 0x17000DD8 RID: 3544
		// (get) Token: 0x060039BF RID: 14783 RVA: 0x000F33F5 File Offset: 0x000F15F5
		// (set) Token: 0x060039C0 RID: 14784 RVA: 0x000F33FD File Offset: 0x000F15FD
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

		// Token: 0x17000DD9 RID: 3545
		// (get) Token: 0x060039C1 RID: 14785 RVA: 0x000F3406 File Offset: 0x000F1606
		// (set) Token: 0x060039C2 RID: 14786 RVA: 0x000F340E File Offset: 0x000F160E
		public FieldDirection Direction
		{
			get
			{
				return this.direction;
			}
			set
			{
				this.direction = value;
			}
		}

		// Token: 0x04002BC9 RID: 11209
		private CodeExpression expression;

		// Token: 0x04002BCA RID: 11210
		private FieldDirection direction;
	}
}
