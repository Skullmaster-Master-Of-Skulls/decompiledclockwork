using System;
using System.Data.Entity.Core.Common.CommandTrees;

namespace System.Data.Entity.Core.Query.PlanCompiler
{
	// Token: 0x0200065E RID: 1630
	internal sealed class ProviderCommandInfo
	{
		// Token: 0x170009D5 RID: 2517
		// (get) Token: 0x06003FB2 RID: 16306 RVA: 0x00123AB6 File Offset: 0x00121CB6
		internal DbCommandTree CommandTree
		{
			get
			{
				return this._commandTree;
			}
		}

		// Token: 0x06003FB3 RID: 16307 RVA: 0x00123ABE File Offset: 0x00121CBE
		internal ProviderCommandInfo(DbCommandTree commandTree)
		{
			this._commandTree = commandTree;
		}

		// Token: 0x040017BD RID: 6077
		private readonly DbCommandTree _commandTree;
	}
}
