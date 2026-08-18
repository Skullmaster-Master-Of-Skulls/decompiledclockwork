using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Utilities;
using System.Linq;
using System.Threading;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x020004DE RID: 1246
	public class EntitySet : EntitySetBase
	{
		// Token: 0x06002E22 RID: 11810 RVA: 0x000DDDB8 File Offset: 0x000DBFB8
		internal EntitySet()
		{
		}

		// Token: 0x06002E23 RID: 11811 RVA: 0x000DDDC0 File Offset: 0x000DBFC0
		internal EntitySet(string name, string schema, string table, string definingQuery, EntityType entityType) : base(name, schema, table, definingQuery, entityType)
		{
		}

		// Token: 0x170006B5 RID: 1717
		// (get) Token: 0x06002E24 RID: 11812 RVA: 0x000DDDCF File Offset: 0x000DBFCF
		public override BuiltInTypeKind BuiltInTypeKind
		{
			get
			{
				return BuiltInTypeKind.EntitySet;
			}
		}

		// Token: 0x170006B6 RID: 1718
		// (get) Token: 0x06002E25 RID: 11813 RVA: 0x000DDDD3 File Offset: 0x000DBFD3
		public new virtual EntityType ElementType
		{
			get
			{
				return (EntityType)base.ElementType;
			}
		}

		// Token: 0x170006B7 RID: 1719
		// (get) Token: 0x06002E26 RID: 11814 RVA: 0x000DDDE0 File Offset: 0x000DBFE0
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

		// Token: 0x170006B8 RID: 1720
		// (get) Token: 0x06002E27 RID: 11815 RVA: 0x000DDDF6 File Offset: 0x000DBFF6
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

		// Token: 0x170006B9 RID: 1721
		// (get) Token: 0x06002E28 RID: 11816 RVA: 0x000DDE0C File Offset: 0x000DC00C
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

		// Token: 0x170006BA RID: 1722
		// (get) Token: 0x06002E29 RID: 11817 RVA: 0x000DDE24 File Offset: 0x000DC024
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

		// Token: 0x06002E2A RID: 11818 RVA: 0x000DDE3C File Offset: 0x000DC03C
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
			ReadOnlyCollection<Tuple<AssociationSet, ReferentialConstraint>> value = new ReadOnlyCollection<Tuple<AssociationSet, ReferentialConstraint>>(list);
			ReadOnlyCollection<Tuple<AssociationSet, ReferentialConstraint>> value2 = new ReadOnlyCollection<Tuple<AssociationSet, ReferentialConstraint>>(list2);
			Interlocked.CompareExchange<ReadOnlyCollection<Tuple<AssociationSet, ReferentialConstraint>>>(ref this._foreignKeyDependents, value, null);
			Interlocked.CompareExchange<ReadOnlyCollection<Tuple<AssociationSet, ReferentialConstraint>>>(ref this._foreignKeyPrincipals, value2, null);
		}

		// Token: 0x06002E2B RID: 11819 RVA: 0x000DDF90 File Offset: 0x000DC190
		public static EntitySet Create(string name, string schema, string table, string definingQuery, EntityType entityType, IEnumerable<MetadataProperty> metadataProperties)
		{
			Check.NotEmpty(name, "name");
			Check.NotNull<EntityType>(entityType, "entityType");
			EntitySet entitySet = new EntitySet(name, schema, table, definingQuery, entityType);
			if (metadataProperties != null)
			{
				entitySet.AddMetadataProperties(metadataProperties.ToList<MetadataProperty>());
			}
			entitySet.SetReadOnly();
			return entitySet;
		}

		// Token: 0x0400119E RID: 4510
		private ReadOnlyCollection<Tuple<AssociationSet, ReferentialConstraint>> _foreignKeyDependents;

		// Token: 0x0400119F RID: 4511
		private ReadOnlyCollection<Tuple<AssociationSet, ReferentialConstraint>> _foreignKeyPrincipals;

		// Token: 0x040011A0 RID: 4512
		private volatile bool _hasForeignKeyRelationships;

		// Token: 0x040011A1 RID: 4513
		private volatile bool _hasIndependentRelationships;
	}
}
