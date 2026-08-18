using System;

namespace System.Data.Entity.Core.Common.EntitySql.AST
{
	// Token: 0x02000239 RID: 569
	internal sealed class RefTypeDefinition : Node
	{
		// Token: 0x060013DD RID: 5085 RVA: 0x00051630 File Offset: 0x0004F830
		internal RefTypeDefinition(Node refTypeIdentifier)
		{
			this._refTypeIdentifier = refTypeIdentifier;
		}

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x060013DE RID: 5086 RVA: 0x0005163F File Offset: 0x0004F83F
		internal Node RefTypeIdentifier
		{
			get
			{
				return this._refTypeIdentifier;
			}
		}

		// Token: 0x04000639 RID: 1593
		private readonly Node _refTypeIdentifier;
	}
}
