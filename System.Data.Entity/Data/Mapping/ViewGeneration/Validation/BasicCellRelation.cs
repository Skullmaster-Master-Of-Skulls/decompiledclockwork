using System;
using System.Collections.Generic;
using System.Data.Common.Utils;
using System.Data.Mapping.ViewGeneration.Structures;
using System.Data.Metadata.Edm;
using System.Text;

namespace System.Data.Mapping.ViewGeneration.Validation
{
	// Token: 0x0200027C RID: 636
	internal class BasicCellRelation : CellRelation
	{
		// Token: 0x06002673 RID: 9843 RVA: 0x00093032 File Offset: 0x00091232
		internal BasicCellRelation(CellQuery cellQuery, ViewCellRelation viewCellRelation, IEnumerable<MemberProjectedSlot> slots) : base(viewCellRelation.CellNumber)
		{
			this.m_cellQuery = cellQuery;
			this.m_slots = new List<MemberProjectedSlot>(slots);
			this.m_viewCellRelation = viewCellRelation;
		}

		// Token: 0x17000790 RID: 1936
		// (get) Token: 0x06002674 RID: 9844 RVA: 0x0009305A File Offset: 0x0009125A
		internal ViewCellRelation ViewCellRelation
		{
			get
			{
				return this.m_viewCellRelation;
			}
		}

		// Token: 0x06002675 RID: 9845 RVA: 0x00093062 File Offset: 0x00091262
		internal void PopulateKeyConstraints(SchemaConstraints<BasicKeyConstraint> constraints)
		{
			if (this.m_cellQuery.Extent is EntitySet)
			{
				this.PopulateKeyConstraintsForEntitySet(constraints);
				return;
			}
			this.PopulateKeyConstraintsForRelationshipSet(constraints);
		}

		// Token: 0x06002676 RID: 9846 RVA: 0x00093088 File Offset: 0x00091288
		private void PopulateKeyConstraintsForEntitySet(SchemaConstraints<BasicKeyConstraint> constraints)
		{
			MemberPath prefix = new MemberPath(this.m_cellQuery.Extent);
			EntityType entityType = (EntityType)this.m_cellQuery.Extent.ElementType;
			List<ExtentKey> keysForEntityType = ExtentKey.GetKeysForEntityType(prefix, entityType);
			this.AddKeyConstraints(keysForEntityType, constraints);
		}

		// Token: 0x06002677 RID: 9847 RVA: 0x000930CC File Offset: 0x000912CC
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

		// Token: 0x06002678 RID: 9848 RVA: 0x000931A8 File Offset: 0x000913A8
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

		// Token: 0x06002679 RID: 9849 RVA: 0x00093210 File Offset: 0x00091410
		protected override int GetHash()
		{
			return this.m_cellQuery.GetHashCode();
		}

		// Token: 0x0600267A RID: 9850 RVA: 0x0009321D File Offset: 0x0009141D
		internal override void ToCompactString(StringBuilder builder)
		{
			builder.Append("BasicRel: ");
			StringUtil.FormatStringBuilder(builder, "{0}", new object[]
			{
				this.m_slots[0]
			});
		}

		// Token: 0x040011D1 RID: 4561
		private CellQuery m_cellQuery;

		// Token: 0x040011D2 RID: 4562
		private List<MemberProjectedSlot> m_slots;

		// Token: 0x040011D3 RID: 4563
		private ViewCellRelation m_viewCellRelation;
	}
}
