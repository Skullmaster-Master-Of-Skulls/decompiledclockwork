using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.Entity.Core.Mapping.ViewGeneration.CqlGeneration;
using System.Text;

namespace System.Data.Entity.Core.Mapping.ViewGeneration.Structures
{
	// Token: 0x02000466 RID: 1126
	internal class CellIdBoolean : TrueFalseLiteral
	{
		// Token: 0x06002952 RID: 10578 RVA: 0x000C821F File Offset: 0x000C641F
		internal CellIdBoolean(CqlIdentifiers identifiers, int index)
		{
			this.m_index = index;
			this.m_slotName = identifiers.GetFromVariable(index);
		}

		// Token: 0x170005A2 RID: 1442
		// (get) Token: 0x06002953 RID: 10579 RVA: 0x000C823B File Offset: 0x000C643B
		internal string SlotName
		{
			get
			{
				return this.m_slotName;
			}
		}

		// Token: 0x06002954 RID: 10580 RVA: 0x000C8244 File Offset: 0x000C6444
		internal override StringBuilder AsEsql(StringBuilder builder, string blockAlias, bool skipIsNotNull)
		{
			string qualifiedName = CqlWriter.GetQualifiedName(blockAlias, this.SlotName);
			builder.Append(qualifiedName);
			return builder;
		}

		// Token: 0x06002955 RID: 10581 RVA: 0x000C8267 File Offset: 0x000C6467
		internal override DbExpression AsCqt(DbExpression row, bool skipIsNotNull)
		{
			return row.Property(this.SlotName);
		}

		// Token: 0x06002956 RID: 10582 RVA: 0x000C8275 File Offset: 0x000C6475
		internal override StringBuilder AsUserString(StringBuilder builder, string blockAlias, bool skipIsNotNull)
		{
			return this.AsEsql(builder, blockAlias, skipIsNotNull);
		}

		// Token: 0x06002957 RID: 10583 RVA: 0x000C8280 File Offset: 0x000C6480
		internal override StringBuilder AsNegatedUserString(StringBuilder builder, string blockAlias, bool skipIsNotNull)
		{
			builder.Append("NOT(");
			builder = this.AsUserString(builder, blockAlias, skipIsNotNull);
			builder.Append(")");
			return builder;
		}

		// Token: 0x06002958 RID: 10584 RVA: 0x000C82A8 File Offset: 0x000C64A8
		internal override void GetRequiredSlots(MemberProjectionIndex projectedSlotMap, bool[] requiredSlots)
		{
			int numBoolSlots = requiredSlots.Length - projectedSlotMap.Count;
			int num = projectedSlotMap.BoolIndexToSlot(this.m_index, numBoolSlots);
			requiredSlots[num] = true;
		}

		// Token: 0x06002959 RID: 10585 RVA: 0x000C82D4 File Offset: 0x000C64D4
		protected override bool IsEqualTo(BoolLiteral right)
		{
			CellIdBoolean cellIdBoolean = right as CellIdBoolean;
			return cellIdBoolean != null && this.m_index == cellIdBoolean.m_index;
		}

		// Token: 0x0600295A RID: 10586 RVA: 0x000C82FC File Offset: 0x000C64FC
		public override int GetHashCode()
		{
			return this.m_index.GetHashCode();
		}

		// Token: 0x0600295B RID: 10587 RVA: 0x000C8317 File Offset: 0x000C6517
		internal override BoolLiteral RemapBool(Dictionary<MemberPath, MemberPath> remap)
		{
			return this;
		}

		// Token: 0x0600295C RID: 10588 RVA: 0x000C831A File Offset: 0x000C651A
		internal override void ToCompactString(StringBuilder builder)
		{
			builder.Append(this.SlotName);
		}

		// Token: 0x04000F63 RID: 3939
		private readonly int m_index;

		// Token: 0x04000F64 RID: 3940
		private readonly string m_slotName;
	}
}
