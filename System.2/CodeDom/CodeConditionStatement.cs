using System;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	// Token: 0x0200062A RID: 1578
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Serializable]
	public class CodeConditionStatement : CodeStatement
	{
		// Token: 0x060039A1 RID: 14753 RVA: 0x000F31C7 File Offset: 0x000F13C7
		public CodeConditionStatement()
		{
		}

		// Token: 0x060039A2 RID: 14754 RVA: 0x000F31E5 File Offset: 0x000F13E5
		public CodeConditionStatement(CodeExpression condition, params CodeStatement[] trueStatements)
		{
			this.Condition = condition;
			this.TrueStatements.AddRange(trueStatements);
		}

		// Token: 0x060039A3 RID: 14755 RVA: 0x000F3216 File Offset: 0x000F1416
		public CodeConditionStatement(CodeExpression condition, CodeStatement[] trueStatements, CodeStatement[] falseStatements)
		{
			this.Condition = condition;
			this.TrueStatements.AddRange(trueStatements);
			this.FalseStatements.AddRange(falseStatements);
		}

		// Token: 0x17000DCD RID: 3533
		// (get) Token: 0x060039A4 RID: 14756 RVA: 0x000F3253 File Offset: 0x000F1453
		// (set) Token: 0x060039A5 RID: 14757 RVA: 0x000F325B File Offset: 0x000F145B
		public CodeExpression Condition
		{
			get
			{
				return this.condition;
			}
			set
			{
				this.condition = value;
			}
		}

		// Token: 0x17000DCE RID: 3534
		// (get) Token: 0x060039A6 RID: 14758 RVA: 0x000F3264 File Offset: 0x000F1464
		public CodeStatementCollection TrueStatements
		{
			get
			{
				return this.trueStatments;
			}
		}

		// Token: 0x17000DCF RID: 3535
		// (get) Token: 0x060039A7 RID: 14759 RVA: 0x000F326C File Offset: 0x000F146C
		public CodeStatementCollection FalseStatements
		{
			get
			{
				return this.falseStatments;
			}
		}

		// Token: 0x04002BBE RID: 11198
		private CodeExpression condition;

		// Token: 0x04002BBF RID: 11199
		private CodeStatementCollection trueStatments = new CodeStatementCollection();

		// Token: 0x04002BC0 RID: 11200
		private CodeStatementCollection falseStatments = new CodeStatementCollection();
	}
}
