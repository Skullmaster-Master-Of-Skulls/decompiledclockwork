using System;
using System.Collections.Generic;
using System.Data.Common.CommandTrees;
using System.Data.Entity;
using System.Data.Metadata.Edm;
using System.Text;

namespace System.Data.Mapping.ViewGeneration.Structures
{
	// Token: 0x02000299 RID: 665
	internal sealed class RoleBoolean : TrueFalseLiteral
	{
		// Token: 0x0600278A RID: 10122 RVA: 0x00099D7F File Offset: 0x00097F7F
		internal RoleBoolean(EntitySetBase extent)
		{
			this.m_metadataItem = extent;
		}

		// Token: 0x0600278B RID: 10123 RVA: 0x00099D7F File Offset: 0x00097F7F
		internal RoleBoolean(AssociationSetEnd end)
		{
			this.m_metadataItem = end;
		}

		// Token: 0x0600278C RID: 10124 RVA: 0x00006174 File Offset: 0x00004374
		internal override StringBuilder AsEsql(StringBuilder builder, string blockAlias, bool skipIsNotNull)
		{
			return null;
		}

		// Token: 0x0600278D RID: 10125 RVA: 0x00006174 File Offset: 0x00004374
		internal override DbExpression AsCqt(DbExpression row, bool skipIsNotNull)
		{
			return null;
		}

		// Token: 0x0600278E RID: 10126 RVA: 0x00099D90 File Offset: 0x00097F90
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

		// Token: 0x0600278F RID: 10127 RVA: 0x00099DE0 File Offset: 0x00097FE0
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

		// Token: 0x06002790 RID: 10128 RVA: 0x00072E1F File Offset: 0x0007101F
		internal override void GetRequiredSlots(MemberProjectionIndex projectedSlotMap, bool[] requiredSlots)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06002791 RID: 10129 RVA: 0x00099E30 File Offset: 0x00098030
		protected override bool IsEqualTo(BoolLiteral right)
		{
			RoleBoolean roleBoolean = right as RoleBoolean;
			return roleBoolean != null && this.m_metadataItem == roleBoolean.m_metadataItem;
		}

		// Token: 0x06002792 RID: 10130 RVA: 0x00099E57 File Offset: 0x00098057
		public override int GetHashCode()
		{
			return this.m_metadataItem.GetHashCode();
		}

		// Token: 0x06002793 RID: 10131 RVA: 0x00048AC0 File Offset: 0x00046CC0
		internal override BoolLiteral RemapBool(Dictionary<MemberPath, MemberPath> remap)
		{
			return this;
		}

		// Token: 0x06002794 RID: 10132 RVA: 0x00099E64 File Offset: 0x00098064
		internal override void ToCompactString(StringBuilder builder)
		{
			AssociationSetEnd associationSetEnd = this.m_metadataItem as AssociationSetEnd;
			if (associationSetEnd != null)
			{
				string str = "InEnd:";
				AssociationSet parentAssociationSet = associationSetEnd.ParentAssociationSet;
				builder.Append(str + ((parentAssociationSet != null) ? parentAssociationSet.ToString() : null) + "_" + associationSetEnd.Name);
				return;
			}
			builder.Append("InSet:" + this.m_metadataItem.ToString());
		}

		// Token: 0x04001226 RID: 4646
		private readonly MetadataItem m_metadataItem;
	}
}
