using System;
using System.Threading;

namespace System.Data.Metadata.Edm
{
	// Token: 0x020001C3 RID: 451
	public sealed class AssociationType : RelationshipType
	{
		// Token: 0x06001F3C RID: 7996 RVA: 0x0006E14F File Offset: 0x0006C34F
		internal AssociationType(string name, string namespaceName, bool foreignKey, DataSpace dataSpace) : base(name, namespaceName, dataSpace)
		{
			this._referentialConstraints = new ReadOnlyMetadataCollection<ReferentialConstraint>(new MetadataCollection<ReferentialConstraint>());
			this._isForeignKey = foreignKey;
		}

		// Token: 0x1700060B RID: 1547
		// (get) Token: 0x06001F3D RID: 7997 RVA: 0x0003BF8C File Offset: 0x0003A18C
		public override BuiltInTypeKind BuiltInTypeKind
		{
			get
			{
				return BuiltInTypeKind.AssociationType;
			}
		}

		// Token: 0x1700060C RID: 1548
		// (get) Token: 0x06001F3E RID: 7998 RVA: 0x0006E172 File Offset: 0x0006C372
		public ReadOnlyMetadataCollection<AssociationEndMember> AssociationEndMembers
		{
			get
			{
				if (this._associationEndMembers == null)
				{
					Interlocked.CompareExchange<FilteredReadOnlyMetadataCollection<AssociationEndMember, EdmMember>>(ref this._associationEndMembers, new FilteredReadOnlyMetadataCollection<AssociationEndMember, EdmMember>(base.Members, new Predicate<EdmMember>(Helper.IsAssociationEndMember)), null);
				}
				return this._associationEndMembers;
			}
		}

		// Token: 0x1700060D RID: 1549
		// (get) Token: 0x06001F3F RID: 7999 RVA: 0x0006E1A6 File Offset: 0x0006C3A6
		[MetadataProperty(BuiltInTypeKind.ReferentialConstraint, true)]
		public ReadOnlyMetadataCollection<ReferentialConstraint> ReferentialConstraints
		{
			get
			{
				return this._referentialConstraints;
			}
		}

		// Token: 0x1700060E RID: 1550
		// (get) Token: 0x06001F40 RID: 8000 RVA: 0x0006E1AE File Offset: 0x0006C3AE
		[MetadataProperty(PrimitiveTypeKind.Boolean, false)]
		public bool IsForeignKey
		{
			get
			{
				return this._isForeignKey;
			}
		}

		// Token: 0x06001F41 RID: 8001 RVA: 0x000089D0 File Offset: 0x00006BD0
		internal override void ValidateMemberForAdd(EdmMember member)
		{
		}

		// Token: 0x06001F42 RID: 8002 RVA: 0x0006E1B6 File Offset: 0x0006C3B6
		internal override void SetReadOnly()
		{
			if (!base.IsReadOnly)
			{
				base.SetReadOnly();
				this.ReferentialConstraints.Source.SetReadOnly();
			}
		}

		// Token: 0x06001F43 RID: 8003 RVA: 0x0006E1D7 File Offset: 0x0006C3D7
		internal void AddReferentialConstraint(ReferentialConstraint referentialConstraint)
		{
			this.ReferentialConstraints.Source.Add(referentialConstraint);
		}

		// Token: 0x04000D19 RID: 3353
		private readonly ReadOnlyMetadataCollection<ReferentialConstraint> _referentialConstraints;

		// Token: 0x04000D1A RID: 3354
		private FilteredReadOnlyMetadataCollection<AssociationEndMember, EdmMember> _associationEndMembers;

		// Token: 0x04000D1B RID: 3355
		private readonly bool _isForeignKey;
	}
}
