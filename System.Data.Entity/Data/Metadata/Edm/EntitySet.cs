using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common.Utils;
using System.Threading;

namespace System.Data.Metadata.Edm
{
	// Token: 0x020001CE RID: 462
	public class EntitySet : EntitySetBase
	{
		// Token: 0x06001F8E RID: 8078 RVA: 0x0006E868 File Offset: 0x0006CA68
		internal EntitySet(string name, string schema, string table, string definingQuery, EntityType entityType) : base(name, schema, table, definingQuery, entityType)
		{
		}

		// Token: 0x1700062A RID: 1578
		// (get) Token: 0x06001F8F RID: 8079 RVA: 0x0006E877 File Offset: 0x0006CA77
		public override BuiltInTypeKind BuiltInTypeKind
		{
			get
			{
				return BuiltInTypeKind.EntitySet;
			}
		}

		// Token: 0x1700062B RID: 1579
		// (get) Token: 0x06001F90 RID: 8080 RVA: 0x0006E87B File Offset: 0x0006CA7B
		public new EntityType ElementType
		{
			get
			{
				return (EntityType)base.ElementType;
			}
		}

		// Token: 0x1700062C RID: 1580
		// (get) Token: 0x06001F91 RID: 8081 RVA: 0x0006E888 File Offset: 0x0006CA88
		internal ReadOnlyCollection<Tuple<AssociationSet, ReferentialConstraint>> ForeignKeyDependents
		{
			get
			{
				if (this._foreignKeyDependents == null)
				{
					this.InitializeForeignKeyLists();
				}
				return this._foreignKeyDependents;
			}
		}

		// Token: 0x1700062D RID: 1581
		// (get) Token: 0x06001F92 RID: 8082 RVA: 0x0006E89E File Offset: 0x0006CA9E
		internal ReadOnlyCollection<Tuple<AssociationSet, ReferentialConstraint>> ForeignKeyPrincipals
		{
			get
			{
				if (this._foreignKeyPrincipals == null)
				{
					this.InitializeForeignKeyLists();
				}
				return this._foreignKeyPrincipals;
			}
		}

		// Token: 0x1700062E RID: 1582
		// (get) Token: 0x06001F93 RID: 8083 RVA: 0x0006E8B4 File Offset: 0x0006CAB4
		internal bool HasForeignKeyRelationships
		{
			get
			{
				if (this._foreignKeyPrincipals == null)
				{
					this.InitializeForeignKeyLists();
				}
				return this._hasForeignKeyRelationships;
			}
		}

		// Token: 0x1700062F RID: 1583
		// (get) Token: 0x06001F94 RID: 8084 RVA: 0x0006E8CC File Offset: 0x0006CACC
		internal bool HasIndependentRelationships
		{
			get
			{
				if (this._foreignKeyPrincipals == null)
				{
					this.InitializeForeignKeyLists();
				}
				return this._hasIndependentRelationships;
			}
		}

		// Token: 0x06001F95 RID: 8085 RVA: 0x0006E8E4 File Offset: 0x0006CAE4
		private void InitializeForeignKeyLists()
		{
			List<Tuple<AssociationSet, ReferentialConstraint>> list = new List<Tuple<AssociationSet, ReferentialConstraint>>();
			List<Tuple<AssociationSet, ReferentialConstraint>> list2 = new List<Tuple<AssociationSet, ReferentialConstraint>>();
			bool hasForeignKeyRelationships = false;
			bool hasIndependentRelationships = false;
			foreach (AssociationSet associationSet in MetadataHelper.GetAssociationsForEntitySet(this))
			{
				if (associationSet.ElementType.IsForeignKey)
				{
					hasForeignKeyRelationships = true;
					ReferentialConstraint referentialConstraint = associationSet.ElementType.ReferentialConstraints[0];
					if (referentialConstraint.ToRole.GetEntityType().IsAssignableFrom(this.ElementType) || this.ElementType.IsAssignableFrom(referentialConstraint.ToRole.GetEntityType()))
					{
						list.Add(new Tuple<AssociationSet, ReferentialConstraint>(associationSet, referentialConstraint));
					}
					if (referentialConstraint.FromRole.GetEntityType().IsAssignableFrom(this.ElementType) || this.ElementType.IsAssignableFrom(referentialConstraint.FromRole.GetEntityType()))
					{
						list2.Add(new Tuple<AssociationSet, ReferentialConstraint>(associationSet, referentialConstraint));
					}
				}
				else
				{
					hasIndependentRelationships = true;
				}
			}
			this._hasForeignKeyRelationships = hasForeignKeyRelationships;
			this._hasIndependentRelationships = hasIndependentRelationships;
			ReadOnlyCollection<Tuple<AssociationSet, ReferentialConstraint>> value = list.AsReadOnly();
			ReadOnlyCollection<Tuple<AssociationSet, ReferentialConstraint>> value2 = list2.AsReadOnly();
			Interlocked.CompareExchange<ReadOnlyCollection<Tuple<AssociationSet, ReferentialConstraint>>>(ref this._foreignKeyDependents, value, null);
			Interlocked.CompareExchange<ReadOnlyCollection<Tuple<AssociationSet, ReferentialConstraint>>>(ref this._foreignKeyPrincipals, value2, null);
		}

		// Token: 0x04000DF2 RID: 3570
		private ReadOnlyCollection<Tuple<AssociationSet, ReferentialConstraint>> _foreignKeyDependents;

		// Token: 0x04000DF3 RID: 3571
		private ReadOnlyCollection<Tuple<AssociationSet, ReferentialConstraint>> _foreignKeyPrincipals;

		// Token: 0x04000DF4 RID: 3572
		private volatile bool _hasForeignKeyRelationships;

		// Token: 0x04000DF5 RID: 3573
		private volatile bool _hasIndependentRelationships;
	}
}
