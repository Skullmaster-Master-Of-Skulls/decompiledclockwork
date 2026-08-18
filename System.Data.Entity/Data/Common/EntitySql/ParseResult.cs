using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common.CommandTrees;

namespace System.Data.Common.EntitySql
{
	// Token: 0x02000358 RID: 856
	public sealed class ParseResult
	{
		// Token: 0x060031C1 RID: 12737 RVA: 0x000C3D13 File Offset: 0x000C1F13
		internal ParseResult(DbCommandTree commandTree, List<FunctionDefinition> functionDefs)
		{
			EntityUtil.CheckArgumentNull<DbCommandTree>(commandTree, "commandTree");
			EntityUtil.CheckArgumentNull<List<FunctionDefinition>>(functionDefs, "functionDefs");
			this._commandTree = commandTree;
			this._functionDefs = functionDefs.AsReadOnly();
		}

		// Token: 0x17000994 RID: 2452
		// (get) Token: 0x060031C2 RID: 12738 RVA: 0x000C3D46 File Offset: 0x000C1F46
		public DbCommandTree CommandTree
		{
			get
			{
				return this._commandTree;
			}
		}

		// Token: 0x17000995 RID: 2453
		// (get) Token: 0x060031C3 RID: 12739 RVA: 0x000C3D4E File Offset: 0x000C1F4E
		public ReadOnlyCollection<FunctionDefinition> FunctionDefinitions
		{
			get
			{
				return this._functionDefs;
			}
		}

		// Token: 0x0400159C RID: 5532
		private readonly DbCommandTree _commandTree;

		// Token: 0x0400159D RID: 5533
		private readonly ReadOnlyCollection<FunctionDefinition> _functionDefs;
	}
}
