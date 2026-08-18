using System;

namespace System.Data.Common.EntitySql.AST
{
	// Token: 0x02000368 RID: 872
	internal sealed class Command : Node
	{
		// Token: 0x06003208 RID: 12808 RVA: 0x000C4B92 File Offset: 0x000C2D92
		internal Command(NodeList<NamespaceImport> nsImportList, Statement statement)
		{
			this._namespaceImportList = nsImportList;
			this._statement = statement;
		}

		// Token: 0x170009AE RID: 2478
		// (get) Token: 0x06003209 RID: 12809 RVA: 0x000C4BA8 File Offset: 0x000C2DA8
		internal NodeList<NamespaceImport> NamespaceImportList
		{
			get
			{
				return this._namespaceImportList;
			}
		}

		// Token: 0x170009AF RID: 2479
		// (get) Token: 0x0600320A RID: 12810 RVA: 0x000C4BB0 File Offset: 0x000C2DB0
		internal Statement Statement
		{
			get
			{
				return this._statement;
			}
		}

		// Token: 0x040015F4 RID: 5620
		private readonly NodeList<NamespaceImport> _namespaceImportList;

		// Token: 0x040015F5 RID: 5621
		private readonly Statement _statement;
	}
}
