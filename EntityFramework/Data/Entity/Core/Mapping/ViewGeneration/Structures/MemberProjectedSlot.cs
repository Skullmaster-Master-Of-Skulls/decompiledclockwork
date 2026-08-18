using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Mapping.ViewGeneration.CqlGeneration;
using System.Data.Entity.Core.Metadata.Edm;
using System.Text;

namespace System.Data.Entity.Core.Mapping.ViewGeneration.Structures
{
	// Token: 0x0200047F RID: 1151
	internal sealed class MemberProjectedSlot : ProjectedSlot
	{
		// Token: 0x06002A7F RID: 10879 RVA: 0x000CD78C File Offset: 0x000CB98C
		internal MemberProjectedSlot(MemberPath node)
		{
			this.m_memberPath = node;
		}

		// Token: 0x170005DB RID: 1499
		// (get) Token: 0x06002A80 RID: 10880 RVA: 0x000CD79B File Offset: 0x000CB99B
		internal MemberPath MemberPath
		{
			get
			{
				return this.m_memberPath;
			}
		}

		// Token: 0x06002A81 RID: 10881 RVA: 0x000CD7A4 File Offset: 0x000CB9A4
		internal override StringBuilder AsEsql(StringBuilder builder, MemberPath outputMember, string blockAlias, int indentLevel)
		{
			TypeUsage typeUsage;
			if (this.NeedToCastCqlValue(outputMember, out typeUsage))
			{
				builder.Append("CAST(");
				this.m_memberPath.AsEsql(builder, blockAlias);
				builder.Append(" AS ");
				CqlWriter.AppendEscapedTypeName(builder, typeUsage.EdmType);
				builder.Append(')');
			}
			else
			{
				this.m_memberPath.AsEsql(builder, blockAlias);
			}
			return builder;
		}

		// Token: 0x06002A82 RID: 10882 RVA: 0x000CD808 File Offset: 0x000CBA08
		internal override DbExpression AsCqt(DbExpression row, MemberPath outputMember)
		{
			DbExpression dbExpression = this.m_memberPath.AsCqt(row);
			TypeUsage toType;
			if (this.NeedToCastCqlValue(outputMember, out toType))
			{
				dbExpression = dbExpression.CastTo(toType);
			}
			return dbExpression;
		}

		// Token: 0x06002A83 RID: 10883 RVA: 0x000CD838 File Offset: 0x000CBA38
		private bool NeedToCastCqlValue(MemberPath outputMember, out TypeUsage outputMemberTypeUsage)
		{
			TypeUsage modelTypeUsage = Helper.GetModelTypeUsage(this.m_memberPath.LeafEdmMember);
			outputMemberTypeUsage = Helper.GetModelTypeUsage(outputMember.LeafEdmMember);
			return !modelTypeUsage.EdmType.Equals(outputMemberTypeUsage.EdmType);
		}

		// Token: 0x06002A84 RID: 10884 RVA: 0x000CD878 File Offset: 0x000CBA78
		internal override void ToCompactString(StringBuilder builder)
		{
			this.m_memberPath.ToCompactString(builder);
		}

		// Token: 0x06002A85 RID: 10885 RVA: 0x000CD886 File Offset: 0x000CBA86
		internal string ToUserString()
		{
			return this.m_memberPath.PathToString(new bool?(false));
		}

		// Token: 0x06002A86 RID: 10886 RVA: 0x000CD89C File Offset: 0x000CBA9C
		protected override bool IsEqualTo(ProjectedSlot right)
		{
			MemberProjectedSlot memberProjectedSlot = right as MemberProjectedSlot;
			return memberProjectedSlot != null && MemberPath.EqualityComparer.Equals(this.m_memberPath, memberProjectedSlot.m_memberPath);
		}

		// Token: 0x06002A87 RID: 10887 RVA: 0x000CD8CB File Offset: 0x000CBACB
		protected override int GetHash()
		{
			return MemberPath.EqualityComparer.GetHashCode(this.m_memberPath);
		}

		// Token: 0x06002A88 RID: 10888 RVA: 0x000CD8E0 File Offset: 0x000CBAE0
		internal MemberProjectedSlot RemapSlot(Dictionary<MemberPath, MemberPath> remap)
		{
			MemberPath node = null;
			if (remap.TryGetValue(this.MemberPath, out node))
			{
				return new MemberProjectedSlot(node);
			}
			return new MemberProjectedSlot(this.MemberPath);
		}

		// Token: 0x06002A89 RID: 10889 RVA: 0x000CD914 File Offset: 0x000CBB14
		internal static List<MemberProjectedSlot> GetKeySlots(IEnumerable<MemberProjectedSlot> slots, MemberPath prefix)
		{
			EntitySet entitySet = prefix.EntitySet;
			List<ExtentKey> keysForEntityType = ExtentKey.GetKeysForEntityType(prefix, entitySet.ElementType);
			return MemberProjectedSlot.GetSlots(slots, keysForEntityType[0].KeyFields);
		}

		// Token: 0x06002A8A RID: 10890 RVA: 0x000CD94C File Offset: 0x000CBB4C
		internal static List<MemberProjectedSlot> GetSlots(IEnumerable<MemberProjectedSlot> slots, IEnumerable<MemberPath> members)
		{
			List<MemberProjectedSlot> list = new List<MemberProjectedSlot>();
			foreach (MemberPath member in members)
			{
				MemberProjectedSlot slotForMember = MemberProjectedSlot.GetSlotForMember(Helpers.AsSuperTypeList<MemberProjectedSlot, ProjectedSlot>(slots), member);
				if (slotForMember == null)
				{
					return null;
				}
				list.Add(slotForMember);
			}
			return list;
		}

		// Token: 0x06002A8B RID: 10891 RVA: 0x000CD9B8 File Offset: 0x000CBBB8
		internal static MemberProjectedSlot GetSlotForMember(IEnumerable<ProjectedSlot> slots, MemberPath member)
		{
			foreach (ProjectedSlot projectedSlot in slots)
			{
				MemberProjectedSlot memberProjectedSlot = (MemberProjectedSlot)projectedSlot;
				if (MemberPath.EqualityComparer.Equals(memberProjectedSlot.MemberPath, member))
				{
					return memberProjectedSlot;
				}
			}
			return null;
		}

		// Token: 0x04000FAE RID: 4014
		private readonly MemberPath m_memberPath;
	}
}
