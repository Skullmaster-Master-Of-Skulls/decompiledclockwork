using System;

namespace System.Data.Metadata.Edm
{
	// Token: 0x020001F4 RID: 500
	public abstract class RelationshipSet : EntitySetBase
	{
		// Token: 0x06002118 RID: 8472 RVA: 0x0006E868 File Offset: 0x0006CA68
		internal RelationshipSet(string name, string schema, string table, string definingQuery, RelationshipType relationshipType) : base(name, schema, table, definingQuery, relationshipType)
		{
		}

		// Token: 0x170006C2 RID: 1730
		// (get) Token: 0x06002119 RID: 8473 RVA: 0x0007484E File Offset: 0x00072A4E
		public new RelationshipType ElementType
		{
			get
			{
				return (RelationshipType)base.ElementType;
			}
		}

		// Token: 0x170006C3 RID: 1731
		// (get) Token: 0x0600211A RID: 8474 RVA: 0x0007485B File Offset: 0x00072A5B
		public override BuiltInTypeKind BuiltInTypeKind
		{
			get
			{
				return BuiltInTypeKind.RelationshipSet;
			}
		}
	}
}
