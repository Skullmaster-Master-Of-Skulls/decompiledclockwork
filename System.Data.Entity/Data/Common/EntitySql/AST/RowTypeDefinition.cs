using System;

namespace System.Data.Common.EntitySql.AST
{
	// Token: 0x02000389 RID: 905
	internal sealed class RowTypeDefinition : Node
	{
		// Token: 0x0600326E RID: 12910 RVA: 0x000C5261 File Offset: 0x000C3461
		internal RowTypeDefinition(NodeList<PropDefinition> propDefList)
		{
			this._propDefList = propDefList;
		}

		// Token: 0x170009EF RID: 2543
		// (get) Token: 0x0600326F RID: 12911 RVA: 0x000C5270 File Offset: 0x000C3470
		internal NodeList<PropDefinition> Properties
		{
			get
			{
				return this._propDefList;
			}
		}

		// Token: 0x0400164D RID: 5709
		private readonly NodeList<PropDefinition> _propDefList;
	}
}
