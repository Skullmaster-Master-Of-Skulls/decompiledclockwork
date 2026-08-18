using System;

namespace System.Data.Entity.Core.Common.EntitySql.AST
{
	// Token: 0x02000219 RID: 537
	internal sealed class Command : Node
	{
		// Token: 0x06001366 RID: 4966 RVA: 0x000504BA File Offset: 0x0004E6BA
		internal Command(NodeList<NamespaceImport> nsImportList, Statement statement)
		{
			this._namespaceImportList = nsImportList;
			this._statement = statement;
		}

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x06001367 RID: 4967 RVA: 0x000504D0 File Offset: 0x0004E6D0
		internal NodeList<NamespaceImport> NamespaceImportList
		{
			get
			{
				return this._namespaceImportList;
			}
		}

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x06001368 RID: 4968 RVA: 0x000504D8 File Offset: 0x0004E6D8
		internal Statement Statement
		{
			get
			{
				return this._statement;
			}
		}

		// Token: 0x040005D7 RID: 1495
		private readonly NodeList<NamespaceImport> _namespaceImportList;

		// Token: 0x040005D8 RID: 1496
		private readonly Statement _statement;
	}
}
