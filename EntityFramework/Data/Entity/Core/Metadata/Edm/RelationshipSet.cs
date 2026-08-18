using System;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x020004C4 RID: 1220
	public abstract class RelationshipSet : EntitySetBase
	{
		// Token: 0x06002CF8 RID: 11512 RVA: 0x000DA99C File Offset: 0x000D8B9C
		internal RelationshipSet(string name, string schema, string table, string definingQuery, RelationshipType relationshipType) : base(name, schema, table, definingQuery, relationshipType)
		{
		}

		// Token: 0x17000634 RID: 1588
		// (get) Token: 0x06002CF9 RID: 11513 RVA: 0x000DA9AB File Offset: 0x000D8BAB
		public new RelationshipType ElementType
		{
			get
			{
				return (RelationshipType)base.ElementType;
			}
		}

		// Token: 0x17000635 RID: 1589
		// (get) Token: 0x06002CFA RID: 11514 RVA: 0x000DA9B8 File Offset: 0x000D8BB8
		public override BuiltInTypeKind BuiltInTypeKind
		{
			get
			{
				return BuiltInTypeKind.RelationshipSet;
			}
		}
	}
}
