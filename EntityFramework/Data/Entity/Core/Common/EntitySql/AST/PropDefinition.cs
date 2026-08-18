using System;

namespace System.Data.Entity.Core.Common.EntitySql.AST
{
	// Token: 0x0200023E RID: 574
	internal sealed class PropDefinition : Node
	{
		// Token: 0x060013E9 RID: 5097 RVA: 0x000516CD File Offset: 0x0004F8CD
		internal PropDefinition(Identifier name, Node typeDefExpr)
		{
			this._name = name;
			this._typeDefExpr = typeDefExpr;
		}

		// Token: 0x1700023D RID: 573
		// (get) Token: 0x060013EA RID: 5098 RVA: 0x000516E3 File Offset: 0x0004F8E3
		internal Identifier Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x060013EB RID: 5099 RVA: 0x000516EB File Offset: 0x0004F8EB
		internal Node Type
		{
			get
			{
				return this._typeDefExpr;
			}
		}

		// Token: 0x04000644 RID: 1604
		private readonly Identifier _name;

		// Token: 0x04000645 RID: 1605
		private readonly Node _typeDefExpr;
	}
}
