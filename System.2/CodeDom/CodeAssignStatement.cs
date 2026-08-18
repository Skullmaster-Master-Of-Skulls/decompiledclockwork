using System;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	// Token: 0x02000619 RID: 1561
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Serializable]
	public class CodeAssignStatement : CodeStatement
	{
		// Token: 0x0600391B RID: 14619 RVA: 0x000F286F File Offset: 0x000F0A6F
		public CodeAssignStatement()
		{
		}

		// Token: 0x0600391C RID: 14620 RVA: 0x000F2877 File Offset: 0x000F0A77
		public CodeAssignStatement(CodeExpression left, CodeExpression right)
		{
			this.Left = left;
			this.Right = right;
		}

		// Token: 0x17000DAD RID: 3501
		// (get) Token: 0x0600391D RID: 14621 RVA: 0x000F288D File Offset: 0x000F0A8D
		// (set) Token: 0x0600391E RID: 14622 RVA: 0x000F2895 File Offset: 0x000F0A95
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

		// Token: 0x17000DAE RID: 3502
		// (get) Token: 0x0600391F RID: 14623 RVA: 0x000F289E File Offset: 0x000F0A9E
		// (set) Token: 0x06003920 RID: 14624 RVA: 0x000F28A6 File Offset: 0x000F0AA6
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

		// Token: 0x04002B90 RID: 11152
		private CodeExpression left;

		// Token: 0x04002B91 RID: 11153
		private CodeExpression right;
	}
}
