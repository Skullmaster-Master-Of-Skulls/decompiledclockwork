using System;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	// Token: 0x02000620 RID: 1568
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Serializable]
	public class CodeBinaryOperatorExpression : CodeExpression
	{
		// Token: 0x06003953 RID: 14675 RVA: 0x000F2C70 File Offset: 0x000F0E70
		public CodeBinaryOperatorExpression()
		{
		}

		// Token: 0x06003954 RID: 14676 RVA: 0x000F2C78 File Offset: 0x000F0E78
		public CodeBinaryOperatorExpression(CodeExpression left, CodeBinaryOperatorType op, CodeExpression right)
		{
			this.Right = right;
			this.Operator = op;
			this.Left = left;
		}

		// Token: 0x17000DB8 RID: 3512
		// (get) Token: 0x06003955 RID: 14677 RVA: 0x000F2C95 File Offset: 0x000F0E95
		// (set) Token: 0x06003956 RID: 14678 RVA: 0x000F2C9D File Offset: 0x000F0E9D
		public CodeExpression Right
		{
			get
			{
				return this.right;
			}
			set
			{
				this.right = value;
			}
		}

		// Token: 0x17000DB9 RID: 3513
		// (get) Token: 0x06003957 RID: 14679 RVA: 0x000F2CA6 File Offset: 0x000F0EA6
		// (set) Token: 0x06003958 RID: 14680 RVA: 0x000F2CAE File Offset: 0x000F0EAE
		public CodeExpression Left
		{
			get
			{
				return this.left;
			}
			set
			{
				this.left = value;
			}
		}

		// Token: 0x17000DBA RID: 3514
		// (get) Token: 0x06003959 RID: 14681 RVA: 0x000F2CB7 File Offset: 0x000F0EB7
		// (set) Token: 0x0600395A RID: 14682 RVA: 0x000F2CBF File Offset: 0x000F0EBF
		public CodeBinaryOperatorType Operator
		{
			get
			{
				return this.op;
			}
			set
			{
				this.op = value;
			}
		}

		// Token: 0x04002B99 RID: 11161
		private CodeBinaryOperatorType op;

		// Token: 0x04002B9A RID: 11162
		private CodeExpression left;

		// Token: 0x04002B9B RID: 11163
		private CodeExpression right;
	}
}
