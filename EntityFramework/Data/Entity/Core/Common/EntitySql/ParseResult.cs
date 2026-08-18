using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Common.CommandTrees;

namespace System.Data.Entity.Core.Common.EntitySql
{
	// Token: 0x02000264 RID: 612
	public sealed class ParseResult
	{
		// Token: 0x060014FB RID: 5371 RVA: 0x000630CD File Offset: 0x000612CD
		internal ParseResult(DbCommandTree commandTree, List<FunctionDefinition> functionDefs)
		{
			this._commandTree = commandTree;
			this._functionDefs = new ReadOnlyCollection<FunctionDefinition>(functionDefs);
		}

		// Token: 0x1700026D RID: 621
		// (get) Token: 0x060014FC RID: 5372 RVA: 0x000630E8 File Offset: 0x000612E8
		public DbCommandTree CommandTree
		{
			get
			{
				return this._commandTree;
			}
		}

		// Token: 0x1700026E RID: 622
		// (get) Token: 0x060014FD RID: 5373 RVA: 0x000630F0 File Offset: 0x000612F0
		public ReadOnlyCollection<FunctionDefinition> FunctionDefinitions
		{
			get
			{
				return this._functionDefs;
			}
		}

		// Token: 0x04000746 RID: 1862
		private readonly DbCommandTree _commandTree;

		// Token: 0x04000747 RID: 1863
		private readonly ReadOnlyCollection<FunctionDefinition> _functionDefs;
	}
}
