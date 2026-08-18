using System;
using System.Collections.Generic;
using System.Data.Common.CommandTrees;
using System.Data.Common.CommandTrees.ExpressionBuilder;
using System.Data.Common.Utils;
using System.Data.Mapping.ViewGeneration.CqlGeneration;
using System.Data.Metadata.Edm;
using System.Text;

namespace System.Data.Mapping.ViewGeneration.Structures
{
	// Token: 0x020002AB RID: 683
	internal sealed class MemberProjectedSlot : ProjectedSlot
	{
		// Token: 0x06002893 RID: 10387 RVA: 0x0009D080 File Offset: 0x0009B280
		internal MemberProjectedSlot(MemberPath node)
		{
			this.m_memberPath = node;
		}

		// Token: 0x170007EF RID: 2031
		// (get) Token: 0x06002894 RID: 10388 RVA: 0x0009D08F File Offset: 0x0009B28F
		internal MemberPath MemberPath
		{
			get
			{
				return this.m_memberPath;
			}
		}

		// Token: 0x06002895 RID: 10389 RVA: 0x0009D098 File Offset: 0x0009B298
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

		// Token: 0x06002896 RID: 10390 RVA: 0x0009D0FC File Offset: 0x0009B2FC
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

		// Token: 0x06002897 RID: 10391 RVA: 0x0009D12C File Offset: 0x0009B32C
		private bool NeedToCastCqlValue(MemberPath outputMember, out TypeUsage outputMemberTypeUsage)
		{
			TypeUsage modelTypeUsage = Helper.GetModelTypeUsage(this.m_memberPath.LeafEdmMember);
			outputMemberTypeUsage = Helper.GetModelTypeUsage(outputMember.LeafEdmMember);
			return !modelTypeUsage.EdmType.Equals(outputMemberTypeUsage.EdmType);
		}

		// Token: 0x06002898 RID: 10392 RVA: 0x0009D16C File Offset: 0x0009B36C
		internal override void ToCompactString(StringBuilder builder)
		{
			this.m_memberPath.ToCompactString(builder);
		}

		// Token: 0x06002899 RID: 10393 RVA: 0x0009D17A File Offset: 0x0009B37A
		internal string ToUserString()
		{
			return this.m_memberPath.PathToString(new bool?(false));
		}

		// Token: 0x0600289A RID: 10394 RVA: 0x0009D190 File Offset: 0x0009B390
		protected override bool IsEqualTo(ProjectedSlot right)
		{
			MemberProjectedSlot memberProjectedSlot = right as MemberProjectedSlot;
			return memberProjectedSlot != null && MemberPath.EqualityComparer.Equals(this.m_memberPath, memberProjectedSlot.m_memberPath);
		}

		// Token: 0x0600289B RID: 10395 RVA: 0x0009D1BF File Offset: 0x0009B3BF
		protected override int GetHash()
		{
			return MemberPath.EqualityComparer.GetHashCode(this.m_memberPath);
		}

		// Token: 0x0600289C RID: 10396 RVA: 0x0009D1D4 File Offset: 0x0009B3D4
		internal MemberProjectedSlot RemapSlot(Dictionary<MemberPath, MemberPath> remap)
		{
			MemberPath node = null;
			if (remap.TryGetValue(this.MemberPath, out node))
			{
				return new MemberProjectedSlot(node);
			}
			return new MemberProjectedSlot(this.MemberPath);
		}

		// Token: 0x0600289D RID: 10397 RVA: 0x0009D208 File Offset: 0x0009B408
		internal static List<MemberProjectedSlot> GetKeySlots(IEnumerable<MemberProjectedSlot> slots, MemberPath prefix)
		{
			EntitySet entitySet = prefix.EntitySet;
			List<ExtentKey> keysForEntityType = ExtentKey.GetKeysForEntityType(prefix, entitySet.ElementType);
			return MemberProjectedSlot.GetSlots(slots, keysForEntityType[0].KeyFields);
		}

		// Token: 0x0600289E RID: 10398 RVA: 0x0009D240 File Offset: 0x0009B440
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

		// Token: 0x0600289F RID: 10399 RVA: 0x0009D2A8 File Offset: 0x0009B4A8
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

		// Token: 0x0400125D RID: 4701
		private readonly MemberPath m_memberPath;
	}
}
