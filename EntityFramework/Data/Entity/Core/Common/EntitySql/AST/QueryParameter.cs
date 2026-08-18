using System;
using System.Data.Entity.Resources;

namespace System.Data.Entity.Core.Common.EntitySql.AST
{
	// Token: 0x02000235 RID: 565
	internal sealed class QueryParameter : Node
	{
		// Token: 0x060013D5 RID: 5077 RVA: 0x00051584 File Offset: 0x0004F784
		internal QueryParameter(string parameterName, string query, int inputPos) : base(query, inputPos)
		{
			this._name = parameterName.Substring(1);
			if (this._name.StartsWith("_", StringComparison.OrdinalIgnoreCase) || char.IsDigit(this._name, 0))
			{
				ErrorContext errCtx = base.ErrCtx;
				string errorMessage = Strings.InvalidParameterFormat(this._name);
				throw EntitySqlException.Create(errCtx, errorMessage, null);
			}
		}

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x060013D6 RID: 5078 RVA: 0x000515E3 File Offset: 0x0004F7E3
		internal string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x04000635 RID: 1589
		private readonly string _name;
	}
}
