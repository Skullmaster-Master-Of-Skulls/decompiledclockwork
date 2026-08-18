using System;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	// Token: 0x0200065A RID: 1626
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Serializable]
	public class CodeTryCatchFinallyStatement : CodeStatement
	{
		// Token: 0x06003AEA RID: 15082 RVA: 0x000F4B84 File Offset: 0x000F2D84
		public CodeTryCatchFinallyStatement()
		{
		}

		// Token: 0x06003AEB RID: 15083 RVA: 0x000F4BB0 File Offset: 0x000F2DB0
		public CodeTryCatchFinallyStatement(CodeStatement[] tryStatements, CodeCatchClause[] catchClauses)
		{
			this.TryStatements.AddRange(tryStatements);
			this.CatchClauses.AddRange(catchClauses);
		}

		// Token: 0x06003AEC RID: 15084 RVA: 0x000F4BFC File Offset: 0x000F2DFC
		public CodeTryCatchFinallyStatement(CodeStatement[] tryStatements, CodeCatchClause[] catchClauses, CodeStatement[] finallyStatements)
		{
			this.TryStatements.AddRange(tryStatements);
			this.CatchClauses.AddRange(catchClauses);
			this.FinallyStatements.AddRange(finallyStatements);
		}

		// Token: 0x17000E2E RID: 3630
		// (get) Token: 0x06003AED RID: 15085 RVA: 0x000F4C54 File Offset: 0x000F2E54
		public CodeStatementCollection TryStatements
		{
			get
			{
				return this.tryStatments;
			}
		}

		// Token: 0x17000E2F RID: 3631
		// (get) Token: 0x06003AEE RID: 15086 RVA: 0x000F4C5C File Offset: 0x000F2E5C
		public CodeCatchClauseCollection CatchClauses
		{
			get
			{
				return this.catchClauses;
			}
		}

		// Token: 0x17000E30 RID: 3632
		// (get) Token: 0x06003AEF RID: 15087 RVA: 0x000F4C64 File Offset: 0x000F2E64
		public CodeStatementCollection FinallyStatements
		{
			get
			{
				return this.finallyStatments;
			}
		}

		// Token: 0x04002C28 RID: 11304
		private CodeStatementCollection tryStatments = new CodeStatementCollection();

		// Token: 0x04002C29 RID: 11305
		private CodeStatementCollection finallyStatments = new CodeStatementCollection();

		// Token: 0x04002C2A RID: 11306
		private CodeCatchClauseCollection catchClauses = new CodeCatchClauseCollection();
	}
}
