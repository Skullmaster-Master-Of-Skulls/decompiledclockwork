using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;

namespace System.Data.Entity.Core.Mapping.Update.Internal
{
	// Token: 0x020003E9 RID: 1001
	internal sealed class AssociationSetMetadata
	{
		// Token: 0x1700050E RID: 1294
		// (get) Token: 0x06002502 RID: 9474 RVA: 0x000AEA87 File Offset: 0x000ACC87
		internal bool HasEnds
		{
			get
			{
				return 0 < this.RequiredEnds.Count || 0 < this.OptionalEnds.Count || 0 < this.IncludedValueEnds.Count;
			}
		}

		// Token: 0x06002503 RID: 9475 RVA: 0x000AEAB8 File Offset: 0x000ACCB8
		internal AssociationSetMetadata(Set<EntitySet> affectedTables, AssociationSet associationSet, MetadataWorkspace workspace)
		{
			bool flag = 1 < affectedTables.Count;
			ReadOnlyMetadataCollection<AssociationSetEnd> associationSetEnds = associationSet.AssociationSetEnds;
			foreach (EntitySet table in affectedTables)
			{
				IEnumerable<EntitySet> influencingEntitySetsForTable = MetadataHelper.GetInfluencingEntitySetsForTable(table, workspace);
				foreach (EntitySet item in influencingEntitySetsForTable)
				{
					foreach (AssociationSetEnd associationSetEnd in associationSetEnds)
					{
						if (associationSetEnd.EntitySet.EdmEquals(item))
						{
							if (flag)
							{
								AssociationSetMetadata.AddEnd(ref this.RequiredEnds, associationSetEnd.CorrespondingAssociationEndMember);
							}
							else if (this.RequiredEnds == null || !this.RequiredEnds.Contains(associationSetEnd.CorrespondingAssociationEndMember))
							{
								AssociationSetMetadata.AddEnd(ref this.OptionalEnds, associationSetEnd.CorrespondingAssociationEndMember);
							}
						}
					}
				}
			}
			AssociationSetMetadata.FixSet(ref this.RequiredEnds);
			AssociationSetMetadata.FixSet(ref this.OptionalEnds);
			foreach (ReferentialConstraint referentialConstraint in associationSet.ElementType.ReferentialConstraints)
			{
				AssociationEndMember element = (AssociationEndMember)referentialConstraint.FromRole;
				if (!this.RequiredEnds.Contains(element) && !this.OptionalEnds.Contains(element))
				{
					AssociationSetMetadata.AddEnd(ref this.IncludedValueEnds, element);
				}
			}
			AssociationSetMetadata.FixSet(ref this.IncludedValueEnds);
		}

		// Token: 0x06002504 RID: 9476 RVA: 0x000AEC8C File Offset: 0x000ACE8C
		internal AssociationSetMetadata(IEnumerable<AssociationEndMember> requiredEnds)
		{
			if (requiredEnds.Any<AssociationEndMember>())
			{
				this.RequiredEnds = new Set<AssociationEndMember>(requiredEnds);
			}
			AssociationSetMetadata.FixSet(ref this.RequiredEnds);
			AssociationSetMetadata.FixSet(ref this.OptionalEnds);
			AssociationSetMetadata.FixSet(ref this.IncludedValueEnds);
		}

		// Token: 0x06002505 RID: 9477 RVA: 0x000AECC9 File Offset: 0x000ACEC9
		private static void AddEnd(ref Set<AssociationEndMember> set, AssociationEndMember element)
		{
			if (set == null)
			{
				set = new Set<AssociationEndMember>();
			}
			set.Add(element);
		}

		// Token: 0x06002506 RID: 9478 RVA: 0x000AECDE File Offset: 0x000ACEDE
		private static void FixSet(ref Set<AssociationEndMember> set)
		{
			if (set == null)
			{
				set = Set<AssociationEndMember>.Empty;
				return;
			}
			set.MakeReadOnly();
		}

		// Token: 0x04000DB9 RID: 3513
		internal readonly Set<AssociationEndMember> RequiredEnds;

		// Token: 0x04000DBA RID: 3514
		internal readonly Set<AssociationEndMember> OptionalEnds;

		// Token: 0x04000DBB RID: 3515
		internal readonly Set<AssociationEndMember> IncludedValueEnds;
	}
}
