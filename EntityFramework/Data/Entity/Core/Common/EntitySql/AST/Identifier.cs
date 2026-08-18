using System;
using System.Data.Entity.Resources;

namespace System.Data.Entity.Core.Common.EntitySql.AST
{
	// Token: 0x02000228 RID: 552
	internal sealed class Identifier : Node
	{
		// Token: 0x0600138D RID: 5005 RVA: 0x0005079C File Offset: 0x0004E99C
		internal Identifier(string name, bool isEscaped, string query, int inputPos) : base(query, inputPos)
		{
			if (!isEscaped)
			{
				bool flag = true;
				if (!CqlLexer.IsLetterOrDigitOrUnderscore(name, out flag))
				{
					if (flag)
					{
						ErrorContext errCtx = base.ErrCtx;
						string errorMessage = Strings.InvalidSimpleIdentifier(name);
						throw EntitySqlException.Create(errCtx, errorMessage, null);
					}
					ErrorContext errCtx2 = base.ErrCtx;
					string errorMessage2 = Strings.InvalidSimpleIdentifierNonASCII(name);
					throw EntitySqlException.Create(errCtx2, errorMessage2, null);
				}
			}
			this._name = name;
			this._isEscaped = isEscaped;
		}

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x0600138E RID: 5006 RVA: 0x00050802 File Offset: 0x0004EA02
		internal string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x0600138F RID: 5007 RVA: 0x0005080A File Offset: 0x0004EA0A
		internal bool IsEscaped
		{
			get
			{
				return this._isEscaped;
			}
		}

		// Token: 0x040005F9 RID: 1529
		private readonly string _name;

		// Token: 0x040005FA RID: 1530
		private readonly bool _isEscaped;
	}
}
