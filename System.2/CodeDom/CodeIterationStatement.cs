using System;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	// Token: 0x0200063A RID: 1594
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Serializable]
	public class CodeIterationStatement : CodeStatement
	{
		// Token: 0x060039F9 RID: 14841 RVA: 0x000F37A7 File Offset: 0x000F19A7
		public CodeIterationStatement()
		{
		}

		// Token: 0x060039FA RID: 14842 RVA: 0x000F37BA File Offset: 0x000F19BA
		public CodeIterationStatement(CodeStatement initStatement, CodeExpression testExpression, CodeStatement incrementStatement, params CodeStatement[] statements)
		{
			this.InitStatement = initStatement;
			this.TestExpression = testExpression;
			this.IncrementStatement = incrementStatement;
			this.Statements.AddRange(statements);
		}

		// Token: 0x17000DE4 RID: 3556
		// (get) Token: 0x060039FB RID: 14843 RVA: 0x000F37EF File Offset: 0x000F19EF
		// (set) Token: 0x060039FC RID: 14844 RVA: 0x000F37F7 File Offset: 0x000F19F7
		public CodeStatement InitStatement
		{
			get
			{
				return this.initStatement;
			}
			set
			{
				this.initStatement = value;
			}
		}

		// Token: 0x17000DE5 RID: 3557
		// (get) Token: 0x060039FD RID: 14845 RVA: 0x000F3800 File Offset: 0x000F1A00
		// (set) Token: 0x060039FE RID: 14846 RVA: 0x000F3808 File Offset: 0x000F1A08
		public CodeExpression TestExpression
		{
			get
			{
				return this.testExpression;
			}
			set
			{
				this.testExpression = value;
			}
		}

		// Token: 0x17000DE6 RID: 3558
		// (get) Token: 0x060039FF RID: 14847 RVA: 0x000F3811 File Offset: 0x000F1A11
		// (set) Token: 0x06003A00 RID: 14848 RVA: 0x000F3819 File Offset: 0x000F1A19
		public CodeStatement IncrementStatement
		{
			get
			{
				return this.incrementStatement;
			}
			set
			{
				this.incrementStatement = value;
			}
		}

		// Token: 0x17000DE7 RID: 3559
		// (get) Token: 0x06003A01 RID: 14849 RVA: 0x000F3822 File Offset: 0x000F1A22
		public CodeStatementCollection Statements
		{
			get
			{
				return this.statements;
			}
		}

		// Token: 0x04002BD3 RID: 11219
		private CodeStatement initStatement;

		// Token: 0x04002BD4 RID: 11220
		private CodeExpression testExpression;

		// Token: 0x04002BD5 RID: 11221
		private CodeStatement incrementStatement;

		// Token: 0x04002BD6 RID: 11222
		private CodeStatementCollection statements = new CodeStatementCollection();
	}
}
