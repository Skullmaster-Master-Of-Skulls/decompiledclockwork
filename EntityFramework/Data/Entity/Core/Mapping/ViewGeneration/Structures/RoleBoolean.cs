using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Text;

namespace System.Data.Entity.Core.Mapping.ViewGeneration.Structures
{
	// Token: 0x02000450 RID: 1104
	internal sealed class RoleBoolean : TrueFalseLiteral
	{
		// Token: 0x06002893 RID: 10387 RVA: 0x000C5656 File Offset: 0x000C3856
		internal RoleBoolean(EntitySetBase extent)
		{
			this.m_metadataItem = extent;
		}

		// Token: 0x06002894 RID: 10388 RVA: 0x000C5665 File Offset: 0x000C3865
		internal RoleBoolean(AssociationSetEnd end)
		{
			this.m_metadataItem = end;
		}

		// Token: 0x06002895 RID: 10389 RVA: 0x000C5674 File Offset: 0x000C3874
		internal override StringBuilder AsEsql(StringBuilder builder, string blockAlias, bool skipIsNotNull)
		{
			return null;
		}

		// Token: 0x06002896 RID: 10390 RVA: 0x000C5677 File Offset: 0x000C3877
		internal override DbExpression AsCqt(DbExpression row, bool skipIsNotNull)
		{
			return null;
		}

		// Token: 0x06002897 RID: 10391 RVA: 0x000C567C File Offset: 0x000C387C
		internal override StringBuilder AsUserString(StringBuilder builder, string blockAlias, bool skipIsNotNull)
		{
			AssociationSetEnd associationSetEnd = this.m_metadataItem as AssociationSetEnd;
			if (associationSetEnd != null)
			{
				builder.Append(Strings.ViewGen_AssociationSet_AsUserString(blockAlias, associationSetEnd.Name, associationSetEnd.ParentAssociationSet));
			}
			else
			{
				builder.Append(Strings.ViewGen_EntitySet_AsUserString(blockAlias, this.m_metadataItem.ToString()));
			}
			return builder;
		}

		// Token: 0x06002898 RID: 10392 RVA: 0x000C56CC File Offset: 0x000C38CC
		internal override StringBuilder AsNegatedUserString(StringBuilder builder, string blockAlias, bool skipIsNotNull)
		{
			AssociationSetEnd associationSetEnd = this.m_metadataItem as AssociationSetEnd;
			if (associationSetEnd != null)
			{
				builder.Append(Strings.ViewGen_AssociationSet_AsUserString_Negated(blockAlias, associationSetEnd.Name, associationSetEnd.ParentAssociationSet));
			}
			else
			{
				builder.Append(Strings.ViewGen_EntitySet_AsUserString_Negated(blockAlias, this.m_metadataItem.ToString()));
			}
			return builder;
		}

		// Token: 0x06002899 RID: 10393 RVA: 0x000C571C File Offset: 0x000C391C
		internal override void GetRequiredSlots(MemberProjectionIndex projectedSlotMap, bool[] requiredSlots)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600289A RID: 10394 RVA: 0x000C5724 File Offset: 0x000C3924
		protected override bool IsEqualTo(BoolLiteral right)
		{
			RoleBoolean roleBoolean = right as RoleBoolean;
			return roleBoolean != null && this.m_metadataItem == roleBoolean.m_metadataItem;
		}

		// Token: 0x0600289B RID: 10395 RVA: 0x000C574B File Offset: 0x000C394B
		public override int GetHashCode()
		{
			return this.m_metadataItem.GetHashCode();
		}

		// Token: 0x0600289C RID: 10396 RVA: 0x000C5758 File Offset: 0x000C3958
		internal override BoolLiteral RemapBool(Dictionary<MemberPath, MemberPath> remap)
		{
			return this;
		}

		// Token: 0x0600289D RID: 10397 RVA: 0x000C575C File Offset: 0x000C395C
		internal override void ToCompactString(StringBuilder builder)
		{
			AssociationSetEnd associationSetEnd = this.m_metadataItem as AssociationSetEnd;
			if (associationSetEnd != null)
			{
				builder.Append(string.Concat(new object[]
				{
					"InEnd:",
					associationSetEnd.ParentAssociationSet,
					"_",
					associationSetEnd.Name
				}));
				return;
			}
			builder.Append("InSet:" + this.m_metadataItem);
		}

		// Token: 0x04000F36 RID: 3894
		private readonly MetadataItem m_metadataItem;
	}
}
