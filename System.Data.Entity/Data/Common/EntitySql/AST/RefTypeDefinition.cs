using System;

namespace System.Data.Common.EntitySql.AST
{
	// Token: 0x02000388 RID: 904
	internal sealed class RefTypeDefinition : Node
	{
		// Token: 0x0600326C RID: 12908 RVA: 0x000C524A File Offset: 0x000C344A
		internal RefTypeDefinition(Node refTypeIdentifier)
		{
			this._refTypeIdentifier = refTypeIdentifier;
		}

		// Token: 0x170009EE RID: 2542
		// (get) Token: 0x0600326D RID: 12909 RVA: 0x000C5259 File Offset: 0x000C3459
		internal Node RefTypeIdentifier
		{
			get
			{
				return this._refTypeIdentifier;
			}
		}

		// Token: 0x0400164C RID: 5708
		private readonly Node _refTypeIdentifier;
	}
}
