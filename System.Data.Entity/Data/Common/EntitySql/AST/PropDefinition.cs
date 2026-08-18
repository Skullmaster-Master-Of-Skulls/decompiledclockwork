using System;

namespace System.Data.Common.EntitySql.AST
{
	// Token: 0x0200038A RID: 906
	internal sealed class PropDefinition : Node
	{
		// Token: 0x06003270 RID: 12912 RVA: 0x000C5278 File Offset: 0x000C3478
		internal PropDefinition(Identifier name, Node typeDefExpr)
		{
			this._name = name;
			this._typeDefExpr = typeDefExpr;
		}

		// Token: 0x170009F0 RID: 2544
		// (get) Token: 0x06003271 RID: 12913 RVA: 0x000C528E File Offset: 0x000C348E
		internal Identifier Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x170009F1 RID: 2545
		// (get) Token: 0x06003272 RID: 12914 RVA: 0x000C5296 File Offset: 0x000C3496
		internal Node Type
		{
			get
			{
				return this._typeDefExpr;
			}
		}

		// Token: 0x0400164E RID: 5710
		private readonly Identifier _name;

		// Token: 0x0400164F RID: 5711
		private readonly Node _typeDefExpr;
	}
}
