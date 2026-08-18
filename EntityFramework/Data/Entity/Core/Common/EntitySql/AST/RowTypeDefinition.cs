using System;

namespace System.Data.Entity.Core.Common.EntitySql.AST
{
	// Token: 0x0200023B RID: 571
	internal sealed class RowTypeDefinition : Node
	{
		// Token: 0x060013E1 RID: 5089 RVA: 0x0005165E File Offset: 0x0004F85E
		internal RowTypeDefinition(NodeList<PropDefinition> propDefList)
		{
			this._propDefList = propDefList;
		}

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x060013E2 RID: 5090 RVA: 0x0005166D File Offset: 0x0004F86D
		internal NodeList<PropDefinition> Properties
		{
			get
			{
				return this._propDefList;
			}
		}

		// Token: 0x0400063B RID: 1595
		private readonly NodeList<PropDefinition> _propDefList;
	}
}
