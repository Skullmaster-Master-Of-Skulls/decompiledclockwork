using System;
using System.Data.Entity;

namespace System.Data.Common.EntitySql.AST
{
	// Token: 0x0200035E RID: 862
	internal sealed class QueryParameter : Node
	{
		// Token: 0x060031E8 RID: 12776 RVA: 0x000C48F8 File Offset: 0x000C2AF8
		internal QueryParameter(string parameterName, string query, int inputPos) : base(query, inputPos)
		{
			this._name = parameterName.Substring(1);
			if (this._name.StartsWith("_", StringComparison.OrdinalIgnoreCase) || char.IsDigit(this._name, 0))
			{
				throw EntityUtil.EntitySqlError(base.ErrCtx, Strings.InvalidParameterFormat(this._name));
			}
		}

		// Token: 0x170009A3 RID: 2467
		// (get) Token: 0x060031E9 RID: 12777 RVA: 0x000C4952 File Offset: 0x000C2B52
		internal string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x040015BB RID: 5563
		private readonly string _name;
	}
}
