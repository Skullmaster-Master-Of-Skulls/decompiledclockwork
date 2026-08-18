using System;

namespace System.Data.Entity.Core.Common.EntitySql.AST
{
	// Token: 0x02000210 RID: 528
	internal abstract class Node
	{
		// Token: 0x06001348 RID: 4936 RVA: 0x0005025F File Offset: 0x0004E45F
		internal Node()
		{
		}

		// Token: 0x06001349 RID: 4937 RVA: 0x00050272 File Offset: 0x0004E472
		internal Node(string commandText, int inputPosition)
		{
			this._errCtx.CommandText = commandText;
			this._errCtx.InputPosition = inputPosition;
		}

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x0600134A RID: 4938 RVA: 0x0005029D File Offset: 0x0004E49D
		// (set) Token: 0x0600134B RID: 4939 RVA: 0x000502A5 File Offset: 0x0004E4A5
		internal ErrorContext ErrCtx
		{
			get
			{
				return this._errCtx;
			}
			set
			{
				this._errCtx = value;
			}
		}

		// Token: 0x0400059D RID: 1437
		private ErrorContext _errCtx = new ErrorContext();
	}
}
