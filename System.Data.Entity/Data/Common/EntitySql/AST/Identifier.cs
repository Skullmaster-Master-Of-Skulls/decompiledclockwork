using System;
using System.Data.Entity;

namespace System.Data.Common.EntitySql.AST
{
	// Token: 0x02000370 RID: 880
	internal sealed class Identifier : Node
	{
		// Token: 0x06003221 RID: 12833 RVA: 0x000C4DA8 File Offset: 0x000C2FA8
		internal Identifier(string name, bool isEscaped, string query, int inputPos) : base(query, inputPos)
		{
			if (!isEscaped)
			{
				bool flag = true;
				if (!CqlLexer.IsLetterOrDigitOrUnderscore(name, out flag))
				{
					if (flag)
					{
						throw EntityUtil.EntitySqlError(base.ErrCtx, Strings.InvalidSimpleIdentifier(name));
					}
					throw EntityUtil.EntitySqlError(base.ErrCtx, Strings.InvalidSimpleIdentifierNonASCII(name));
				}
			}
			this._name = name;
			this._isEscaped = isEscaped;
		}

		// Token: 0x170009BD RID: 2493
		// (get) Token: 0x06003222 RID: 12834 RVA: 0x000C4E02 File Offset: 0x000C3002
		internal string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x170009BE RID: 2494
		// (get) Token: 0x06003223 RID: 12835 RVA: 0x000C4E0A File Offset: 0x000C300A
		internal bool IsEscaped
		{
			get
			{
				return this._isEscaped;
			}
		}

		// Token: 0x04001605 RID: 5637
		private readonly string _name;

		// Token: 0x04001606 RID: 5638
		private readonly bool _isEscaped;
	}
}
