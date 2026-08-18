using System;

namespace System.Data.Metadata.Edm
{
	// Token: 0x020001F2 RID: 498
	public abstract class RelationshipEndMember : EdmMember
	{
		// Token: 0x06002113 RID: 8467 RVA: 0x000747D6 File Offset: 0x000729D6
		internal RelationshipEndMember(string name, RefType endRefType, RelationshipMultiplicity multiplicity) : base(name, TypeUsage.Create(endRefType, new FacetValues
		{
			Nullable = new bool?(false)
		}))
		{
			this._relationshipMultiplicity = multiplicity;
			this._deleteBehavior = OperationAction.None;
		}

		// Token: 0x170006C0 RID: 1728
		// (get) Token: 0x06002114 RID: 8468 RVA: 0x00074809 File Offset: 0x00072A09
		// (set) Token: 0x06002115 RID: 8469 RVA: 0x00074811 File Offset: 0x00072A11
		[MetadataProperty(BuiltInTypeKind.OperationAction, true)]
		public OperationAction DeleteBehavior
		{
			get
			{
				return this._deleteBehavior;
			}
			internal set
			{
				Util.ThrowIfReadOnly(this);
				this._deleteBehavior = value;
			}
		}

		// Token: 0x170006C1 RID: 1729
		// (get) Token: 0x06002116 RID: 8470 RVA: 0x00074820 File Offset: 0x00072A20
		[MetadataProperty(BuiltInTypeKind.RelationshipMultiplicity, false)]
		public RelationshipMultiplicity RelationshipMultiplicity
		{
			get
			{
				return this._relationshipMultiplicity;
			}
		}

		// Token: 0x06002117 RID: 8471 RVA: 0x00074828 File Offset: 0x00072A28
		public EntityType GetEntityType()
		{
			if (base.TypeUsage == null)
			{
				return null;
			}
			return (EntityType)((RefType)base.TypeUsage.EdmType).ElementType;
		}

		// Token: 0x04000EA2 RID: 3746
		private OperationAction _deleteBehavior;

		// Token: 0x04000EA3 RID: 3747
		private RelationshipMultiplicity _relationshipMultiplicity;
	}
}
