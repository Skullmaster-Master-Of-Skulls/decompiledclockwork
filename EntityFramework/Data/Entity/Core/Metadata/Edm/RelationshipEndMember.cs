using System;
using System.Diagnostics.CodeAnalysis;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x020004C1 RID: 1217
	public abstract class RelationshipEndMember : EdmMember
	{
		// Token: 0x06002CD9 RID: 11481 RVA: 0x000DA700 File Offset: 0x000D8900
		internal RelationshipEndMember(string name, RefType endRefType, RelationshipMultiplicity multiplicity) : base(name, TypeUsage.Create(endRefType, new FacetValues
		{
			Nullable = new bool?(false)
		}))
		{
			this._relationshipMultiplicity = multiplicity;
			this._deleteBehavior = OperationAction.None;
		}

		// Token: 0x17000627 RID: 1575
		// (get) Token: 0x06002CDA RID: 11482 RVA: 0x000DA740 File Offset: 0x000D8940
		// (set) Token: 0x06002CDB RID: 11483 RVA: 0x000DA748 File Offset: 0x000D8948
		[MetadataProperty(BuiltInTypeKind.OperationAction, true)]
		public OperationAction DeleteBehavior
		{
			get
			{
				return this._deleteBehavior;
			}
			set
			{
				Util.ThrowIfReadOnly(this);
				this._deleteBehavior = value;
			}
		}

		// Token: 0x17000628 RID: 1576
		// (get) Token: 0x06002CDC RID: 11484 RVA: 0x000DA757 File Offset: 0x000D8957
		// (set) Token: 0x06002CDD RID: 11485 RVA: 0x000DA75F File Offset: 0x000D895F
		[MetadataProperty(BuiltInTypeKind.RelationshipMultiplicity, false)]
		public RelationshipMultiplicity RelationshipMultiplicity
		{
			get
			{
				return this._relationshipMultiplicity;
			}
			set
			{
				Util.ThrowIfReadOnly(this);
				this._relationshipMultiplicity = value;
			}
		}

		// Token: 0x06002CDE RID: 11486 RVA: 0x000DA76E File Offset: 0x000D896E
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
		public EntityType GetEntityType()
		{
			if (this.TypeUsage == null)
			{
				return null;
			}
			return (EntityType)((RefType)this.TypeUsage.EdmType).ElementType;
		}

		// Token: 0x04001083 RID: 4227
		private OperationAction _deleteBehavior;

		// Token: 0x04001084 RID: 4228
		private RelationshipMultiplicity _relationshipMultiplicity;
	}
}
