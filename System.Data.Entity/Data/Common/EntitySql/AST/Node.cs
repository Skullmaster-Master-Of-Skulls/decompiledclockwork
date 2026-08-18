using System;

namespace System.Data.Common.EntitySql.AST
{
	// Token: 0x02000360 RID: 864
	internal abstract class Node
	{
		// Token: 0x060031EE RID: 12782 RVA: 0x000C49AD File Offset: 0x000C2BAD
		internal Node()
		{
		}

		// Token: 0x060031EF RID: 12783 RVA: 0x000C49C0 File Offset: 0x000C2BC0
		internal Node(string commandText, int inputPosition)
		{
			this._errCtx.CommandText = commandText;
			this._errCtx.InputPosition = inputPosition;
		}

		// Token: 0x170009A6 RID: 2470
		// (get) Token: 0x060031F0 RID: 12784 RVA: 0x000C49EB File Offset: 0x000C2BEB
		// (set) Token: 0x060031F1 RID: 12785 RVA: 0x000C49F3 File Offset: 0x000C2BF3
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

		// Token: 0x040015BE RID: 5566
		private ErrorContext _errCtx = new ErrorContext();
	}
}
