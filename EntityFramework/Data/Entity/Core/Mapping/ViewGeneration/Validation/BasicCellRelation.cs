using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Mapping.ViewGeneration.Structures;
using System.Data.Entity.Core.Metadata.Edm;
using System.Text;

namespace System.Data.Entity.Core.Mapping.ViewGeneration.Validation
{
	// Token: 0x0200048F RID: 1167
	internal class BasicCellRelation : CellRelation
	{
		// Token: 0x06002B21 RID: 11041 RVA: 0x000D098C File Offset: 0x000CEB8C
		internal BasicCellRelation(CellQuery cellQuery, ViewCellRelation viewCellRelation, IEnumerable<MemberProjectedSlot> slots) : base(viewCellRelation.CellNumber)
		{
			this.m_cellQuery = cellQuery;
			this.m_slots = new List<MemberProjectedSlot>(slots);
			this.m_viewCellRelation = viewCellRelation;
		}

		// Token: 0x170005F0 RID: 1520
		// (get) Token: 0x06002B22 RID: 11042 RVA: 0x000D09B4 File Offset: 0x000CEBB4
		internal ViewCellRelation ViewCellRelation
		{
			get
			{
				return this.m_viewCellRelation;
			}
		}

		// Token: 0x06002B23 RID: 11043 RVA: 0x000D09BC File Offset: 0x000CEBBC
		internal void PopulateKeyConstraints(SchemaConstraints<BasicKeyConstraint> constraints)
		{
			if (this.m_cellQuery.Extent is EntitySet)
			{
				this.PopulateKeyConstraintsForEntitySet(constraints);
				return;
			}
			this.PopulateKeyConstraintsForRelationshipSet(constraints);
		}

		// Token: 0x06002B24 RID: 11044 RVA: 0x000D09E0 File Offset: 0x000CEBE0
		private void PopulateKeyConstraintsForEntitySet(SchemaConstraints<BasicKeyConstraint> constraints)
		{
			MemberPath prefix = new MemberPath(this.m_cellQuery.Extent);
			EntityType entityType = (EntityType)this.m_cellQuery.Extent.ElementType;
			List<ExtentKey> keysForEntityType = ExtentKey.GetKeysForEntityType(prefix, entityType);
			this.AddKeyConstraints(keysForEntityType, constraints);
		}

		// Token: 0x06002B25 RID: 11045 RVA: 0x000D0A24 File Offset: 0x000CEC24
		private void PopulateKeyConstraintsForRelationshipSet(SchemaConstraints<BasicKeyConstraint> constraints)
		{
			AssociationSet associationSet = this.m_cellQuery.Extent as AssociationSet;
			Set<MemberPath> set = new Set<MemberPath>(MemberPath.EqualityComparer);
			bool flag = false;
			foreach (AssociationSetEnd associationSetEnd in associationSet.AssociationSetEnds)
			{
				AssociationEndMember correspondingAssociationEndMember = associationSetEnd.CorrespondingAssociationEndMember;
				MemberPath prefix = new MemberPath(associationSet, correspondingAssociationEndMember);
				List<ExtentKey> keysForEntityType = ExtentKey.GetKeysForEntityType(prefix, associationSetEnd.EntitySet.ElementType);
				if (MetadataHelper.DoesEndFormKey(associationSet, correspondingAssociationEndMember))
				{
					this.AddKeyConstraints(keysForEntityType, constraints);
					flag = true;
				}
				set.AddRange(keysForEntityType[0].KeyFields);
			}
			if (!flag)
			{
				ExtentKey extentKey = new ExtentKey(set);
				ExtentKey[] keys = new ExtentKey[]
				{
					extentKey
				};
				this.AddKeyConstraints(keys, constraints);
			}
		}

		// Token: 0x06002B26 RID: 11046 RVA: 0x000D0B04 File Offset: 0x000CED04
		private void AddKeyConstraints(IEnumerable<ExtentKey> keys, SchemaConstraints<BasicKeyConstraint> constraints)
		{
			foreach (ExtentKey extentKey in keys)
			{
				List<MemberProjectedSlot> slots = MemberProjectedSlot.GetSlots(this.m_slots, extentKey.KeyFields);
				if (slots != null)
				{
					BasicKeyConstraint constraint = new BasicKeyConstraint(this, slots);
					constraints.Add(constraint);
				}
			}
		}

		// Token: 0x06002B27 RID: 11047 RVA: 0x000D0B6C File Offset: 0x000CED6C
		protected override int GetHash()
		{
			return this.m_cellQuery.GetHashCode();
		}

		// Token: 0x06002B28 RID: 11048 RVA: 0x000D0B7C File Offset: 0x000CED7C
		internal override void ToCompactString(StringBuilder builder)
		{
			builder.Append("BasicRel: ");
			StringUtil.FormatStringBuilder(builder, "{0}", new object[]
			{
				this.m_slots[0]
			});
		}

		// Token: 0x04000FF0 RID: 4080
		private readonly CellQuery m_cellQuery;

		// Token: 0x04000FF1 RID: 4081
		private readonly List<MemberProjectedSlot> m_slots;

		// Token: 0x04000FF2 RID: 4082
		private readonly ViewCellRelation m_viewCellRelation;
	}
}
