using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Data.Entity.Utilities;
using System.Linq;
using System.Threading;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x020004C2 RID: 1218
	public sealed class AssociationEndMember : RelationshipEndMember
	{
		// Token: 0x06002CDF RID: 11487 RVA: 0x000DA794 File Offset: 0x000D8994
		internal AssociationEndMember(string name, RefType endRefType, RelationshipMultiplicity multiplicity) : base(name, endRefType, multiplicity)
		{
		}

		// Token: 0x06002CE0 RID: 11488 RVA: 0x000DA79F File Offset: 0x000D899F
		internal AssociationEndMember(string name, EntityType entityType) : base(name, new RefType(entityType), RelationshipMultiplicity.ZeroOrOne)
		{
		}

		// Token: 0x17000629 RID: 1577
		// (get) Token: 0x06002CE1 RID: 11489 RVA: 0x000DA7AF File Offset: 0x000D89AF
		public override BuiltInTypeKind BuiltInTypeKind
		{
			get
			{
				return BuiltInTypeKind.AssociationEndMember;
			}
		}

		// Token: 0x1700062A RID: 1578
		// (get) Token: 0x06002CE2 RID: 11490 RVA: 0x000DA7B2 File Offset: 0x000D89B2
		// (set) Token: 0x06002CE3 RID: 11491 RVA: 0x000DA7BA File Offset: 0x000D89BA
		internal Func<RelationshipManager, RelatedEnd, RelatedEnd> GetRelatedEnd
		{
			get
			{
				return this._getRelatedEndMethod;
			}
			set
			{
				Interlocked.CompareExchange<Func<RelationshipManager, RelatedEnd, RelatedEnd>>(ref this._getRelatedEndMethod, value, null);
			}
		}

		// Token: 0x06002CE4 RID: 11492 RVA: 0x000DA7CC File Offset: 0x000D89CC
		public static AssociationEndMember Create(string name, RefType endRefType, RelationshipMultiplicity multiplicity, OperationAction deleteAction, IEnumerable<MetadataProperty> metadataProperties)
		{
			Check.NotEmpty(name, "name");
			Check.NotNull<RefType>(endRefType, "endRefType");
			AssociationEndMember associationEndMember = new AssociationEndMember(name, endRefType, multiplicity);
			associationEndMember.DeleteBehavior = deleteAction;
			if (metadataProperties != null)
			{
				associationEndMember.AddMetadataProperties(metadataProperties.ToList<MetadataProperty>());
			}
			associationEndMember.SetReadOnly();
			return associationEndMember;
		}

		// Token: 0x04001085 RID: 4229
		private Func<RelationshipManager, RelatedEnd, RelatedEnd> _getRelatedEndMethod;
	}
}
