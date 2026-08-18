using System;
using System.Collections.Generic;
using System.Data.Common.CommandTrees;

namespace System.Data.Query.PlanCompiler
{
	// Token: 0x02000049 RID: 73
	internal sealed class ProviderCommandInfo
	{
		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000602 RID: 1538 RVA: 0x00019CE1 File Offset: 0x00017EE1
		internal DbCommandTree CommandTree
		{
			get
			{
				return this._commandTree;
			}
		}

		// Token: 0x06000603 RID: 1539 RVA: 0x00019CEC File Offset: 0x00017EEC
		internal ProviderCommandInfo(DbCommandTree commandTree, List<ProviderCommandInfo> children)
		{
			this._commandTree = commandTree;
			this._children = children;
			if (this._children == null)
			{
				this._children = new List<ProviderCommandInfo>();
			}
			foreach (ProviderCommandInfo providerCommandInfo in this._children)
			{
				providerCommandInfo._parent = this;
			}
		}

		// Token: 0x04000766 RID: 1894
		private DbCommandTree _commandTree;

		// Token: 0x04000767 RID: 1895
		private ProviderCommandInfo _parent;

		// Token: 0x04000768 RID: 1896
		private List<ProviderCommandInfo> _children;
	}
}
