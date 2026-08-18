using System;
using System.Collections.Generic;
using System.Data.Common.Utils;
using System.Data.Metadata.Edm;
using System.Linq;

namespace System.Data.Mapping.Update.Internal
{
	// Token: 0x020002BC RID: 700
	internal sealed class AssociationSetMetadata
	{
		// Token: 0x17000826 RID: 2086
		// (get) Token: 0x060029A2 RID: 10658 RVA: 0x000A185C File Offset: 0x0009FA5C
		internal bool HasEnds
		{
			get
			{
				return 0 < this.RequiredEnds.Count || 0 < this.OptionalEnds.Count || 0 < this.IncludedValueEnds.Count;
			}
		}

		// Token: 0x060029A3 RID: 10659 RVA: 0x000A188C File Offset: 0x0009FA8C
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

		// Token: 0x060029A4 RID: 10660 RVA: 0x000A1A60 File Offset: 0x0009FC60
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

		// Token: 0x060029A5 RID: 10661 RVA: 0x000A1A9D File Offset: 0x0009FC9D
		private static void AddEnd(ref Set<AssociationEndMember> set, AssociationEndMember element)
		{
			if (set == null)
			{
				set = new Set<AssociationEndMember>();
			}
			set.Add(element);
		}

		// Token: 0x060029A6 RID: 10662 RVA: 0x000A1AB2 File Offset: 0x0009FCB2
		private static void FixSet(ref Set<AssociationEndMember> set)
		{
			if (set == null)
			{
				set = Set<AssociationEndMember>.Empty;
				return;
			}
			set.MakeReadOnly();
		}

		// Token: 0x04001287 RID: 4743
		internal readonly Set<AssociationEndMember> RequiredEnds;

		// Token: 0x04001288 RID: 4744
		internal readonly Set<AssociationEndMember> OptionalEnds;

		// Token: 0x04001289 RID: 4745
		internal readonly Set<AssociationEndMember> IncludedValueEnds;
	}
}
